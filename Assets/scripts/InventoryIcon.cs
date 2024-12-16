using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryIcon : MonoBehaviour
{
    public int index;
    public float score;
    
    public Image image;
    public Image bgImage;

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = SpriteManager.instance.GetIngredientSprite(index - 1);
        if (score > 80f) {
            bgImage.color = new Color(229f/255, 217f/255, 148f/255);
        } else if (score >= 50f) {
            bgImage.color = new Color(170f/255, 126f/255, 109f/255);
        } else {
            bgImage.color = new Color(58f/255, 58f/255, 69f/255);
        }
    }
}
