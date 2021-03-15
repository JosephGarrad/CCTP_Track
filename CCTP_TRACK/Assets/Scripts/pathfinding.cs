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
    public NodeScript Neighbour;
    public NodeScript currentNode;
    private Vector3 TrackRot;
    public int Retries = 0;
    public int Hill_Amount;
    List<NodeScript > oldtrack = new List<NodeScript>();
    public int HillMovementCostToNeighb;
    private int FlatMovementCostToNeighb;
    List<NodeScript> path = new List<NodeScript>();
    public bool Hilly_track;
    public bool Quickest_track;
    public bool flat_track;
    private void Awake()
    {
        Grid = GetComponent<gride>(); //getting the gride script
        MS = MeshG.gameObject.GetComponent<MeshGenerator>();
    }
    private void Start()
    {
        
    }
    void Update()
    {
        findpath(MS.startPoint, MS.Endpoint,MS.MidPoint);
        retry_track();
       // Debug.Log("seker" + seeker.position);
    }
    void findpath(Vector3 startPos, Vector3 targetpos, Vector3 MidPointPos)
    {
       NodeScript startnode = Grid.NodefromWorldPoint(startPos);
        NodeScript targetnode = Grid.NodefromWorldPoint(targetpos);
        NodeScript MidPointnode = Grid.NodefromWorldPoint(MidPointPos);
        List<NodeScript> openSet = new List<NodeScript>();
        HashSet<NodeScript> closeSet = new HashSet<NodeScript>();
        openSet.Add(startnode);

        while(openSet.Count > 0 )
        {
            NodeScript currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if(Retries == 0 && openSet[i].fcost < currentNode.fcost || openSet[i].fcost == currentNode.fcost && openSet[i].hCost < currentNode.hCost) // checking to see if the next node has a lower cost, if it does make it the current node
                {
                    currentNode = openSet[i];
                  
                }
               
            }
            openSet.Remove(currentNode); // when you move to the next node taske the cuuent node out of the open list so we cant go back to it 
            closeSet.Add(currentNode); // add to the closed list so we know its been tested;

            //if(currentNode == targetnode) // if we have hit out target then we are complete and leave the loop
            //{

            //    reTracePath(startnode, targetnode);

            //    return;
            //}

            //if (currentNode == MidPointnode)
            //{
            //    reTracePath(startnode, MidPointnode);
            //    Debug.Log(targetnode.worldPos);
            //    targetnode = startnode;
            //    startnode = MidPointnode;
            //    Debug.Log(targetnode.worldPos);
            //    return;
            //    //reTracePath(MidPointnode, targetnode);
            //}
            if (currentNode == targetnode) // if we have hit out target then we are complete and leave the loop
            {

                reTracePath(startnode, targetnode);

                return;
            }


            if (Quickest_track)
            {
                foreach (NodeScript Neighbour in Grid.GetNeighbours(currentNode))
                {
                    if ( !Neighbour.walkable || closeSet.Contains(Neighbour))
                    {
                        continue;
                    }
                    int newMovementCostToNeighb = currentNode.gCost + getDistance(currentNode, Neighbour);
                    if (newMovementCostToNeighb < Neighbour.gCost || !openSet.Contains(Neighbour)) // checking to see if the nieghbour has a shorter path then the others or that it is not in the open list
                    { // if the neighbour is shorter then set its cost to the distance it is away from the target node
                        Neighbour.gCost = newMovementCostToNeighb;
                        Neighbour.hCost = getDistance(Neighbour, targetnode);
                        Neighbour.parent = currentNode;

                        if (!openSet.Contains(Neighbour))
                        {
                            openSet.Add(Neighbour);
                        }
                    }

                }
            }
            if (Hilly_track)
            {
                
                foreach (NodeScript Neighbour in Grid.GetNeighbours(currentNode))
                {
                    
                  
                    if (!Neighbour.walkable || closeSet.Contains(Neighbour))
                    {
                        continue;
                    }
                  
                    int newMovementCostToNeighb = currentNode.gCost + getDistance(currentNode, Neighbour);
                    if (newMovementCostToNeighb < Neighbour.gCost || !openSet.Contains(Neighbour))// && currentNode.worldPos.y! < currentNode.parent.worldPos.y) // checking to see if the nieghbour has a shorter path then the others or that it is not in the open list
                    { // if the neighbour is shorter then set its cost to the distance it is away from the target node
                        if (Neighbour.worldPos.y < currentNode.worldPos.y  )
                        {
                            currentNode.gCost += Hill_Amount;
                            HillMovementCostToNeighb = currentNode.gCost + getDistance(currentNode, Neighbour);
                            Neighbour.gCost = HillMovementCostToNeighb;
                        }
                        else
                        {
                            Neighbour.gCost = newMovementCostToNeighb;
                        }
                        
                        Neighbour.hCost = getDistance(Neighbour, targetnode);
                        Neighbour.parent = currentNode;

                        if (!openSet.Contains(Neighbour))
                        {
                            openSet.Add(Neighbour);
                        }
                    }

                }
            }
        }
    }
    //somewhere above i need to create a function that chnages the cost of the neghbour is they have a higher y position

    void reTracePath(NodeScript startnode, NodeScript endnode)
    {
       
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

    void retry_track()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
           
            Retries += 1;
            findpath(MS.startPoint, MS.Endpoint, MS.MidPoint);
        }
    }
}
