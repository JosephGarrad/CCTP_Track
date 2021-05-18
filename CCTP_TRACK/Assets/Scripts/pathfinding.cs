using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class pathfinding : MonoBehaviour
{
    public Canvas StartMenu;
    grid Grid;
    CamController CC;
    public GameObject CCobject;
    MeshGenerator MS;
    public InputField HillAmountINPUT;
    public Transform seeker;
        public  Transform Target;
    public GameObject MeshG;
    public NodeScript Neighbour;
    public bool generated;
    public NodeScript currentNode;
    private Vector3 TrackRot;
    public int Retries = 0;
    public int hillAmount;
    List<NodeScript > oldtrack = new List<NodeScript>();
    public int HillMovementCostToNeighb;
    private int FlatMovementCostToNeighb;
   public List<NodeScript> path = new List<NodeScript>();
    public bool hillyTrack = false;
    public bool quickestTrack = false;
    public bool flatTrack;
    public bool straightTrack = false;
    public bool circuitTrack = false;
    public GameObject StartPoint;
    public Vector3 midPoint;
    List<Vector3> Points = new List<Vector3>();
    List<NodeScript> allNodes = new List<NodeScript>();
    public bool midHit;
    private int tracksBuilt = 0;
    private void Awake()
    {
        Grid = GetComponent<grid>(); //getting the gride script
        MS = MeshG.gameObject.GetComponent<MeshGenerator>();
        CC = CCobject.gameObject.GetComponent<CamController>();
    }
    private void Start()
    {
       
        Points.Add(MS.startPoint); // adding the points that go into the circuit to a new list
        Points.Add(MS.MidPoint);
        Points.Add(MS.Otherpoint);
        Points.Add(MS.Endpoint);
        Points.Add(MS.startPoint);


    }
    void Update()
    {
  

      
        retry_track();

    }
    public void Generator()
    {
       
        if (circuitTrack) // if circuit track is choosen then this is executed 
        {

            for (int i = 0; i < 4; i++) // loops through all points in the circuit
            {
                if (tracksBuilt != 4)
                {
                    ToNextPoint(Points[i], Points[i + 1]); // creates a path between the two current points
                    tracksBuilt++;
                }
            }
        }
        if(straightTrack) // if straight track is choosen then this is executed.
        {
            findpath(MS.startPoint, MS.Endpoint); // as a straight path is only created between between the start and end point.
        }
        StartMenu.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        CC.Started = true;
    }

    void ToNextPoint(Vector3 Currentpoint, Vector3 NextPoint) // called when a circuit track is created
    {

            findpath(Currentpoint, NextPoint); 
   
     
    }
    void findpath(Vector3 startPos, Vector3 targetpos) // used to find the path between two points
    {
        NodeScript startnode = Grid.NodefromWorldPoint(startPos);
        NodeScript targetnode = Grid.NodefromWorldPoint(targetpos);
        NodeScript MidPointnode = Grid.NodefromWorldPoint(midPoint);
        List<NodeScript> openSet = new List<NodeScript>();
        HashSet<NodeScript> closeSet = new HashSet<NodeScript>();
    openSet.Add(startnode);

        while(openSet.Count > 0 )
        {
            NodeScript currentNode = openSet[0];
            for(int i = 1; i < openSet.Count; i++)
            {
                if (Retries == 0 && openSet[i].fcost < currentNode.fcost || openSet[i].fcost == currentNode.fcost && openSet[i].hCost < currentNode.hCost ) // checking to see if the next node has a lower cost, if it does make it the current node
                {
                    currentNode = openSet[i];
                   
                }
                allNodes.Add(currentNode);
            }
            openSet.Remove(currentNode); // when you move to the next node taske the cuuent node out of the open list so we cant go back to it 
            closeSet.Add(currentNode); // add to the closed list so we know its been tested;

        
            if (currentNode == targetnode) // if we have hit out target then we are complete and leave the loop
            {
      
                reTracePath(startnode, targetnode);
      
                return;
            }


            if (quickestTrack)
            {
                foreach (NodeScript Neighbour in Grid.GetNeighbours(currentNode))
                {
                    if ( !Neighbour.walkable || closeSet.Contains(Neighbour)|| allNodes.Contains(Neighbour))
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
            if (hillyTrack)
            {
                
                foreach (NodeScript Neighbour in Grid.GetNeighbours(currentNode))
                {
                    
                  
                    if (!Neighbour.walkable || closeSet.Contains(Neighbour))
                    {
                        continue;
                    }
                  
                    int newMovementCostToNeighb = currentNode.gCost + getDistance(currentNode, Neighbour);
                    if (newMovementCostToNeighb < Neighbour.gCost || !openSet.Contains(Neighbour)) // checking to see if the nieghbour has a shorter path then the others or that it is not in the open list
                    { // if the neighbour is shorter then set its cost to the distance it is away from the target node
                        if (Neighbour.worldPos.y < currentNode.worldPos.y  )
                        {
                            currentNode.gCost += hillAmount;
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
   
    void reTracePath(NodeScript startnode, NodeScript endnode) // retraces the path that has been created
    {
        
            NodeScript currentNode = endnode;
            while (currentNode != startnode)
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
            findpath(MS.startPoint, MS.Endpoint);
        }
    }
}
