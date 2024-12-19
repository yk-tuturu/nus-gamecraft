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

    public GameObject[] locked;
    public GameObject chocolate;
    public GameObject hojicha;
    public GameObject matcha;
    
    // Start is called before the first frame update
    void Start()
    {
        ui = GetComponent<uiTransitions>();

        if (LevelLoader.instance.currentLevel == 1) {
            chocolate.SetActive(false);
            hojicha.SetActive(false);
            for (int i = 0; i < 2; i++) {
                locked[i].SetActive(true);
            }
        } else if (LevelLoader.instance.currentLevel == 0) {
            chocolate.SetActive(false);
            hojicha.SetActive(false);
            matcha.SetActive(false);
            for (int i = 0; i < 3; i++) {
                locked[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close() {
        if (LevelLoader.instance.currentLevel == 0 && tutorialController.instance.currentStep <= 5 ) {
            return;
        }

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
        if (LevelLoader.instance.currentLevel == 0 && tutorialController.instance.currentStep <= 4 ) {
            return;
        }

        if (selected == 0) {
            return;
        }

        coffee.currentDrink = selected; 
        selectedOutlines[selected - 1].SetActive(false);
        selected = 0;
        coffee.ChangeState(coffee.RhythmState);
    }

    

    
}
