using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColorChanger : MonoBehaviour {

    float cycleTime = Game.ColourCycleTime;
    public Image image;

    public int colourOffset; // from 1 to 6
                            
    void Update() {
        image.color = getImageColor();
    }

    Color getImageColor() {
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
                Debug.Log("Value other than 0-5 in material colour.");
                return new Color(0, 0, 0);
        }
    }
}
