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

    float[] highscores;

    public float musicVolume;

    public int quality;

    public int resolution;

    public bool fullScreen;

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
        Game inst = SaveLoad.Load() ? SaveLoad.savedGame : new Game();
        return inst;
    }

    public static float GetHighScore()
    {
        return GetHighScore(Instance.gameMode);
    }

    public static float GetHighScore(GameMode mode)
    {
        return Instance.highscores[(int)mode];
    }

    public static bool IsMode(GameMode mode)
    {
        return Instance.gameMode == mode;
    }

    public static void SetGameMode(GameMode mode)
    {
        Instance.gameMode = mode;
    }

    public static bool IsPaused()
    {
        return Instance._paused;
    }

    public static void SetPaused(bool value)
    {
        Instance._paused = value;
    }

    public static void SetMusicVolume(float volume)
    {
        Instance.musicVolume = volume;
        Camera.main.GetComponent<MusicVolume>().Start();
    }

    public static void SetQuality(int qualityIndex)
    {
        Instance.quality = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex, true);
    }

    public static void SetResolution(int resolutionIndex)
    {
        Instance.resolution = resolutionIndex;
        Resolution r = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
    }

    public static void SetFullScreen(bool isFullScreen)
    {
        Instance.fullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
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

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Game"/> class, with all highscores at 0 and default settings.
    /// </summary>
    private Game()
    {
        highscores = new float[System.Enum.GetNames(typeof(GameMode)).Length];
        musicVolume = 0.7f;

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].height == Screen.currentResolution.height && Screen.resolutions[i].width == Screen.currentResolution.width) resolution = i;
        }

        SetQuality(QualitySettings.GetQualityLevel());
        SetResolution(resolution);
        SetFullScreen(Screen.fullScreen);

        newHighScore = false;
        _paused = false;
    }

    /// <summary>
    /// Resets the instance of the <see cref="T:Game"/> class, sets all highscores at 0 and returns settings to default.
    /// </summary>
    public static void Reset()
    {
        Game b = new Game(); // create a game to copy the base game settings from
        Instance.highscores = b.highscores;
        Instance.musicVolume = b.musicVolume;
        Instance.newHighScore = b.newHighScore;
        Instance._paused = b._paused;

        Game.SetQuality(b.quality);
        Game.SetResolution(b.resolution);
        Game.SetFullScreen(b.fullScreen);
    }

    public static void Start()
    {
        Game.SetQuality(Instance.quality);
        Game.SetResolution(Instance.resolution);
        Game.SetFullScreen(Instance.fullScreen);
        QualitySettings.vSyncCount = 1;

        // Add highscores for new game modes to the list
        int numModes = System.Enum.GetNames(typeof(GameMode)).Length;
        if (Instance.highscores.Length != numModes)
        {
            float[] oldHighscores = Instance.highscores;
            Instance.highscores = new float[numModes];
            Array.Copy(oldHighscores, Instance.highscores, oldHighscores.Length);
        }

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
