using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void SetAnimBlend(float x, float z)
    {
        anim.SetFloat("x", x);
        anim.SetFloat("z", z);
    }

}
