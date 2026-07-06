using UnityEngine;

public class Move : MonoBehaviour
{
    public CharacterController ctl;
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;
    public float groundDistance = 0.4f;

    private Vector3 velocity;
    private bool isGrounded;
    public Transform groundCheck;

    void Start()
    {
        ctl = GetComponent<CharacterController>();
    }
    void Update()
    {
        HandleMovement();
    }
    void HandleMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, LayerMask.GetMask("Ground"));
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        ctl.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        ctl.Move(velocity * Time.deltaTime);
    }
}
