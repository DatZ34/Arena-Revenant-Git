using UnityEngine;
using Unity.Cinemachine;
public class Move : MonoBehaviour
{
    [Header("Scripts Preferences")]
    [SerializeField] private CharacterAnim animControl;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float distance = 2.5f;
    public float radius = 0.18f;
    public bool isGrounded; 

    [Header("Character Controller Preferences")]
    private CharacterController ctl;
    public  float speed = 5f;
    private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 1.5f; 
    private Vector3 velocity; // là Vector3 để lưu trữ vận tốc của nhân vật

    [Header("Camera Preferences")]
    public CinemachineInputAxisController controllerCamera;
    private float currentRotationY = 0f;
    private float newRotaionY = 0f;
    private float rotY
    {
        get
        {
            return controllerCamera.transform.localEulerAngles.y;
        }
    }
    private void Start()
    {
        ctl = GetComponent<CharacterController>();
        if (animControl == null) animControl = GetComponentInChildren<CharacterAnim>();
        controllerCamera.enabled = false;
    }
    private void Update()
    {
        HandleMovement();
        HandleInputAxisCamera();
    }
    void HandleMovement()
    {
        RaycastGroundCheck();

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        animControl.SetAnimBlend(x, z);
        HandleInputRoll();
        Vector3 move = transform.right * x + transform.forward * z;
        ctl.Move(move * speed * Time.deltaTime);
        if(controllerCamera != null && controllerCamera.enabled)
        {
            if(rotY != currentRotationY && (x != 0 || z != 0))
            {
                newRotaionY = rotY;
                currentRotationY = newRotaionY;
                transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);
            }else
            {
                currentRotationY = transform.rotation.eulerAngles.y;

            }
        }
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            animControl.PlayBoolAnim("isJump", true);
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            
        }
        velocity.y += gravity * Time.deltaTime;
        ctl.Move(velocity * Time.deltaTime);
    }
    void HandleInputRoll()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            animControl.PlayTriggerAnim("TG_Roll");
        }
    }
    void HandleInputAxisCamera()
    {
        if (Input.GetMouseButton(0))
        {
            controllerCamera.enabled = true;
        }
        else
        {
            controllerCamera.enabled = false;
        }
    }

    void RaycastGroundCheck()
    {
        RaycastHit[] hits = new RaycastHit[5]; // mảng chứa kết quả
        int hitCount = Physics.SphereCastNonAlloc(
            groundCheck.position, 
            radius, 
            Vector3.down, 
            hits, 
            distance
        );

        if (hitCount > 0)
        {
            // lấy hit gần nhất
            RaycastHit hit = hits[0];
            
            float distanceToGround = hit.distance;

            if (distanceToGround < 0.1f)
            {
                isGrounded = true;
                if (velocity.y < 0)
                {
                    animControl.PlayBoolAnim("isJump", false);
                    velocity.y = 0f;
                }
            }else if(distanceToGround <= 1.5f)
            {
                animControl.PlayBoolAnim("isFalling", false);
            }
            else if (distanceToGround >= 2f)
            {
                isGrounded = false;
                animControl.PlayBoolAnim("isFalling", true);
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
                animControl.PlayBoolAnim("isFalling", true);

            isGrounded = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        // Vẽ hình cầu tại vị trí nhân vật
        Gizmos.DrawWireSphere(groundCheck.transform.position, radius);

        // Vẽ hình cầu tại điểm cuối cast
        Gizmos.DrawWireSphere(groundCheck.transform.position + Vector3.down * distance, radius);

        // Vẽ đường nối giữa hai hình cầu
        Gizmos.DrawLine(groundCheck.transform.position, groundCheck.transform.position + Vector3.down * distance);
    }
}
