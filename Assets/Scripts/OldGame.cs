/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game {

    public enum GameMode {
        Classic, Frozen, Easy, Hard, Cluster
    }
    
    private static Game instance;

    public static Game Instance {
        get {
            // Load the Game object
            if (instance == null) {
                SaveLoad.Load();
                instance = SaveLoad.savedGame;
            }
            return instance;
        }
    }

    public static void Reset() {
        instance = new Game();
    }

    float[] highscores;

    public float musicVolume;

    [System.NonSerialized]
    public float score;

    [System.NonSerialized]
    public bool newHighScore;
    
    [System.NonSerialized]
    public Game.GameMode gameMode;

    public Game() {
        highscores = new float[System.Enum.GetNames(typeof(GameMode)).Length];
        musicVolume = 0.7f;
        newHighScore = false;
    }

    /// <summary>Set the most recent score of the game.
    /// <returns>Returns <see langword="true"/> if score is bigger than the previous highscore, otherwise <see langword="false"/>. </returns>
    /// </summary>
    public bool CheckAndSetScore(float score) {
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

    public float GetHighScore() { return highscores[(int)gameMode]; }

    /// <summary>Does nothing, but calling this will force the <see cref="Game.Instance"/> get method to be run.</summary>
    public void EnsureLoaded() {} 
}
*/