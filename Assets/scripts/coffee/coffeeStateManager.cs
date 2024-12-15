using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeStateManager : MonoBehaviour, Interactable
{
    CoffeeBaseState currentState;

    public CoffeeIdleState IdleState = new CoffeeIdleState();

    public GameObject rhythmEvent;
    
    void Start()
    {
        currentState = IdleState; 
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void Interact() {
        currentState.Interact(this);
    }

    public void ChangeState(CoffeeBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public CoffeeBaseState checkCurrentState()
    {
        return currentState;
    }

}
