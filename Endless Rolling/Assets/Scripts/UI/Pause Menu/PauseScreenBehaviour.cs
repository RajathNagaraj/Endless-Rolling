using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenBehaviour : MainMenuBehaviour
{
    public static bool paused;

    [Tooltip("Reference to the Pause Menu object to turn on/off")]
    public GameObject pauseMenu;

    /// <summary>
    /// Reloads our current scene,effectively restarting our game
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    /// <summary>
    /// Will turn our pause menu on/off
    /// </summary>
    /// <param name="isPaused"></param>
    public void SetPauseMenu(bool isPaused)
    {
        paused = isPaused;
        Time.timeScale = paused ? 0 : 1;
        pauseMenu.SetActive(paused);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //Must be reset in Awake otherwise game will be paused upon restart
        SetPauseMenu(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
