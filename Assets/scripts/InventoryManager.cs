using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<InventoryIcon> inventory = new List<InventoryIcon>();
    public GameObject iconPrefab;
    public Transform inventoryPanel;
    // Start is called before the first frame update
    void Awake() {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddIngredient(int index, float score) {
        GameObject iconObject = Instantiate(iconPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        InventoryIcon icon = iconObject.GetComponent<InventoryIcon>();
        icon.index = index;
        icon.score = score;
        iconObject.transform.SetParent(inventoryPanel);

        inventory.Add(icon);
    }
}
