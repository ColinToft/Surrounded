using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SaveAndQuit();
        }
    }

    public void PlayClassic() {
        Game.LoadGame();
        Game.setGameMode(GameMode.Classic);
    }

    public void SaveAndQuit() {
        SaveLoad.Save();
        Game.QuitGame();
    }

}
