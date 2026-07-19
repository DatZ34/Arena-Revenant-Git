using UnityEngine;

[RequireComponent(typeof(CharacterInput))]
public class CharacterAnim : MonoBehaviour
{
    public Animator anim;
    private void Awake()
    {
        if (anim == null)
        {
            var parenAnim = GetComponent<Animator>();
            if (parenAnim != null)
            {
                anim = parenAnim;
            }else
            {
                anim = GetComponentInChildren<Animator>();
            }
        }
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
