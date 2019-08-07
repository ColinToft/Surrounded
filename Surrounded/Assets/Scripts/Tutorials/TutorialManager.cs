using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    private static TutorialManager _instance;

    public List<Frame> frames = new List<Frame>();

    public TMP_Text cornerText;
    public GameObject cornerPanel;

    public TMP_Text popupText;
    public GameObject popupPanel;
    public RectTransform popupButtonGroup;

    enum PanelType { Corner, Popup, None };

    public TutorialMode tutorialMode;

    private Frame currentFrame;
    private PanelType currentPanel = PanelType.None;

    public void AddFrame(Frame frame)
    {
        if (!frames.Contains(frame)) frames.Add(frame);
    }

    public Frame GetFrameByOrder(int order)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            if (frames[i].order == order) return frames[i];
        }

        Debug.Log("Unable to find frame, returning null.");
        return null;
    }

    public void SetFrame(int order)
    {
        currentFrame = GetFrameByOrder(order);

        try
        {
            if (tutorialMode != TutorialMode.Popups)
            {
                cornerText.SetText(currentFrame.message);
            }
            if (tutorialMode != TutorialMode.Corner) popupText.SetText(currentFrame.message);

            currentFrame.StartFrame();

            if (tutorialMode == TutorialMode.Corner) SetActivePanel(PanelType.Corner);
            else SetActivePanel(PanelType.Popup);

        } catch (NullReferenceException) {
            CompletedAllFrames();
        }
        
    }

    public void GoToNextFrame()
    {
        SetFrame(currentFrame.order + 1);
    }

    public void CompletedAllFrames()
    {
        SetActivePanel(PanelType.None);
    }

    void SetActivePanel(PanelType type)
    {
        currentPanel = type;

        switch (type)
        {
            case PanelType.Corner:
                cornerPanel.SetActive(true);
                popupPanel.SetActive(false);
                LayoutRebuilder.ForceRebuildLayoutImmediate(cornerPanel.GetComponent<RectTransform>());
                Game.Unpause();
                break;

            case PanelType.Popup:
                cornerPanel.SetActive(false);
                popupPanel.SetActive(true);
                LayoutRebuilder.ForceRebuildLayoutImmediate(popupPanel.GetComponent<RectTransform>());
                LayoutRebuilder.ForceRebuildLayoutImmediate(popupButtonGroup);
                Game.Pause();
                break;

            case PanelType.None:
                cornerPanel.SetActive(false);
                popupPanel.SetActive(false);
                Game.Unpause();
                break;
        }
    }
    
    public void Start()
    {
        if (Game.IsDoingTutorial() && Game.IsMode(GameMode.Classic)) SetFrame(1);
    }

    public void Update()
    {
        if (currentFrame && currentFrame.IsComplete()) GoToNextFrame();
    }

    public void SkipTutorial()
    {
        Game.FinishedTutorial();
        currentFrame = null;
        SetActivePanel(PanelType.None);
    }

    /// <summary>
    /// Called when the user presses OK on a tutorial popup.
    /// </summary>
    public void PopupOK()
    {
        if (tutorialMode == TutorialMode.Both) SetActivePanel(PanelType.Corner);
        if (tutorialMode == TutorialMode.Popups) SetActivePanel(PanelType.None);
    }

    /// <summary>
    /// Makes sure that a ball should be spawned according to the current tutorial frame. If the tutorial is not in progress, this will always be true.
    /// </summary>
    public static bool ShouldSpawnBall()
    {
        if (!Game.IsDoingTutorial()) return true;
        try
        {
            return GameObject.FindObjectOfType<TutorialManager>().currentFrame.ShouldSpawnBall();
        } catch (NullReferenceException)
        {
            return true;
        }
    }

    public static bool CanUnpauseGame()
    {
        return GameObject.FindObjectOfType<TutorialManager>().currentPanel != PanelType.Popup;
    }

    public static void HidePopup()
    {
        GameObject.FindObjectOfType<TutorialManager>().popupPanel.SetActive(false);
    }

    public static void UnhidePopup()
    {
        TutorialManager tm = GameObject.FindObjectOfType<TutorialManager>();
        if (tm.currentPanel == PanelType.Popup) tm.popupPanel.SetActive(true);
    }
}

public enum TutorialMode { Popups, Corner, Both };
