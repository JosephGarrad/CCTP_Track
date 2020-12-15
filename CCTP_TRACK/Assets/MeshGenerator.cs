﻿using System.Collections;
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
  
    public Color[] colorMap;
    public float Freq_1;
    public float Freq_2;
    public float Freq_3;
    public float Amp_1;
    public float Amp_2;
    public float Amp_3;
    public Gradient Grad;
   
    public GameObject Cube;
    float minHeight;
    float maxHeight;
    public Vector3[] StartPos;
    public float XVert;
    public float zVert;
    // Start is called before the first frame update
    void Start()
    {
     
        LandMesh = new Mesh();
        GetComponent<MeshFilter>().mesh = LandMesh;
        LandMesh.name = "RaceTrack Terrain";

        CreateTerrain();
         UpdateTerrain();
        placeTrack();
    }
 
    private void Update()
    {
       
        
    }

    void placeTrack()
    {

        var matrix = transform.localToWorldMatrix;

        Matrix4x4 localtoworld = transform.localToWorldMatrix;
        MeshFilter mf = this.GetComponent<MeshFilter>();
        XVert = vertices[50].x;
        zVert = vertices[2].z;
    

            for (int i = 0; i < mf.mesh.vertices.Length; i++)
        {
            Vector3 startPoint = new Vector3(XVert, vertices[i].y, vertices[i].z/*zVert + 2*/) ;

            if (startPoint == vertices[i])
            {
                Instantiate(Cube, localtoworld.MultiplyPoint3x4(startPoint) , Quaternion.identity);
           }


        }
    }
       
    void CreateTerrain()
    {
        vertices = new Vector3[(XSize + 1) * (ZSize + 1)];

        for (int j = 0, z = 0; z <= ZSize; z++)
        {
            for (int x = 0; x <= XSize; x++)
            {

                float Elevation = CalculateMultiNoise(x, z);
                vertices[j] = new Vector3(x, Elevation, z);

                if(Elevation > maxHeight)
                {
                    maxHeight = Elevation;
                }
                if(Elevation < minHeight)
                {
                    minHeight = Elevation;
                }
                j++;

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

        colorMap = new Color[vertices.Length];
        for (int i = 0, z = 0; z < ZSize; z++)
        {
            for (int x = 0; x < XSize; x++)
            {
                float height = Mathf.InverseLerp(minHeight,maxHeight, vertices[i].y);// normalising the hieght of terrain to work with gradient;
                colorMap[i] = Grad.Evaluate(height); 
                i++;
            }
        }
    }
    void UpdateTerrain()
    {

        //Color[] colourMap = new Color[XSize * ZSize];
        //Texture2D texture = new Texture2D(XSize, ZSize);

        //for (int x = 0; x < XSize; x++)
        //{
        //    for(int z=0; z<ZSize;z++)
        //        { 
        //    float currentHeight = CalculateMultiNoise(x, z);
        //        for(int i = 0; i<Regions.Length; i++)
        //        {
        //            if(currentHeight<= Regions[i].height)
        //            {

        //                //colourMap[z * XSize + x] = Regions[i].Colour;
        //                texture.SetPixel(x,z,Regions[i].Colour);
        //                break;
        //            }
        //        }
        //    }

        //}


       // texture.Apply();

        LandMesh.Clear(); // clears any previous mesh loaded in to this object in the scene

        LandMesh.vertices = vertices;
        LandMesh.triangles = triangles;
        
        LandMesh.RecalculateNormals();
        LandMesh.colors = colorMap;// recalculating the lighting and rendering
       // RD.sharedMaterial.SetTexture("texture",texture);
    }
    //public Texture2D TextureMap()
    //{
       
    //}
float CalculateMultiNoise(float x, float z)
    {
      
            float[] OctaveFreq = new float[] { Freq_1, Freq_2, Freq_3 };
            float[] OctaveAmpl = new float[] { Amp_1, Amp_2, Amp_3 };
            float y = 0;
            for (int i = 0; i < OctaveFreq.Length; i++)
            {
                y += OctaveAmpl[i] * Mathf.PerlinNoise(OctaveFreq[i] * x + .3f,
                    OctaveFreq[i] * z + .3f) * multiplyHeight;
            }
        
        return y;
    }
 
    //[System.Serializable]
    //public struct TerrainTypes
    //{
    //    public string name;
    //    public float height;
    //    public Color Colour;

    //}
    private void OnDrawGizmos()
    {
        //for(int i = 0; i< vertices.Length; i++)
        //{
         //   Gizmos.DrawSphere(vertices[i],0.05f);
       // }
        
    }
}
