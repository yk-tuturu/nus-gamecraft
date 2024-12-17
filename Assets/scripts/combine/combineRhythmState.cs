using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineRhythmState : CombineBaseState
{
    rhythmEventManager currentRhythmEvent;
    bool rhythmEnded = false;

    public override void EnterState(CombineStateManager combine) {
        GameObject rhythm = Object.Instantiate(combine.rhythmEvent, new Vector3(0, 0, 0), Quaternion.identity);
        currentRhythmEvent = rhythm.GetComponent<rhythmEventManager>();
        currentRhythmEvent.rhythmEnd.AddListener(OnEnd);
        
        combine.freezePlayer.Invoke();
    }

    public override void UpdateState(CombineStateManager combine) {
        if (rhythmEnded) {
            combine.ChangeState(combine.DoneState);
        }
    }

    public override void ExitState(CombineStateManager combine) {
        combine.rhythmScore = ((float)currentRhythmEvent.score / (currentRhythmEvent.noteCount * 300)) * 100;
        combine.finalScore = (float)(combine.rhythmScore + combine.caffeineScore)/2;
        currentRhythmEvent.rhythmEnd.RemoveListener(OnEnd);
        rhythmEnded = false;
        combine.unfreezePlayer.Invoke();
    }

    public override void Interact(CombineStateManager combine) {
        
    }

    void OnEnd() {
        rhythmEnded = true;
    }
}
