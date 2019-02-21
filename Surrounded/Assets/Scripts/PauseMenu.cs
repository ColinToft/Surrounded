using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;

    public KeepScore score;
	
	// Update is called once per frame
	void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Game.IsPaused()) Resume();
            else Pause();
        }
	}

    public void GoToMainMenu() {
        Game.Instance.CheckAndSetScore(score.score);
        Game.SetPaused(false);
        Time.timeScale = 1f;
        SaveLoad.Save();
        Game.LoadMainMenu();
    }

    public void Resume() {
        Camera.main.GetComponent<GameAudio>().Resume();
        Game.SetPaused(false);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause () {
        Camera.main.GetComponent<GameAudio>().Pause();
        Game.SetPaused(true);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SaveAndQuit() {
        Game.Instance.CheckAndSetScore(score.score);
        SaveLoad.Save();
        Game.QuitGame();
    }

    void OnApplicationPause(bool pause) {
        if (pause) Pause();
        else if (Game.GetActiveScene() != "Surrounded") Resume();
    }
}
