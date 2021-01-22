using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerProgress
{
    //Mantiene control de los datos del jugador a lo largo de la partida para cargarlos y guardarlos cuando sea necesario
    public int lastLevelUnlocked_standard;
    public int lastLevelUnlocked_ice;
    public int hints;

    public PlayerProgress(int lastLevelSt, int lastLevelIc, int h)
    {
        lastLevelUnlocked_standard = lastLevelSt;
        lastLevelUnlocked_ice = lastLevelIc;
        hints = h;
    }

    public void Save()
    {
        SaveSystem.SaveProgress(this);
    }

    public void Load()
    {
        PlayerProgress data = SaveSystem.LoadProgress();

        lastLevelUnlocked_standard = data.lastLevelUnlocked_standard;
        lastLevelUnlocked_ice = data.lastLevelUnlocked_ice;
        hints = data.hints;
    }
}
