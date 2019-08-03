using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour { 

    void Awake()
    {
        Game.Start();
    }

    public void PlayClassic()
    {
        Game.LoadGame(GameMode.Classic);
    }

    public void SaveAndQuit()
    {
        SaveLoad.Save();
        Game.QuitGame();
    }

}
