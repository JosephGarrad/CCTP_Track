using System.Collections;
using UnityEngine;


public class NodeScript 
{ 

    public bool walkable;
    public Vector3 worldPos;
    public int GridX;
    public int GridY;
       public int gridx;
    public int gridy;
    public int gCost;
    public int hCost;
 
    public NodeScript parent;
    
    public NodeScript(bool _walkable, Vector3 _worldPos, int gridx, int gridy)
    {
        walkable = _walkable;
        worldPos = _worldPos;
        GridX = gridx;
        GridY = gridy;
      
    }
    public int fcost
    {
        get
        {
            return gCost + hCost; // creating the fcost of the surronding nodes
        }
    }
}
