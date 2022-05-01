using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class ScoreBehaviour : MonoBehaviour
{
    

    private Text destroyText;
    // Start is called before the first frame update
    void Start()
    {
        destroyText = GetComponentInChildren<Text>();
        
    }

    public void DisplayDestroy(int count)
    {
        Debug.Log(count);
        destroyText.text = "Destroys: "+count.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
