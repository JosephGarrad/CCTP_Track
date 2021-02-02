using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotationscript : MonoBehaviour
{

  //  public GameObject Pathfinding;
    pathfinding PS;
    // Start is called before the first frame update
    void Start()
    {
        PS = GameObject.FindGameObjectWithTag("A*").gameObject.GetComponent<pathfinding>();

        this.transform.Rotate(PS.angleBetweenNode());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
