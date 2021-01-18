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

        for (int i = 0; i < MG.VertIsies.Length; i++)
        {
            for (int j = 0; j < GD.path.Count; j++)
            {
                if (j == i)
                {

                    Instantiate(Cube,new Vector3(MG.VertIsies[i].x,MG.VertIsies[i].y,MG.VertIsies[i].z), Quaternion.identity);
            }
        }

        }
    }
}
