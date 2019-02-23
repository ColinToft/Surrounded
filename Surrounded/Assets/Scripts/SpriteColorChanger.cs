using UnityEngine;

public class SpriteColorChanger : ColorChanger {

    SpriteRenderer rend;
    
	new protected void Start () {
		rend = GetComponent<SpriteRenderer>();
        base.Start();
	}
	
	override protected void SetColor (Color color) {
		rend.color = color;
	}
}