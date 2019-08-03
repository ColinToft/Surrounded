using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootFrame : Frame
{
    public override bool IsComplete()
    {
        return GameObject.FindGameObjectsWithTag("Mouse Ball").Length > 0;
    }
}
