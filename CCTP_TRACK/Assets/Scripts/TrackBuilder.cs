using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder :  MonoBehaviour
{
    MeshGenerator MG;
    public GameObject Track;
    public GameObject TerrainGenerator;
    gride GD;
    public GameObject GridScript;
    public GameObject Pathfinding;
    pathfinding PS;
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
        PS = Pathfinding.gameObject.GetComponent<pathfinding>();
       // ND = NodeScript.gameObject.GetComponent<NodeScript>();
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

        foreach (NodeScript n in GD.grid)
        {
            if(GD.path.Contains(n))
            {
                Instantiate(Cube, n.worldPos,Quaternion.identity);
                //Debug.Log("placeTRack");
            }
        }

        //for (int j = 0, z = 0; z <= MG.ZSize; z++)
        //{
        //    for (int x = 0; x <= MG.XSize; x++)
        //    {

        //        //  Debug.Log("Path X " + GD.path[j].worldPos.x);
        //        // Debug.Log("Vert X " + MG.VertIsies[j].x);

        //        Vector3 pathVec = new Vector3(GD.path[j].worldPos.x, GD.path[j].worldPos.y, GD.path[j].worldPos.z);
        //        if (pathVec == MG.VertIsies[j])
        //        {
        //            Debug.Log("cummies");
        //            // Instantiate(Cube, new Vector3(MG.VertIsies[j].x, MG.VertIsies[j].y, MG.VertIsies[j].z), Quaternion.identity);
        //        }
        //        else
        //        {
        //            //Debug.Log("Path: " + pathVec);
        //            //Debug.Log("Vert: " + MG.VertIsies[j]);

        //        }
        //        //if(j == 32)
        //        {
        //            Debug.Log("Vert: " + MG.VertIsies[j]);
        //        }
        //        j++;
        //    }

        //}

    }
}
