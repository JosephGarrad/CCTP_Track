using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinding : MonoBehaviour
{
    gride Grid;
    MeshGenerator MS;
    public Transform seeker;
        public  Transform Target;
    public GameObject MeshG;
    private void Awake()
    {
        Grid = GetComponent<gride>(); //getting the gride script
        MS = MeshG.gameObject.GetComponent<MeshGenerator>();
    }
    void Update()
    {
        findpath(MS.startPoint, MS.Endpoint);
       // Debug.Log("seker" + seeker.position);
    }
    void findpath(Vector3 startPos, Vector3 targetpos)
    {
        NodeScript startnode = Grid.NodefromWorldPoint(startPos);
        NodeScript targetnode = Grid.NodefromWorldPoint(targetpos);
        List<NodeScript> openSet = new List<NodeScript>();
        HashSet<NodeScript> closeSet = new HashSet<NodeScript>();
        openSet.Add(startnode);

        while(openSet.Count > 0 )
        {
            NodeScript currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].fcost < currentNode.fcost || openSet[i].fcost == currentNode.fcost && openSet[i].hCost < currentNode.hCost) // checking to see if the next node has a lower cost, if it does make it the current node
                {
                    currentNode = openSet[i];
                }
            }
            openSet.Remove(currentNode); // when you move to the next node taske the cuuent node out of the open list so we cant go back to it 
            closeSet.Add(currentNode); // add to the closed list so we know its been tested;

            if(currentNode == targetnode) // if we have hit out target then we are complete and leave the loop
            {
                reTracePath(startnode, targetnode);
                return;
            }
            foreach(NodeScript neighbour in Grid.GetNeighbours(currentNode))
            {
                if(!neighbour.walkable || closeSet.Contains(neighbour))
                {
                    continue;
                }
                int newMovementCostToNeighb = currentNode.gCost + getDistance(currentNode, neighbour);
                if(newMovementCostToNeighb < neighbour.gCost || !openSet.Contains(neighbour)) // checking to see if the nieghbour has a shorter path then the others or that it is not in the open list
                { // if the neighbour is shorter then set its cost to the distance it is away from the target node
                    neighbour.gCost = newMovementCostToNeighb;
                    neighbour.hCost = getDistance(neighbour,targetnode);
                    neighbour.parent = currentNode;

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }

            }
        }
    }

    void reTracePath(NodeScript startnode, NodeScript endnode)
    {
        List<NodeScript> path = new List<NodeScript>();
        //Debug.Log(path[path.Count]);
        NodeScript currentNode = endnode;
        while(currentNode != startnode)
        {
            path.Add(currentNode);
            
            currentNode = currentNode.parent;

        }
       path.Reverse();
        Grid.path = path;
    }
    int getDistance(NodeScript a, NodeScript b)
    {
        int distanceX = Mathf.Abs(a.GridX - b.GridX);
        int distanceZ = Mathf.Abs(a.GridY - b.GridY);

        if (distanceX > distanceZ)
        {
            return 14 * distanceZ + 10 * (distanceX - distanceZ); // working out the distance between the start and end node and how much ti will cost
        }
        else
        {
            return 14 * distanceX + 10 * (distanceZ - distanceX);
        }
        
    }
}
