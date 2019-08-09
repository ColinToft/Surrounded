using UnityEngine;
using System.Linq;

public class CircleMask : MonoBehaviour
{

    static Texture2D mask;

    public Transform playerTrans;

    static Vector3 prevBottomLeft, prevTopRight;
    Vector2 screenSize;

    readonly float zPosition = -2f;

    readonly float visibleRadius = 3f;

    private int TEXTURE_SCREEN_WIDTH = 1920;
    
    void Start()
    {
        gameObject.SetActive(Game.IsMode(GameMode.Invisible));
        Sprite circleSprite = gameObject.GetComponent<SpriteRenderer>().sprite;

        float oneWorldUnit = Vector2.Distance(Camera.main.WorldToScreenPoint(new Vector2(0, 0)), Camera.main.WorldToScreenPoint(new Vector2(1, 0)));

        Sprite sprite = Sprite.Create(circleSprite.texture,
              new Rect(0, 0, circleSprite.texture.width, circleSprite.texture.height),
              new Vector2(0.5f, 0.5f), oneWorldUnit);

        Update();
    }

    void Update()
    {
        if (!Game.IsMode(GameMode.Invisible)) return;

        GetComponent<Transform>().position = new Vector3(playerTrans.position.x, playerTrans.position.y, zPosition);

        // if (bottomLeft != prevBottomLeft || topRight != prevTopRight) CreateMask(bottomLeft, topRight);
        // Check for screen resizing
        // Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        // Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }
    
    /*
    void CreateMask()
    {
        Profiler.BeginSample("Create Mask");
        mask = new Texture2D(Screen.width * 2, Screen.height * 2);

        float oneWorldUnit = Vector2.Distance(Camera.main.WorldToScreenPoint(new Vector2(0, 0)), Camera.main.WorldToScreenPoint(new Vector2(1, 0)));
        float screenVisibleDistance = oneWorldUnit * visibleRadius;

        float distanceSquared = screenVisibleDistance * screenVisibleDistance;
        float centerX = mask.width * 0.5f;
        float centerY = mask.height * 0.5f;

        // First, fill the texture with black
        Color[] pixels = mask.GetPixels();
        for (int i = 0; i < pixels.Length; i++) pixels[i] = Color.black;

        // Then, set pixels close enough to the center to clear
        for (int x = (int)Mathf.Floor(centerX - screenVisibleDistance); x < centerX + screenVisibleDistance; x++)
        {
            for (int y = (int)Mathf.Floor(centerY - screenVisibleDistance); y < centerY + screenVisibleDistance; y++)
            {
                if ((x - centerX) * (x - centerX) + (y - centerY) * (y - centerY) <= distanceSquared) {
                    pixels[y * mask.width + x] = Color.clear;
                }
            }
        }

        mask.SetPixels(pixels);
        mask.Apply();
        
        Vector3 cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = Sprite.Create(mask, new Rect(0, 0, mask.width, mask.height), new Vector2(0.5f, 0.5f), oneWorldUnit);
        sr.material.mainTexture = mask;

        GetComponent<Transform>().position = new Vector3(cameraPos.x, cameraPos.y, zPosition);
        GetComponent<Transform>().localScale = new Vector3(1, 1, 0);

        prevBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
        prevTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        Profiler.EndSample();
    }
    */
}
