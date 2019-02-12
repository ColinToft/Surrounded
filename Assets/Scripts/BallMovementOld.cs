/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementOld : MonoBehaviour
{


    Transform trans;
    Camera cam;

    public float movementSpeed = 5f;

    public Vector3 direction;

    /// <summary>
    /// How many more times the ball needs to be hit before it dies.
    /// </summary>
    int lives = 1;

    void Start()
    {
        trans = GetComponent<Transform>();
        cam = Camera.main;

    }

    void FixedUpdate()
    {
        // Move the ball
        trans.position += direction * Time.fixedDeltaTime * movementSpeed;

        // Bouncing
        float ballRadius = trans.localScale.x / 2; // Ball radius in world coords

        float halfHeight = cam.orthographicSize; // half of world height (from center to top of screen)
        float halfWidth = halfHeight * ((float)Screen.width / (float)Screen.height); // half of world width (from center to left of screen)

        if (Game.isMode(GameMode.Easy))
        {
            // instead of bouncing, balls go off the screen on easy mode, this is logic to destroy balls off the screen
            if (trans.position.x < -halfWidth - ballRadius || trans.position.x > halfWidth + ballRadius || trans.position.y < -halfHeight - ballRadius || trans.position.y > halfHeight + ballRadius) Destroy(gameObject);

            return; // Return to avoid calculating the bouncing
        }

        float leftBound = -halfWidth + ballRadius;
        float rightBound = halfWidth - ballRadius;
        float topBound = halfHeight - ballRadius;
        float bottomBound = -halfHeight + ballRadius;

        if (trans.position.x < leftBound || trans.position.x > rightBound)
        {
            direction.x = -direction.x;
            if (trans.position.x < 0)
            {
                trans.position += new Vector3((leftBound - trans.position.x) * 2, 0, 0);
            }
            else
            {
                trans.position -= new Vector3((trans.position.x - rightBound) * 2, 0, 0);
            }
        }
        else if (trans.position.y < bottomBound || trans.position.y > topBound)
        {
            direction.y = -direction.y;
            if (trans.position.y < 0)
            {
                trans.position += new Vector3((bottomBound - trans.position.y) * 2, 0, 0);
            }
            else
            {
                trans.position -= new Vector3((trans.position.y - topBound) * 2, 0, 0);
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D coll)
    {
        if ((coll.gameObject.CompareTag("Ball") || coll.gameObject.CompareTag("Mouse Ball")) && !Game.isMode(GameMode.Hard))
        {
            Destroy(this.gameObject);
        }
    }

}
*/