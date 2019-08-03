using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour {

	public GameObject ball;
    public Transform playerTrans;
    Camera cam;

    /// <summary>How many seconds apart the balls will spawn at the end of the game.</summary>
    public float maxSpawnRate = 0.3f;

    bool atMaxSpawnRate = false;

    // Values used to calculate time until next ball will spawn, in the GetNextBallTime method.
    float a = 0.4406427f; float b = -0.3032243f; float c = 3.0f; // This means a spawn time of 3s at time 0.0, 2s at 15.0 and 0.5 at 120.0

    float timeOffset; // Equal to Time.Time minus the time since the round was started

    /** The minimum distance spawned balls should be from the player. Min is sqrt(0.5) + 0.5 = 1.207, distance from center of circle to center of square, touching corners. */
    public float minDistanceFromPlayer = 1.5f;

    public float minDistanceFromBalls = 1.25f;

    /** The time at which the next ball will spawn. */
    float nextBallTime;


	// Use this for initialization
	void Start () {
        timeOffset = Time.time; // add Time.time for when the game restarts, so there aren't a whole bunch of balls
        nextBallTime = GetNextBallTime();
        cam = Camera.main;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Spawn a ball if it is the right time
        if (Time.time >= nextBallTime && TutorialManager.ShouldSpawnBall()) {
            float ballRadius = ball.GetComponent<Transform>().localScale.x / 2; // Ball radius in world coords
            ballRadius = cam.WorldToScreenPoint(new Vector3(ballRadius, 0, 0)).x - cam.WorldToScreenPoint(new Vector3(0, 0, 0)).x; // Ball radius in screen coords

            Vector3 randomPos;
            float closestBallDistance;
            do {
                randomPos = cam.ScreenToWorldPoint(new Vector3(Random.Range(ballRadius, Screen.width - ballRadius), Random.Range(ballRadius, Screen.height - ballRadius)));
                randomPos.z = 0;
                closestBallDistance = 9999f;
                foreach (GameObject obj in FindObjectsOfType<GameObject>()) if (obj.tag.EndsWith("Ball")) closestBallDistance = Mathf.Min(closestBallDistance, Vector3.Distance(obj.GetComponent<Transform>().position, randomPos));
            } while (Vector3.Distance(playerTrans.position, randomPos) < minDistanceFromPlayer || closestBallDistance < minDistanceFromBalls);
                
			GameObject spawned = Instantiate(ball, randomPos, Quaternion.identity);
            float radians = Random.Range(-Mathf.PI, Mathf.PI);
            Vector3 direction = new Vector3(Mathf.Sin(radians), Mathf.Cos(radians));
            spawned.GetComponent<BallMovement>().BeginMovement(direction);
            if (Game.IsMode(GameMode.TwoHit)) spawned.GetComponent<BallMovement>().lives = 2;

            // Calculate the time that the next ball will be spawned
            nextBallTime = GetNextBallTime();
		}

	}

    float GetNextBallTime() {
        float function = Mathf.Pow(Time.time - timeOffset, a) * b + c;
        if (atMaxSpawnRate || function < maxSpawnRate) {
            atMaxSpawnRate = true;
            return Time.time + maxSpawnRate;
        }
        return Time.time + function; // subtract timeOffset to get time sinadd timeOffset again to return to the actual time
    }

}
