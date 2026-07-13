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
    void Init() // Hàm khởi động các thành phần cần thiết của CharacterMotor
    {
        animCtl = GetComponent<CharacterAnim>();
        cameraCtl = GetComponent<CharacterCamera>();
        ctl = GetComponent<CharacterController>();
        inputCtl = GetComponent<CharacterInput>();
        
    }
    void HandleMovement(float x, float z)
    {
        
    }
    void HandleRoll(float x, float z)
    {
        
    }
    void RaycastGroundCheck()
    {
        
    }
    
}
