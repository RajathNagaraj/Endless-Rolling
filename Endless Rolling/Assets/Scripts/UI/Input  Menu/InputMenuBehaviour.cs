using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputMenuBehaviour : MonoBehaviour
{
    
    private Button[] buttons;
    private string buttonName;

    private void Awake()
    {
        buttons = GetComponentsInChildren<Button>();
    }

    private void Start()
    {
        foreach (Button button in buttons)
        {

            button.onClick.AddListener(() =>

            {
                buttonName = button.gameObject.name;
                switch (buttonName)
                {
                    case "Accelerometer":
                        GameMode.Instance.inputMode = InputStyle.Accelerometer;
                        Debug.Log("button pressed is " + buttonName);
                        break;
                    case "Screen Touch":
                        GameMode.Instance.inputMode = InputStyle.ScreenTouch;
                        Debug.Log("button pressed is " + buttonName);
                        break;
                    case "Swipe Gesture":
                        GameMode.Instance.inputMode = InputStyle.SwipeGesture;
                        Debug.Log("button pressed is " + buttonName);
                        break;

                    default:
                        Debug.Log("button pressed is " + buttonName);
                        break;
                }

                LoadLevel("Difficulty");




            });

            

        }
    }

    private void Update()
    {
        
    }


    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }



}
