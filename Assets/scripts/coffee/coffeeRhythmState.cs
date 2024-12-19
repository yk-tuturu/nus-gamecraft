using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoffeeRhythmState : CoffeeBaseState
{
    rhythmEventManager currentRhythmEvent;
    bool rhythmEnded = false;

    public override void EnterState(CoffeeStateManager coffee) {
        if (tutorialController.instance != null && tutorialController.instance.currentStep <= 6) {
            return;
        }

        GameObject rhythm = Object.Instantiate(coffee.rhythmEvent, new Vector3(0, 0, 0), Quaternion.identity);
        currentRhythmEvent = rhythm.GetComponent<rhythmEventManager>();
        currentRhythmEvent.rhythmEnd.AddListener(OnEnd);
        
        coffee.freezePlayer.Invoke();
    }

    public override void UpdateState(CoffeeStateManager coffee) {
        if (rhythmEnded) {
            coffee.ChangeState(coffee.OccupiedState);
        }
    }

    public override void ExitState(CoffeeStateManager coffee) {
        if (tutorialController.instance != null && tutorialController.instance.currentStep <= 7) {
            return;
        }

        coffee.currentScore = ((float)currentRhythmEvent.score / (currentRhythmEvent.noteCount * 300)) * 100;
        currentRhythmEvent.rhythmEnd.RemoveListener(OnEnd);
        rhythmEnded = false;
        coffee.unfreezePlayer.Invoke();
        LevelManager.instance.unfreezePatience.Invoke();
    }

    public override void Interact(CoffeeStateManager coffee) {
        
    }

    void OnEnd() {
        rhythmEnded = true;
    }
}
