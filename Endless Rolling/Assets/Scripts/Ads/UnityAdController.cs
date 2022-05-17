using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class UnityAdController : MonoBehaviour
{
    /// <summary>
    /// If we should display ads or not
    /// </summary>
    public static bool showAds = true;

    private string gameId = "4756395";

    /// <summary>
    /// If the game is in test mode or not
    /// </summary>
    private bool testMode = true;

    /// <summary>
    /// Unity ads should be initialised, or else ads will not work as intended
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        //No need to initialize if already done
        if(!Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId,testMode);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ShowAd()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }


}
