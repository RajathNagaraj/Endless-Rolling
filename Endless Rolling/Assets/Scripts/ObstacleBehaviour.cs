using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleBehaviour : MonoBehaviour
{
    private GameController gameController;
    [Tooltip("The time to wait before the game restarts")]
    public float waitTime = 2f;

   
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
            gameController.OnEndGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
