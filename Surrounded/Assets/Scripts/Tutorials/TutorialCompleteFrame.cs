using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCompleteFrame : Frame
{
    float startTime = 0;

    public override void StartFrame()
    {
        base.StartFrame();
        Game.FinishedTutorial();
        startTime = Time.fixedTime;
    }

    public override bool IsComplete()
    {
        TutorialManager tm = GameObject.FindObjectOfType<TutorialManager>();

        Debug.Log(Time.fixedTime - startTime);
        return Time.fixedTime - startTime > 15f;
    }
}
