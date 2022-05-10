using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public  class ScoreBehaviour : MonoBehaviour
{
    public static Action<int> OnDestroyUpdated;
    private static Text destroyText;

    private void Awake()
    {
        destroyText = GetComponentInChildren<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
       
        OnDestroyUpdated += DisplayDestroy;
        
    }

    private static void DisplayDestroy(int count)
    {
        Debug.Log("Score Behaviour: Destroys left: "+count);
        destroyText.text = "Destroys: "+count;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
