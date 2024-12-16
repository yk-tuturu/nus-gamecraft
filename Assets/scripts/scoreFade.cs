using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class scoreFade : MonoBehaviour
{
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(Fade());
    }
    
    IEnumerator Fade() {
        yield return new WaitForSeconds(0.5f);
        sprite.DOFade(0f, 0.2f).OnComplete(()=> {
            Destroy(gameObject);
        });
    }

    
}
