using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineIdleState : CombineBaseState
{
    public override void EnterState(CombineStateManager combine) {
        
    }

    public override void UpdateState(CombineStateManager combine) {
        
    }

    public override void ExitState(CombineStateManager combine) {
        
    }

    public override void Interact(CombineStateManager combine) {
        Debug.Log("interacting with combine station");
        
        //Object.Instantiate(coffee.rhythmEvent, new Vector3(0, 0, 0), Quaternion.identity);
        combine.ChangeState(combine.MenuState);
    }
}
