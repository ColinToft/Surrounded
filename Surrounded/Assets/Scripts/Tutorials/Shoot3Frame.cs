using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot3Frame : Frame
{
    int mouseBallCount;
    int totalDestroyed = 0;

    public override bool IsComplete()
    {
        int newMouseBallCount = GameObject.FindGameObjectsWithTag("Mouse Ball").Length;
        if (newMouseBallCount < mouseBallCount) totalDestroyed++; // a mouse ball has disappeared, therefore a ball was destroyed
        mouseBallCount = newMouseBallCount;
        return totalDestroyed >= 3;
    }
}
