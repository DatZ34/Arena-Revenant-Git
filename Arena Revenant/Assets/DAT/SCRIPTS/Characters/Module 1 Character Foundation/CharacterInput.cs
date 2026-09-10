using System;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    [Header("CharacterInput Preferences")]
    public Vector3 moveInput { get; private set; }
    public Vector3 lastMoveInput { get; private set; }
    public bool isJumpDown { get; private set; }
    public bool isLeftMousePressed { get; private set; }
    public bool isRollPressed { get; private set; }
    public bool isLeftShiftPressed { get; private set; }
    public bool isAttackDown { get; private set; } // thêm
    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        // Di chuyển
        moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (moveInput.x != 0 || moveInput.z != 0)
        {
            lastMoveInput = new Vector3Int(
                Mathf.RoundToInt(moveInput.x),
                Mathf.RoundToInt(moveInput.y),
                Mathf.RoundToInt(moveInput.z)
            );
        }
        // Nhảy
        isJumpDown = Input.GetButtonDown("Jump");
        // Click chuột -Trái- 
        isLeftMousePressed = Input.GetMouseButton(0);
        // Roll
        isRollPressed = Input.GetKey(KeyCode.R);
        // Run
        isLeftShiftPressed = Input.GetKey(KeyCode.LeftShift);
        // Attack
        isAttackDown = Input.GetMouseButtonDown(0);
    }


}
