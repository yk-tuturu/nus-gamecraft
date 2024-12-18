using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<InventoryIcon> inventory = new List<InventoryIcon>();
    public List<InventoryIcon> tray = new List<InventoryIcon>();
    public GameObject iconPrefab;
    public GameObject deleteableIconPrefab;
    public Transform inventoryPanel;
    public Transform trayPanel;

    public int nextId = 0;
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
        icon.id = nextId;
        icon.index = index;
        icon.score = score;
        iconObject.transform.SetParent(inventoryPanel);

        nextId++;
        inventory.Add(icon);
    }

    public void AddDrinkToTray(int index, float score) {
        GameObject iconObject = Instantiate(deleteableIconPrefab, new Vector3(0, 0, 0), Quaternion.identity, trayPanel);
        InventoryIcon icon = iconObject.GetComponent<InventoryIcon>();
        icon.id = nextId;
        icon.index = index;
        icon.score = score;

        nextId++;
        tray.Add(icon);
    }

    public InventoryIcon GetIngredientById(int id) {
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].id == id) {
                return inventory[i];
            }
        }

        Debug.Log("couldn't find id");
        return null;
    }

    public int GetIngredientIndexById(int id) {
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].id == id) {
                return i;
            }
        }

        Debug.Log("couldn't find id");
        return -1;
    }

    public void ConsumeIngredient(int id) {
        for (int i = 0; i < inventory.Count; i++) {
            if (inventory[i].id == id) {
                GameObject temp = inventory[i].gameObject;
                inventory.RemoveAt(i);
                Destroy(temp);
                return;
            }
        }
    }

    public void ConsumeTray(int id) {
        for (int i = 0; i < tray.Count; i++) {
            if (tray[i].id == id) {
                GameObject temp = tray[i].gameObject;
                tray.RemoveAt(i);
                Destroy(temp);
                return;
            }
        }
    }

    public InventoryIcon GetDrinkById(int id) {
        for (int i = 0; i < tray.Count; i++) {
            if (tray[i].id == id) {
                return tray[i];
            }
        }
        Debug.Log("couldn't find id");
        return null;
    }
}
