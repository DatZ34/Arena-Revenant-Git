using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnim : MonoBehaviour
{
    private Animator anim;

    private static readonly int[] UnarmedAttacks =
    {
        Animator.StringToHash("Base Layer.UnarmedCombat.Attack_01"),
        Animator.StringToHash("Base Layer.UnarmedCombat.Attack_02"),
        Animator.StringToHash("Base Layer.UnarmedCombat.Attack_03"),
        Animator.StringToHash("Base Layer.UnarmedCombat.Attack_04")
    };

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayUnarmedAttack(int attackIndex)
    {
        if (attackIndex < 0 || attackIndex >= UnarmedAttacks.Length)
        {
            return;
        }

        anim.CrossFade(UnarmedAttacks[attackIndex], 0.05f, 0);
    }

    public void SetAnimBlend(float x, float z)
    {
        anim.SetFloat("x", x);
        anim.SetFloat("z", z);
    }

    public void PlayBoolAnim(string name, bool value)
    {
        anim.SetBool(name, value);
    }

    public void PlayTriggerAnim(string name)
    {
        anim.SetTrigger(name);
    }
}