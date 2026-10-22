using System.Collections.Generic;

// DAFTAR KARAKTER TETAP (hardcoded, tidak butuh asset). Jangan diubah.
public static class CharacterDatabase
{
    static CharacterData Make(string name, ElementType el, WeaponType w, Rarity r,
        int hp, int atk, int def, float spd, string skill, string burst, int burstCost,
        string vJoin, string vSkill, string vBurst, string vDef)
    {
        return new CharacterData
        {
            characterName = name, element = el, weaponType = w, rarity = r,
            baseHP = hp, baseATK = atk, baseDEF = def, baseSpeed = spd,
            normalAttack = new SkillData("Serangan Normal", 0, 0, 100),
            elementalSkill = new SkillData(skill, 8, 0, 250),
            elementalBurst = new SkillData(burst, 15, burstCost, 600),
            voiceJoinParty = vJoin, voiceSkill = vSkill, voiceBurst = vBurst, voiceDefeated = vDef
        };
    }

    public static readonly List<CharacterData> All = new List<CharacterData>
    {
        Make("Aether",  ElementType.Anemo,   WeaponType.Sword,    Rarity.B5, 12000, 280, 700, 5f, "Badai Angin",     "Tornado Surgawi",   60, "Ayo berangkat!", "Angin!", "Terbanglah!", "Maaf..."),
        Make("Lyenxe",  ElementType.Electro, WeaponType.Catalyst, Rarity.B5, 10500, 320, 600, 5f, "Sambaran Petir", "Murka Halilintar", 80, "Siap beraksi.", "Petir!", "Gemuruh!", "Tidak..."),
        Make("Ignara",  ElementType.Pyro,    WeaponType.Claymore, Rarity.B5, 13000, 300, 750, 4.5f, "Api Membara",   "Ledakan Inferno",   70, "Mari bakar!", "Panas!", "Terbakarlah!", "Padam..."),
        Make("Aqualia", ElementType.Hydro,   WeaponType.Bow,      Rarity.B5, 11000, 290, 650, 5.5f, "Ombak Pasang",  "Tsunami Dewi",      70, "Halo semua!", "Air!", "Hanyutlah!", "Aku jatuh..."),
        Make("Verdan",  ElementType.Dendro,  WeaponType.Catalyst, Rarity.B4, 10000, 250, 600, 5f, "Benih Rimba",    "Hutan Abadi",       60, "Hijau!", "Tumbuh!", "Rimba!", "Layu..."),
        Make("Glacia",  ElementType.Cryo,    WeaponType.Polearm,  Rarity.B4, 11500, 260, 680, 5f, "Tombak Es",      "Badai Salju",       60, "Dingin sekali.", "Beku!", "Membeku!", "Mencair..."),
        Make("Terrax",  ElementType.Geo,     WeaponType.Sword,    Rarity.B4, 12500, 240, 800, 4.5f, "Dinding Batu",  "Gempa Bumi",        60, "Kokoh!", "Batu!", "Hancur!", "Retak...")
    };

    // Party awal: 4 karakter pertama (tombol 1-2-3-4)
    public static List<CharacterData> DefaultParty { get { return All.GetRange(0, 4); } }
}
