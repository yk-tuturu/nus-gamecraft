using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeOccupiedState : CoffeeBaseState
{
    public float startTime; 
    public float targetTime; 
    public float loadingTime = 2.5f;

    public override void EnterState(CoffeeStateManager coffee) {
        startTime = Time.time; 
        targetTime = startTime + loadingTime;
        coffee.bar.gameObject.SetActive(true);
        coffee.bar.SetFill(0f);

        coffee.bubble.SetActive(true);
        coffee.bubble.GetComponent<Bubble>().SetSprite(coffee.currentDrink);
    }

    public override void UpdateState(CoffeeStateManager coffee) {
        float position = Mathf.Min((float)(Time.time - startTime) / loadingTime, 1f);
        coffee.bar.SetFill(position);

        if (position >= 1f) {
            coffee.ChangeState(coffee.DoneState);
        }
    }

    public override void ExitState(CoffeeStateManager coffee) {
        coffee.bubble.GetComponent<Bubble>().PauseAnim();
    }

    public override void Interact(CoffeeStateManager coffee) {

    }
}
