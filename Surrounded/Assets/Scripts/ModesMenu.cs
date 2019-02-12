using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModesMenu : MonoBehaviour {

    public void PlayFrozen() {
        Game.LoadGame();
        Game.setGameMode(GameMode.Frozen);
    }

    public void PlayEasy() {
        Game.LoadGame();
        Game.setGameMode(GameMode.Easy);
    }

    public void PlayHard() {
        Game.LoadGame();
        Game.setGameMode(GameMode.Hard);
    }

    public void PlayCluster() {
        Game.LoadGame();
        Game.setGameMode(GameMode.Cluster);
    }

    public void PlayTwoHit() {
        Game.LoadGame();
        Game.setGameMode(GameMode.TwoHit);
    }

    public void PlayTeleport() {
        Game.LoadGame();
        Game.setGameMode(GameMode.Teleport);
    }
}
