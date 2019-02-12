using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Game.isPaused()) Resume();
            else Pause();
        }
	}

    public void GoToMainMenu() {
        Game.LoadMainMenu();
    }

    public void Resume() {
        Camera.main.GetComponent<GameAudio>().Resume();
        Game.setPaused(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause () {
        Camera.main.GetComponent<GameAudio>().Pause();
        Game.setPaused(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SaveAndQuit() {
        Game.Instance.CheckAndSetScore(FindObjectOfType<Text>().GetComponent<KeepScore>().score);
        SaveLoad.Save();
        Game.QuitGame();
    }

    void OnApplicationPause(bool pause) {
        if (pause) Pause();
        else if (Game.GetActiveScene() != "Surrounded") Resume();
    }
}
