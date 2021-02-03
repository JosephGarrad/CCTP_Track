using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotationscript : MonoBehaviour
{

  //  public GameObject Pathfinding;
    pathfinding PS;
    gride GS;
    // Start is called before the first frame update
    void Start()
    {
       // PS = GameObject.FindGameObjectWithTag("A*").gameObject.GetComponent<pathfinding>();
    //    GS = GameObject.FindGameObjectWithTag("GRIDE").gameObject.GetComponent<gride>();
    //    for (int i = 0; i <= GS.path.Count; i++)
    //    {
    //        Vector2 direction = (GS.path[i+1].worldPos - this.transform.position).normalized;
    //        float distance = Vector2.Distance(GS.path[i].worldPos, GS.path[i + 1].worldPos);
    //        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

    //        //rectTransform.anchoredPosition = dotPositionA + direction * distance * 0.5f; // Placed in middle between A + B
    //        transform.localScale = new Vector3(distance, transform.localScale.y, transform.localScale.z);
    //    }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
