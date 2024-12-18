using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilkOccupiedState : MilkBaseState
{
    public float timeElapsed = 0f;
    public float loadingTime = 3.5f;
    public bool freeze = false;

    public override void EnterState(MilkStateManager milk) {
        timeElapsed = 0f;
        milk.bar.gameObject.SetActive(true);
        milk.bar.SetFill(0f);

        milk.bubble.SetActive(true);
        milk.bubble.GetComponent<Bubble>().SetSprite(milk.currentDrink);

        LevelManager.instance.freezePatience.AddListener(()=>{freeze=true;});
        LevelManager.instance.unfreezePatience.AddListener(()=>{freeze=false;});
    }

    public override void UpdateState(MilkStateManager milk) {
        if (!freeze) {
            timeElapsed += milk.deltaTime;
            float position = Mathf.Min((float)timeElapsed / loadingTime, 1f);
            milk.bar.SetFill(position);

            if (position >= 1f) {
                milk.ChangeState(milk.DoneState);
            }
        }
    }

    public override void ExitState(MilkStateManager milk) {
        milk.bubble.GetComponent<Bubble>().PauseAnim();
        freeze = false;
    }

    public override void Interact(MilkStateManager milk) {

    }
}
