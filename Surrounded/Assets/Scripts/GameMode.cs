public enum GameMode
{ 
    Classic, Frozen, Easy, Hard, Cluster, TwoHit, Teleport, Dodge
}

static class GameModeMethods
{
    public static string GetName(this GameMode s1)
    {
        switch (s1)
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
            default:
                return "?";
        }
    }

}