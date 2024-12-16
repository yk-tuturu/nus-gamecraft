using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkOccupiedState : MilkBaseState
{
    public float startTime; 
    public float targetTime; 
    public float loadingTime = 3.5f;

    public override void EnterState(MilkStateManager milk) {
        startTime = Time.time; 
        targetTime = startTime + loadingTime;
        milk.bar.gameObject.SetActive(true);
        milk.bar.SetFill(0f);

        milk.bubble.SetActive(true);
        milk.bubble.GetComponent<Bubble>().SetSprite(milk.currentDrink);
    }

    public override void UpdateState(MilkStateManager milk) {
        float position = Mathf.Min((float)(Time.time - startTime) / loadingTime, 1f);
        milk.bar.SetFill(position);

        if (position >= 1f) {
            milk.ChangeState(milk.DoneState);
        }
    }

    public override void ExitState(MilkStateManager milk) {
        milk.bubble.GetComponent<Bubble>().PauseAnim();
    }

    public override void Interact(MilkStateManager milk) {

    }
}
