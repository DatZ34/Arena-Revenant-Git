using UnityEngine;
[RequireComponent(typeof(CharacterAttribute))] 

public class CharacterHealth : MonoBehaviour
{
    public CharacterAttribute attributes; // tham chiếu để lấy MaxHP
    // public int CurrentHP { get; private set; }
    public int CurrentHP;
    private void Awake()
    {
        attributes = GetComponent<CharacterAttribute>();

        // gọi để cập nhập chỉ số ngay khi khởi chạy game
        attributes.RecalculateAttributes();
        
        InitHealth();
    }

    void InitHealth()
    {
        CurrentHP = attributes.MaxHP;
    }

    public void Heal(int amount)
    {
        CurrentHP += amount;
        if (CurrentHP > attributes.MaxHP)
            CurrentHP = attributes.MaxHP;
    }

    public void TakeDamage(int amount)
    {
        CurrentHP -= amount;
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
            Die();
        }
    }

    void Die()
    {
        // xử lý logic chết: animation, disable input, notify game manager...
        Debug.Log($"{gameObject.name} has died.");
    }
}
