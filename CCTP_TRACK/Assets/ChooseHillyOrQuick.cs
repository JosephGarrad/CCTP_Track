using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseHillyOrQuick : MonoBehaviour
{
    public GameObject Pathfinding;
    pathfinding PS;
    // Start is called before the first frame update
    void Start()
    {
        PS = Pathfinding.gameObject.GetComponent<pathfinding>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hill()
    {
        PS.Hilly_track = true;
        PS.Quickest_track = false;
    }
    public void Quick()
    {
        PS.Quickest_track = true;
        PS.Hilly_track = false;
    }
}
