using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombineBaseState
{
    public abstract void EnterState(CombineStateManager combine);

    public abstract void UpdateState(CombineStateManager combine);

    public abstract void ExitState(CombineStateManager combine);

    public abstract void Interact(CombineStateManager combine);
}
