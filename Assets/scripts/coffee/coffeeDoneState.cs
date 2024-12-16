using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeDoneState : CoffeeBaseState
{
    public override void EnterState(CoffeeStateManager coffee) {
        
    }

    public override void UpdateState(CoffeeStateManager coffee) {
        
    }

    public override void ExitState(CoffeeStateManager coffee) {
        
    }

    public override void Interact(CoffeeStateManager coffee) {
        Debug.Log("obtained drink");
        Debug.Log(coffee.currentDrink);
        coffee.currentDrink = 0;

        coffee.bar.gameObject.SetActive(false);
        coffee.bubble.SetActive(false);
        coffee.ChangeState(coffee.IdleState);
    }
}
