using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Action<bool> OnNextTileSpawned;
    public Action<float> OnEndGame;
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
    public int initNoObstacles;
    /// <summary>
    /// The number of obstacles on each tile(randomly)
    /// </summary>
    private int noOfObstaclesOnTile;
    /// <summary>
    /// Where the next tile should spawn at
    /// </summary>
    private Vector3 nextTileLocation;
    /// <summary>
    /// The rotation of the spawned tile
    /// </summary>
    private Quaternion nextTileRotation;
    /// <summary>
    /// The length of each tile
    /// </summary>
    private float lengthOfTile;
    

    private void Awake()
    {
        
        
       if(GameMode.Instance != null)
        {
            SetDifficulty(GameMode.Instance.difficulty);
        }
        else
        {
            SetDifficulty(GameDifficulty.Hard);
        }
        
       
    }

    // Start is called before the first frame update
    void Start()
    {
        //set our Starting Point
        nextTileLocation = startPoint;
        //Set our rotation when spawned
        nextTileRotation = Quaternion.identity;       

        OnNextTileSpawned += SpawnNextTile;
        OnEndGame += (float waitTime) => { Invoke("ResetGame", waitTime); };

        for(int i = 0; i < initSpawnNum; ++i)
        {
            SpawnNextTile(i >= initNoObstacles);
        }
    }

    private void SetDifficulty(GameDifficulty difficultySetting)
    {
        switch(difficultySetting)
        {
            case GameDifficulty.Easy: lengthOfTile = 3f;
                initNoObstacles = 4;
                break;
            case GameDifficulty.Medium: lengthOfTile = 2f;
                initNoObstacles = 2;
                break;
            case GameDifficulty.Hard: lengthOfTile = 1f;
                initNoObstacles = 1;
                break;

            default:
                break;
        }

    }

    /// <summary>
    /// Will spawn a tile at a certain position and setup a new startpoint
    /// </summary>
    private void SpawnNextTile(bool spawnObstacles)
    {
        var nextTile = Instantiate(tile,nextTileLocation,nextTileRotation);
        nextTile = SetTileLength(nextTile);
        //Figure out where and what rotation the next tile should be Spawned
        var nextEndPoint = nextTile.Find("Next Spawn Point");
        nextTileLocation = nextEndPoint.position;
        nextTileRotation = nextEndPoint.rotation;

        if(spawnObstacles)
        {
            SpawnObstacle(nextTile);
        }
    }

    private Transform SetTileLength(Transform nextTile)
    {
        float scaleFactor = nextTile.localScale.z;
        scaleFactor *= lengthOfTile;
        nextTile.localScale = new Vector3(nextTile.localScale.x, nextTile.localScale.y, scaleFactor);
        return nextTile;
    }

    private void SpawnObstacle(Transform nextTile)
    {
        var obstacleSpawnPoints = new List<GameObject>();
        var uniqueSpawnObjects = new HashSet<GameObject>();
        foreach(Transform child in nextTile)
        {
            if(child.CompareTag("ObstacleSpawn"))
            {
                obstacleSpawnPoints.Add(child.gameObject);
            }
        }
        // Number of obstacles on each tile
        noOfObstaclesOnTile = ((new System.Random()).Next(1, obstacleSpawnPoints.Count));
        //Get references for a maximum number of those obstacles in a HashSet(duplicates avoided)
        for(int i = 1; i <= noOfObstaclesOnTile; ++i)
        {
            uniqueSpawnObjects.Add(obstacleSpawnPoints[UnityEngine.Random.Range(0, obstacleSpawnPoints.Count)]);
        }
        //Spawn each obstacle in the HashSet
        foreach(GameObject block in uniqueSpawnObjects)
        {
            var spawnPos = block.transform.position;
            var newObstacle = Instantiate(obstacle, spawnPos, Quaternion.identity);
            newObstacle.SetParent(block.transform);

        }
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ResetGame()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
