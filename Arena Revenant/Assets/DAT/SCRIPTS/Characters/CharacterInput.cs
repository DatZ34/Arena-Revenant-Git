using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public Vector3 moveInput;
    public bool isJumpPressed;
    public bool isLeftMousePressed;

    private void Update()
    {
        HandleInput();
    }
    private void HandleInput()
    {
        moveInput = new Vector3(Input.GetAxis("Horizontal"),0f,Input.GetAxis("Vertical")); 

        isJumpPressed = Input.GetButton("Jump");
        isLeftMousePressed = Input.GetMouseButton(0);
    }
}
