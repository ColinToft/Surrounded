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

    bool[] completedTutorials;

    public float musicVolume;

    public int quality;

    public int screenWidth, screenHeight, refreshRate;

    public bool fullScreen;

    public bool vSyncOn;

    [System.NonSerialized]
    public float score;

    [System.NonSerialized]
    public bool newHighScore;

    [System.NonSerialized]
    public GameMode gameMode;

    [System.NonSerialized]
    public bool doingTutorial;

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

    public static bool IsResolutionAvailable(int width, int height, int refreshRate)
    {
        Resolution r;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            r = Screen.resolutions[i];
            if (r.width == width && r.height == height && r.refreshRate == refreshRate) return true;
        }

        return false;
    }

    public static int GetResolutionIndex()
    {
        Resolution r;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            r = Screen.resolutions[i];
            if (r.width == Instance.screenWidth && r.height == Instance.screenHeight && r.refreshRate == Instance.refreshRate) return i;
        }

        Debug.Log("Unable to find previous resolution, setting to current resolution.");
        Game.SetResolution(Screen.currentResolution);
        return GetResolutionIndex(); 
    }

    public static bool IsMode(GameMode mode)
    {
        return Instance.gameMode == mode;
    }

    public static bool IsDoingTutorial()
    {
        return Instance.doingTutorial;
    }

    public static bool HasCompletedTutorial(GameMode mode)
    {
        return Instance.completedTutorials[(int)mode];
    }

    public static void FinishedTutorial()
    {
        Instance.completedTutorials[(int)Instance.gameMode] = true;
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
        Resolution r = Screen.resolutions[resolutionIndex];
        Game.SetResolution(r);
    }

    public static void SetResolution(Resolution r)
    {
        Game.SetResolution(r.width, r.height, r.refreshRate);
    }

    public static void SetResolution(int width, int height, int refreshRate)
    {
        Instance.screenWidth = width;
        Instance.screenHeight = height;
        Instance.refreshRate = refreshRate;
        Screen.SetResolution(width, height, Screen.fullScreen, refreshRate);
    }

    public static void SetFullScreen(bool isFullScreen)
    {
        Instance.fullScreen = isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public static void SetVSync(bool vSyncOn)
    {
        Instance.vSyncOn = vSyncOn;
        QualitySettings.vSyncCount = vSyncOn ? 1 : 0;
    }

    public static void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void LoadGame(GameMode mode)
    {
        if (!Game.HasCompletedTutorial(GameMode.Classic))
        {
            // TODO: Ask if they would like to do the classic tutorial
            Instance.gameMode = GameMode.Classic;
            Instance.doingTutorial = true;
        }
        else
        {
            Instance.gameMode = mode;
            if (!Game.HasCompletedTutorial(mode)) Instance.doingTutorial = true;
            else Instance.doingTutorial = false;
        }

        SceneManager.LoadScene("Surrounded");
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

        completedTutorials = new bool[System.Enum.GetNames(typeof(GameMode)).Length];

        screenWidth = Screen.currentResolution.width;
        screenHeight = Screen.currentResolution.height;
        refreshRate = Screen.currentResolution.refreshRate;

        quality = QualitySettings.GetQualityLevel();
        fullScreen = Screen.fullScreen;
        vSyncOn = QualitySettings.vSyncCount == 0 ? false : true;

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
        Instance.completedTutorials = b.completedTutorials;

        Game.SetQuality(b.quality);
        Game.SetResolution(b.screenWidth, b.screenHeight, b.refreshRate);
        Game.SetFullScreen(b.fullScreen);
        Game.SetVSync(b.vSyncOn);
    }

    public static void Start()
    {
        Game.SetQuality(Instance.quality);

        Game.SetResolution(Instance.screenWidth, Instance.screenHeight, Instance.refreshRate); // If this is no longer available Unity automatically finds the best alternative (closest dimensions and highest refresh rate)

        if (!Game.IsResolutionAvailable(Instance.screenWidth, Instance.screenHeight, Instance.refreshRate))
        {
            Game.SetResolution(Screen.currentResolution); // If our saved resolution is no longer available store the new resolution
        }

        Game.SetFullScreen(Instance.fullScreen);
        Game.SetVSync(Instance.vSyncOn);

        // Add highscores and tutorials for new game modes to the list
        int numModes = System.Enum.GetNames(typeof(GameMode)).Length;
        if (Instance.highscores.Length != numModes)
        {
            float[] oldHighscores = Instance.highscores;
            Instance.highscores = new float[numModes];
            Array.Copy(oldHighscores, Instance.highscores, oldHighscores.Length);

            bool[] oldCompletedTutorials = Instance.completedTutorials;
            Instance.completedTutorials = new bool[numModes];
            Array.Copy(oldCompletedTutorials, Instance.completedTutorials, oldCompletedTutorials.Length);
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
