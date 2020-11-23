using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    Mesh LandMesh;
    Vector3[] vertices;
    int[] triangles;


    [Header("Size of Terrain:")]

    public int XSize;
    public int ZSize;
    [Header("Height of Perlin Noise:")]

    public float HeightAcrossX;
    public float HeightAcrossZ;
    public float multiplyHeight;
    // Start is called before the first frame update
    void Start()
    {
        LandMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = LandMesh;
        LandMesh.name = "RaceTrack Terrain";

        //CreateTerrain();
       // UpdateTerrain();
    }
    private void Update()
    {
        CreateTerrain();
        UpdateTerrain();
    }
    void CreateTerrain()
    {
        vertices = new Vector3[(XSize + 1) * (ZSize + 1)];
       
        for (int i = 0,z = 0; z <= ZSize; z++)
        {
            for(int x = 0; x<=XSize;x++)
            {
                float y = Mathf.PerlinNoise(x * HeightAcrossX, z * HeightAcrossZ) * multiplyHeight;
                vertices[i] = new Vector3(x, y, z);
                i++;
            }

        }
   
        triangles = new int[XSize * ZSize * 6]; // xsize and z size hold each quad and the six is the size of the quad
        int vert = 0;
        int tri = 0;
        for (int z = 0; z < ZSize; z++)
        {
            for (int x = 0; x < XSize; x++)
            {
           
                // creating the triangle verticie posistions
                triangles[tri + 0] = vert + 0; // updating the next triangles verticiy rather is tri + 0;
                triangles[tri + 1] = vert + XSize + 1;
                triangles[tri + 2] = vert + 1;
                triangles[tri + 3] = vert + 1;
                triangles[tri + 4] = vert + XSize + 1;
                triangles[tri + 5] = vert + XSize + 2;

                vert++; // as it loops through it shifts the triangles across by one verticy thus putting them next to eachother
                tri += 6; // everytime this loops adds six triangles
            }
            vert++;
           
        }
    }
    void UpdateTerrain()
    {
        LandMesh.Clear(); // clears any previous mesh loaded in to this object in the scene

        LandMesh.vertices = vertices;
        LandMesh.triangles = triangles;

        LandMesh.RecalculateNormals();// recalculating the lighting and rendering
        
    }
    private void OnDrawGizmos()
    {
        for(int i = 0; i< vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i],0.05f);
        }
    }
}
