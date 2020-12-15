using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node 
{
    public bool walkable;
    public Vector3 worldPos;
    public int GridX;
    public int GridY;
    public int gCost;
    public int hCost;
    public node parent;
    
    public node(bool _walkable, Vector3 _worldPos, int gridx, int gridy)
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
            return gCost + hCost;
        }
    }
}
