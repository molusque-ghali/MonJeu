using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

   public Vector3 positionMourir=Vector3.zero;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void goPositionMourir()
    {
        Debug.Log("hye");
        transform.position = positionMourir;
    }
}
