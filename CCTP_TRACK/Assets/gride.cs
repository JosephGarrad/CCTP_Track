﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gride : MonoBehaviour
{
    public LayerMask unwalkable;
  
    public Vector3 Worldsize;
    float nodeDiam;
   public NodeScript[,] grid;
    MeshGenerator MG;
    public GameObject TerrainGenerator;
    public int GridXsize;
    public int GridZsize;
    public int GridHeight;
    public float VerticiesRadius;
    bool walkable;
    // Start is called before the first frame update
    private void Start()
    {

        MG = TerrainGenerator.gameObject.GetComponent<MeshGenerator>();
         
   

        nodeDiam = VerticiesRadius ;
        GridXsize = Mathf.RoundToInt(Worldsize.x / nodeDiam);
        GridZsize = Mathf.RoundToInt(Worldsize.y / nodeDiam);
        GridXsize = MG.XSize;
        GridZsize = MG.ZSize;
        createGrid();
    }
    private void Update()
    {
       
    }
   public void createGrid()
    {

        grid = new NodeScript[GridXsize, GridZsize];
       
        
        for (int i = 0, x = 0; x < GridXsize; x++)
        {
            for (int z = 0; z < GridZsize; z++)
            {

                Vector3 worldpoint = new Vector3(MG.VertIsies[i].x, MG.VertIsies[i].y, MG.VertIsies[i].z);//new Vector3(0,0,0) + Vector3.right * (x * nodeDiam + VerticiesRadius) + Vector3.forward * (z * nodeDiam + VerticiesRadius);
              
                     walkable = !(Physics.CheckSphere(worldpoint, VerticiesRadius, unwalkable));
                    grid[x, z] = new NodeScript(walkable, worldpoint, x, z);
               // Debug.Log("Nodes" + worldpoint);
               // Debug.Log("verts" + new Vector3(MG.VertIsies[i].x, MG.VertIsies[i].y, MG.VertIsies[i].z));
               i++;
            }
            
        }
    }

    public List<NodeScript> GetNeighbours(NodeScript Node)
    {
        List<NodeScript> neighbours = new List<NodeScript>();

        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0)
                {
                    continue;
                }
                int checkX = Node.GridX + x;
                int checkZ = Node.GridY + z;
                if (checkX >= 0 && checkX < GridXsize && checkZ >= 0 && checkZ < GridZsize)
                {
                    neighbours.Add(grid[checkX, checkZ]);
                }
            }
        }
        // going around the current nodes x and z axis and checking for all the surronding nodes and if they are not the current node adding them to the neighbours list
        return neighbours;
    }
    public NodeScript NodefromWorldPoint(Vector3 worldPos) // getting the position of the current node aka player node
    {
        float percentx = (worldPos.x + Worldsize.x /2) / Worldsize.x;
        float percentz = (worldPos.z + Worldsize.y/2) / Worldsize.y;
        percentx = Mathf.Clamp01(percentx); // clamping an nomralising the position. 
        percentz = Mathf.Clamp01(percentz);

        int x = (int)worldPos.x; //Mathf.RoundToInt((GridXsize - 1) * percentx);
        int y = (int)worldPos.z;//Mathf.RoundToInt((GridZsize - 1) * percentz);
        //Debug.Log(worldPos);
        return grid[x, y];
       
    }

   


    public List<NodeScript> path;
    private void OnDrawGizmos()
    {
     
        if (grid != null)
        {
            //nodeScript startNode = NodefromWorldPoint(startPoint.position);
            foreach (NodeScript n in grid)
            {
                Gizmos.color = (n.walkable) ? Color.white : Color.red;
                //if (startNode == n)
               // {
                 //   Gizmos.color = Color.green;
               // }
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
