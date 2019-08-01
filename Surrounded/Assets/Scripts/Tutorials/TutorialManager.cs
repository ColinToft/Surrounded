using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{

    private static readonly Lazy<TutorialManager> _instance = new Lazy<TutorialManager>(InitializeTutorialManager);

    public static TutorialManager Instance
    {
        get { return _instance.Value; }
    }

    private static TutorialManager InitializeTutorialManager()
    {
        TutorialManager tm = GameObject.FindObjectOfType<TutorialManager>();
        if (tm == null) Debug.Log("Unable to find TutorialManager");
        else tm.SetTutorial(0);

        return tm;
    }

    public List<Tutorial> tutorials = new List<Tutorial>();

    public TMP_Text messageText;

    private Tutorial currentTutorial;

    public static void AddTutorial(Tutorial tutorial)
    {
        Instance.tutorials.Add(tutorial);
    }

    public Tutorial GetTutorialByOrder(int order)
    {
        for (int i = 0; i < tutorials.Count; i++)
        {
            if (tutorials[i].order == order) return tutorials[i];
        }

        return null;
    }

    public void SetTutorial(int order)
    {
        currentTutorial = GetTutorialByOrder(order);

        if (!currentTutorial)
        {
            CompletedAllTutorials();
            return;
        }

        messageText.text = currentTutorial.message;
    }

    public void CompletedTutorial()
    {
        SetTutorial(currentTutorial.order + 1);
    }

    public void CompletedAllTutorials()
    {
        messageText.text = "Congratulations, you have completed the tutorial!";
    }

    public void Update()
    {
        if (currentTutorial)
        {
            if (currentTutorial.IsComplete()) Instance.CompletedTutorial();
        }
    }
}
