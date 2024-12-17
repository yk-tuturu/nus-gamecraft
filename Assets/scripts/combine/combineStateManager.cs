using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombineStateManager : MonoBehaviour, Interactable
{
    CombineBaseState currentState;

    public CombineIdleState IdleState = new CombineIdleState();
    public CombineMenuState MenuState = new CombineMenuState();
    public CombineRhythmState RhythmState = new CombineRhythmState();
    public CombineDoneState DoneState = new CombineDoneState();
    
    public GameObject bubble;
    public GameObject menuPanel;
    public GameObject rhythmEvent;

    public UnityEvent freezePlayer;
    public UnityEvent unfreezePlayer;

    public int currentDrink = 0;
    public float caffeineScore = 0;
    public float rhythmScore = 0; 
    public float finalScore = 0;
    
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

    public void ChangeState(CombineBaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public CombineBaseState checkCurrentState()
    {
        return currentState;
    }

}
