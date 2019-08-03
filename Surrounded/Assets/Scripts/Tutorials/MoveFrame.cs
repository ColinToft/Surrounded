using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFrame : Frame
{
    private bool verticalDone = false, horizontalDone = false;

    public override bool IsComplete()
    {
        if (Input.GetAxis("Vertical") != 0f) verticalDone = true;
        if (Input.GetAxis("Horizontal") != 0f) horizontalDone = true;

        return verticalDone && horizontalDone;
    }

    public override bool ShouldSpawnBall()
    {
        return false;
    }
}
