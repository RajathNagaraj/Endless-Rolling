using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public static GameMode Instance;

    public GameDifficulty difficulty;
    public InputStyle inputMode;
    private void Awake()
    {
        if(Instance != null)
        {
            //Destroy(Instance);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
}
