using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorChanger : MonoBehaviour {

	float cycleTime = Game.ColourCycleTime;
    SpriteRenderer rend;

	public int colourOffset; // from 1 to 6
	// Use this for initialization
	void Start () {
		rend = GetComponent<SpriteRenderer>();
        Update();
	}
	
	// Update is called once per frame
	void Update () {
		rend.color = getSpriteColor();
	}

	Color getSpriteColor() {
		float colour = (Time.time / cycleTime) % 1 * 6;
		colour = (colour + colourOffset) % 6; // adjust the colour using the colour offset variable
		float decimals = colour % 1;
		switch ((int)Mathf.Floor(colour)) {
		case 0: return new Color(1, decimals, 0);
		case 1: return new Color(1 - decimals, 1, 0);
		case 2:	return new Color(0, 1, decimals);
		case 3:	return new Color(0, 1 - decimals, 1);
		case 4:	return new Color(decimals, 0, 1);
		case 5:	return new Color(1, 0, 1 - decimals);
		default:
			Debug.Log("Value other than 0-5 in material colour.");
			return new Color(0, 0, 0);
		}
	}
}
