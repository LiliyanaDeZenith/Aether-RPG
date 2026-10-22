using UnityEngine;

public class LevelSystem : Singleton<LevelSystem>
{
    public bool trainingDeluxeMode = false;
    public bool showTrainingNotification = false;

    public static int GetEXPForLevel(int level)
    {
        if (level <= 20) return level * 100;
        if (level <= 40) return level * 150;
        if (level <= 60) return level * 200;
        return level * 250;
    }

    public static int GetMaxLevelForAscension(int phase)
    {
        switch (phase)
        {
            case 0: return 20; case 1: return 40; case 2: return 50;
            case 3: return 60; case 4: return 70; case 5: return 80; case 6: return 90;
            default: return 20;
        }
    }

    public void ToggleTrainingMode()
    {
        trainingDeluxeMode = !trainingDeluxeMode;
        showTrainingNotification = true;
        Invoke("HideNotification", 3f);
    }
    void HideNotification() { showTrainingNotification = false; }
}
