﻿using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem{
    //Guarda el progreso en un fichero json
    public static void SaveProgress(PlayerProgress progress)
    {
        string json = JsonUtility.ToJson(progress);

        //Genera el hash asignado al json que hemos creado y lo guarda
        string hashValue = SecureHelper.Hash(json);
        PlayerPrefs.SetString("HASH", hashValue);

        string path = Application.persistentDataPath + "/progress.txt";
        FileStream stream = new FileStream(path, FileMode.Create);

        using(StreamWriter writer = new StreamWriter(stream))
        {
            writer.Write(json);
        }
        stream.Close();
    }

    public static PlayerProgress LoadProgress()
    {
        PlayerProgress progress = new PlayerProgress(new int[0], 0);
        Debug.Log(Application.persistentDataPath);
        string path = Application.persistentDataPath + "/progress.txt";
        if (File.Exists(path))
        {
            using(StreamReader reader = new StreamReader(path))
            {
                string defaultValue = "HASH_NOT_GENERATED";
                string json = reader.ReadToEnd();
                string hashValue = SecureHelper.Hash(json);
                //cargamos el hash que teníamos guardado
                string hashStored = PlayerPrefs.GetString("HASH", defaultValue);
                //comprueba que el hash del fichero de guardado sea el mismo que el último hash que hemos guardado
                if(hashStored == hashValue || hashStored == defaultValue)
                {
                    JsonUtility.FromJsonOverwrite(json, progress);
                }
            }

            return progress;
        }
        else
        {
            int[] l = new int[2] {0,0};
            PlayerProgress p = new PlayerProgress(l, 0);
            SaveProgress(p);
            return p;
        }
    }
}
