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
    public GameObject skipTutorialButton;

    enum PanelType { Corner, Popup, None };

    public TutorialMode tutorialMode;

    private Frame currentFrame;
    private PanelType currentPanel = PanelType.None;

    private String GetTextForGameMode(GameMode mode)
    {
        switch (mode)
        {
            case GameMode.Frozen:
                return "In Frozen mode, you are frozen in place and unable to move! Shoot oncoming balls to survive as long as possible!";
            case GameMode.Cluster:
                return "In Cluster mode, when balls collide they freeze in place to form a cluster, and give you less points than normal balls. When you hit one ball in a cluster, the entire cluster disappears.";
            case GameMode.Easy:
                return "In Easy mode, balls simply disappear off the edge of the screen instead of bouncing off the walls.";
            case GameMode.Hard:
                return "In Hard mode, balls do not disappear when they collide! Instead, they bounce off of each other, meaning you have to destroy all the balls yourself.";
            case GameMode.TwoHit:
                return "In Two-hit mode, balls need to be hit twice to disappear (balls that you shoot still only need one hit). A ball will appear cracked after it has been hit once.";
            case GameMode.Teleport:
                return "In Teleport mode, balls don't bounce off the walls. Instead, they will disappear off one side of the screen and reappear on the opposite side.";
            case GameMode.Dodge:
                return "In Dodge mode, you are unable to shoot! Dodge oncoming balls to last as long as you can.";
            case GameMode.Invisible:
                return "In Invisible mode, most of the screen is invisible, and you can only see balls that are close to you.";
            default:
                return "The tutorial for this game mode is coming soon!";
        }
    }

    public void AddFrame(Frame frame)
    {
        if (!frames.Contains(frame)) frames.Add(frame);
    }

    private Frame GetFrameByOrder(int order)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            if (frames[i].order == order) return frames[i];
        }

        Debug.Log("Unable to find frame, returning null.");
        return null;
    }

    private void SetFrame(int order)
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

        }
        catch (NullReferenceException)
        {
            CompletedAllFrames();
        }

    }

    private void GoToNextFrame()
    {
        SetFrame(currentFrame.order + 1);
    }

    private void CompletedAllFrames()
    {
        SetActivePanel(PanelType.None);
    }

    private bool IsLastFrame()
    {
        return GetFrameByOrder(currentFrame.order + 1) == null;
    }

    private void SetActivePanel(PanelType type)
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
                if (IsLastFrame()) skipTutorialButton.SetActive(false);
                LayoutRebuilder.ForceRebuildLayoutImmediate(popupPanel.GetComponent<RectTransform>());
                Game.Pause();
                break;

            case PanelType.None:
                cornerPanel.SetActive(false);
                popupPanel.SetActive(false);
                Game.Unpause();
                break;
        }
    }

    void Start()
    {
        if (Game.IsDoingTutorial())
        {
            if (Game.IsMode(GameMode.Classic)) SetFrame(1);
            else ShowGameModePopup();
        }

    }

    void Update()
    {
        if (currentFrame && currentFrame.IsComplete() && !popupPanel.activeSelf) GoToNextFrame();
    }

    private void ShowGameModePopup()
    {
        popupText.SetText(GetTextForGameMode(Game.Instance.gameMode));
        skipTutorialButton.SetActive(false);
        Game.Pause();
        SetActivePanel(PanelType.Popup);
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

        if (!Game.IsMode(GameMode.Classic))
        {
            CompletedAllFrames();
            Game.FinishedTutorial();
            Game.Unpause();
        }
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
        }
        catch (NullReferenceException)
        {
            return true;
        }
    }

    /// <summary>
    /// Makes sure that a ball should be shot according to the current tutorial frame. If the tutorial is not in progress, this will always be true.
    /// </summary>
    public static bool ShouldShootBall()
    {
        if (!Game.IsDoingTutorial()) return true;
        try
        {
            return GameObject.FindObjectOfType<TutorialManager>().currentFrame.ShouldShootBall();
        }
        catch (NullReferenceException)
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
