using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
   public Mesh LandMesh;
   public Vector3[] VertIsies;
  // public List<Vector3[]> Verticies2 = new List<Vector3[]>();
 
     public int[] triangles;
   

    [Header("Size of Terrain:")]

    public int XSize;
    public int ZSize;
    [Header("Height of Perlin Noise:")]

    public float HeightAcrossX;
    public float HeightAcrossZ;
    public float multiplyHeight;

    gride GS;
    public GameObject Gride;
    public Color[] colorMap;
    public float Freq_1;
    public float Freq_2;
    public float Freq_3;
    public float Amp_1;
    public float Amp_2;
    public float Amp_3;
    public Gradient Grad;
    public GameObject aStarManager;
    public GameObject Cube;
    float minHeight;
    float maxHeight;
    public Vector3[] StartPos;
    public float XVert;
    public float zVert;
    public float XVert2;
    public float zVert2;
    public float VertHeight;
    public Vector3 startPoint;
    public Vector3 Endpoint;
    public Vector3 MidPoint;
    public Vector3 Otherpoint;
    // Start is called before the first frame update
    void Start()
    {
        GS = Gride.gameObject.GetComponent<gride>();
        for (int i = 0; i < VertIsies.Length; i++)
        {
            VertHeight = LandMesh.vertices[i].y;
        }
       
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
        XVert = VertIsies[50].x;
        zVert = VertIsies[11].z;

        XVert2 = VertIsies[70].x;
        zVert2 = VertIsies[54].z;

        int zPos = Random.Range(1, 20000);
        int xPos = Random.Range(1, 20000);
        int zPos1 = Random.Range(1, 100);
        int xPos1 = Random.Range(1, 100);
        //for (int i = 0; i < mf.mesh.vertices.Length; i++)
        //{
        // for (int j = 0, x = 0; x <= XSize; x++)
        // {
        //for (int z = 0; z <= ZSize; z++,j++)
        //{
        startPoint = new Vector3(VertIsies[30].x, 0, VertIsies[30].z); // new Vector3(10, 0, 3/*zVert + 2*/) ; // adding 20 spaces onto the z vector
        Endpoint = new Vector3(VertIsies[150].x, 0, 0);//+ Vector3.forward * 71;
        MidPoint = new Vector3(VertIsies[xPos].x, 0, VertIsies[xPos].y);
        Otherpoint = new Vector3(VertIsies[zPos].x, 0, VertIsies[zPos].y); // make the position a verticie
        Debug.Log(xPos);
        Debug.Log(zPos);
        //j++;
        //}
        //}
        // if (startPoint == VertIsies[i])
        // {
        //     Instantiate(Cube, localtoworld.MultiplyPoint3x4(startPoint) , Quaternion.identity);
        //}


        //}
    }
       
    void CreateTerrain()
    {
        VertIsies = new Vector3[(XSize + 1) * (ZSize + 1)];
        // aStarManager.gameObject.GetComponent<gride>().createGrid();
        for (int j = 0, z = 0; z <= ZSize; z++)
        {
            for (int x = 0; x <= XSize; x++)
            {
                
                float Elevation = CalculateMultiNoise(x, z);
                VertIsies[j] = new Vector3(x, Elevation, z);
                //Debug.Log("vert" + VertIsies[j]);
                if(Elevation > maxHeight)
                {
                    maxHeight = Elevation;
                }
                if(Elevation < minHeight)
                {
                    minHeight = Elevation;
                }
                //GS.createGrid(z, x, j);
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

        colorMap = new Color[VertIsies.Length];
        for (int i = 0, z = 0; z < ZSize; z++)
        {
            for (int x = 0; x < XSize; x++)
            {
                float height = Mathf.InverseLerp(minHeight,maxHeight, VertIsies[i].y);// normalising the hieght of terrain to work with gradient;
                colorMap[i] = Grad.Evaluate(height); 
                i++;
            }
        }
    }
    void UpdateTerrain()
    {




       // texture.Apply();

        LandMesh.Clear(); // clears any previous mesh loaded in to this object in the scene

        LandMesh.vertices = VertIsies;
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
    private void OnDrawGizmos()
    {
        for(int i = 0; i< VertIsies.Length; i++)
        {
          //Gizmos.DrawSphere(VertIsies[i],0.5f);
        }
        
    }
}
