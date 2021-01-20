using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerProgress
{
    public int lastLevelUnlocked;
    public int hints;

    public PlayerProgress(int lastLevel, int h)
    {
        lastLevelUnlocked = lastLevel;
        hints = h;
    }

    public void Save()
    {
        SaveSystem.SaveProgress(this);
    }

    public void Load()
    {
        PlayerProgress data = SaveSystem.LoadProgress();

        lastLevelUnlocked = data.lastLevelUnlocked;
        hints = data.hints;
    }
}
