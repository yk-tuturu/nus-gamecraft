using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkDoneState : MilkBaseState
{
    public override void EnterState(MilkStateManager milk) {
        
    }

    public override void UpdateState(MilkStateManager milk) {
        
    }

    public override void ExitState(MilkStateManager milk) {
        
    }

    public override void Interact(MilkStateManager milk) {
        InventoryManager.instance.AddIngredient(milk.currentDrink, milk.currentScore);

        milk.bar.gameObject.SetActive(false);
        milk.bubble.SetActive(false);

        milk.ChangeState(milk.IdleState);
    }
}
