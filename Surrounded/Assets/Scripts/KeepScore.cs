using UnityEngine;
using TMPro;

public class KeepScore : MonoBehaviour {
    
    public float score;
    public TMP_Text scoreText;
    public Transform player;

    public float scoreForDistance = 50f; // Amount of points per second per one unit of distance 
    public float clusteredScoreForDistance = 10f; // Amount of points per second per one unit of distance when a ball is in a cluster (clustered game mode)
    bool counting;

	void Start () {
        score = 0;
        counting = true;
	}
	
	void Update () {
        if (!counting) return;
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls) score += Vector3.Distance(player.position, ball.transform.position) * Time.deltaTime * (Game.IsMode(GameMode.Cluster) && ball.GetComponent<BallMovement>().clustered ? clusteredScoreForDistance : scoreForDistance);
        scoreText.text = score.ToString("0");
	}

    public void Stop() {
        counting = false;
    }
}
