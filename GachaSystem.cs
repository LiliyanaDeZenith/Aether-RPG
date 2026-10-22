using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : Singleton<GachaSystem>
{
    public int pityCount = 0, softPityStart = 74, hardPity = 90, guaranteedB5 = 0;

    protected override void Awake() { base.Awake(); LoadPity(); }

    public static BannerData DefaultBanner()
    {
        var all = CharacterDatabase.All;
        return new BannerData
        {
            bannerName = "Banner Standar", featuredB5 = all[1],
            standardB5 = new List<CharacterData> { all[0], all[2], all[3] },
            rateUpB4 = new List<CharacterData> { all[4], all[5], all[6] }
        };
    }

    public PullResult PullOnce(BannerData banner)
    {
        pityCount++;
        float rateB5 = 0.006f;
        if (pityCount >= softPityStart) rateB5 += 0.06f * (pityCount - softPityStart + 1);

        var result = new PullResult();
        float roll = Random.value;

        if (pityCount >= hardPity || roll <= rateB5)
        {
            result.rarity = Rarity.B5;
            result.character = GetRandomB5(banner);
            pityCount = 0;
            Debug.Log("=== BINTANG 5 DIDAPAT! ===");
        }
        else if (roll <= 0.051f + rateB5)
        {
            result.rarity = Rarity.B4;
            result.character = banner.rateUpB4[Random.Range(0, banner.rateUpB4.Count)];
        }
        else
        {
            result.rarity = Rarity.B4;
            result.weapon = new WeaponData { weaponName = "Senjata Acak", rarity = 4, baseATK = 40 + Random.Range(0, 20) };
        }
        SavePity();
        return result;
    }

    CharacterData GetRandomB5(BannerData banner)
    {
        if (guaranteedB5 == 1) { guaranteedB5 = 0; return banner.featuredB5; }
        if (Random.value < 0.5f) { guaranteedB5 = 0; return banner.featuredB5; }
        guaranteedB5 = 1;
        return banner.standardB5[Random.Range(0, banner.standardB5.Count)];
    }

    void SavePity() { PlayerPrefs.SetInt("Gacha_Pity", pityCount); PlayerPrefs.SetInt("Gacha_Guarantee", guaranteedB5); }
    void LoadPity() { pityCount = PlayerPrefs.GetInt("Gacha_Pity", 0); guaranteedB5 = PlayerPrefs.GetInt("Gacha_Guarantee", 0); }
}
