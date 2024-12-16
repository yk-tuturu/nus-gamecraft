using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class uiTransitions : MonoBehaviour
{
    RectTransform panel;
    // Start is called before the first frame update
    void Start()
    {
        panel = GetComponent<RectTransform>();
    }

    public void UIShrink(bool disableAtEnd) {
        if (disableAtEnd) {
            transform.DOScale(0f, 0.3f).SetEase(Ease.OutQuad).OnComplete(()=> {
                gameObject.SetActive(false);
            });
        } else {
            transform.DOScale(new Vector3(0f, 0f, 0f), 0.3f).SetEase(Ease.Linear);
        }
    }

    public void UIExpand() {
        Debug.Log("expand");
        transform.DOScale(1f, 0.3f).SetEase(Ease.Linear);
    }
}
