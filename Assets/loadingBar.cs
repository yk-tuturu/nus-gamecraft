using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingBar : MonoBehaviour
{
    public float maximum = 1f; 
    public float currentValue = 0f; 
    public Image bar; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetFill(float amount) {
        bar.fillAmount = amount;
    }
}
