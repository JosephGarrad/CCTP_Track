using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotationscript : MonoBehaviour
{

    //  public GameObject Pathfinding;
    private Vector3 Direction;
    TrackBuilder TB;
    public GameObject Tb;
    // Start is called before the first frame update
    void Start()
    {
        TB = Tb.gameObject.GetComponent<TrackBuilder>();

      //  foreach (var Block in TB.TrackPeices)
      //  {
           // GameObject Block1 = Block +1;
       //     Direction = (Block.transform.position - Block.transform.position);
       // }
     

       

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < TB.TrackPeices.Count; i++)
        {
            Direction = (TB.TrackPeices[i].gameObject.transform.position - TB.TrackPeices[i + 1].gameObject.transform.position);
            TB.TrackPeices[i].gameObject.transform.localRotation = Quaternion.LookRotation(Direction);
        }
    }
}
