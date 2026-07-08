using UnityEngine;

public class Combat : MonoBehaviour
{
    CharacterAnim animControl;
    Move moveControl;
    bool isCombatState = false;
    private void Start()
    {
        animControl = GetComponent<CharacterAnim>();
        moveControl = GetComponent<Move>();
    }
    private void Update()
    {
        TransitionState();
        CombatInputClick();

    }
    void TransitionState()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCombatState = !isCombatState;
            if (isCombatState)
            {
                moveControl.speed = 2.5f;
            }
            else
            {
                moveControl.speed = 5f;
            }
            animControl.PlayBoolAnim("isCombat", isCombatState);
        }
    }
    void CombatInputClick()
    {
        if (Input.GetMouseButtonDown(1)) 
        {
            animControl.PlayTriggerAnim("Atk1");
        }
    }
}

