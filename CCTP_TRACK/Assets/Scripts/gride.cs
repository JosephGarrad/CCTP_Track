using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gride : MonoBehaviour
{
    public LayerMask unwalkable;

    public Vector3 Worldsize;
    float nodeDiam;
    public NodeScript[,] grid;
    MeshGenerator MG;
    pathfinding PF;
    public GameObject TerrainGenerator;
    public GameObject Pathfinder;
    public int GridXsize;
    public int GridZsize;
    public int GridHeight;
    public float VerticiesRadius;
    bool walkable;
    public GameObject Block;
    public Vector3 worldpoint;
    // Start is called before the first frame update
    private void Start()
    {
        PF = Pathfinder.gameObject.GetComponent<pathfinding>();
        MG = TerrainGenerator.gameObject.GetComponent<MeshGenerator>();



        nodeDiam = VerticiesRadius;
       // GridXsize = Mathf.RoundToInt(Worldsize.x / nodeDiam);
       // GridZsize = Mathf.RoundToInt(Worldsize.y / nodeDiam);
        GridXsize = MG.XSize;
        GridZsize = MG.ZSize;

    }
    private void Update()
    {
        createGrid();
    }
    public void createGrid()

    {
        
        grid = new NodeScript[GridZsize, GridXsize];
        for (int i = 0; i < MG.VertIsies.Length; i++)
        {
           
        }
        for (int i = 0, z = 0; z < GridZsize;z++)
        {
            

            for (int  x = 0; x < GridXsize; x++)
            {
                //put this into a function and cal it when the veerticies are made?
                //kill myself?
                //god this pain is endl
                /*  Vector3 worldpoint = new Vector3(MG.VertIsies[i].x, MG.VertIsies[i].y, MG.VertIsies[i].z);*///new Vector3(0, 0, 0) + Vector3.right * (x * nodeDiam + VerticiesRadius) + Vector3.forward * (z * nodeDiam + VerticiesRadius) ;
                Vector3 worldpoint = new Vector3(MG.VertIsies[i].x, MG.VertIsies[i].y, MG.VertIsies[i].z);
                if (MG.VertIsies[i].y >= 14 || MG.VertIsies[i].y <= 5)     //Debug.Log("Nodes" + worldpoint);
                {
                    Instantiate(Block, MG.VertIsies[i], Quaternion.identity);
                }
            
                walkable = !(Physics.CheckSphere(worldpoint, VerticiesRadius, unwalkable));
                    grid[z, x] = new NodeScript(walkable, worldpoint, z, x);
                    i++;
               
            }
        }
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
        //float percentx = (worldPos.x + Worldsize.x /2) / Worldsize.x;
        //float percentz = (worldPos.z + Worldsize.y/2) / Worldsize.y;
        //percentx = Mathf.Clamp01(percentx); // clamping an nomralising the position. 
        //percentz = Mathf.Clamp01(percentz);

        int x = (int)worldPos.x; //Mathf.RoundToInt((GridXsize - 1) * percentx);
        int z = (int)worldPos.z;
        //int y = (int)worldPos.y;//Mathf.RoundToInt((GridZsize - 1) * percentz);
        //Debug.Log(worldPos);
        return grid[z, x];
       
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
