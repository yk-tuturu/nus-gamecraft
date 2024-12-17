using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineMenuState : CombineBaseState
{
    public override void EnterState(CombineStateManager combine) {
        Debug.Log("entered menu state");
        combine.menuPanel.SetActive(true);
        combine.menuPanel.GetComponent<combineMenuManager>().OpenMenu();
        combine.freezePlayer.Invoke();
    }

    public override void UpdateState(CombineStateManager combine) {
        
    }

    public override void ExitState(CombineStateManager combine) {
        combine.menuPanel.GetComponent<uiTransitions>().UIShrink(true);
        combine.unfreezePlayer.Invoke();
    }

    public override void Interact(CombineStateManager combine) {
        
    }
}
