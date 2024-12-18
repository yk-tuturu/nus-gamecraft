using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpriteManager : MonoBehaviour
{
    public static SpriteManager instance; 
    public List<Sprite> ingredientList = new List<Sprite>();
    public Dictionary<int, Sprite> drinksDict = new Dictionary<int, Sprite>();
    public List<Sprite> customerSprites = new List<Sprite>();
    // Start is called before the first frame update
    void Awake() {
        instance = this;

        var ingredients = Resources.LoadAll("ingredients", typeof(Sprite)).Cast<Sprite>().ToArray();
        var drinks = Resources.LoadAll("drinks", typeof(Sprite)).Cast<Sprite>().ToArray();
        var customers = Resources.LoadAll("customers", typeof(Sprite)).Cast<Sprite>().ToArray();

        foreach (Sprite sprite in ingredients) {
            ingredientList.Add(sprite);
        }

        foreach (Sprite sprite in customers) {
            customerSprites.Add(sprite);
        }

        foreach (Sprite sprite in drinks) {
            int index = int.Parse(sprite.name);
            drinksDict.Add(index, sprite);
        }        
    }

    void Start() {
        if (LevelLoader.instance.currentLevel == 1) {
            drinksDict.Remove(35);
            drinksDict.Remove(45);
        }
    }

    public Sprite GetIngredientSprite(int index) {
        return ingredientList[index];
    }

    public Sprite GetDrinkSprite(int index) {
        if (!drinksDict.ContainsKey(index)) {
            Debug.Log("error getting drink sprite");
            return null;
        } 
        return drinksDict[index];
    }

    public Sprite GetSprite(int index) {
        if (index > 10) {
            return GetDrinkSprite(index);
        } else {
            return GetIngredientSprite(index - 1);
        }
    }

    public int GetRandomDrink() {
        int index = Random.Range(0, drinksDict.Count);
        return drinksDict.ElementAt(index).Key;
    }

    public Sprite GetCustomerSprite() {
        int index = Random.Range(0, customerSprites.Count);
        return customerSprites[index];
    }
}
