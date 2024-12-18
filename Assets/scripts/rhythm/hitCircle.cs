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

    public GameObject sprite300; 
    public GameObject sprite100;
    public GameObject spriteMiss;

    public List<Sprite> comboSprites = new List<Sprite>();

    // timing constants 
    public float fadeInTime;
    public float fadeOutTime;

    public float targetTime = 4f; 
    public float perfectWindow = 0.05f; //50ms perfect 
    public float goodWindow = 0.12f; // 120ms good
    public float offset = 0f;
    public int combo = 1;

    bool despawning = false;

    public AudioSource audioSource; 
    public AudioClip hitSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = hitSound;

        numberSprite.sprite = comboSprites[combo - 1];

        float songPos = MusicManager.instance.GetSongPos();
        approachCircle.DOScale(new Vector3(1f, 1f, 1f), targetTime - songPos).SetEase(Ease.Linear);
        FadeSprite(0.7f, fadeInTime);
    }

    void Update() {
        float songPos = MusicManager.instance.GetSongPos();
        if (songPos > targetTime + goodWindow && !despawning) {
            Instantiate(spriteMiss, transform.position, Quaternion.identity);
            Despawn();
        }
    }

    void Despawn() {
        if (!despawning) {
            despawning = true;
            hitCircleSprite.DOFade(0f, 0.1f);
            hitOverlaySprite.DOFade(0f, 0.1f);
            approachCircleSprite.DOFade(0f, 0.1f);
            numberSprite.DOFade(0f, 0.1f).OnComplete(() => {
                DOTween.Kill(hitCircleSprite);
                DOTween.Kill(hitOverlaySprite);
                DOTween.Kill(approachCircleSprite);
                DOTween.Kill(numberSprite);

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

        float songPos = MusicManager.instance.GetSongPos();
        float hitError = Mathf.Abs(songPos - targetTime);
        Debug.Log(songPos - targetTime);
        if (hitError < perfectWindow) {
            Debug.Log("perfect");
            audioSource.PlayOneShot(hitSound);
            Instantiate(sprite300, transform.position, Quaternion.identity);
            Despawn();
            return 300;

        } else if (hitError < goodWindow) {
            Debug.Log("good");
            audioSource.PlayOneShot(hitSound);
            Instantiate(sprite100, transform.position, Quaternion.identity);
            Despawn();
            return 100;
        } else {
            Debug.Log("miss");
            Instantiate(spriteMiss, transform.position, Quaternion.identity);
            Despawn();
            return 0;
        }
    }
}
