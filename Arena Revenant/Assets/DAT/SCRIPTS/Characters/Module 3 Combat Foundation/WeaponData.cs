using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "RPG/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Basic Info")]
    public string weaponName;
    public int damage;
    public float range;
    public float attackSpeed;

    [Header("Skill")]
    public string skillName; // hoặc tham chiếu tới SkillData ScriptableObject

    [Header("Prefab")]
    public GameObject prefab; // model hoặc object của vũ khí
}
