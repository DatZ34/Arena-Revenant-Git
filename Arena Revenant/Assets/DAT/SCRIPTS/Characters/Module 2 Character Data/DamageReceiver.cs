using UnityEngine;

[RequireComponent(typeof(CharacterHealth))]
public class DamageReceiver : MonoBehaviour
{
    private CharacterHealth health;

    private void Awake()
    {
        health = GetComponent<CharacterHealth>();
    }

    public void ReceiveDamage(int amount)
    {
        health.TakeDamage(amount);
    }

    public void ReceiveHeal(int amount)
    {
        health.Heal(amount);
    }
}