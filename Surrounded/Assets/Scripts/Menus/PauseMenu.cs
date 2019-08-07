using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PauseMenu : MonoBehaviour {

    public GameObject pauseMenuUI;

    public KeepScore score;
	
	void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Game.IsPaused() && pauseMenuUI.activeSelf) Resume();
            else Pause();
        }
	}

    public void GoToMainMenu() {
        Game.Instance.CheckAndSetScore(score.score);
        Game.Unpause();
        SaveLoad.Save();
        Game.LoadMainMenu();
    }

    public void Resume() {
        Camera.main.GetComponent<GameAudio>().Resume();
        if (TutorialManager.CanUnpauseGame()) Game.Unpause();
        pauseMenuUI.SetActive(false);
        TutorialManager.UnhidePopup();
    }

    public void Pause () {
        Camera.main.GetComponent<GameAudio>().Pause();
        Game.Pause();
        TutorialManager.HidePopup();
        pauseMenuUI.SetActive(true);
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
