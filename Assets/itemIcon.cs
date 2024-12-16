using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemIcon : MonoBehaviour
{
    public GameObject selectedOutline;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onSelect() {
        selectedOutline.SetActive(true);
    }

    public void onDeselect() {
        selectedOutline.SetActive(false);
    }
}
