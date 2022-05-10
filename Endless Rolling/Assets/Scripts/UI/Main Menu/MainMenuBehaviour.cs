using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject gameModePrefab;

    

    /// <summary>
    /// Will load a scene upon being called
    /// </summary>
    /// <param name="levelName">The name of the level we want to load</param>
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        gameModePrefab = Instantiate(gameModePrefab);
    }
}
