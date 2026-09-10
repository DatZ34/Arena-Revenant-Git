using UnityEngine;

public class UnarmedHitbox : MonoBehaviour
{
    public CharacterAttribute attributes;
    public GameObject owner;
    private Collider hitboxCollider;

    private void Awake()
    {
        hitboxCollider = GetComponent<Collider>();
        hitboxCollider.enabled = false; // mặc định tắt
        attributes = GetComponentInParent<CharacterAttribute>();
    }

    public void EnableHitbox()
    {
        hitboxCollider.enabled = true;
    }

    public void DisableHitbox()
    {
        hitboxCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner) return;

        DamageReceiver receiver = other.GetComponent<DamageReceiver>();
        if (receiver != null)
        {
            int damage = attributes.Damage; // lấy từ Strength/Vitality
            receiver.ReceiveDamage(damage);
            Debug.Log($"{owner.name} unarmed hit {other.name} for {damage} damage!");
        }
    }
}
