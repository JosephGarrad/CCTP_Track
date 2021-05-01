using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTextureToRoad : MonoBehaviour
{
    Material Mat;
    GameObject Player;
    public int Radius = 10;
    TrackBuilder TB;
    public GameObject TBS;
    void Start()
    {
        TB = TBS.gameObject.GetComponent<TrackBuilder>();
        // Get the material
        Mat = GetComponent<Renderer>().material;
        // Get the player object
        //Mat.SetFloat("_NumOfPeices", TB.TrackPeices.Count);
    }

    void Update()
    {
        // Set the player position in the shader file
        foreach (var tr in TB.TrackPeices)
        {
           
        }
        for(int i = 0; i <= TB.TrackPeices.Count; i++)
        {
           
           
            Mat.SetVector("_PlayerPos", TB.TrackPeices[i].gameObject.transform.position);
            Mat.SetFloat("_Dist", Radius);
        }
       
        // Set the distance or radius

    }
}
