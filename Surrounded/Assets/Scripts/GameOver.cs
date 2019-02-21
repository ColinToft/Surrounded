using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour {

    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    
    public AudioClip electro;
    public AudioClip lemonade;

    public void Start() {
        // Load the text displaying the score and highscore
        scoreText.SetText("SCORE: " + Game.Instance.score.ToString("0"));
        if (Game.Instance.newHighScore) highscoreText.SetText("NEW HIGHSCORE!");
        else {
            highscoreText.SetText("HIGHSCORE: " + Game.GetHighScore().ToString("0"));
            highscoreText.fontSize = 65;
        }
        
        // Load the audio
        AudioSource audioSource = Camera.main.GetComponent<AudioSource>();
        if (Game.Instance.newHighScore) audioSource.clip = lemonade;
        else audioSource.clip = electro;
        audioSource.Play();
        
    }

    public void GoToMenu () {
        Game.LoadMainMenu();
    }

    public void RestartGame() {
        Game.LoadGame();
    }
}
