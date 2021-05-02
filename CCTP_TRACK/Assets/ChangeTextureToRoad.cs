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
    gride GD;
    private int Tracknum;
    public GameObject GridScript;
    void Start()
    {
        GD = GridScript.gameObject.GetComponent<gride>();
        TB = TBS.gameObject.GetComponent<TrackBuilder>();
        // Get the material
       Mat = GetComponent<Renderer>().material;
        // Get the player object
       
    }

    void Update()
    {

        // Mat.SetFloat("_NumOfPeices", TB.TrackPeices.Count);


        //        // Set the player position in the shader file
        //        foreach (var tr in TB.TrackPeices)
        //{

        //}
        //  Player = GameObject.FindGameObjectsWithTag("Track");
        //  Debug.Log(Player.Length);

        // Set the distance or radius
       
            createDirtTrack();
        
    }
   public void createDirtTrack()
    {
        foreach (NodeScript n in GD.path)
        {
            if (GD.path.Contains(n))
            {
               // Material Mat = new Material(GetComponent<Renderer>().material);
                //GetComponent<Renderer>().material = Mat;

                Mat.SetVector("_NodePos", n.worldPos);
                Mat.SetFloat("_Dist", Radius);
            }
        
        }
      
        //for (int i = 0; i <= Player.Length; i++)
        //{
        //    Debug.Log(i);
        //    Mat.SetVector("_PlayerPos", Player[i].gameObject.transform.position);
        //    Mat.SetFloat("_Dist", Radius);

        //}
    }
}
