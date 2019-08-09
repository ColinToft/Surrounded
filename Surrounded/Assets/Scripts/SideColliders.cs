using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script adds side colliders for the balls to bounce off of during gameplay.
/// Based on Invertex's code found at: https://forum.unity.com/threads/collision-with-sides-of-screen.228865/
/// </summary>
public class SideColliders : MonoBehaviour {

    public float colThickness = 4f;
    public float zPosition = 0f;

    Vector2 screenSize;

    Dictionary<string, Transform> colliders;

    Vector3 prevBottomLeft, prevTopRight;

    public PhysicsMaterial2D ballPhysMat;

    void Start() {

        if (Game.IsMode(GameMode.Easy) || Game.IsMode(GameMode.Teleport)) return; // No colliders on easy or teleport mode, the balls are supposed to go off the screen.

        //Create a Dictionary to contain all colliders and their transform components
        colliders = new Dictionary<string, Transform>
        {
            { "Top", new GameObject().transform },
            { "Bottom", new GameObject().transform },
            { "Right", new GameObject().transform },
            { "Left", new GameObject().transform }
        };

        Update();
    }

    void Update()
    {
        if (Game.IsMode(GameMode.Easy) || Game.IsMode(GameMode.Teleport)) return; // No colliders on easy or teleport mode, the balls are supposed to go off the screen.

        // Check for screen resizing
        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        if (bottomLeft != prevBottomLeft || topRight != prevTopRight) UpdatePosition();

    }

    void UpdatePosition()
    {
        Vector3 cameraPos = Camera.main.transform.position;

        //Generate world space point information for position and scale calculations
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f; //Grab the world-space position values of the start and end positions of the screen, then calculate the distance between them and store it as half, since we only need half that value for distance away from the camera to the edge
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        //For each Transform/Object in our Dictionary
        foreach (KeyValuePair<string, Transform> valPair in colliders)
        {
            valPair.Value.gameObject.AddComponent<BoxCollider2D>(); //Add our colliders. Remove the "2D", if you would like 3D colliders.
            valPair.Value.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            valPair.Value.name = valPair.Key + "Collider"; //Set the object's name to it's "Key" name, and take on "Collider".  i.e: TopCollider
            valPair.Value.parent = transform; //Make the object a child of whatever object this script is on (preferably the camera)

            if (valPair.Key == "Left" || valPair.Key == "Right") //Scale the object to the width and height of the screen, using the world-space values calculated earlier
                valPair.Value.localScale = new Vector3(colThickness, screenSize.y * 2, colThickness);
            else
                valPair.Value.localScale = new Vector3(screenSize.x * 2, colThickness, colThickness);
        }

        //Change positions to align perfectly with outter-edge of screen, adding the world-space values of the screen we generated earlier, and adding/subtracting them with the current camera position, as well as add/subtracting half out objects size so it's not just half way off-screen
        colliders["Right"].position = new Vector3(cameraPos.x + screenSize.x + (colliders["Right"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Left"].position = new Vector3(cameraPos.x - screenSize.x - (colliders["Left"].localScale.x * 0.5f), cameraPos.y, zPosition);
        colliders["Top"].position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (colliders["Top"].localScale.y * 0.5f), zPosition);
        colliders["Bottom"].position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (colliders["Bottom"].localScale.y * 0.5f), zPosition);

        prevBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        prevTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }
}
