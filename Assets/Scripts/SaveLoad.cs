using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;

public static class SaveLoad {

    public static Game savedGame;

    public static void Save() {
        BinaryFormatter bf = new BinaryFormatter();
        Debug.Log("Saving game to: " + Application.persistentDataPath + "/save.dat");
        FileStream file = File.Create(Application.persistentDataPath + "/save.dat");
        bf.Serialize(file, Game.Instance);
        file.Close();
    }

    public static bool Load() {
        if (File.Exists(Application.persistentDataPath + "/save.dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open);
            SaveLoad.savedGame = (Game)bf.Deserialize(file);
            file.Close();
            return true;
        } else {
            Debug.Log("No save file found.");
            return false;
        }
    }
}
