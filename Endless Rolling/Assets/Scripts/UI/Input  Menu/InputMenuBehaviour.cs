using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputMenuBehaviour : MonoBehaviour
{
    [Header("Scriptable Object")]
    [SerializeField]
    private InputStyleObject inputStyleObject;


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
                        inputStyleObject.horizMovement = MobileHorizontalMovement.Accelerometer;
                        Debug.Log("button pressed is " + buttonName);
                        break;
                    case "Screen Touch":
                        inputStyleObject.horizMovement = MobileHorizontalMovement.ScreenTouch;
                        Debug.Log("button pressed is " + buttonName);
                        break;
                    case "Swipe Gesture":
                        inputStyleObject.horizMovement = MobileHorizontalMovement.SwipeGesture;
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
