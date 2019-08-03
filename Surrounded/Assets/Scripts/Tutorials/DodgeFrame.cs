using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeFrame : Frame
{

    public KeepScore score;

    public override bool IsComplete()
    {
        return score.score > 5000f;
    }

    public override bool ShouldSpawnBall()
    {
        return GameObject.FindGameObjectsWithTag("Ball").Length == 0;
    }
}
