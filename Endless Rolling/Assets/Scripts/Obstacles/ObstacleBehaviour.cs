using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleBehaviour : MonoBehaviour
{
    public GameObject explosion;
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
            gameController.OnEndGame(waitTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// If this object is touched, we spawn an explosion and destroy this object
    /// </summary>
    private void PlayerTouch()
    {
        if(PlayerBehaviour.InitNoOfDestroys > 0)
        {
            if (explosion != null)
            {
                var particles = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(particles, 1f);
            }
            PlayerBehaviour.OnObstacleDestroyed(--PlayerBehaviour.InitNoOfDestroys);
            Debug.Log("Destroying Obstacle");
            Destroy(this.gameObject);
        }       
    }
   
}
