using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class IntEvent : UnityEvent<int> {}

public class selectableInventoryItem : InventoryIcon
{
    public GameObject selectedOutline; 
    public bool selected = false;

    public IntEvent OnSelect = new IntEvent(); 
    public IntEvent OnDeselect = new IntEvent();
    
    public void OnClick() {
        if (!selected) {
            selectedOutline.SetActive(true);
            selected = true;
            OnSelect.Invoke(id);
        } else {
            selectedOutline.SetActive(false);
            selected = false;
            OnDeselect.Invoke(id);
        }
        
    }
}
