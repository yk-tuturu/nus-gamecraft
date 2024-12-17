using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineDoneState : CombineBaseState
{
    public override void EnterState(CombineStateManager combine) {
        combine.bubble.SetActive(true);
        combine.bubble.GetComponent<Bubble>().SetSprite(combine.currentDrink);
    }

    public override void UpdateState(CombineStateManager combine) {
        
    }

    public override void ExitState(CombineStateManager combine) {
        
    }

    public override void Interact(CombineStateManager combine) {
        if (InventoryManager.instance.tray.Count >= 2) {
            return;
        }
        
        InventoryManager.instance.AddDrinkToTray(combine.currentDrink, combine.finalScore);
        combine.currentDrink = 0;
        combine.caffeineScore = 0;
        combine.rhythmScore = 0;
        combine.finalScore = 0;

        combine.bubble.SetActive(false); 
        combine.ChangeState(combine.IdleState);
    }
}
