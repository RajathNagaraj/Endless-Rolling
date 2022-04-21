using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Action<bool> OnNextTileSpawned;
    public Action OnResetGame;
    [Tooltip("A reference to the tile we want to spawn")]
    public Transform tile;
    [Tooltip("A reference to the obstacle we want to spawn")]
    public Transform obstacle;
    [Tooltip("The location of the first tile")]
    public Vector3 startPoint = new Vector3(0 ,-1 ,-5);
    [Tooltip("The number of tiles to create in advance")]
    [Range(1 ,15)]
    public int initSpawnNum = 10;
    [Tooltip("The number of tiles we want to spawn initially with no obstacles")]
    [Range(1,10)]
    public int initNoObstacles = 4;
    /// <summary>
    /// Where the next tile should spawn at
    /// </summary>
    private Vector3 nextTileLocation;
    /// <summary>
    /// The rotation of the spawned tile
    /// </summary>
    private Quaternion nextTileRotation;

    // Start is called before the first frame update
    void Start()
    {
        //set our Starting Point
        nextTileLocation = startPoint;
        //Set our rotation when spawned
        nextTileRotation = Quaternion.identity;

        OnNextTileSpawned += SpawnNextTile;
        OnResetGame += ResetGame;

        for(int i = 0; i < initSpawnNum; ++i)
        {
            OnNextTileSpawned(i >= initNoObstacles);
        }
    }

    /// <summary>
    /// Will spawn a tile at a certain position and setup a new startpoint
    /// </summary>
    private void SpawnNextTile()
    {
        var nextTile = Instantiate(tile,nextTileLocation,nextTileRotation);
        //Figure out where and what rotation the next tile should be Spawned
        var nextEndPoint = nextTile.Find("Next Spawn Point");
        nextTileLocation = nextEndPoint.position;
        nextTileRotation = nextEndPoint.rotation;

        if(spawnObstacles)
        {
            SpawnObstacle(nextTile);
        }
    }

    private void SpawnObstacle(Transform nextTile)
    {
        var obstacleSpawnPoints = new List<GameObject>();
        foreach(Transform child in nextTile)
        {
            if(child.CompareTag("ObstacleSpawn"))
            {
                obstacleSpawnPoints.Add(child.gameObject);
            }
        }

        if (obstacleSpawnPoints.Count > 0)
        {
            var spawnPoint = obstacleSpawnPoints[UnityEngine.Random.Range(0, obstacleSpawnPoints.Count)];
            var spawnPos = spawnPoint.transform.position;
            var newObstacle = Instantiate(obstacle,spawnPos,Quaternion.identity);
            newObstacle.SetParent(spawnPoint.transform);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log(SceneManager.GetActiveScene().name);
    }
}
