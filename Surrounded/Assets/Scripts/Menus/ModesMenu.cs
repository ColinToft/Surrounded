using UnityEngine;

public class ModesMenu : MonoBehaviour {

    public void PlayFrozen() {
        Game.LoadGame(GameMode.Frozen);
    }

    public void PlayEasy() {
        Game.LoadGame(GameMode.Easy);
    }

    public void PlayHard() {
        Game.LoadGame(GameMode.Hard);
    }

    public void PlayCluster() {
        Game.LoadGame(GameMode.Cluster);
    }

    public void PlayTwoHit() {
        Game.LoadGame(GameMode.TwoHit);
    }

    public void PlayTeleport() {
        Game.LoadGame(GameMode.Teleport);
    }

    public void PlayDodge() {
        Game.LoadGame(GameMode.Dodge);
    }

    public void PlayInvisible() {
        Game.LoadGame(GameMode.Invisible);
    }
}
