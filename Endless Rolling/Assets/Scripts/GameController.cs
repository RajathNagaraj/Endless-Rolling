using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Action OnNextTileSpawned;
    [Tooltip("A reference to the tile we want to spawn")]
    public Transform tile;
    [Tooltip("The location of the first tile")]
    public Vector3 startPoint = new Vector3(0 ,-1 ,-5);
    [Tooltip("The number of tiles to create in advance")]
    [Range(1 ,15)]
    public int initSpawnNum = 10;
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

        for(int i = 0; i < initSpawnNum; ++i)
        {
            SpawnNextTile();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
