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

    // Start is called before the first frame update
    void Start()
    {
        MG = TerrainGenerator.gameObject.GetComponent<MeshGenerator>();
        GD = GridScript.gameObject.GetComponent<gride>();
        PS = Pathfinding.gameObject.GetComponent<pathfinding>();
        CTTR = Trackchnager.gameObject.GetComponent<ChangeTextureToRoad>();
 
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
       
            TrackPeices[i].gameObject.transform.localRotation = Quaternion.LookRotation(Direction);

        }
    }
    void stretch()
    {
       

        for (int i = 0; i < TrackPeices.Count; i++)
        {
            if (TrackPeices[i] != TrackPeices[TrackPeices.Count - 1] && TrackPeices[i] != TrackPeices[0])
            {
                Dis = Vector3.Distance(TrackPeices[i].gameObject.transform.position, TrackPeices[i + 1].gameObject.transform.position);
        
            
            }
        }
 

        foreach (var Peice in TrackPeices)
        {

            
       
               Peice.gameObject.transform.localScale = new Vector3(Peice.gameObject.transform.localScale.x, Peice.gameObject.transform.localScale.y, Dis);
      
        }

  
        
    }
}
