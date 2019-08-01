using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoresScreen : MonoBehaviour
{
    public TMP_Text highscoreTextLeft;
    public TMP_Text highscoreTextRight;

    public void Display()
    {
        // Load the text displaying the score and highscore
        string leftText = "", rightText = "";

        int modeCount = System.Enum.GetValues(typeof(GameMode)).Length;

        foreach (GameMode mode in System.Enum.GetValues(typeof(GameMode)))
        {
            if ((int)mode <= modeCount / 2)
            {
                leftText += mode.GetName() + ": " + Game.GetHighScore(mode).ToString("0") + "\n";
            } else
            {
                rightText += mode.GetName() + ": " + Game.GetHighScore(mode).ToString("0") + "\n";
            }
        }

        highscoreTextLeft.SetText(leftText);
        highscoreTextRight.SetText(rightText);

        // LayoutRebuilder.ForceRebuildLayoutImmediate(highscoreTextGroup);

        this.gameObject.SetActive(true);
    }

}
