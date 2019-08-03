using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    private static TutorialManager _instance;

    public List<Frame> frames = new List<Frame>();

    public TMP_Text messageText;
    public GameObject messagePanel;

    private Frame currentFrame;

    public void AddFrame(Frame frame)
    {
        if (!frames.Contains(frame)) frames.Add(frame);
    }

    public Frame GetFrameByOrder(int order)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            if (frames[i].order == order) return frames[i];
            else Debug.Log("Not the right order: " + frames[i].order.ToString());
        }

        Debug.Log("Unable to find frame, returning null.");
        return null;
    }

    public void SetFrame(int order)
    {
        currentFrame = GetFrameByOrder(order);
        Debug.Log(currentFrame.message);

        try
        {
            messageText.SetText(currentFrame.message);
        } catch (NullReferenceException) {
            CompletedAllFrames();
        }

        Debug.Log(messageText.text);
    }

    public void GoToNextFrame()
    {
        SetFrame(currentFrame.order + 1);
    }

    public void CompletedAllFrames()
    {
        messageText.SetText("Congratulations, you have completed the tutorial!");
        Game.FinishedTutorial();
    }
    
    public void Start()
    {
        if (Game.IsDoingTutorial())
        {
            SetFrame(1);
            messagePanel.SetActive(true);
        } 
    }

    public void Update()
    {
        if (currentFrame && currentFrame.IsComplete()) GoToNextFrame();
        
    }
}
