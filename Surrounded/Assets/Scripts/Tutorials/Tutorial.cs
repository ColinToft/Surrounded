using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tutorial : MonoBehaviour
{

    public int order;

    public string message;

    void Awake()
    {
        TutorialManager.AddTutorial(this);
    }

    public abstract bool IsComplete();

  
}
