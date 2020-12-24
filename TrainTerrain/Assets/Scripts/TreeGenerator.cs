using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour
{
    public GameObject firTree;

    public GameObject oakTree;
    public GameObject popTree;
    public Transform train;
    private int quadsPerTile = 100;
    private int halfTile = 10;
    void Start()
    {
        if (train == null)
        {
            train = Camera.main.transform;
        }

        StartCoroutine(GenerateFirTrees());
    }

    private IEnumerator GenerateFirTrees()
    {   
        int playerX = (int)(Mathf.Floor((train.transform.position.x) / (quadsPerTile)) * quadsPerTile);
        int playerZ = (int)(Mathf.Floor((train.transform.position.z) / (quadsPerTile)) * quadsPerTile);

        for (int z = 0; z < halfTile; z++)
        {
            Vector3 pos1 = new Vector3(treeLocation(playerX),
                        -1,
                        (treeLocation(playerZ)));
            Vector3 pos2 = new Vector3(treeLocation(playerX),
                        -1,
                        (treeLocation(playerZ)));

            Instantiate(firTree, pos1, Quaternion.identity);
            Instantiate(firTree, pos2, Quaternion.identity);
        }
        yield return null;
    }

    int treeLocation(int playerCO)
    {
        return Random.Range(playerCO, 500); 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
