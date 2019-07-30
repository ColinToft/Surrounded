using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad {

    public static Game savedGame;

    private static readonly string saveFile = "/save.dat";

    public static void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log("Saving game to: " + Application.persistentDataPath + saveFile);
        FileStream file = File.Create(Application.persistentDataPath + saveFile);
        bf.Serialize(file, Game.Instance);
        file.Close();
    }

    public static bool Load() {
        if (File.Exists(Application.persistentDataPath + saveFile)) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + saveFile, FileMode.Open);
            try
            {
                SaveLoad.savedGame = (Game)bf.Deserialize(file);
            } catch (SerializationException)
            {
                file.Close();
                Debug.Log("Error loading data from file. A new save file will be created.");
                return false;
            }
            file.Close();
            return true;
        } else {
            Debug.Log("No save file found.");
            return false;
        }
    }
}
