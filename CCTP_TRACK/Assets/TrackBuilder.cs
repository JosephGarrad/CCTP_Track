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
    public Terrain raceterrain;
    public GameObject Cube;
   public bool Built = false;
    float m_AngleX;
    public GameObject rTrack;
    gride Grid;

    ChangeTextureToRoad CTTR;
    public GameObject Trackchnager;
    int endpeice;
    private int Tracknum;
    private Vector3 Direction;
    private Quaternion LookRotation;
    public List<GameObject> TrackPeices = new List<GameObject>();
    float Dis;
    Mesh nowmesh;
    Mesh NextMesh;
    //int offset = 197;
    // Start is called before the first frame update
    void Start()
    {
        MG = TerrainGenerator.gameObject.GetComponent<MeshGenerator>();
        GD = GridScript.gameObject.GetComponent<gride>();
        PS = Pathfinding.gameObject.GetComponent<pathfinding>();
        CTTR = Trackchnager.gameObject.GetComponent<ChangeTextureToRoad>();
       // ND = NodeScript.gameObject.GetComponent<NodeScript>();
        Grid = GetComponent<gride>();
       
    }

    // Update is called once per frame
    void Update()
    {
       if(!Built)
        {
            build();
           stretch();
            Built = true;
        }
       
        rotate();

       

    }
   void build()
   {
        
            foreach (NodeScript n in GD.path)
            {
                if (GD.path.Contains(n) )
                {
                rTrack = Instantiate(Cube,new Vector3(n.worldPos.x,n.worldPos.y,n.worldPos.z), Quaternion.identity);
                TrackPeices.Add(rTrack);
                rTrack.name = ("Track");



                 //Direction = new Vector3(rTrack.transform.position.x -TrackPeices[Tracknum + 1].gameObject.transform.position.x, rTrack.transform.position.y - TrackPeices[Tracknum + 1].gameObject.transform.position.y, rTrack.transform.position.z - TrackPeices[Tracknum + 1].gameObject.transform.position.z).normalized;
               
                // chnage the direction of each sqaure in its own script 
                //get the list and it it tht way 
                
                
                
                
                // LookRotation = Quaternion.LookRotation(Direction);
                //for (int i = 1; i < TrackPeices.Count; i++)
                //{
                //   // Vector3 CPos = new Vector3(rTrack.transform.position.x + TrackPeices[i - 1].transform.position.x, rTrack.transform.position.z + TrackPeices[i - 1].transform.position.z) / 2;
                //    float scaleZ = Mathf.Abs(TrackPeices[i].transform.position.z + TrackPeices[i + 1].transform.position.z);
                //    rTrack.transform.localScale = new Vector3(1,1,scaleZ);
                //}

                //for (int i = 1; i < TrackPeices.Count; i++)
                //{
                //    Dis = Vector3.Distance(rTrack.transform.position, TrackPeices[i - 1].transform.position);
                //}
                //Vector3 newScale = Dis;
                //rTrack.transform.localScale = new Vector3(rTrack.transform.localScale.x, rTrack.transform.localScale.y, newScale.z);


               // rTrack.transform.localRotation = Quaternion.LookRotation(Direction);

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
    void rotate()
    {
        for (int i = 0; i < TrackPeices.Count; i++)
        {
            Direction = (TrackPeices[i].gameObject.transform.position - TrackPeices[i + 1].gameObject.transform.position);
            //m_AngleX = Vector2.Angle(TrackPeices[i-1].gameObject.transform.position, TrackPeices[i+1].gameObject.transform.position);
            TrackPeices[i].gameObject.transform.localRotation = Quaternion.LookRotation(Direction);
         
            //rTrack.transform.localRotation = Quaternion.LookRotation(Direction);
        }
    }
    void stretch()
    {
       

        for (int i = 0; i < TrackPeices.Count; i++)
        {
            if (TrackPeices[i] != TrackPeices[TrackPeices.Count - 1] && TrackPeices[i] != TrackPeices[0])
            {
                Dis = Vector3.Distance(TrackPeices[i].gameObject.transform.position, TrackPeices[i + 1].gameObject.transform.position);
        
                //Debug.Log("Dis" + Dis);
                //TrackPeices[i].gameObject.transform.localScale = new Vector3(TrackPeices[i].gameObject.transform.localScale.x, TrackPeices[i].gameObject.transform.localScale.y, Dis);
            }
        }
 

        foreach (var Peice in TrackPeices)
        {

            
               
             // Debug.Log("Dis" + Dis);
               Peice.gameObject.transform.localScale = new Vector3(Peice.gameObject.transform.localScale.x, Peice.gameObject.transform.localScale.y, Dis);
      
        }

            //Vector3 objectScale = TrackPeices[i].transform.localScale;
            //float distance = Vector3.Distance(TrackPeices[i - 1].transform.position, TrackPeices[i +1].transform.position);
            //Vector3 newScale = new Vector3(objectScale.x, objectScale.y, distance);
            //TrackPeices[i].transform.localScale = newScale;

        
    }
}
