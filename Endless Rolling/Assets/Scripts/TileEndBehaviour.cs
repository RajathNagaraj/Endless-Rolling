using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEndBehaviour : MonoBehaviour
{
    public float destroyTime = 1.5f;
    private GameController gameController;

    private void OnTriggerEnter(Collider collider)
    {
       if(collider.gameObject.tag == "Player")
        {
            gameController.OnNextTileSpawned();
            Destroy(this.transform.parent.gameObject,destroyTime);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("Game Controller").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
