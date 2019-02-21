using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovement : MonoBehaviour {

	public float movementSpeed;
    public KeepScore score;
    Transform trans;
    Camera cam;

	void Start () {
		trans = GetComponent<Transform>();
        cam = Camera.main;
	}
	
	void FixedUpdate () {
        if (Game.IsMode(GameMode.Frozen)) return;
        
		if (Input.GetAxis("Vertical") != 0f && Input.GetAxis("Horizontal") != 0f) {
			trans.position += new Vector3(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime * Mathf.Sqrt(0.5f), 0, 0);
			trans.position += new Vector3(0, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime * Mathf.Sqrt(0.5f), 0);
		} else if (Input.GetAxis("Horizontal") != 0f) {
			trans.position += new Vector3(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, 0, 0);
		} else if (Input.GetAxis("Vertical") != 0f) {
			trans.position += new Vector3(0, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime, 0);
		}

        // Keep square in bounds
        float halfSquareWidth = trans.localScale.x / 2; // Half of square width in world coords

        float halfHeight = cam.orthographicSize; // half of world height (from center to top of screen)
        float halfWidth = halfHeight * ((float)Screen.width / (float)Screen.height); // half of world width (from center to left of screen)

        float leftBound = -halfWidth + halfSquareWidth;
        float rightBound = halfWidth - halfSquareWidth;
        float topBound = halfHeight - halfSquareWidth;
        float bottomBound = -halfHeight + halfSquareWidth;

        if (trans.position.x < leftBound) trans.position += new Vector3(leftBound - trans.position.x, 0, 0);
        else if (trans.position.x > rightBound) trans.position -= new Vector3(trans.position.x - rightBound, 0, 0);

        if (trans.position.y < bottomBound) trans.position += new Vector3(0, bottomBound - trans.position.y, 0);
        else if (trans.position.y > topBound) trans.position -= new Vector3(0, trans.position.y - topBound, 0);

	}

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag.EndsWith("Ball")) {
            score.Stop();
            Game.Instance.CheckAndSetScore(score.score);
            Game.LoadGameOver();
        }
    }
}
