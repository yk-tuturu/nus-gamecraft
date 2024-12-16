using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuManager : MonoBehaviour
{
    public CoffeeStateManager coffee;
    uiTransitions ui;

    // coffee=1; matcha=2;hojicha=3
    public int selected = 0;

    public List<GameObject> selectedOutlines = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        ui = GetComponent<uiTransitions>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close() {
        if (selected != 0) {
            selectedOutlines[selected - 1].SetActive(false);
        }
        
        selected = 0;
        coffee.ChangeState(coffee.IdleState);
    }

    public void updateSelected(int index) {
        if (selected != 0) {
            selectedOutlines[selected - 1].SetActive(false);
        }
        selected = index;
        selectedOutlines[index -1].SetActive(true);
    }

    public void Go() {
        if (selected == 0) {
            return;
        }

        coffee.currentDrink = selected; 
        selectedOutlines[selected - 1].SetActive(false);
        selected = 0;
        coffee.ChangeState(coffee.RhythmState);
    }

    

    
}
