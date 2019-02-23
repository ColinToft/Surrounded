using UnityEngine;
using UnityEngine.UI;

public class ImageColorChanger : ColorChanger {

    public Image image;
                                
    override protected void SetColor(Color color) {
        image.color = color;
    }
}
