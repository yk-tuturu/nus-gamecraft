using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryIcon : MonoBehaviour
{
    public int id; //a unique identifier for every inventory item created
    public int index;
    public float score; // out of 100
    
    public Image image;
    public Image bgImage;

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = SpriteManager.instance.GetSprite(index);
        if (score > 80f) {
            bgImage.color = new Color(229f/255, 217f/255, 148f/255);
        } else if (score >= 40f) {
            bgImage.color = new Color(170f/255, 126f/255, 109f/255);
        } else {
            bgImage.color = new Color(58f/255, 58f/255, 69f/255);
        }
    }
    
}
