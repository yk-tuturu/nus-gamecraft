using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeOccupiedState : CoffeeBaseState
{
    public float timeElapsed = 0f;
    public float loadingTime = 2.5f;
    public bool freeze = false;

    public override void EnterState(CoffeeStateManager coffee) {
        timeElapsed = 0f;
        coffee.bar.gameObject.SetActive(true);
        coffee.bar.SetFill(0f);

        coffee.bubble.SetActive(true);
        coffee.bubble.GetComponent<Bubble>().SetSprite(coffee.currentDrink);

        LevelManager.instance.freezePatience.AddListener(()=>{freeze=true;});
        LevelManager.instance.unfreezePatience.AddListener(()=>{freeze=false;});
    }

    public override void UpdateState(CoffeeStateManager coffee) {
        if (!freeze) {
            timeElapsed += coffee.deltaTime;
            float position = Mathf.Min((float)timeElapsed / loadingTime, 1f);
            coffee.bar.SetFill(position);

            if (position >= 1f) {
                coffee.ChangeState(coffee.DoneState);
            }
        }
        
    }

    public override void ExitState(CoffeeStateManager coffee) {
        coffee.bubble.GetComponent<Bubble>().PauseAnim();
        freeze = false;
    }

    public override void Interact(CoffeeStateManager coffee) {

    }
}
