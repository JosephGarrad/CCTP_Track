using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gride : MonoBehaviour
{
    public LayerMask unwalkable;
  
    public Vector2 Worldsize;
    float nodeDiam;
    NodeScript[,] grid;
    public int XSize;
    public int ZSize;
    public float VerticiesRadius;
    // Start is called before the first frame update
    private void Start()
    {
       
        //grid = go.gameObject.GetComponent<NodeScript[,]>();

        nodeDiam = VerticiesRadius * 2;
        XSize = Mathf.RoundToInt(Worldsize.x / nodeDiam);
        ZSize = Mathf.RoundToInt(Worldsize.y / nodeDiam);
        createGrid();
    }
    void createGrid()
    {

        grid = new NodeScript[XSize, ZSize];
        Vector3 worldbottomLeft = transform.position - Vector3.right * Worldsize.x / 2 - Vector3.forward * Worldsize.y / 2;
        
        for (int x = 0; x < XSize; x++)
        {
            for (int z = 0; z < ZSize; z++)
            {
                Vector3 worldpoint = worldbottomLeft + Vector3.right * (x * nodeDiam + VerticiesRadius) + Vector3.forward * (z * nodeDiam + VerticiesRadius);
                bool walkable = !(Physics.CheckSphere(worldpoint, VerticiesRadius, unwalkable));
                grid[x, z] = new NodeScript(walkable, worldpoint,x,z);
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
                if (checkX >= 0 && checkX < XSize && checkZ >= 0 && checkZ < ZSize)
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
        float percentx = (worldPos.x + Worldsize.x / 2) / Worldsize.x;
        float percentz = (worldPos.z + Worldsize.y / 2) / Worldsize.y;
        percentx = Mathf.Clamp01(percentx); // clamping an nomralising the position. 
        percentz = Mathf.Clamp01(percentz);

        int x =  Mathf.RoundToInt((XSize - 1) * percentx);
        int y = Mathf.RoundToInt((ZSize - 1) * percentz);
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
