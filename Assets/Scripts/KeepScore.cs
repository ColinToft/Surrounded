using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeepScore : MonoBehaviour {
    
    public float score;
    public Text scoreText;
    public Transform player;

    public float scoreForDistance = 50f; // Amount of points per second per one unit of distance 
    float clusteredScoreForDistance = 10f; // Amount of points per second per one unit of distance when a ball is in a cluster (clustered game mode)
    bool counting;

	// Use this for initialization
	void Start () {
        score = 0;
        counting = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!counting) return;
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in balls) score += Vector3.Distance(player.position, ball.transform.position) * (Game.isMode(GameMode.Cluster) && ball.GetComponent<BallMovement>().clustered ? clusteredScoreForDistance : scoreForDistance) * Time.deltaTime;
        scoreText.text = score.ToString("0");
	}

    public void Stop() { counting = false; }
}
