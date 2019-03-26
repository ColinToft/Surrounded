using UnityEngine;

public class ModesMenu : MonoBehaviour {

    public void PlayFrozen() {
        Game.SetGameMode(GameMode.Frozen);
        Game.LoadGame();
    }

    public void PlayEasy() {
        Game.SetGameMode(GameMode.Easy);
        Game.LoadGame();
    }

    public void PlayHard() {
        Game.SetGameMode(GameMode.Hard);
        Game.LoadGame();
    }

    public void PlayCluster() {
        Game.SetGameMode(GameMode.Cluster);
        Game.LoadGame();
    }

    public void PlayTwoHit() {
        Game.SetGameMode(GameMode.TwoHit);
        Game.LoadGame();
    }

    public void PlayTeleport() {
        Game.SetGameMode(GameMode.Teleport);
        Game.LoadGame();
    }

    public void PlayDodge() {
        Game.SetGameMode(GameMode.Dodge);
        Game.LoadGame();
    }

    public void PlayInvisible() {
        Game.SetGameMode(GameMode.Invisible);
        Game.LoadGame();
    }
}
