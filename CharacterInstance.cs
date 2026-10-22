using System;
using UnityEngine;

[Serializable]
public class CharacterInstance
{
    public CharacterData data;
    public int level = 1, ascensionPhase = 0, currentEXP = 0;
    public float currentHP, maxHP, energy = 0;
    public bool isActive;
    public int talentNormalAttack = 1, talentSkill = 1, talentBurst = 1;
    public WeaponData equippedWeapon;
    public Artifact[] artifacts = new Artifact[5];
    float skillCooldownTimer = 0, burstCooldownTimer = 0;

    public CharacterInstance(CharacterData data)
    {
        this.data = data; currentHP = data.baseHP; maxHP = data.baseHP;
    }

    public int maxEXP { get { return LevelSystem.GetEXPForLevel(level); } }

    public void AddEXP(int amount, bool trainingMode = false)
    {
        currentEXP += amount;
        while (currentEXP >= maxEXP && level < 90)
        {
            currentEXP -= maxEXP; level++; RecalculateStats();
            if (trainingMode) for (int i = 0; i < 4 && level < 90; i++) { level++; RecalculateStats(); }
        }
    }

    public void RecalculateStats()
    {
        float bonus = 1f + ascensionPhase * 0.05f;
        maxHP = Mathf.RoundToInt(data.baseHP * (1f + level * 0.01f) * bonus);
        currentHP = Mathf.Min(currentHP, maxHP);
    }

    public void NormalAttack() { Debug.Log(data.characterName + ": Serangan Normal!"); }
    public void UseElementalSkill()
    {
        if (skillCooldownTimer > 0) return;
        Debug.Log(data.characterName + " [" + data.element + "]: " + data.elementalSkill.skillName + "!");
        skillCooldownTimer = data.elementalSkill.cooldown;
        RegenEnergy(20);
    }
    public void UseElementalBurst()
    {
        if (energy < data.elementalBurst.energyCost) return;
        Debug.Log(data.characterName + " BURST: " + data.elementalBurst.skillName + "!");
        energy -= data.elementalBurst.energyCost;
    }
    public void RegenEnergy(float a) { energy = Mathf.Min(100f, energy + a); }
    public void UpdateCooldowns()
    {
        if (skillCooldownTimer > 0) skillCooldownTimer -= Time.deltaTime;
        if (burstCooldownTimer > 0) burstCooldownTimer -= Time.deltaTime;
    }
}
