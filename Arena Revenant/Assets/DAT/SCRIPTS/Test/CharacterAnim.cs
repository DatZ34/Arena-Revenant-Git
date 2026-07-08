using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    public Animator anim;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
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
