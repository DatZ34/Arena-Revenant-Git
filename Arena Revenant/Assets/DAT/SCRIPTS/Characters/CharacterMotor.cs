using UnityEngine;

[RequireComponent(typeof(CharacterController))] 
[RequireComponent(typeof(CharacterAnim))] 
[RequireComponent(typeof(CharacterCamera))] 
[RequireComponent(typeof(CharacterInput))] 
public class CharacterMotor : MonoBehaviour
{
    // Chỉ lo :
        // Move
        // Jump
        // Gravity
        // Roll

    // === Tiến độ === 
        // Tạm thời hoàn chỉnh

    [Header("Scripts Preferences")]
    [SerializeField] private CharacterAnim animCtl;
    [SerializeField] private CharacterCamera cameraCtl;
    [SerializeField] private CharacterInput inputCtl;

    [Header("Character Controller Preferences")]
    private CharacterController ctl;
    private float speed = 5f;
    private float gravity = -9.81f;
    private float jumpHeight = 1.5f;
    private Vector3 velocity; // là Vector3 để lưu trữ vận tốc nhân vật

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
        HandleJump(inputCtl.isJumpPressed);
    }
    void Init() // Hàm khởi động các thành phần cần thiết của CharacterMotor
    {
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
    }
    void HandleJump(bool isJump)
    {
        if(isJump && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    void RaycastGroundCheck()
    {
        // Hàm này phụ trách bắn tia theo hướng Vector3.Down để check xem có chạm đất hay không ?
        // ở đây hits là nơi lưu về các colider mà Raycast va chạm theo thứ tự.
        RaycastHit[] hits = new RaycastHit[4];
        int hitCount = Physics.SphereCastNonAlloc(
            groundCheck.position,   // vị trí bắn tia
            radius,                 // độ rộng khu vực
            Vector3.down,           // hướng bắn của tia
            hits,                   // nơi lưu
            distance                // độ dài của tia
        );
        if(hitCount > 0)
        {
            // lấy hit gần nhất
            RaycastHit hit = hits[0];
            
            float distanceToGround = hit.distance;
            if(distanceToGround < 0.1f) 
            {
                isGrounded = true;
            }else if(distanceToGround >= 2.5f)
            {
                isGrounded = false;
            }
            else
            {
                isGrounded = false;
            }
        }
        else
        {
            isGrounded = false;
        }
    }
    void OnDrawGizmos() // dùng để vẽ raycast trực quan trong editor xem
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
