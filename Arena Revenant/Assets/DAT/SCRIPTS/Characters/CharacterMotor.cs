using UnityEngine;

[RequireComponent(typeof(CharacterController))] 
[RequireComponent(typeof(CharacterAnim))] 
[RequireComponent(typeof(CharacterCamera))] 
[RequireComponent(typeof(CharacterInput))] 
[RequireComponent(typeof(CharacterStateController))]
public class CharacterMotor : MonoBehaviour
{
    [Header("Runtime Values")] // dùng để quan sát ở editor - ko quan trọng lắm
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isRolling;

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
    private float distance = 2.5f;
    private float radius = 0.18f;
    [SerializeField] private bool isGrounded;

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        RaycastGroundCheck();
        HandleMovement(inputCtl.moveInput.x, inputCtl.moveInput.z);
        HandleRoll(inputCtl.isRollPressed);
        HandleJump(inputCtl.isJumpPressed);

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

        if (isJumping  && !isGrounded)
        {
            stateCtl.ChangeState(CharacterState.Jumping);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
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
            speed = baseSpeed;
            stateCtl.ChangeState(CharacterState.Idle);
        }
    }

    void HandleJump(bool isJump)
    {
        if(isJump && isGrounded)
        {
            // stateCtl.ChangeState(CharacterState.Jumping);
            isJumping = true;
            isGrounded = false;

            // velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if(isGrounded)
        {
            isJumping = false;
        }
    }

    void HandleRoll(bool isRoll)
    {
        isRolling = isRoll;
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
            RaycastHit hit = hits[0];
            float distanceToGround = hit.distance;

            if(distanceToGround < 0.1f) 
            {
                if(velocity.y < 0)
                {
                    isGrounded = true;
                    velocity.y = 0f;
                }
            }else if(distanceToGround <= 1.5f) // độ cao vừa vặn để chuyển anim đáp đất
            {
                stateCtl.ChangeState(CharacterState.Landing);
            }
            else if(distanceToGround >= 2.5f) // Chân không chạm đât và đang ở độ cao trên 2.5m
            {   
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
            stateCtl.ChangeState(CharacterState.Falling);
            isGrounded = false;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.transform.position, radius);
        Gizmos.DrawWireSphere(groundCheck.transform.position + Vector3.down * distance, radius);
        Gizmos.DrawLine(groundCheck.transform.position, groundCheck.transform.position + Vector3.down * distance);
    }
}
