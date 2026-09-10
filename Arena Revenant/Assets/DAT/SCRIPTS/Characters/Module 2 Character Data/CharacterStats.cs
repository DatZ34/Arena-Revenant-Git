using UnityEngine;
public class CharacterStats : MonoBehaviour
{
    [Header("Scripts Preferences")]
    private CharacterAttribute attributes;

    [Header("Base Status")]
    public int Strength;
    public int Vitality;
    public int Spirit;
    public int Agility;

    [Header("Bonus Status")]
    public int BonusStrength;
    public int BonusVitality;
    public int BonusSpirit;
    public int BonusAgility;

    [Header("Level Status")]
    // public int Level { get; private set; } = 1;
    // public int CurrentXP { get; private set; } = 0;
    // public int XPToNextLevel { get; private set; } = 100;
    // hoàn trả sau khi quan sát và test
    public int Level = 1;
    public int CurrentXP = 0;
    public int XPToNextLevel = 100;

    [Header("Free Attribute Points")]
    // Free Attribute Points
    public int FreePoints { get; private set; } = 0;

    private void Awake()
    {
        attributes = GetComponent<CharacterAttribute>();

        
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            GainXP(50);
        }
    }
    public void GainXP(int amount)
    {
        CurrentXP += amount;
        while (CurrentXP >= XPToNextLevel)
        {
            CurrentXP -= XPToNextLevel;
            LevelUp();
        }
        // Sau khi XP thay đổi → tính lại Attribute
        attributes.RecalculateAttributes();
    }

    void LevelUp() // Cần thêm logic cộng điểm thuộc tính tự do
    {
        Level++;
        FreePoints += 5;
        XPToNextLevel = 100 + (Level - 1) * 50;
        attributes.RecalculateAttributes();
    }
    #region Add Free Attribute Points
// Người chơi cộng điểm vào Base Stats
    public void StrengthChangePoint(int point)
    {
        if (FreePoints > 0 || point < 0)
        {
            Strength += point;
            FreePoints -= point;
            attributes.RecalculateAttributes();
            Debug.Log("+- Cập nhập điểm free point = " + FreePoints);
        } else
        {
            Debug.Log("@@không có điểm free point = " + FreePoints);
        }
    }

    public void VitalityChangePoint(int point)
    {
        if (FreePoints > 0)
        {
            Vitality += point;
            FreePoints -= point;
            attributes.RecalculateAttributes();
        }
    }
    public void SpiritChangePoint(int point)
    {
        if (FreePoints > 0)
        {
            Spirit += point;
            FreePoints -= point;
            attributes.RecalculateAttributes();
        }
    }
    public void AgilityChangePoint(int point)
    {
        if (FreePoints > 0)
        {
            Agility += point;
            FreePoints -= point;
            attributes.RecalculateAttributes();
        }
    }
    #endregion
// Tạm chưa có Item để dùng hàm 
    // public void EquipItem(Item item)
    // {
    //     BonusStrength += item.StrBonus;
    //     BonusVitality += item.VitBonus;
    //     BonusSpirit += item.SprBonus;
    //     BonusAgility += item.AgiBonus;
    //     attributes.RecalculateAttributes();
    // }

    // public void UnequipItem(Item item)
    // {
    //     BonusStrength -= item.StrBonus;
    //     BonusVitality -= item.VitBonus;
    //     BonusSpirit -= item.SprBonus;
    //     BonusAgility -= item.AgiBonus;
    //     attributes.RecalculateAttributes();
    // }
}
