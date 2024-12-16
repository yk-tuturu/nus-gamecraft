using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using DG.Tweening;

public class rhythmEventManager : MonoBehaviour
{
    public GameObject hitCircle; 
    public Camera mainCamera;
    public LayerMask hitCircleLayer;
    public SpriteRenderer overlay;

    public int score = 0;
    float approachRate = 0.8f;
    float endTime;

    List<Vector3> pattern = new List<Vector3>();
    List<float> beats = new List<float>();

    bool started = false;
    public UnityEvent rhythmEnd;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; 

        overlay.DOFade((float)188/255, 0.5f).OnComplete(()=> {
            Begin();
        });
    }

    void Begin() {
        started = true;
        float songPos = MusicManager.instance.GetSongPos();

        pattern = PatternManager.instance.getPattern();
        beats = PatternManager.instance.getBeats(songPos + 1f, 3);

        endTime = beats[beats.Count - 1] + 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (started) {
            if (Keyboard.current.anyKey.wasPressedThisFrame)
            {
                // Get the mouse position in world space
                Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, hitCircleLayer);

                if (hit.collider != null)
                {
                    hitCircle circleScript = hit.collider.GetComponent<hitCircle>();
                    if (circleScript != null) {
                        score += circleScript.calculateScore();
                    }
                }
            }

            // spawns hitcircles
            float songPos = MusicManager.instance.GetSongPos();
            if (beats.Count > 0 && songPos >= beats[0] - approachRate) {
                GameObject newCircle = Instantiate(hitCircle, pattern[0], Quaternion.identity);
                newCircle.GetComponent<hitCircle>().targetTime = beats[0];
                
                beats.RemoveAt(0);
                pattern.RemoveAt(0);
            }

            // if reached end, destroy this object
            if (songPos > endTime && started) {
                overlay.DOFade(0f, 0.2f).OnComplete(()=> {
                    rhythmEnd.Invoke();
                    Destroy(gameObject);
                });
            }
        }
        
    }
}
