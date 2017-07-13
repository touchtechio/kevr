﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GloveController : MonoBehaviour
{


    public GameObject LeftHandObject;
    public GameObject RightHandObject;
    public Text leftRotation;
    public Text rightRotation;
    public Image leftRotDial;
    public Image rightRotDial;

    public float maxBounds = 850f;

    public static int leftThumb = 3270;
    public static int leftIndex = 4630;
    public static int leftMiddle = 5820;
    public static int leftRing = 5510;
    public static int leftPink = 4720;
    public static int rightThumb = 2870;
    public static int rightIndex = 4400;
    public static int rightMiddle = 5700;
    public static int rightRing = 5500;
    public static int rightPink = 4445;

    private int[] fingerThresholds =
    {
        leftPink, leftRing, leftMiddle, leftIndex, leftThumb,
        rightThumb, rightIndex, rightMiddle, rightRing, rightPink
    };

   

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown( GameControllerVR.HOTKEY_GLOVE)) {

            LeftHandObject.SetActive(!LeftHandObject.activeSelf);
            RightHandObject.SetActive(!RightHandObject.activeSelf);

        }
    }

    public void fingerBendLeft(int i, float fingerBendDataLeft)
    {
        //Debug.Log("finger" + i + "bend value" + fingerBendDataLeft);
    }


    public void fingerBendRight(int i, float fingerBendDataRight)
    {
       // Debug.Log("finger" + i + "bend value" + fingerBendDataRight);
    }



    internal void SetWristAngle(GameObject Hand, int degrees)
    {
        //sets positional transform of the whole hand
        Transform transform = Hand.GetComponent<Transform>();
        //  transform.rotation = Quaternion.Euler(-90, 180, 0) * Quaternion.Euler(0, -degrees - 90, 0);
        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, degrees);
    

    }

    internal void SetLeftWristAngle(int degrees)
    {
        SetWristAngle(LeftHandObject, degrees+25);
        // send wrist rotation data to UI
        leftRotation.text = ((int)degrees).ToString();
        //leftRotDial.transform.rotation = Quaternion.Euler(0,0,degrees);
        leftRotDial.fillAmount = ((float)(degrees+30) / 120.0f);
        //Debug.Log("rotation" + degrees);
        //Debug.Log("rotation"+ leftRotDial.fillAmount);
    }

    internal void SetRightWristAngle(int degrees)
    {
        SetWristAngle(RightHandObject, degrees);
        rightRotation.text = ((int)degrees).ToString();
       // leftRotDial.transform.rotation = Quaternion.Euler(0, 0, degrees);
       rightRotDial.fillAmount = (int)degrees/ 360;
    

    }

    public Vector3 GetRightPosition()
    {
        Transform transformToMove = RightHandObject.GetComponent<Transform>();
        return transformToMove.localPosition;
    }

    public Vector3 GetLeftPosition()
    {
        Transform transformToMove = LeftHandObject.GetComponent<Transform>();
        return transformToMove.localPosition;
    }

    //atempts to scale syncphony finger bend to 0.0 - 1.0
    internal float GetFingerBend(int finger, int gloveValue)
    {
        int threshold = fingerThresholds[finger];
        float bendPercentage = ((gloveValue - threshold) + maxBounds/2) / maxBounds;
     //   Debug.Log("finger: " + finger + " glovevalue: " + gloveValue + " threshold: " + threshold + " bend %: " + bendPercentage);
        return bendPercentage;

    }

    //todo:  x and z transforms are flipped

    public void rsZoneLeftXZ(float rsZoneDataLeftX, float rsZoneDataLeftZ)
    {
        // Debug.Log("left hand is at x pos " + rsZoneDataLeftX + " y pos " + rsZoneDataLeftZ);
        Transform transformToMove = LeftHandObject.GetComponent<Transform>();


        Vector3 pos;
        pos = new Vector3(rsZoneDataLeftX, transformToMove.localPosition.y, rsZoneDataLeftZ);
        transformToMove.localPosition = pos;


        //HandObject.GetComponent<Transform>() = new Vector3(rsZoneDataLeftX, 0, rsZoneDataLeftZ);
    }

    // realsense old y axis
    //public void rsZoneLeftY(float rsZoneDataLeftY)
    //{
    //    // Debug.Log("left hand is at y pos " + rsZoneDataLeftY);
    //    Transform transformToMove = LeftHandObject.GetComponent<Transform>();
    //    Vector3 pos;
    //    pos = new Vector3(transformToMove.position.x, rsZoneDataLeftY, transformToMove.position.z);
    //    transformToMove.localPosition = pos;
    //}


    public void rsZoneLeftY(float rsZoneDataLeftY)
    {
        // Debug.Log("left hand is at y pos " + rsZoneDataLeftY);

        Transform transformToMove = LeftHandObject.GetComponent<Transform>();

        Vector3 pos;
        pos = new Vector3(transformToMove.localPosition.x, rsZoneDataLeftY, transformToMove.localPosition.z);
        transformToMove.localPosition = pos;


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
        pos = new Vector3(transformToMove.localPosition.x, rsZoneDataRightY, transformToMove.localPosition.z);
        transformToMove.localPosition = pos;

    }

    internal void enableRight()
    {
        RightHandObject.SetActive(true);
    }

    
    internal void disableRight()
    {
        RightHandObject.SetActive(false);
    }

    internal void enableLeft()
    {
        LeftHandObject.SetActive(true);
    }

    internal void disableLeft()
    {
        LeftHandObject.SetActive(false);
    }
}
