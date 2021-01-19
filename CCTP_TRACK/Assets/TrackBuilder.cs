using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder :  MonoBehaviour
{
    MeshGenerator MG;
    public GameObject TerrainGenerator;
    gride GD;
    public GameObject GridScript;
    NodeScript ND;
    public GameObject NodeScript;
    public GameObject Cube;
    bool Built = false;
    gride Grid;
    //int offset = 197;
    // Start is called before the first frame update
    void Start()
    {
        MG = TerrainGenerator.gameObject.GetComponent<MeshGenerator>();
        GD = GridScript.gameObject.GetComponent<gride>();
        ND = NodeScript.gameObject.GetComponent<NodeScript>();
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

        //for (int i = 0; i < MG.VertIsies.Length; i++)// looping through the verticies
        //{

            //Debug.Log("Path X " + GD.path[i].worldPos);
            //Debug.Log("Vert X " + MG.VertIsies[i]);
            ////Debug.Log("Node X " + GD.path[i].worldPos);
            ////Debug.Log("Node Z " + GD.path[i].worldPos);

            ////Debug.Log("vert X " + MG.VertIsies[i].x);
            ////Debug.Log("vert Z " + MG.VertIsies[i].z);
            //Vector3 pathVec = new Vector3(GD.path[i].worldPos.x, GD.path[i].worldPos.y, GD.path[i].worldPos.z);
            //if (pathVec.x == MG.VertIsies[i].x && pathVec.z == MG.VertIsies[i].z)
            //{
            //    //Debug.Log("cummies");
            //    Instantiate(Cube, new Vector3(MG.VertIsies[i].x, MG.VertIsies[i].y, MG.VertIsies[i].z), Quaternion.identity);
            //}
            //else
            //{
            //    Debug.Log("Max");

            //}
            //offset += 1;

            for (int j = 0, z = 0; z <= MG.ZSize; z++)
           {
                for (int x = 0; x <= MG.XSize; x++)
               {

                Debug.Log("Path X " + GD.path[j].worldPos.x);
                Debug.Log("Vert X " + MG.VertIsies[j].x);
                //Debug.Log("Node X " + GD.path[i].worldPos);
                //Debug.Log("Node Z " + GD.path[i].worldPos);

                //Debug.Log("vert X " + MG.VertIsies[i].x);
                //Debug.Log("vert Z " + MG.VertIsies[i].z);
                Vector3 pathVec = new Vector3(GD.path[j].worldPos.x -3, GD.path[j].worldPos.y, GD.path[j].worldPos.z);
                if (pathVec.x == MG.VertIsies[j].x )
                {
                    Debug.Log("cummies");
                    Instantiate(Cube, new Vector3(MG.VertIsies[j].x, MG.VertIsies[j].y, MG.VertIsies[j].z), Quaternion.identity);
                }
                else
                {
                    Debug.Log("Max");

                }

               
               }
            j++;
        }
        
    }
}
