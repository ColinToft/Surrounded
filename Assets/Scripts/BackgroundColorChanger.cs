using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColorChanger : MonoBehaviour {
    
	float cycleTime = Game.ColourCycleTime;

    Camera cam;

	void Start () {
		cam = Camera.main;
	}

	// Update is called once per frame
	void Update () {
		float dt = Time.deltaTime;
		cam.backgroundColor = getBackgroundColor();
	}

	Color getBackgroundColor() {
		float colour = (Time.time / cycleTime) % 1 * 6;
		float decimals = colour % 1;
		switch ((int)Mathf.Floor(colour)) {
		case 0: return new Color(1, decimals, 0);
		case 1: return new Color(1 - decimals, 1, 0);
		case 2:	return new Color(0, 1, decimals);
		case 3:	return new Color(0, 1 - decimals, 1);
		case 4:	return new Color(decimals, 0, 1);
		case 5:	return new Color(1, 0, 1 - decimals);
		default:
			Debug.Log("Value other than 0-5 in background colour.");
			return new Color(0, 0, 0);
		}
	}
}
