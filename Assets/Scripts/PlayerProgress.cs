using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerProgress
{
    //Mantiene control de los datos del jugador a lo largo de la partida para cargarlos y guardarlos cuando sea necesario
    public int[] lastLvls;
    public int hints;

    public PlayerProgress(int[] last, int h)
    {
        lastLvls = last;
        hints = h;
    }

    public void Save()
    {
        SaveSystem.SaveProgress(this);
    }

    public void Load()
    {
        PlayerProgress data = SaveSystem.LoadProgress();

        lastLvls = data.lastLvls;
        hints = data.hints;
    }
}
