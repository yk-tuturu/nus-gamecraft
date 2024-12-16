using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMenuState : CoffeeBaseState
{
    public override void EnterState(CoffeeStateManager coffee) {
        Debug.Log("entered menu state");
        coffee.menuPanel.SetActive(true);
        coffee.menuPanel.GetComponent<uiTransitions>().UIExpand();
        coffee.freezePlayer.Invoke();
    }

    public override void UpdateState(CoffeeStateManager coffee) {
        
    }

    public override void ExitState(CoffeeStateManager coffee) {
        coffee.menuPanel.GetComponent<uiTransitions>().UIShrink(true);
        coffee.unfreezePlayer.Invoke();
    }

    public override void Interact(CoffeeStateManager coffee) {
        
    }
}
