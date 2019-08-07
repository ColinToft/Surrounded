using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFrame : Frame
{
    private bool verticalDone = false, horizontalDone = false;

    float completeTime = 0;
    
    public override bool IsComplete()
    {
        if (Input.GetAxis("Vertical") != 0f) verticalDone = true;
        if (Input.GetAxis("Horizontal") != 0f) horizontalDone = true;

        if (verticalDone && horizontalDone && completeTime == 0) completeTime = Time.fixedTime;

        return verticalDone && horizontalDone && Time.fixedTime - completeTime > 1.5f; // Wait a bit before moving to the next frame
    }

    public override bool ShouldSpawnBall()
    {
        return false;
    }
}
