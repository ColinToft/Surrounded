using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModesMenu : MonoBehaviour {

    public void PlayFrozen() {
        Game.LoadGame();
        Game.SetGameMode(GameMode.Frozen);
    }

    public void PlayEasy() {
        Game.LoadGame();
        Game.SetGameMode(GameMode.Easy);
    }

    public void PlayHard() {
        Game.LoadGame();
        Game.SetGameMode(GameMode.Hard);
    }

    public void PlayCluster() {
        Game.LoadGame();
        Game.SetGameMode(GameMode.Cluster);
    }

    public void PlayTwoHit() {
        Game.LoadGame();
        Game.SetGameMode(GameMode.TwoHit);
    }

    public void PlayTeleport() {
        Game.LoadGame();
        Game.SetGameMode(GameMode.Teleport);
    }
}
