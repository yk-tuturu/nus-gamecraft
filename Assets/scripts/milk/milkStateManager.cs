using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MilkStateManager : MonoBehaviour, Interactable
{
    MilkBaseState currentState;

    public MilkIdleState IdleState = new MilkIdleState();
    public MilkOccupiedState OccupiedState = new MilkOccupiedState();
    public MilkDoneState DoneState = new MilkDoneState();

    public loadingBar bar;
    public GameObject bubble;

    public int currentDrink = 4;
    public float currentScore = 100f;

    public float deltaTime = 0f;
    
    void Start()
    {
        currentState = IdleState; 
        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
        deltaTime = Time.deltaTime;
    }

    public void Interact() {
        currentState.Interact(this);
    }

    public void ChangeState(MilkBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public MilkBaseState checkCurrentState()
    {
        return currentState;
    }

}
