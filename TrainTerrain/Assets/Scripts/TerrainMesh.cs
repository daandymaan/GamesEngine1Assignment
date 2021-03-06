﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMesh : MonoBehaviour
{
    public int quadsPerTile = 100;

    public Material meshMaterial;

    public float amplitude = 50;
    public int terrainOption;

    Mesh m;

    void Awake() {
        MeshFilter mf = gameObject.AddComponent<MeshFilter>(); // Container for the mesh
        MeshRenderer mr = gameObject.AddComponent<MeshRenderer>(); // Draw
        MeshCollider mc = gameObject.AddComponent<MeshCollider>();
        m = mf.mesh;

        int verticesPerQuad = 4;
        Vector3[] vertices = new Vector3[verticesPerQuad * quadsPerTile * quadsPerTile];
        Vector2[] uv = new Vector2[verticesPerQuad * quadsPerTile * quadsPerTile];

        int vertexTriangesPerQuad = 6;
        int[] triangles = new int[vertexTriangesPerQuad * quadsPerTile * quadsPerTile];

        Vector3 bottomLeft = new Vector3(-quadsPerTile / 2, 0, -quadsPerTile / 2);
        int vertex = 0;
        int triangleVertex = 0;
        float minY = float.MaxValue;
        float maxY = float.MinValue;
        for (int row = 0; row < quadsPerTile; row++)
        {
            for (int col = 0; col < quadsPerTile; col++)
            {
                Vector3 bl;
                Vector3 tl;
                Vector3 tr; 
                Vector3 br;
                if(terrainOption == 0)
                {
                    bl = bottomLeft + new Vector3(col, generateBmps(transform.position.x + col, transform.position.z + row), row);
                    tl = bottomLeft + new Vector3(col, generateBmps(transform.position.x + col, transform.position.z + row + 1), row + 1);
                    tr = bottomLeft + new Vector3(col + 1, generateBmps(transform.position.x + col + 1, transform.position.z + row + 1), row + 1);
                    br = bottomLeft + new Vector3(col + 1, generateBmps(transform.position.x + col + 1, transform.position.z + row), row);
                } else if(terrainOption == 1)
                {
                    bl = bottomLeft + new Vector3(col, generateMtn(transform.position.x + col, transform.position.z + row), row);
                    tl = bottomLeft + new Vector3(col, generateMtn(transform.position.x + col, transform.position.z + row + 1), row + 1);
                    tr = bottomLeft + new Vector3(col + 1, generateMtn(transform.position.x + col + 1, transform.position.z + row + 1), row + 1);
                    br = bottomLeft + new Vector3(col + 1, generateMtn(transform.position.x + col + 1, transform.position.z + row), row);
                } else 
                {
                    bl = bottomLeft + new Vector3(col, 0, row);
                    tl = bottomLeft + new Vector3(col, 0, row + 1);
                    tr = bottomLeft + new Vector3(col + 1, 0, row + 1);
                    br = bottomLeft + new Vector3(col + 1, 0, row);
                }
               

                
                int startVertex = vertex;
                vertices[vertex++] = bl;
                vertices[vertex++] = tl;
                vertices[vertex++] = tr;
                vertices[vertex++] = br;
                               

                vertex = startVertex;
                float fNumQuads = quadsPerTile;
                uv[vertex++] = new Vector2(col / fNumQuads, row / fNumQuads);
                uv[vertex++] = new Vector2(col / fNumQuads, (row + 1) / fNumQuads);
                uv[vertex++] = new Vector2((col + 1) / fNumQuads, (row + 1) / fNumQuads);
                uv[vertex++] = new Vector2((col + 1) / fNumQuads, row / fNumQuads);

                triangles[triangleVertex++] = startVertex;
                triangles[triangleVertex++] = startVertex + 1;
                triangles[triangleVertex++] = startVertex + 3;
                triangles[triangleVertex++] = startVertex + 3;
                triangles[triangleVertex++] = startVertex + 1;
                triangles[triangleVertex++] = startVertex + 2;

                if (bl.y > maxY)
                {
                    maxY = bl.y;
                }
                if (bl.y < minY)
                {
                    minY = bl.y;
                }
            }
        }
        m.vertices = vertices;
        m.uv = uv;
        m.triangles = triangles;        
        m.RecalculateNormals();
        mr.material = meshMaterial;
        mc.sharedMesh = m;
        mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        mr.receiveShadows = true;
	}

    public static float generateBmps(float x, float y)
    {
        float flatness = 0.2f;
        float noise = Mathf.PerlinNoise(10000 + x , 10000 + y );
        if (noise > 0.5f + flatness)
        {
            noise = noise - flatness;
        }
        else if (noise < 0.5f - flatness)
        {
            noise = noise + flatness;
        }
        else
        {
            noise = 0.5f;
        }
        
        return (noise * 300) + (Mathf.PerlinNoise(1000 + x / 5, 100 + y / 5) * 2);
    }

    public static float generateMtn(float x, float y)
    {
        float flatness = 0.2f;
        float noise = Mathf.PerlinNoise(10000 + x / 500, 10000 + y / 350);
        if (noise > 0.5f + flatness)
        {
            noise = noise - flatness;
        }
        else if (noise < 0.5f - flatness)
        {
            noise = noise + flatness;
        }
        else
        {
            noise = 0.5f;
        }
        
        return (noise * 300) + (Mathf.PerlinNoise(1000 + x / 5, 100 + y / 5) * 2);
    }

    void Start()
    {

    }

    private void Update()
    {
        // UpdateMesh();
    }
}

