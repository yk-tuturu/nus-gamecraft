using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkIdleState : MilkBaseState
{
    public override void EnterState(MilkStateManager milk) {
        
    }

    public override void UpdateState(MilkStateManager milk) {
        
    }

    public override void ExitState(MilkStateManager milk) {
        
    }

    public override void Interact(MilkStateManager milk) {
        if (tutorialController.instance != null && tutorialController.instance.currentStep <= 7) {
            return;
        }
        
        //Object.Instantiate(coffee.rhythmEvent, new Vector3(0, 0, 0), Quaternion.identity);
        milk.ChangeState(milk.OccupiedState);
    }
}
