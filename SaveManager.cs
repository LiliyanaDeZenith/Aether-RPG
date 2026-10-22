using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    public void SaveGame()
    {
        var ar = AdventureRank.Instance;
        PlayerPrefs.SetInt("AR", ar.currentAR);
        PlayerPrefs.SetInt("AR_EXP", ar.currentEXP);
        PlayerPrefs.SetInt("WorldLevel", ar.worldLevel);
        var party = CharacterManager.Instance.partyMembers;
        for (int i = 0; i < party.Count; i++)
        {
            PlayerPrefs.SetString("C" + i + "_Name", party[i].data.characterName);
            PlayerPrefs.SetInt("C" + i + "_Level", party[i].level);
            PlayerPrefs.SetInt("C" + i + "_EXP", party[i].currentEXP);
            PlayerPrefs.SetInt("C" + i + "_Asc", party[i].ascensionPhase);
        }
        PlayerPrefs.Save();
        Debug.Log("Game Tersimpan!");
    }

    public void LoadGame()
    {
        if (!PlayerPrefs.HasKey("AR")) return;
        var ar = AdventureRank.Instance;
        ar.currentAR = PlayerPrefs.GetInt("AR", 1);
        ar.currentEXP = PlayerPrefs.GetInt("AR_EXP", 0);
        ar.worldLevel = PlayerPrefs.GetInt("WorldLevel", 1);
        var party = CharacterManager.Instance.partyMembers;
        for (int i = 0; i < party.Count; i++)
        {
            party[i].level = PlayerPrefs.GetInt("C" + i + "_Level", 1);
            party[i].currentEXP = PlayerPrefs.GetInt("C" + i + "_EXP", 0);
            party[i].ascensionPhase = PlayerPrefs.GetInt("C" + i + "_Asc", 0);
            party[i].RecalculateStats();
        }
        Debug.Log("Game Dimuat!");
    }
}
