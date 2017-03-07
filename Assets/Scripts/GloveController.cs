using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveController : MonoBehaviour
{

    public GameObject LeftHandObject;
    public GameObject RightHandObject;

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
        Transform transformToMove = LeftHandObject.GetComponent<Transform>();


        Vector3 pos;
        pos = new Vector3(rsZoneDataLeftX, transformToMove.localPosition.y, rsZoneDataLeftZ);
        transformToMove.localPosition = pos;


        //HandObject.GetComponent<Transform>() = new Vector3(rsZoneDataLeftX, 0, rsZoneDataLeftZ);
    }


    public void rsZoneLeftY(float rsZoneDataLeftY)
    {
        // Debug.Log("left hand is at y pos " + rsZoneDataLeftY);

        Transform transformToMove = LeftHandObject.GetComponent<Transform>();

        Vector3 pos;
        pos = new Vector3(transformToMove.position.x, rsZoneDataLeftY, transformToMove.position.z );
        transformToMove.transform.position = pos;


    }

    public void rsZoneRightXZ(float rsZoneDataRightX, float rsZoneDataRightZ)
    {
        // Debug.Log("right hand is at x pos " + rsZoneDataRightX + " y pos " + rsZoneDataRightZ);
        //  HandObject.GetComponent<Transform> = new Vector3(rsZoneLeftX, rsZoneLeftY, rsZoneLeftZ);
        Transform transformToMove = RightHandObject.GetComponent<Transform>();


        Vector3 pos;
        pos = new Vector3(rsZoneDataRightX, transformToMove.localPosition.y, rsZoneDataRightZ);
        transformToMove.localPosition = pos;
    }


    public void rsZoneRightY(float rsZoneDataRightY)
    {
        // Debug.Log("left hand is at y pos " + rsZoneDataRightY);
        Transform transformToMove = RightHandObject.GetComponent<Transform>();

        Vector3 pos;
        pos = new Vector3(transformToMove.position.x, rsZoneDataRightY, transformToMove.position.z);
        transformToMove.transform.position = pos;

    }

    internal void SetWristAngle(GameObject Hand, int degrees)
    {
        Transform transform = Hand.GetComponent<Transform>();
        transform.rotation = Quaternion.Euler(-90, 180, 0) * Quaternion.Euler(0, -degrees-90, 0);
    }

    internal void SetLeftWristAngle(int degrees)
    {
        SetWristAngle(LeftHandObject, degrees);
    }

    internal void SetRightWristAngle(int degrees)
    {
        SetWristAngle(RightHandObject, degrees);

    }
}
