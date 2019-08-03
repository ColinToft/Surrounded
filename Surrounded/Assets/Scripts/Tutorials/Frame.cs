using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Frame : MonoBehaviour
{
    public int order;

    [TextAreaAttribute(3, 10)]
    public string message;

    void Awake()
    {
        GameObject.FindObjectOfType<TutorialManager>().AddFrame(this);
    }

    public abstract bool IsComplete();

  
}
