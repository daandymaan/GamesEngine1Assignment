﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    
    public GameObject tilePrefab;
    public Transform train;
    public int quadsPerTile;

    public int halfTile = 2;

    Dictionary<string, Tile> tiles = new Dictionary<string, Tile>();
    Queue<GameObject> oldGameObjects = new Queue<GameObject>();


    // Use this for initialization
    void Start()
    {

        TerrainMesh tt = tilePrefab.GetComponent<TerrainMesh>();
        if (tt != null)
        {
            quadsPerTile = tt.quadsPerTile;
        }
        
        if (train == null)
        {
            train = Camera.main.transform;
        }

        StartCoroutine(GenerateWorldAroundPlayer());

    }

    // Update is called once per frame
    void Update()
    {

    }

    
    private IEnumerator GenerateWorldAroundPlayer()
    {        
        // Make sure this happens at once at the start
        int xMove = int.MaxValue;
        int zMove = int.MaxValue;

        // Adapted from https://www.youtube.com/watch?v=dycHQFEz8VI

        while (true)
        {
            if (oldGameObjects.Count > 0)
            {
                GameObject.Destroy(oldGameObjects.Dequeue());
            }
            if (Mathf.Abs(xMove) >= quadsPerTile|| Mathf.Abs(zMove) >= quadsPerTile)
            {
                float updateTime = Time.realtimeSinceStartup;

                //force integer position and round to nearest tilesize
                // Find which tile the player is on
                int playerX = (int)(Mathf.Floor((train.transform.position.x) / (quadsPerTile)) * quadsPerTile);
                int playerZ = (int)(Mathf.Floor((train.transform.position.z) / (quadsPerTile)) * quadsPerTile);

                List<Vector3> newTiles = new List<Vector3>();

                for (int z = -halfTile; z < halfTile; z++)
                {
                    // The position of the new tile
                    Vector3 pos1 = new Vector3((playerX + 120),
                        -151.5f,
                        (z * quadsPerTile + playerZ));
                    Vector3 pos2 = new Vector3((playerX + 20),
                        -151.5f,
                        (z * quadsPerTile + playerZ));

                    string tilename1 = "Tile_" + ((int)(pos1.x)).ToString() + "_" + ((int)(pos1.z)).ToString();
                    string tilename2 = "Tile_" + ((int)(pos2.x)).ToString() + "_" + ((int)(pos2.z)).ToString();
                    if (!tiles.ContainsKey(tilename1) || !tiles.ContainsKey(tilename2))
                    {
                        newTiles.Add(pos1);
                        newTiles.Add(pos2);
                    }
                    else
                    {
                        (tiles[tilename1] as Tile).creationTime = updateTime;
                        (tiles[tilename2] as Tile).creationTime = updateTime;

                    }
                }

                // Sort in order of distance from the player
                newTiles.Sort((a, b) => (int)Vector3.SqrMagnitude(train.transform.position - a) - (int)Vector3.SqrMagnitude(train.transform.position - b));
                foreach (Vector3 pos in newTiles)
                {
                    GameObject t = GameObject.Instantiate<GameObject>(tilePrefab, pos, Quaternion.identity);
                    t.transform.parent = this.transform;
                    string tilename = "Tile_" + ((int)(pos.x)).ToString() + "_" + ((int)(pos.z)).ToString();
                    t.name = tilename;
                    Tile tile = new Tile(t, updateTime);
                    tiles[tilename] = tile;
                    yield return null;
                }

                //destroy all tiles not just created or with time updated
                //and put new tiles and tiles to be kepts in a new hashtable
                Dictionary<string, Tile> newTerrain = new Dictionary<string, Tile>();
                foreach (Tile tile in tiles.Values)
                {
                    if (tile.creationTime != updateTime)
                    {
                        oldGameObjects.Enqueue(tile.theTile);
                    }
                    else
                    {
                        newTerrain[tile.theTile.name] = tile;
                    }
                }
                //copy new hashtable contents to the working hashtable
                tiles = newTerrain;
                startPos = train.transform.position;
            }
            yield return null;
            //determine how far the player has moved since last terrain update
            xMove = (int)(train.transform.position.x - startPos.x);
            zMove = (int)(train.transform.position.z - startPos.z);
        }
    }

    Vector3 startPos;
    class Tile
    {
        public GameObject theTile;
        public float creationTime;


        public Tile(GameObject t, float ct)
        {
            theTile = t;
            creationTime = ct;
        }
    }
}
