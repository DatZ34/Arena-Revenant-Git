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
public class CharacterStateController : MonoBehaviour
{
    private CharacterAnim animCtl;
    public CharacterState currentState = CharacterState.Idle;

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        animCtl = GetComponent<CharacterAnim>();
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
                animCtl.PlayBoolAnim("isJump", false);

                break;
            case CharacterState.Running:
                animCtl.PlayBoolAnim("isJump", false);

                break;
            case CharacterState.Jumping:
                animCtl.PlayBoolAnim("isJump", true);

                break;
            case CharacterState.Falling:
                animCtl.PlayBoolAnim("isFalling", true);
                
                break;
            case CharacterState.Landing:
                animCtl.PlayBoolAnim("isJump", false);
                animCtl.PlayBoolAnim("isFalling", false);
                
                break;
            case CharacterState.Rolling:
                animCtl.PlayTriggerAnim("TG_Roll");
                break;
            case CharacterState.Attacking:

                break;
            case CharacterState.Dead:

                break;
        }
    }

}
