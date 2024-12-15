using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class hitCircle : MonoBehaviour
{
    public Transform approachCircle; 

    public SpriteRenderer hitCircleSprite;
    public SpriteRenderer hitOverlaySprite;
    public SpriteRenderer approachCircleSprite;
    public SpriteRenderer numberSprite;

    // timing constants 
    public float fadeInTime;
    public float fadeOutTime;

    public float targetTime = 4f; 
    public float perfectWindow = 0.05f; //50ms perfect 
    public float goodWindow = 0.12f; // 120ms good
    public float offset = 0f;

    bool despawning = false;

    public AudioSource audioSource; 
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = hitSound;

        float songPos = MusicManager.instance.songPosition;
        targetTime = targetTime + offset;
        approachCircle.DOScale(new Vector3(1f, 1f, 1f), targetTime-songPos).SetEase(Ease.Linear).SetLink(gameObject);
        FadeSprite(1f, fadeInTime);
    }

    void Update() {
        float songPos = MusicManager.instance.songPosition;
        if (songPos > targetTime + goodWindow && !despawning) {
            Despawn();
        }
    }

    void Despawn() {
        if (!despawning) {
            despawning = true;
            hitCircleSprite.DOFade(0f, 0.1f).SetLink(gameObject);
            hitOverlaySprite.DOFade(0f, 0.1f).SetLink(gameObject);
            approachCircleSprite.DOFade(0f, 0.1f).SetLink(gameObject);
            numberSprite.DOFade(0f, 0.1f).SetLink(gameObject).OnComplete(() => {
                Destroy(gameObject);
            });
        }
    }

    void FadeSprite(float targetAlpha, float fadeTime) {
        hitCircleSprite.DOFade(targetAlpha, fadeTime);
        hitOverlaySprite.DOFade(targetAlpha, fadeTime);
        approachCircleSprite.DOFade(targetAlpha, fadeTime);
        numberSprite.DOFade(targetAlpha, fadeTime);
    }

    public int calculateScore() {
        if (despawning) {
            return 0;
        }

        float songPos = MusicManager.instance.songPosition;
        float hitError = Mathf.Abs(songPos - targetTime);
        Debug.Log(songPos - targetTime);
        if (hitError < perfectWindow) {
            Debug.Log("perfect");
            audioSource.PlayOneShot(hitSound);
            Despawn();
            return 300;

        } else if (hitError < goodWindow) {
            Debug.Log("good");
            audioSource.PlayOneShot(hitSound);
            Despawn();
            return 100;
        } else {
            Debug.Log("miss");
            return 0;
        }
    }
}
