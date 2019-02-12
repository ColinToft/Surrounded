using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class TextColorChanger : MonoBehaviour {

    float cycleTime = Game.ColourCycleTime;

    public int colourOffset = 3; // from 1 to 6

    TMP_ColorGradient gradient;

    // Update is called once per frame
    void Update() {
        if (gradient == null && !FindGradient()) return;

        float dt = Time.deltaTime;
        gradient.topLeft = gradient.topRight = getTextColor();
        gradient.bottomLeft = gradient.bottomRight = (gradient.topLeft * 0.5f);
        gradient.bottomLeft.a = gradient.bottomRight.a = 1f; // Alpha also gets changed, so fix it
        foreach (TMP_Text t in FindObjectsOfType<TMP_Text>()) t.ForceMeshUpdate();
    }

    Color getTextColor() {
        float colour = (Time.time / cycleTime) % 1 * 6;
        colour = (colour + colourOffset) % 6; // adjust the colour using the colour offset variable
        float decimals = colour % 1;
        switch ((int)Mathf.Floor(colour))
        {
            case 0: return new Color(1, decimals, 0);
            case 1: return new Color(1 - decimals, 1, 0);
            case 2: return new Color(0, 1, decimals);
            case 3: return new Color(0, 1 - decimals, 1);
            case 4: return new Color(decimals, 0, 1);
            case 5: return new Color(1, 0, 1 - decimals);
            default:
                Debug.Log("Value other than 0-5 in background colour.");
                return new Color(0, 0, 0);
        }
    }

    // if gradient is null the catch statement might not work???
    bool FindGradient() {
        foreach (TMP_Text t in FindObjectsOfType<TMP_Text>()) {
            try {
                gradient = t.colorGradientPreset;
                if (gradient != null) return true;
            }
            catch (NullReferenceException e) {
                Debug.Log(t.text);
            }
        }
        return false;
    }
}
