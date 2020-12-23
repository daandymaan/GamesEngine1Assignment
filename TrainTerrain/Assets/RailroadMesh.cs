﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailroadMesh : MonoBehaviour {

    public int quadsPerTile = 10;

    public Material meshMaterial;

    public float amplitude = 50;

    Mesh m;

    // private delegate float SampleCell(float x, float y);

    // SampleCell[] sampleCell = {
    //            new SampleCell(SampleCell1)
    //           , new SampleCell(SampleCell2)
    //           , new SampleCell(SampleCell3)
    //           , new SampleCell(SampleCell4)
    // };

    // public int whichSampler = 0;

    // Use this for initialization
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
                // Vector3 bl = bottomLeft + new Vector3(col, sampleCell[whichSampler](transform.position.x + col, transform.position.z + row), row);
                // Vector3 tl = bottomLeft + new Vector3(col, sampleCell[whichSampler](transform.position.x + col, transform.position.z + row + 1), row + 1);
                // Vector3 tr = bottomLeft + new Vector3(col + 1, sampleCell[whichSampler](transform.position.x + col + 1, transform.position.z + row + 1), row + 1);
                // Vector3 br = bottomLeft + new Vector3(col + 1, sampleCell[whichSampler](transform.position.x + col + 1, transform.position.z + row), row);
                Vector3 bl = bottomLeft + new Vector3(col, 0, row);
                Vector3 tl = bottomLeft + new Vector3(col, 0, row + 1);
                Vector3 tr = bottomLeft + new Vector3(col + 1, 0, row + 1);
                Vector3 br = bottomLeft + new Vector3(col + 1, 0, row);

                
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

    // Start is called before the first frame update
    void Start()
    {
        // mesh = new Mesh();
        // GetComponent<MeshFilter>().mesh = mesh;
        // mr = gameObject.AddComponent<MeshRenderer>(); // Draw
        // mc = gameObject.AddComponent<MeshCollider>();

        // StartCoroutine(CreateShape());
    }

    private void Update()
    {
        // UpdateMesh();
    }

    // IEnumerator CreateShape()
    // {
    //     vertices = new Vector3[(xSize + 1) * (zSize + 1)];
    //     for (int i = 0 , z = 0; z <= zSize; z++)
    //     {
    //         for (int x = 0; x <= xSize; x++)
    //         {
    //             vertices[i] = new Vector3(x, 0, z);
    //             i++;
    //         }
    //     }

    //     int tris = 0;
    //     int vert = 0;
    //     triangles = new int[xSize * zSize * 6];
    //     for (int z = 0; z < zSize; z++)
    //     {
    //         for (int x = 0; x < xSize; x++)
    //         {
    //             triangles[tris + 0] = vert + 0;
    //             triangles[tris + 1] = vert + xSize + 1;
    //             triangles[tris + 2] = vert + 1;
    //             triangles[tris + 3] = vert + 1;
    //             triangles[tris + 4] = vert + xSize + 1;
    //             triangles[tris + 5] = vert + xSize + 2;

    //             vert++;
    //             tris += 6;

    //             yield return new WaitForSeconds(.1f);
    //         }
    //         vert++;
    //     }

    //     uv = new Vector2[(xSize + 1) * (zSize + 1)];
    //     int index = 0;
    //     for (int z = 0; z < zSize; z++)
    //     {
    //         for (int x = 0; x < xSize; x++)
    //         {
    //             uv[index++] = new Vector2(x / xSize, z / zSize);
    //             uv[index++] = new Vector2(x / xSize, (z + 1) / zSize);
    //             uv[index++] = new Vector2((x + 1) / xSize, (z + 1) / zSize);
    //             uv[index++] = new Vector2((x + 1) / xSize, z / zSize);
    //         }
    //     }
    // }

    void UpdateMesh()
    {
        // mesh.Clear();
        // mesh.vertices = vertices;
        // mesh.triangles = triangles; 
        // mesh.uv = uv;
        // mesh.RecalculateNormals();
        // mr.material = meshMaterial;
        // mc.sharedMesh = mesh;
        // mr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        // mr.receiveShadows = true;
    }

    // private void OnDrawGizmos()
    // {
    //     if(vertices == null)
    //     {
    //         return; 
    //     }

    //     for (int i = 0; i < vertices.Length; i++)
    //     {
    //         Gizmos.DrawSphere(vertices[i], .1f);
    //     }
    // }

}
