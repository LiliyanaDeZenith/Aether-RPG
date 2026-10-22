using System;
using System.Collections.Generic;

[Serializable]
public class SkillData
{
    public string skillName;
    public int cooldown;
    public int energyCost;
    public int baseDamage;
    public string animationName;
    public SkillData(string n, int cd, int cost, int dmg) { skillName = n; cooldown = cd; energyCost = cost; baseDamage = dmg; }
}

[Serializable]
public class CharacterData
{
    public string characterName;
    public ElementType element;
    public WeaponType weaponType;
    public Rarity rarity;
    public int baseHP, baseATK, baseDEF;
    public float baseSpeed;
    public SkillData normalAttack, elementalSkill, elementalBurst;
    public string voiceJoinParty, voiceSkill, voiceBurst, voiceDefeated;
}

[Serializable]
public class WeaponData { public string weaponName; public int rarity; public int baseATK; public string subStat; }

[Serializable]
public class Artifact
{
    public string name, slot, mainStat;
    public ElementType setType;
    public float mainStatValue;
    public List<string> subStats = new List<string>();
    public List<float> subStatValues = new List<float>();
}

public class PullResult { public Rarity rarity; public CharacterData character; public WeaponData weapon; }

[Serializable]
public class BannerData
{
    public string bannerName;
    public CharacterData featuredB5;
    public List<CharacterData> standardB5;
    public List<CharacterData> rateUpB4;
    public DateTime endDate;
}
