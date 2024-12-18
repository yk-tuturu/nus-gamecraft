using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class combineMenuManager : MonoBehaviour
{
    public GameObject selectableIconPrefab;
    public GameObject inventoryIconPrefab;
    public Transform inventoryPanel;
    public List<selectableInventoryItem> inventoryIcons = new List<selectableInventoryItem>();

    public List<selectableInventoryItem> selected = new List<selectableInventoryItem>();
    public Transform selectParent; 
    public Transform productParent;

    public uiTransitions ui;

    public int targetDrink = 0;
    public float caffeineScore = 0;

    public CombineStateManager combiner;

    void Awake() {
        ui = GetComponent<uiTransitions>();
    }

    
    // Start is called before the first frame update
    public void OpenMenu()
    {
        
        List<InventoryIcon> inventory = InventoryManager.instance.inventory;
        for (int i = 0; i < inventory.Count; i++) {
            GameObject icon = Instantiate(selectableIconPrefab, new Vector3(0, 0, 0), Quaternion.identity, inventoryPanel);

            selectableInventoryItem iconScript = icon.GetComponent<selectableInventoryItem>();
            iconScript.id = inventory[i].id;
            iconScript.index = inventory[i].index;
            iconScript.score = inventory[i].score;
            iconScript.OnSelect.AddListener(OnSelect);
            iconScript.OnDeselect.AddListener(OnDeselect);

            inventoryIcons.Add(iconScript);
        }

        ui.UIExpand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnSelect(int id) {
        int i = InventoryManager.instance.GetIngredientIndexById(id);

        if (selected.Count >= 3) {
            // forcefully deselect the item, since counter is full
            inventoryIcons[i].OnClick();
            return;
        }

        GameObject icon = Instantiate(selectableIconPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        icon.transform.SetParent(selectParent);

        selectableInventoryItem iconScript = icon.GetComponent<selectableInventoryItem>();
        iconScript.id = inventoryIcons[i].id;
        iconScript.index = inventoryIcons[i].index;
        iconScript.score = inventoryIcons[i].score;
        
        iconScript.OnSelect.AddListener(OnRemove);
        selected.Add(iconScript);

        CalculateDrink();
    }

    void OnDeselect(int id) {
        for (int i = 0; i < selected.Count; i++) {
            if (selected[i].id == id) {
                GameObject temp = selected[i].gameObject;
                selected.RemoveAt(i);
                Destroy(temp);
            }
        }

        CalculateDrink();
    }

    void OnRemove(int id) {
        for (int i = 0; i < selected.Count; i++) {
            if (selected[i].id == id) {
                GameObject temp = selected[i].gameObject;
                selected.RemoveAt(i);
                Destroy(temp);
            }
        }

        int j = InventoryManager.instance.GetIngredientIndexById(id);
        inventoryIcons[j].OnClick();

        CalculateDrink();
    }

    void OnDisable() {
        foreach(Transform child in productParent)
        {
            Destroy(child.gameObject);
        }

        foreach(Transform child in selectParent)
        {
            Destroy(child.gameObject);
        }

        foreach(Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        inventoryIcons = new List<selectableInventoryItem>();
        selected = new List<selectableInventoryItem>();
        targetDrink = 0;
    }

    public void Close() {
        combiner.ChangeState(combiner.IdleState);
    }

    public void CalculateDrink() {
        int caffeineID = 0;
        int milkID = 0; 
        bool invalid = false;
        

        foreach(Transform child in productParent)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < selected.Count; i++) {
            if (selected[i].index >= 1 && selected[i].index <= 4) {
                // this means we already have a caffeine in place, which makes an invalid combo
                if (caffeineID != 0) {
                    invalid = true;
                    break;
                }

                caffeineID = selected[i].index;
                caffeineScore = selected[i].score;
            
            } else if (selected[i].index == 5) {
                if (milkID != 0) {
                    invalid = true;
                    break;
                }

                milkID = selected[i].index;
            }
        }

        if (caffeineID == 0 || milkID == 0) {
            invalid = true;
        }

        if (!invalid) {
            GameObject icon = Instantiate(inventoryIconPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            icon.transform.SetParent(productParent);

            // this instance of the icon has the default id coz we are unlikely to need the id, and no score coz we cant determine it yet
            InventoryIcon iconScript = icon.GetComponent<InventoryIcon>();
            iconScript.id = 0;
            iconScript.index = caffeineID * 10 + milkID;
            iconScript.score = 70;

            targetDrink = caffeineID * 10 + milkID;
        } else {
            targetDrink = 0;
            caffeineScore = 0;
        }
    }

    public void Go() {
        if (targetDrink == 0) {
            return;
        }

        for (int i = 0; i < selected.Count; i++) {
            InventoryManager.instance.ConsumeIngredient(selected[i].id);
        }

        combiner.currentDrink = targetDrink;
        combiner.caffeineScore = caffeineScore;
        combiner.ChangeState(combiner.RhythmState);
    }

    
}
