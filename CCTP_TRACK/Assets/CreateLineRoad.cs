using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLineRoad : MonoBehaviour
{
    gride GD;
    LineRenderer LR;
    public GameObject GridScript;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        GD = GridScript.gameObject.GetComponent<gride>();
        LR = gameObject.GetComponent<LineRenderer>();
        LR.positionCount = GD.path.Count;
    }

    // Update is called once per frame
    void Update()
    {
      
       



        //for (int i = 0; i < GD.path.Count; i++)
        //{
        //    foreach (NodeScript n in GD.path)
        //    {


        //        if (GD.path.Contains(n))
        //        {
        //            LR.SetPosition(i, new Vector3(n.worldPos.x, n.worldPos.y, n.worldPos.z));
        //        }
        //    }

        //}

    }
}
