using UnityEngine;

public class AdventureRank : Singleton<AdventureRank>
{
    public int currentAR = 1, currentEXP = 0, worldLevel = 1;
    public int maxEXP { get { return GetEXPForAR(currentAR); } }

    public static int GetEXPForAR(int ar)
    {
        if (ar <= 10) return ar * 200;
        if (ar <= 20) return ar * 500;
        if (ar <= 30) return ar * 1000;
        if (ar <= 40) return ar * 1800;
        if (ar <= 50) return ar * 3000;
        return ar * 5000;
    }

    public void AddEXP(int amount)
    {
        currentEXP += amount;
        while (currentEXP >= maxEXP && currentAR < 60) { currentEXP -= maxEXP; currentAR++; OnRankUp(); }
    }

    void OnRankUp()
    {
        if (currentAR < 20) worldLevel = 1;
        else if (currentAR < 25) worldLevel = 2;
        else if (currentAR < 30) worldLevel = 3;
        else if (currentAR < 35) worldLevel = 4;
        else if (currentAR < 40) worldLevel = 5;
        else if (currentAR < 45) worldLevel = 6;
        else if (currentAR < 50) worldLevel = 7;
        else worldLevel = 8;
        Debug.Log("AR Naik! Rank " + currentAR + " | World Level: " + worldLevel);
    }
}
