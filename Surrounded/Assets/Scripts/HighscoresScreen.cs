using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoresScreen : MonoBehaviour
{
    public TMP_Text highscoreText;

    public void Display()
    {
        // Load the text displaying the score and highscore
        string text = "";

        foreach (GameMode mode in System.Enum.GetValues(typeof(GameMode)))
        {
            text += mode.GetName() + ": " + Game.GetHighScore(mode).ToString("0") + "\n";
        }

        highscoreText.SetText(text);

        this.gameObject.SetActive(true);
    }

}
