using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBuilder :  MonoBehaviour
{
    MeshGenerator MG;
    public GameObject Track;
    public GameObject TerrainGenerator;
    gride GD;
    public GameObject GridScript;
    public GameObject Pathfinding;
    pathfinding PS;
    //NodeScript ND;
    //public GameObject NodeScript;
    public GameObject Cube;
    bool Built = false;
    private GameObject rTrack;
    gride Grid;
    int endpeice;
    private int Tracknum;
    private Vector3 Direction;
    private Quaternion LookRotation;
    public List<GameObject> TrackPeices = new List<GameObject>();
    //int offset = 197;
    // Start is called before the first frame update
    void Start()
    {
        MG = TerrainGenerator.gameObject.GetComponent<MeshGenerator>();
        GD = GridScript.gameObject.GetComponent<gride>();
        PS = Pathfinding.gameObject.GetComponent<pathfinding>();
       // ND = NodeScript.gameObject.GetComponent<NodeScript>();
        Grid = GetComponent<gride>();

    }

    // Update is called once per frame
    void Update()
    {
       if(!Built)
        {
            build();
            Built = true;
        }
       
    }
   void build()
   {
        
            foreach (NodeScript n in GD.grid)
            {
                if (GD.path.Contains(n) )
                {
                rTrack = Instantiate(Cube, n.worldPos, Quaternion.identity);
                TrackPeices.Add(rTrack);
                Direction =  (rTrack.transform.position -TrackPeices[Tracknum + 1].gameObject.transform.position ).normalized;
                LookRotation = Quaternion.LookRotation(Direction);


                rTrack.transform.localRotation = Quaternion.LookRotation(Direction);

                // rTrack.transform.LookAt(TrackPeices[Tracknum+1].gameObject.transform.position);


                // rTrack = Instantiate(Cube, n.worldPos, Quaternion.identity);
                // TrackPeices.Add(rTrack);

                // Vector3 direction = (TrackPeices[Tracknum + 1].gameObject.transform.position - rTrack.transform.position).normalized;
                // float distance = Vector3.Distance(rTrack.transform.position, TrackPeices[Tracknum + 1].gameObject.transform.position);
                // float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

                // //rectTransform.anchoredPosition = dotPositionA + direction * distance * 0.5f; // Placed in middle between A + B
                // //rTrack.transform.localScale = new Vector3(distance, transform.localScale.x, transform.localScale.z);
                ////rTrack.transform.localEulerAngles = new Vector3(angle, angle,angle);

                // Debug.Log(angle);


                if (Tracknum <= TrackPeices.Count)
                    continue;

                Tracknum++;
           
            }
        }

        
        endpeice =TrackPeices.Count;
    
    }
}
