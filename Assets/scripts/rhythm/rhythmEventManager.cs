using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class rhythmEventManager : MonoBehaviour
{
    public GameObject hitCircle; 
    public Camera mainCamera;
    public LayerMask hitCircleLayer;
    public SpriteRenderer overlay;

    public int score = 0;
    public int noteCount = 3;
    public float approachRate = 0.8f;
    float endTime;
    int nextCombo = 1;

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

        LevelManager.instance.freezePatience.Invoke();
        pauseManager.instance.DisablePause();
    }

    void Begin() {
        started = true;
        float songPos = MusicManager.instance.GetSongPos();

        pattern = PatternManager.instance.getPattern();
        beats = PatternManager.instance.getBeats(songPos + 1f, noteCount);

        endTime = beats[beats.Count - 1] + 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (started) {
            if (Input.anyKeyDown)
            {
                //Get the mouse position in world space
                Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, Mathf.Infinity, hitCircleLayer);

                if (hit.collider != null)
                {
                    hitCircle circleScript = hit.collider.GetComponent<hitCircle>();
                    if (circleScript != null) {
                        score += circleScript.calculateScore();
                    }
                }
            }

            if (Input.touchCount > 0) // Check if there is at least one touch
            {
                Touch touch = Input.GetTouch(0); // Get the first touch

                if (touch.phase == TouchPhase.Began)
                {
                    Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
                    touchPosition.z = 0f;
                    
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero, Mathf.Infinity, hitCircleLayer);
                    if (hit.collider != null)
                    {
                        hitCircle circleScript = hit.collider.GetComponent<hitCircle>();
                        if (circleScript != null) {
                            score += circleScript.calculateScore();
                        }
                    }
                }
            }

            // spawns hitcircles
            float songPos = MusicManager.instance.GetSongPos();
            if (beats.Count > 0 && songPos >= beats[0] - approachRate) {
                GameObject newCircle = Instantiate(hitCircle, pattern[0], Quaternion.identity);
                hitCircle hitCircleScript = newCircle.GetComponent<hitCircle>();
                hitCircleScript.targetTime = beats[0];
                hitCircleScript.combo = nextCombo;
                
                beats.RemoveAt(0);
                pattern.RemoveAt(0);
                nextCombo++;
            }

            // if reached end, destroy this object
            if (songPos > endTime && started) {
                overlay.DOFade(0f, 0.2f).OnComplete(()=> {
                    rhythmEnd.Invoke();
                    DOTween.Kill(overlay);
                    Destroy(gameObject);
                });
            }
        }
        
    }

    void OnDisable() {
        LevelManager.instance.unfreezePatience.Invoke();
        pauseManager.instance.EnablePause();
    }
}
