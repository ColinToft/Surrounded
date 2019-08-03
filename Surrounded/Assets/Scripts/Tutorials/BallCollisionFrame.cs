using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionFrame : Frame
{

    int ballCount = 0;
    float startTime;

    protected override void Awake()
    {
        base.Awake();
        startTime = Time.fixedTime;
    }

    public override bool IsComplete()
    {
        // Count balls to determine if there are less than there were before (balls have collided)
        int newBallCount = GameObject.FindGameObjectsWithTag("Ball").Length;
        if (newBallCount > ballCount) ballCount = newBallCount;
        Debug.Log("fgjfkgj" + (Time.fixedTime - startTime).ToString());
        return (newBallCount < ballCount) && (Time.fixedTime - startTime > 10f);
    }

    public override bool ShouldSpawnBall()
    {
        return ballCount < 2;
    }
}
