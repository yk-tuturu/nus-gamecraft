using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MilkBaseState
{
    public abstract void EnterState(MilkStateManager milk);

    public abstract void UpdateState(MilkStateManager milk);

    public abstract void ExitState(MilkStateManager milk);

    public abstract void Interact(MilkStateManager milk);
}
