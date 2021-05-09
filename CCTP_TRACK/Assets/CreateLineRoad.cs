using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLineRoad : MonoBehaviour
{
    grid GD;
    LineRenderer LR;
    public GameObject GridScript;
    int i;
    // Start is called before the first frame update
    void Start()
    {
        GD = GridScript.gameObject.GetComponent<grid>();
        LR = gameObject.GetComponent<LineRenderer>();
        LR.positionCount = GD.path.Count;
    }


}
