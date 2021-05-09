﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gride : MonoBehaviour
{
    public LayerMask unwalkable;

    public Vector3 Worldsize;
    float nodeDiam;
    public NodeScript[,] grid;
    MeshGenerator MG;
    pathfinding PF;
    public bool gridmade;
    public InputField Mheight;
    public InputField MDepth;
    public GameObject TerrainGenerator;
    public GameObject Pathfinder;
    public int GridXsize;
    public int GridZsize;
    public int GridHeight;
    public float VerticiesRadius;
    bool walkable;
    private int Tracksize = 1;
    public GameObject Block;
    public Vector3 worldpoint;
    public int maxHeight;
    public int minDepth;
    // Start is called before the first frame update
    private void Start()
    {
        PF = Pathfinder.gameObject.GetComponent<pathfinding>();
        MG = TerrainGenerator.gameObject.GetComponent<MeshGenerator>();



       nodeDiam = VerticiesRadius*1;
     
        GridXsize = MG.XSize;
        GridZsize = MG.ZSize;
       

    }
    private void Update()
    {

       

        createGrid();
        
    }
    public void createGrid()

    {
       
        grid = new NodeScript[GridXsize, GridZsize];
        Vector3 BottomLeft = transform.position - Vector3.right * Worldsize.x / 2 - Vector3.forward * Worldsize.z / 2;
        for (int i = 0; i < MG.VertIsies.Length; i++)
        {
           
        }
        for (int i = 0, x = 0; x < GridXsize;x++)
        {
            

            for (int  z = 0; z < GridZsize; z++)
            {
               
                Vector3 worldpoint = new Vector3(MG.VertIsies[i].x, MG.VertIsies[i].y, MG.VertIsies[i].z);
                if (MG.VertIsies[i].y >= maxHeight || MG.VertIsies[i].y <= minDepth)  
               {
                Instantiate(Block, MG.VertIsies[i], Quaternion.identity);
                }
                float PosX = x * Tracksize;
                float posZ = z * -Tracksize;
                walkable = !(Physics.CheckSphere(worldpoint, VerticiesRadius, unwalkable));
                    grid[x, z] = new NodeScript(walkable, worldpoint, x, z);
                    i++;
               
            }
        }
        gridmade = true;
    }



    public List<NodeScript> GetNeighbours(NodeScript Node)
    {
        List<NodeScript> neighbours = new List<NodeScript>();

        for (int z = -1; z <= 1; z++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x == 0 && z == 0)
                {
                    continue;
                }
                int checkX = Node.GridX + x;
                int checkZ = Node.GridY + z;
                if (checkX >= 0 && checkX < GridXsize && checkZ >= 0 && checkZ < GridZsize)
                {
                    neighbours.Add(grid[checkZ, checkX]);
                }
            }
        }
        // going around the current nodes x and z axis and checking for all the surronding nodes and if they are not the current node adding them to the neighbours list
        return neighbours;
    }
    public NodeScript NodefromWorldPoint(Vector3 worldPos) // getting the position of the current node aka player node
    {


        int x = (int)worldPos.x;
      
        int z = (int)worldPos.z;
    
        return grid[x, z];

    }
   


    public List<NodeScript> path;
    private void OnDrawGizmos()
    {
     
        if (grid != null)
        {
          
            foreach (NodeScript n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
             
                if (path != null)
                {
                   if(path.Contains(n))
                    {
                        Gizmos.color = Color.black;
                    }
                }
            

                Gizmos.DrawCube(n.worldPos, Vector3.one * (nodeDiam - 0.1f));
               
            }
        }
    }
}
