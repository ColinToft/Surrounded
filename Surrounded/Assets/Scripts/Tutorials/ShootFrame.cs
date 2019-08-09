using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFrame : Frame
{
    float shootTime = 0;

    public override bool IsComplete()
    {
        if (GameObject.FindGameObjectsWithTag("Mouse Ball").Length > 0 && shootTime == 0) shootTime = Time.fixedTime;
        return shootTime != 0 && Time.fixedTime - shootTime > 0.7f; // Wait a bit before moving to the next frame
    }
}
