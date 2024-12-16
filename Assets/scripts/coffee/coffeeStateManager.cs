using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoffeeStateManager : MonoBehaviour, Interactable
{
    CoffeeBaseState currentState;

    public CoffeeIdleState IdleState = new CoffeeIdleState();
    public CoffeeMenuState MenuState = new CoffeeMenuState();
    public CoffeeRhythmState RhythmState = new CoffeeRhythmState();
    public CoffeeOccupiedState OccupiedState = new CoffeeOccupiedState();
    public CoffeeDoneState DoneState = new CoffeeDoneState();

    public GameObject rhythmEvent;
    public GameObject menuPanel;
    public loadingBar bar;
    public GameObject bubble;
    public UnityEvent freezePlayer;
    public UnityEvent unfreezePlayer;

    public int currentDrink = 0;
    public int currentScore = 0;
    
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
