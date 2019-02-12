using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public sealed class Game {

    private static readonly Lazy<Game> _instance = new Lazy<Game>(InitializeGame);
    
    public static Game Instance
    {
        get { return _instance.Value; }
    }

    /// <summary> How long it takes for color changer scripts to go through the whole rainbow. </summary>
    public static float ColourCycleTime = 60f;


    float[] highscores;

    public float musicVolume;

    [System.NonSerialized]
    public float score;

    [System.NonSerialized]
    public bool newHighScore;

    [System.NonSerialized]
    public GameMode gameMode;

    [System.NonSerialized]
    private bool _paused;

    private static Game InitializeGame()
    {
        if (SaveLoad.Load()) return SaveLoad.savedGame;
        else return new Game();
    }

    public static float GetHighScore()
    {
        return Instance.highscores[(int)Instance.gameMode];
    }

    public static float GetHighScore(GameMode mode)
    {
        return Instance.highscores[(int)mode];
    }

    public static bool isMode(GameMode mode)
    {
        return Instance.gameMode == mode;
    }

    public static void setGameMode(GameMode mode)
    {
        Instance.gameMode = mode;
    }

    public static bool isPaused()
    {
        return Instance._paused;
    }

    public static void setPaused(bool value)
    {
        Instance._paused = value;
    }

    public static void Reset()
    {
        Game b = new Game(); // create a game to copy the base game settings from
        Instance.highscores = b.highscores;
        Instance.musicVolume = b.musicVolume;
        Instance.newHighScore = false;
        Instance._paused = false;
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadGame()
    {
        SceneManager.LoadScene("Surrounded");
    }

    public static void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public static void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    public static string GetActiveScene()
    {
        return SceneManager.GetActiveScene().name;
    }
    
    private Game()
    {
        highscores = new float[System.Enum.GetNames(typeof(GameMode)).Length];
        musicVolume = 0.7f;
        newHighScore = false;
        _paused = false;
    }


    /// <summary>Set the most recent score of the game.
    /// <returns>Returns <see langword="true"/> if score is bigger than the previous highscore, otherwise <see langword="false"/>. </returns>
    /// </summary>
    public bool CheckAndSetScore(float score)
    {
        this.score = score;
        if (score > highscores[(int)gameMode]) {
            highscores[(int)gameMode] = score;
            newHighScore = true;
            SaveLoad.Save();
            return true;
        }
        this.newHighScore = false;
        SaveLoad.Save();
        return false;
    }

}
