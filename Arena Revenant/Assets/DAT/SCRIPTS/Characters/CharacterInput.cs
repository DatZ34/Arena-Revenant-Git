using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    [Header("CharacterInput Preferences")]
    public Vector3 moveInput { get; private set; }
    public bool isJumpPressed { get; private set; }
    public bool isLeftMousePressed { get; private set; }
    public bool isRollPressed { get; private set; }
    public bool isLeftShiftPressed { get; private set; }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        // Di chuyển
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        // Nhảy
        isJumpPressed = Input.GetButton("Jump");
        // Click chuột -Trái- 
        isLeftMousePressed = Input.GetMouseButton(0);
        // Roll
        isRollPressed = Input.GetKey(KeyCode.R);
        // Run
        isLeftShiftPressed = Input.GetKey(KeyCode.LeftShift);
    }


}
