using UnityEngine;

[RequireComponent(
    typeof(CharacterAttribute),
    typeof(CharacterAnim),
    typeof(CharacterInput)
)]
public class CharacterCombat : MonoBehaviour
{
    private CharacterAttribute attributes;
    private CharacterAnim animCtl;
    private CharacterInput input;

    [Header("Combo")]
    [SerializeField] private float comboResetTime = 2f;

    private float lastAttackTime = -Mathf.Infinity;
    private int comboStep = 0;
    bool isCombatState = false; // Để lại sau này tích hợp vào CharracterStateController

    // Debug
    [SerializeField] private bool canAttackDebug;
    [SerializeField] private int comboStepDebug;

    public bool CanAttack
    {
        get
        {
            return Time.time >= lastAttackTime + GetAttackCooldown();
        }
    }

    private void Awake()
    {
        attributes = GetComponent<CharacterAttribute>();
        animCtl = GetComponent<CharacterAnim>();
        input = GetComponent<CharacterInput>();
    }

    private void Update()
    {
        canAttackDebug = CanAttack;
        comboStepDebug = comboStep;

        if (input.isAttackDown)
        {
            Attack();
        }
        // chuyển đổi trạng thái idle
        TransitionState();
    }

    public void Attack()
    {
        if (!CanAttack)
        {
            return;
        }

        // If the player waited too long, restart the combo.
        if (Time.time > lastAttackTime + comboResetTime)
        {
            comboStep = 0;
        }

        // Ask CharacterAnim to play the current combo attack.
        animCtl.PlayUnarmedAttack(comboStep);

        lastAttackTime = Time.time;

        // 0 -> 1 -> 2 -> 3 -> 0
        comboStep = (comboStep + 1) % 4;
    }

    private float GetAttackCooldown()
    {
        return 1f / Mathf.Max(0.5f, attributes.AttackSpeed);
    }
    void TransitionState()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCombatState = !isCombatState;
            animCtl.PlayBoolAnim("isCombat", isCombatState);
        }
    }
}