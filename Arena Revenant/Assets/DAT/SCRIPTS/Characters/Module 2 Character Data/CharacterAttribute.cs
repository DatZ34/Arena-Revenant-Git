using UnityEngine;
using System.Collections.Generic;
using System;
[RequireComponent(typeof(CharacterStats))] 

public class CharacterAttribute : MonoBehaviour
{
    // Event thông báo khi stats thay đổi
    public event Action OnStatsChanged; 
        // LƯU Ý: OnStatsChanged Dùng để phát sự kiện cho UI khi chỉ số thay đổi mà không cần gọi thủ công ở từng nơi.
        
    [Header("Scripts Preferences")]
    public CharacterStats stats;
    
    [Header("Runtime Stats")]
    // Runtime Stats (tính toán từ Base Stats)
    public int MaxHP;
    public int MP;
    public int Damage;
    public int Defense;
    public float MoveSpeed;
    public float CriticalChance;

    [Header("Runtime Special Stats")]
    // các chỉ số sau này
    public int Stamina { get; private set; }
    public float HpRegen { get; private set; }
    public float MpRegen { get; private set; }
    public int Armor { get; private set; }
    public int MagicResist { get; private set; }
    public float AttackSpeed;
    public float Accuracy { get; private set; }
    public float Evasion { get; private set; }
    public float Luck { get; private set; }

    public void Awake()
    {
        stats = GetComponent<CharacterStats>(); // vẫn bị null khi start game = nên gán ngay trên inpestor
    }
    public void RecalculateAttributes()
    {
        int str = stats.Strength + stats.BonusStrength;
        int vit = stats.Vitality + stats.BonusVitality;
        int spr = stats.Spirit + stats.BonusSpirit;
        int agi = stats.Agility + stats.BonusAgility;

        MaxHP = vit * 15;
        MP = spr * 10;
        Damage = str * 5 + vit * 2;
        Defense = str * 1 + vit * 2;
        MoveSpeed = agi * 0.1f + vit * 0.05f;
        CriticalChance = spr * 0.01f + agi * 0.01f;

        Stamina = vit * 5;
        HpRegen = vit * 0.2f;
        MpRegen = spr * 0.3f;
        Armor = vit * 2;
        MagicResist = spr * 2;
        AttackSpeed = agi * 1f; // trước mắt đặt bằng như này - sau tôi ưu lại logic
        Accuracy = agi * 0.02f;
        Evasion = agi * 0.02f;
        Luck = spr * 0.01f;
        // Gọi event khi stats thay đổi
        OnStatsChanged?.Invoke();
    }
    
}



