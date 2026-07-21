using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterAnim : MonoBehaviour
{
    // chỉ lo:
        // 
    public Animator anim;
    private void Awake()
    {
        Init();   
    }
    void Init()
    {
        anim = GetComponent<Animator>();
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
