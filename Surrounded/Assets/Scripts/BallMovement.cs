using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour {

    Transform trans;
    Camera cam;

    public float movementSpeed = 5f;

    public Vector3 direction;

    Rigidbody2D rb;

    public List<GameObject> cluster;

    public bool clustered;

    public Sprite cracked;

    /// <summary>
    /// How many more times the ball needs to be hit before it dies.
    /// </summary>
    public int lives = 1;
    
    private bool _offScreen = false; // Used in teleport mode, whether the ball is currently off the screen

    // For easy mode (in FixedUpdate method)
    float ballRadius;
    float halfHeight;
    float halfWidth;

	void Start () {
		trans = GetComponent<Transform>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(direction * movementSpeed * 50f); // For some reason 50 is the number to make the magnitude same as movement speed
        if (Game.IsMode(GameMode.Cluster) && gameObject.CompareTag("Ball")) {
            cluster = new List<GameObject>();
            clustered = false;
        }

        ballRadius = trans.localScale.x / 2; // Ball radius in world coords
        halfHeight = cam.orthographicSize; // half of world height (from center to top of screen)
        halfWidth = halfHeight * ((float)Screen.width / (float)Screen.height); // half of world width (from center to left of screen)
    }

    /// <summary>
    /// Destroy balls off the screen in Easy mode, teleports balls to the other side of the screen in Teleport mode.
    /// </summary> 
    void FixedUpdate()
    {
        if (Game.IsMode(GameMode.Easy)) {
            if (trans.position.x < -halfWidth - ballRadius || trans.position.x > halfWidth + ballRadius || trans.position.y < -halfHeight - ballRadius || trans.position.y > halfHeight + ballRadius) Damage();
        } else if (Game.IsMode(GameMode.Teleport)) {
            if ((trans.position.x < -halfWidth - ballRadius || trans.position.x > halfWidth + ballRadius)) {
                if (!_offScreen) {
                    trans.position = new Vector3(-trans.position.x, trans.position.y, trans.position.z);
                    _offScreen = true;
                }
            } else if ((trans.position.y < -halfHeight - ballRadius || trans.position.y > halfHeight + ballRadius)) {
                if (!_offScreen) {
                    trans.position = new Vector3(trans.position.x, -trans.position.y, trans.position.z);
                    _offScreen = true;
                }
            } else {
                _offScreen = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (gameObject.CompareTag("Ball")) {
            if (coll.gameObject.CompareTag("Ball")) { // This is a all colliding with Ball
                if (Game.IsMode(GameMode.Cluster)) {
                    if (!clustered) {
                        rb.velocity = new Vector2(0f, 0f);
                        rb.isKinematic = true; // stop this ball in the cluster
                        clustered = true;
                        cluster = new List<GameObject>(coll.gameObject.GetComponent<BallMovement>().cluster); // Make a copy
                        cluster.Remove(gameObject); // If this is the first two balls forming cluster, the other ball will have this one in its list so we get rid of it if it exists
                        cluster.Add(coll.gameObject); // add the collided ball to the list of balls in this cluster
                        foreach (GameObject ball in cluster.ToArray()) ball.gameObject.GetComponent<BallMovement>().AddToCluster(gameObject); // add this ball to the other balls' list of balls in the cluster (use ToArray to make a copy of the list, so it doesn't change when balls are destroyed)
                    }
                } else if (!Game.IsMode(GameMode.Hard)) Damage();
            } else if (coll.gameObject.CompareTag("Mouse Ball")) { // This is a ball colliding with a Mouse Ball
                if (Game.IsMode(GameMode.Cluster)) foreach (GameObject ball in cluster.ToArray()) if (ball != gameObject) ball.GetComponent<BallMovement>().Damage(); // Destroy all other balls in the cluster (use ToArray to make a copy of the list, so it doesn't change when balls are destroyed)
                Damage();
            }
        }

        else
        { // This is a mouse ball
            if (coll.gameObject.tag.EndsWith("Ball"))
            {
                Damage();
                return;
            }
        }

        Debug.Log("ball movement line 104");
        Debug.Log(movementSpeed);
        Debug.Log(rb.velocity.magnitude);
        if (rb.velocity.magnitude != movementSpeed && !clustered)
        {
            rb.velocity *= movementSpeed / rb.velocity.magnitude;
        }
    }

    public void AddToCluster(GameObject ball) { // In cluster mode, add a ball to the list of balls in the cluster
        if (ball != gameObject && !cluster.Contains(ball)) cluster.Add(ball);
    }

    /// <summary>
    /// Damage this ball, will destroy the ball unless the ball has more than one life (two-hit mode). In this case, the function will switch to the cracked texture.
    /// </summary>
    public void Damage()
    {
        if (--lives == 0) Destroy(this.gameObject);
        else GetComponent<SpriteRenderer>().sprite = cracked;
    }


}
