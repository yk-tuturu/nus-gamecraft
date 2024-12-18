using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Bubble : MonoBehaviour
{
    public RectTransform rect;
    public Image image;
    public Vector3 originalPos;
    public Vector3 targetPos;
    private Tween movementTween;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake() {
        rect = GetComponent<RectTransform>();
        originalPos = rect.anchoredPosition;
        targetPos = new Vector3(originalPos.x, originalPos.y + 0.08f, originalPos.z);
        movementTween = rect.DOAnchorPos(targetPos, 1f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    void OnEnable() {
        movementTween.Play();
    }

    void OnDisable() {
        PauseAnim();
    }

    public void SetSprite(int index) {
        Sprite sprite = SpriteManager.instance.GetSprite(index);
        image.sprite = sprite;
    }

    public void PauseAnim() {
        movementTween.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
