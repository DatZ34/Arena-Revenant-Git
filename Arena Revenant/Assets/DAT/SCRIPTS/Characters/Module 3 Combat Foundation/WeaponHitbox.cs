using UnityEngine;

public class WeaponHitbox : MonoBehaviour
{
    public WeaponData weaponData; // dữ liệu vũ khí ScriptableObject
    public CharacterAttribute attributes; // dữ liệu dame của nhân vật
    public GameObject owner;      // nhân vật cầm vũ khí
    private Collider hitboxCollider;

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
            int baseDamage = weaponData != null ? weaponData.damage : 0;
            int charDamage = attributes != null ? attributes.Damage : 0;

            int totalDamage = baseDamage + charDamage;

            receiver.ReceiveDamage(totalDamage);
            Debug.Log($"{owner.name} hit {other.name} for {totalDamage} damage!");
        }
    }

}
