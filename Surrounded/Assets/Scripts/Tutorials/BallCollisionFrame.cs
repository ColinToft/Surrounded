using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionFrame : Frame
{

    int ballCount = 0;
    float startTime;

    public override void StartFrame()
    {
        base.StartFrame();
        startTime = Time.fixedTime;
    }

    public override bool IsComplete()
    {
        // Count balls to determine if there are less than there were before (balls have collided)
        int newBallCount = GameObject.FindGameObjectsWithTag("Ball").Length;
        if (newBallCount > ballCount) ballCount = newBallCount;
        TutorialManager tm = GameObject.FindObjectOfType<TutorialManager>();
        return (newBallCount < ballCount) && (Time.fixedTime - startTime > 10f || tm.tutorialMode != TutorialMode.Corner);
    }

    public override bool ShouldSpawnBall()
    {
        return ballCount < 2 + (int)((Time.fixedTime - startTime) / 5f);
    }

    public override bool ShouldShootBall()
    {
        return false;
    }
}
