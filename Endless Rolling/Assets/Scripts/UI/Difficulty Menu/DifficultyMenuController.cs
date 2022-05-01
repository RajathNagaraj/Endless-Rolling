using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultyMenuController : MonoBehaviour
{

    public Difficulty difficulty;
    private Button[] buttons;
    private string buttonName;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }
    // Start is called before the first frame update
    private void Start()
    {
        foreach (Button button in buttons)
        {

            button.onClick.AddListener(() =>

            {
                buttonName = button.gameObject.name;
                switch (buttonName)
                {
                    case "EASY":
                        difficulty.mode = GameMode.Easy;
                        Debug.Log("button pressed is " + buttonName);
                        break;
                    case "MEDIUM":
                        difficulty.mode = GameMode.Medium;
                        Debug.Log("button pressed is " + buttonName);
                        break;
                    case "HARD":
                        difficulty.mode = GameMode.Hard;
                        Debug.Log("button pressed is " + buttonName);
                        break;

                    default:
                        Debug.Log("button pressed is " + buttonName);
                        break;
                }

                LoadLevel("Gameplay");




            });



        }
    }


    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
