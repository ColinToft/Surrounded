using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ColorChanger : MonoBehaviour
{
    /// <summary> How long it takes for color changer scripts to go through the whole rainbow. </summary>
    public static float ColorCycleTime = 60f;

    public float colorOffset;

    protected abstract void SetColor(Color color);

    protected void Start()
    {
        SetColor(GetColor());
    }

    protected void Update()
    {
        SetColor(GetColor());
    }

    private Color GetColor()
    {
        float color = (Time.time / ColorCycleTime) % 1 * 6;
        color = (color + colorOffset) % 6; // adjust the colour using the colour offset variable
        float decimals = color % 1;
        switch ((int)Mathf.Floor(color))
        {
            case 0: return new Color(1, decimals, 0);
            case 1: return new Color(1 - decimals, 1, 0);
            case 2: return new Color(0, 1, decimals);
            case 3: return new Color(0, 1 - decimals, 1);
            case 4: return new Color(decimals, 0, 1);
            case 5: return new Color(1, 0, 1 - decimals);
            default:
                Debug.Log("Value other than 0-5 in material colour.");
            return new Color(0, 0, 0);
        }
    }
}
