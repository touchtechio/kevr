using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveController : MonoBehaviour
{

    public GameObject HandObject;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void fingerBendLeft(int i, float fingerBendDataLeft)
    {
        //Debug.Log("finger" + i + "bend value" + fingerBendDataLeft);
    }


    public void fingerBendRight(int i, float fingerBendDataRight)
    {
       // Debug.Log("finger" + i + "bend value" + fingerBendDataRight);
    }

    public void rsZoneLeftXZ(float rsZoneDataLeftX, float rsZoneDataLeftZ)
    {
       // Debug.Log("left hand is at x pos " + rsZoneDataLeftX + " y pos " + rsZoneDataLeftZ);
        Transform transformToMove = HandObject.GetComponent<Transform>();


        Vector3 pos;
        pos = new Vector3(rsZoneDataLeftX, transformToMove.position.y, rsZoneDataLeftZ);
        transformToMove.transform.position = pos;


        //HandObject.GetComponent<Transform>() = new Vector3(rsZoneDataLeftX, 0, rsZoneDataLeftZ);
    }


    public void rsZoneLeftY(float rsZoneDataLeftY)
    {
        // Debug.Log("left hand is at y pos " + rsZoneDataLeftY);

        Transform transformToMove = HandObject.GetComponent<Transform>();

        Vector3 pos;
        pos = new Vector3(transformToMove.position.x, rsZoneDataLeftY, transformToMove.position.z );
        transformToMove.transform.position = pos;


    }

    public void rsZoneRightXZ(float rsZoneDataRightX, float rsZoneDataRightZ)
    {
       // Debug.Log("right hand is at x pos " + rsZoneDataRightX + " y pos " + rsZoneDataRightZ);
        //  HandObject.GetComponent<Transform> = new Vector3(rsZoneLeftX, rsZoneLeftY, rsZoneLeftZ);
    }


    public void rsZoneRightY(float rsZoneDataRightY)
    {
       // Debug.Log("left hand is at y pos " + rsZoneDataRightY);
    }
}
