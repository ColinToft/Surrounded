public enum GameMode
{ 
    Classic, Frozen, Easy, Hard, Cluster, TwoHit, Teleport, Dodge, Invisible
}

static class GameModeMethods
{
    public static string GetName(this GameMode mode)
    {
        switch (mode)
        {
            case GameMode.Classic:
                return "CLASSIC";
            case GameMode.Frozen:
                return "FROZEN";
            case GameMode.Easy:
                return "EASY";
            case GameMode.Hard:
                return "HARD";
            case GameMode.Cluster:
                return "CLUSTER";
            case GameMode.TwoHit:
                return "TWO-HIT";
            case GameMode.Teleport:
                return "TELEPORT";
            case GameMode.Dodge:
                return "DODGE";
            case GameMode.Invisible:
                return "INVISIBLE";
            default:
                return "?";
        }
    }

}