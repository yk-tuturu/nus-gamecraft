using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance; 
    public List<Sprite> ingredientList = new List<Sprite>();
    // Start is called before the first frame update
    void Awake() {
        instance = this;

        var ingredients = Resources.LoadAll("ingredients", typeof(Sprite)).Cast<Sprite>().ToArray();

        foreach (Sprite sprite in ingredients) {
            ingredientList.Add(sprite);
        }
    }

    public Sprite GetIngredientSprite(int index) {
        return ingredientList[index];
    }
}
