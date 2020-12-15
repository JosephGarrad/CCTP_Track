using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathfinding : MonoBehaviour
{
    gride Grid;

    public Transform seeker, target;
    private void Awake()
    {
        Grid = GetComponent<gride>(); //getting the gride script
    }
    void Update()
    {
        findpath(seeker.position, target.position);
    }
    void findpath(Vector3 startPos, Vector3 targetpos)
    {
        node startnode = Grid.NodefromWorldPoint(startPos);
        node targetnode = Grid.NodefromWorldPoint(targetpos);

        List<node> openSet = new List<node>();
        HashSet<node> closeSet = new HashSet<node>();
        openSet.Add(startnode);

        while(openSet.Count > 0 )
        {
            node currentNode = openSet[0];
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
            foreach( node neighbour in Grid.GetNeighbours(currentNode))
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

    void reTracePath(node startnode, node endnode)
    {
        List<node> path = new List<node>();
        node currentNode = endnode;
        while(currentNode != startnode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;

        }
        path.Reverse();
        Grid.path = path;
    }
    int getDistance(node a, node b)
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
