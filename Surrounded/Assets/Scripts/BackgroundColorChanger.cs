using UnityEngine;

public class BackgroundColorChanger : ColorChanger {
    
    Camera cam;

	new protected void Start () {
		cam = Camera.main;
        colorOffset = 0;
        base.Start();
    }

    override protected void SetColor(Color color)
    {
        cam.backgroundColor = color;
    }
}