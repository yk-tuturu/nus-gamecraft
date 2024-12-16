using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeIdleState : CoffeeBaseState
{
    public override void EnterState(CoffeeStateManager coffee) {
        
    }

    public override void UpdateState(CoffeeStateManager coffee) {
        
    }

    public override void ExitState(CoffeeStateManager coffee) {
        
    }

    public override void Interact(CoffeeStateManager coffee) {
        Debug.Log("interacting in idle state!!");
        
        //Object.Instantiate(coffee.rhythmEvent, new Vector3(0, 0, 0), Quaternion.identity);
        coffee.ChangeState(coffee.MenuState);
    }
}
