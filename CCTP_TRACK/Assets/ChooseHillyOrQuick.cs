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

    public void Hill() // if button is pressed to select hill
    {
        PS.hillyTrack = true; // set the hilly track to true 
        PS.quickestTrack = false; // set the quick track to false
    }
    public void Quick()
    {
        PS.quickestTrack = true;
        PS.hillyTrack = false;
    }
}
