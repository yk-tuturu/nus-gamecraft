using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CoffeeBaseState
{
    public abstract void EnterState(CoffeeStateManager coffee);

    public abstract void UpdateState(CoffeeStateManager coffee);

    public abstract void ExitState(CoffeeStateManager coffee);

    public abstract void Interact(CoffeeStateManager coffee);
}
