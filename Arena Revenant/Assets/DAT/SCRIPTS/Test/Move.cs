using UnityEngine;
using Unity.Cinemachine;
public class Move : MonoBehaviour
{
    [Header("Scripts Preferences")]
    [SerializeField] private CharacterAnim animControl;
    [Header("Preferences")]
    public CinemachineInputAxisController controllerCamera;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    private CharacterController ctl;
    private  float speed = 5f;
    private float gravity = -9.81f;
    private float jumpHeight = 1.5f; 
    private Vector3 velocity; // là Vector3 để lưu trữ vận tốc của nhân vật
    public bool isGrounded; 
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
        if(groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance);    
            if(isGrounded && velocity.y < 0)
            {
                velocity.y = 0f;
            }        
        }
        else
        {
            Debug.LogWarning("GroundCheck Transform is not assigned.");
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        animControl.SetAnimBlend(x, z);

        Vector3 move = transform.right * x + transform.forward * z;
        ctl.Move(move * speed * Time.deltaTime);
        if(controllerCamera != null)
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
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            
        }
        velocity.y += gravity * Time.deltaTime;
        ctl.Move(velocity * Time.deltaTime);
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
}
