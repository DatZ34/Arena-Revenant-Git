using UnityEngine;


public enum CharacterState
{
    Idle,
    Walking,
    Running,
    Jumping,
    Falling,
    Landing,
    Rolling,
    Attacking,
    Dead
}
[RequireComponent(typeof(CharacterAnim))]
[RequireComponent(typeof(CharacterInput))]

public class CharacterStateController : MonoBehaviour
{
    private CharacterAnim animCtl;
    private CharacterInput inputCtl;
    public CharacterState currentState = CharacterState.Idle;

    private void Start()
    {
        Init();
    }
    private void Update()
    {
        animCtl.SetAnimBlend(inputCtl.moveInput.x,inputCtl.moveInput.z);
    }
    private void Init()
    {
        animCtl = GetComponent<CharacterAnim>();
        inputCtl = GetComponent<CharacterInput>();
    }
    
    public void ChangeState(CharacterState newState)
    {
        if(currentState == newState) return;
        currentState = newState;

        switch (newState)
        {
            case CharacterState.Idle:
                animCtl.PlayBoolAnim("isJump", false);

                break;
            case CharacterState.Walking:

                break;
            case CharacterState.Running:

                break;
            case CharacterState.Jumping:
                animCtl.PlayBoolAnim("isJump", true);

                break;
            case CharacterState.Falling:
                animCtl.PlayBoolAnim("isFalling", true);
                
                break;
            case CharacterState.Landing:
                animCtl.PlayBoolAnim("isFalling", false);

                break;
            case CharacterState.Rolling:

                break;
            case CharacterState.Attacking:

                break;
            case CharacterState.Dead:

                break;
        }
    }
}
