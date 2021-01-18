using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder :  MonoBehaviour
{
    MeshGenerator MG;
    public GameObject TerrainGenerator;
    gride GD;
    public GameObject GridScript;
    public GameObject Cube;
    bool Built = false;
    gride Grid;
    int offset = 197;
    // Start is called before the first frame update
    void Start()
    {
        MG = TerrainGenerator.gameObject.GetComponent<MeshGenerator>();
        GD = GridScript.gameObject.GetComponent<gride>();
        Grid = GetComponent<gride>();

    }

    // Update is called once per frame
    void Update()
    {
       if(!Built)
        {
            build();
            Built = true;
        }
    }
    void build()
    {

        //foreach (NodeScript n in Grid.grid)
        //{
        //    if (GD.path.Contains(n))
        //    {
        //        Debug.Log("cum");
        //    }
        //}

        for (int i = 0; i < MG.VertIsies.Length; i++)// looping through the verticies
        {
            
            Debug.Log("Node X " + GD.path[i].GridX);
            Debug.Log("Node Z " + GD.path[i].GridY);

            Debug.Log("vert X " + MG.VertIsies[i].x);
            Debug.Log("vert Z " + MG.VertIsies[i].z);

            if (Mathf.Floor(GD.path[i].worldPos.x)- offset == Mathf.Floor(MG.VertIsies[i].x))
            {
                Debug.Log("cummies");
               
            }
            else
            {
                Debug.Log("Max");

            }
            offset -= 2;


            //Instantiate(Cube,new Vector3(MG.VertIsies[i].x,MG.VertIsies[i].y,MG.VertIsies[i].z), Quaternion.identity);
            // }


        }
    }
}
