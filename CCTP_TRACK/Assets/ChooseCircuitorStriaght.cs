using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCircuitorStriaght : MonoBehaviour
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
    public void Circuit()
    {
        PS.circuitTrack = true;
        PS.straightTrack = false;

    }
    public void Straight()
    {
        PS.straightTrack = true;
        PS.circuitTrack = false;
    }
}
