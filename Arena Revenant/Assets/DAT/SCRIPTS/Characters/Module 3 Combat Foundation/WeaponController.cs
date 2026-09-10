using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponData CurrentWeapon { get; private set; }
    private WeaponHitbox currentWeaponHitbox;

    [Header("Unarmed Hitboxes")]
    public UnarmedHitbox leftFistHitbox;
    public UnarmedHitbox rightFistHitbox;
    public UnarmedHitbox leftFootHitbox;
    public UnarmedHitbox rightFootHitbox;

    private bool isUnarmed = true;

    public void Equip(WeaponData weaponData)
    {
        if (weaponData == null) return;

        CurrentWeapon = weaponData;
        isUnarmed = false;

        if (weaponData.prefab != null)
        {
            GameObject weaponInstance = Instantiate(weaponData.prefab, transform);
            weaponInstance.name = weaponData.weaponName;

            currentWeaponHitbox = weaponInstance.GetComponent<WeaponHitbox>();
            if (currentWeaponHitbox != null)
            {
                currentWeaponHitbox.weaponData = weaponData;
                currentWeaponHitbox.owner = gameObject;
            }
        }

        Debug.Log($"{gameObject.name} equipped {weaponData.weaponName}");
    }

    public void Unequip()
    {
        if (CurrentWeapon == null) return;

        Debug.Log($"{gameObject.name} unequipped {CurrentWeapon.weaponName}");

        Transform weaponTransform = transform.Find(CurrentWeapon.weaponName);
        if (weaponTransform != null)
        {
            Destroy(weaponTransform.gameObject);
        }

        CurrentWeapon = null;
        currentWeaponHitbox = null;
        isUnarmed = true;
    }

    // Gọi khi nhân vật Attack (ví dụ animation event)
    public void PerformAttack(string attackType)
    {
        if (isUnarmed)
        {
            switch (attackType)
            {
                case "LeftPunch":
                    leftFistHitbox?.EnableHitbox();
                    break;
                case "RightPunch":
                    rightFistHitbox?.EnableHitbox();
                    break;
                case "LeftKick":
                    leftFootHitbox?.EnableHitbox();
                    break;
                case "RightKick":
                    rightFootHitbox?.EnableHitbox();
                    break;
            }
        }
        else
        {
            currentWeaponHitbox?.EnableHitbox();
        }
    }

    public void EndAttack(string attackType)
    {
        if (isUnarmed)
        {
            switch (attackType)
            {
                case "LeftPunch":
                    leftFistHitbox?.DisableHitbox();
                    break;
                case "RightPunch":
                    rightFistHitbox?.DisableHitbox();
                    break;
                case "LeftKick":
                    leftFootHitbox?.DisableHitbox();
                    break;
                case "RightKick":
                    rightFootHitbox?.DisableHitbox();
                    break;
            }
        }
        else
        {
            currentWeaponHitbox?.DisableHitbox();
        }
    }
}
