using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour {

    float angle;
    Camera cam;
    Transform trans;

    public GameObject ball;
    readonly float ballSpawnSpacing = 0.2f; // space between player and ball

    float ballRadius;
    float playerRadius;

	void Start () {
        cam = Camera.main;
        trans = GetComponent<Transform>();

        ballRadius = ball.GetComponent<Transform>().lossyScale.x / 2;
        playerRadius = transform.lossyScale.x / 2;
	}
	
	void Update () {
        if (Input.GetButtonDown("Fire") && !Game.IsPaused() && !Game.IsMode(GameMode.Dodge)) {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector2 difference = new Vector2(mousePos.x, mousePos.y) - new Vector2(trans.position.x, trans.position.y);
            float sign = (trans.position.x > mousePos.x) ? -1.0f : 1.0f;
            angle = Vector2.Angle(Vector2.up, difference) * sign * Mathf.Deg2Rad; // angle of ball to be shot in radians
            Vector3 direction = new Vector3(Mathf.Sin(angle), Mathf.Cos(angle));

            float adjustedAngle = Mathf.Abs(angle) % (90 * Mathf.Deg2Rad); // adjust because we only want a value between 0 and 45 degrees
            if (adjustedAngle > (45 * Mathf.Deg2Rad)) adjustedAngle = (90 * Mathf.Deg2Rad) - adjustedAngle;
            float distance = ballRadius + (playerRadius / Mathf.Cos(adjustedAngle)) + ballSpawnSpacing; // (ball radius) + (formula to calculate distance to edge of square) + (ballSpawnSpacing)

            Vector3 ballPos = trans.position + (direction * distance);
            GameObject spawned = Instantiate(ball, ballPos, Quaternion.identity);
            spawned.GetComponent<BallMovement>().direction = direction;
            spawned.GetComponent<BallMovement>().tag = "Mouse Ball";
            
        }
	}
}
