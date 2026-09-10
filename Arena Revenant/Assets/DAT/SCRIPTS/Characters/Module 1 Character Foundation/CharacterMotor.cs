using UnityEngine;
using System.Collections;
using System.Collections.Generic;
[RequireComponent(typeof(CharacterController))] 
[RequireComponent(typeof(CharacterAnim))] 
[RequireComponent(typeof(CharacterCamera))] 
[RequireComponent(typeof(CharacterInput))] 
[RequireComponent(typeof(CharacterStateController))]
public class CharacterMotor : MonoBehaviour
{
    [Header("Runtime Values")] // dùng để quan sát ở editor 
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isFalling;
    [SerializeField] private bool isLanding;
    [SerializeField] private bool canMove = true;
    [SerializeField] private bool isRolling;
    [SerializeField] private bool isStunTime; // đây không phải giá trị quản lý của State


    [Header("Scripts Preferences")]
    [SerializeField] private CharacterStateController stateCtl;
    [SerializeField] private CharacterAnim animCtl;
    [SerializeField] private CharacterCamera cameraCtl;
    [SerializeField] private CharacterInput inputCtl;

    [Header("Character Controller Preferences")]
    private CharacterController ctl;
    private float speed = 5f;
    private float baseSpeed = 5f;
    private float gravity = -9.81f;
    private float jumpHeight = 1.5f;
    private Vector3 velocity;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float distance = 2.5f;
    private float radius = 0.18f;
    [SerializeField] private bool isGrounded;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        RaycastGroundCheck();
        if (canMove)
        {
            HandleMovement(inputCtl.moveInput.x, inputCtl.moveInput.z);
            animCtl.SetAnimBlend(inputCtl.moveInput.x,inputCtl.moveInput.z);
            HandleJump(inputCtl.isJumpDown);
            
        }
        HandleRoll(inputCtl.isRollPressed);

    }

    void Init()
    {
        stateCtl = GetComponent<CharacterStateController>();
        animCtl = GetComponent<CharacterAnim>();
        cameraCtl = GetComponent<CharacterCamera>();
        ctl = GetComponent<CharacterController>();
        inputCtl = GetComponent<CharacterInput>();
        if(groundCheck == null)
        {
            groundCheck = transform.Find("GroundCheck");
        }
    }

    void HandleMovement(float x, float z)
    {
        Vector3 move = transform.right * x + transform.forward * z;
        ctl.Move(move * speed * Time.deltaTime);

        
        velocity.y += gravity * Time.deltaTime;
        ctl.Move(velocity * Time.deltaTime);

            // === Gọi ChangeState ở đây ===
        if (move.magnitude > 0.1f && isGrounded)
        {
            // Nếu giữ Shift thì chạy, ngược lại đi bộ
            if (inputCtl.isLeftShiftPressed)
            {
                speed = baseSpeed * 2;
                stateCtl.ChangeState(CharacterState.Running);
            }else
            {
                speed = baseSpeed;
                stateCtl.ChangeState(CharacterState.Walking);
            }
        }
        else
        {
            if (isGrounded)
            {
                speed = baseSpeed;
                stateCtl.ChangeState(CharacterState.Idle);
            }
        }
    }

    void HandleJump(bool isJump)
    {
        if(isJump && isGrounded)
        {
            isJumping = true;
            isGrounded = false;

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); 
            // chỉ nên áp dụng lực nhảy 1 lần không nên để nó chạy liên tục nhiều frame
        }
        else 
        {
            if (isGrounded)
            {
                isJumping = false;
            }
        }
        if (isJumping && !isFalling)
        {
            stateCtl.ChangeState(CharacterState.Jumping);
        }
    }
    void HandleRoll(bool isRoll)
    {
        if(isRoll && isGrounded)
        {
            isRolling = true;
            ApplyStun(1.75f, false, true, CharacterState.Rolling);
        }
    }

    void RaycastGroundCheck()
    {
        RaycastHit[] hits = new RaycastHit[4];
        int hitCount = Physics.SphereCastNonAlloc(
            groundCheck.position,
            radius,
            Vector3.down,
            hits,
            distance
        );

        if(hitCount > 0)
        {
            float minDistance = float.MaxValue;
            RaycastHit closestHit = hits[0];
            for(int i = 0; i < hitCount; i++)
            {
                if(hits[i].distance < minDistance)
                {
                    minDistance = hits[i].distance;
                    closestHit = hits[i];
                }
            }
            float distanceToGround = closestHit.distance;

            if(distanceToGround < 0.1f) 
            {
                if(velocity.y < 0)
                {
                    isGrounded = true;
                    velocity.y = 0f;
                }
            }else if(distanceToGround <= 2.5f) // độ cao vừa vặn để chuyển anim đáp đất
            {
                if (isFalling)
                {
                    animCtl.SetAnimBlend(inputCtl.lastMoveInput.x,inputCtl.lastMoveInput.z);
                    ApplyStun(1f, true, false, CharacterState.Landing); // stun 1 giây
                    isFalling = false;
                }
            }
            else if(distanceToGround >= 2.5f) // Chân không chạm đât và đang ở độ cao trên 2.5m
            {   
                isFalling = true;
                stateCtl.ChangeState(CharacterState.Falling);
                isGrounded = false;
            }
            else // hmm để dư 
            {
                isGrounded = false;
            }
        }
        else // không hit đồng nghĩa với (distanceToGround >= 2.5f)
        {
            isFalling = true;
            isJumping = false;
            isGrounded = false;
            stateCtl.ChangeState(CharacterState.Falling);

        }
    }

    public void ApplyStun(float duration, bool isLand, bool isRoll, CharacterState characterState)
    {
        if (!isStunTime) // tránh gọi nhiều lần
            StartCoroutine(StunRoutine(duration, isLand , isRoll, characterState));
    }

    IEnumerator StunRoutine(float duration, bool isLand, bool isRoll, CharacterState characterState)
    {
        isStunTime = true;
        canMove = false;

        isLanding = isLand;
        isRolling = isRoll;
        stateCtl.ChangeState(characterState);

        yield return new WaitForSeconds(duration);

        isStunTime = false;
        canMove = true;

        isRolling = false;
        isLanding = false;
        stateCtl.ChangeState(CharacterState.Idle);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, radius);
        Gizmos.DrawWireSphere(groundCheck.transform.position + Vector3.down * distance, radius);
        Gizmos.DrawLine(groundCheck.transform.position, groundCheck.transform.position + Vector3.down * distance);
    }
}
