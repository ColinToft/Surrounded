using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditsScreen : MonoBehaviour {

    public float scrollSpeed = 0.2f;
    public ScrollRect scrollRect;

    public RectTransform scrollTrans;
    public RectTransform content;
    public TMP_Text credits;
    public RectTransform creditsTrans;
    //public RectTransform viewport;

    float viewportOffset;

    void Start () {
        scrollRect.verticalNormalizedPosition = 1f;
        float viewportHeight = scrollTrans.sizeDelta.y;
        viewportHeight = 300; //test
        content.sizeDelta = new Vector2(content.sizeDelta.x, creditsTrans.sizeDelta.y);

        float lineHeight = creditsTrans.sizeDelta.y / credits.textInfo.lineCount;
        float linesInView = viewportHeight / lineHeight;
        int secondStartLine = credits.textInfo.lineCount / 2 + 1;
        float secondStart = creditsTrans.sizeDelta.y - (lineHeight * secondStartLine); // in pixels from bottom
        viewportOffset = secondStart - viewportHeight; // distance from bottom of credits to bottom of viewport frame just before jumping IN SCREEN COORDS
        viewportOffset /= creditsTrans.sizeDelta.y - viewportHeight;
	}
	
	void Update () {
        //scrollRect.verticalNormalizedPosition += scrollSpeed * Time.deltaTime;
        scrollRect.verticalNormalizedPosition -= scrollSpeed * Time.deltaTime;
        if (scrollRect.verticalNormalizedPosition < viewportOffset) {
            scrollRect.verticalNormalizedPosition = 1f /*- (viewportOffset - scrollRect.verticalNormalizedPosition)*/;
        } 
	}
}
