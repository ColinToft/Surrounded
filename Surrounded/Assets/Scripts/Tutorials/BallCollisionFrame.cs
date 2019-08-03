using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollisionFrame : Frame
{

    int ballCount = 0;

    public override bool IsComplete()
    {
        // Count balls to determine if there are less than there were before (balls have collided)
        int newBallCount = GameObject.FindGameObjectsWithTag("Ball").Length;
        if (newBallCount > ballCount) ballCount = newBallCount;
        return newBallCount < ballCount;
    }
}
