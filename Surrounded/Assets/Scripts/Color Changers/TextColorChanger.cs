using System;
using UnityEngine;
using TMPro;

public class TextColorChanger : ColorChanger {

    TMP_ColorGradient gradient;

    new protected void Start()
    {
        colorOffset = 3;
        base.Start();
    }

    override protected void SetColor(Color color)
    {
        if (gradient == null && !FindGradient()) return;

        float dt = Time.deltaTime;
        gradient.topLeft = gradient.topRight = color;
        gradient.bottomLeft = gradient.bottomRight = (gradient.topLeft * 0.5f);
        gradient.bottomLeft.a = gradient.bottomRight.a = 1f; // Alpha also gets changed, so fix it
        foreach (TMP_Text t in FindObjectsOfType<TMP_Text>()) t.ForceMeshUpdate();
    }

    bool FindGradient() {
        foreach (TMP_Text t in FindObjectsOfType<TMP_Text>()) {
            try {
                gradient = t.colorGradientPreset;
                if (gradient != null) return true;
            }
            catch (NullReferenceException) {
                Debug.Log(t.text);
            }
        }
        return false;
    }
}
