using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DifficultyMenuController : MonoBehaviour
{

    
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
                    case "Easy":
                        GameMode.Instance.difficulty = GameDifficulty.Easy;
                        Debug.Log("button pressed is " + buttonName);
                        break;
                    case "Medium":
                        GameMode.Instance.difficulty = GameDifficulty.Medium;
                        Debug.Log("button pressed is " + buttonName);
                        break;
                    case "Hard":
                        GameMode.Instance.difficulty = GameDifficulty.Hard;
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
