using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem{
    public static void SaveProgress(PlayerProgress progress)
    {
        string json = JsonUtility.ToJson(progress);

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
        string path = Application.persistentDataPath + "/progress.txt";
        if (File.Exists(path))
        {
            using(StreamReader reader = new StreamReader(path))
            {
                string defaultValue = "HASH_NOT_GENERATED";
                string json = reader.ReadToEnd();
                string hashValue = SecureHelper.Hash(json);
                string hashStored = PlayerPrefs.GetString("HASH", defaultValue);

                if(hashStored == hashValue || hashStored == defaultValue)
                {
                    JsonUtility.FromJsonOverwrite(json, progress);
                }
            }

            return progress;
        }
        else
        {
            Debug.LogError("Save file not fount");
            return null;
        }
    }
}
