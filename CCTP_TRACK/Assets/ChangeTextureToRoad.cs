using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextureToRoad : MonoBehaviour
{
  Material Mat;
    GameObject[] Player;
    public int Radius = 10;
    TrackBuilder TB;
    public GameObject TBS;
    grid GD;
    private int Tracknum;
    public GameObject GridScript;
    Vector3[] array;
    void Start()
    {
        GD = GridScript.gameObject.GetComponent<grid>();
        TB = TBS.gameObject.GetComponent<TrackBuilder>();
    
       Mat = GetComponent<Renderer>().material;
        
    }

    void Update()
    {

       
            createDirtTrack();
        Mat.SetInt("Count", GD.path.Count);
    }
   public void createDirtTrack()
    {
        foreach (NodeScript n in GD.path)
        {
            if (GD.path.Contains(n)) // if the grid contains a path
            {
              

                Mat.SetVector("_NodePos", n.worldPos); // get the path nodes position and set the nodePose vector to it 
                Mat.SetFloat("_Dist", Radius);
            }
        
        }

    }
}
