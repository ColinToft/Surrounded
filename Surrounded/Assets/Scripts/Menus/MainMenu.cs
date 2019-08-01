using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour { 

    void Awake()
    {
        Game.Start();
    }

    public void PlayClassic() {
        Game.LoadGame();
        Game.SetGameMode(GameMode.Classic);
    }

    public void SaveAndQuit() {
        SaveLoad.Save();
        Game.QuitGame();
    }

}
