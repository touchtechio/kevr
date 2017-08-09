using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StickController : MonoBehaviour
{

    public GameObject stickObject1;
   // public GameObject StageBoundaries;
    Animator drumstickAnimator;
    bool receivedHit = false;
    public bool stickUsingRaw = false;
    private int degrees;

    // Use this for initialization
    void Start()
    {
        drumstickAnimator = GameObject.Find("Drumsticks").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(GameControllerVR.HOTKEY_GLOVE))
        {

            stickObject1.SetActive(!stickObject1.activeSelf);
         


        }

        if (Input.GetKeyDown(GameControllerVR.HOTKEY_STICK))
        {

            drumstickAnimator.SetTrigger("drumhit");
            //SetWristAngle(stickObject1, degrees);
        }


        // trigger idle stick position if not hit data received after hit
        if (!receivedHit)
        {
            drumstickAnimator.SetTrigger("stophit");
        }

        
    }


    public void SetStickAngle(GameObject stickObject, int degrees)
    {
        //sets positional transform of the whole hand
        Transform transform = stickObject.GetComponent<Transform>();
        //  transform.rotation = Quaternion.Euler(-90, 180, 0) * Quaternion.Euler(0, -degrees - 90, 0);
        transform.rotation = Quaternion.Euler(90, degrees, 0); // add 90 to start it at same position as zone 1
        Debug.Log("stick rotated");


    }



    // get position of stick based on position coordinates from OSC
    public Vector3 GetStickPosition()
    {
        Transform transformToMove = stickObject1.GetComponent<Transform>();
        return transformToMove.localPosition;
    }

    // sets position of stick either within a zone or using raw UWB
    public void SetStickPositionWithZone( Vector3 rawPosition, Vector3 ZonePosition) {

        Vector3 position = rawPosition;

        if (!stickUsingRaw)
        {
            position = ZonePosition;

        }

        SetStickPosition(position[0], position[1], position[2]);
        //Debug.Log("stage-pos-right: " + position[0] + " " + position[1] + " " + position[2]);

        return;

    }


    // position stick in game based on position coordinates from OSC
    // to do pass in rotation!!!
    public void SetStickPosition(float x, float y, float z)
    {
        Transform transformToMove = stickObject1.GetComponent<Transform>();
        Vector3 pos;
        pos = new Vector3(x, y, z); // passed in floats from OSC
        transformToMove.localPosition = pos;
    
        /*
        // using left glove to set boundaries of drumstage
        Transform transformToMoveCube = StageBoundaries.GetComponent<Transform>();
        Vector3 posCube;
        posCube = new Vector3(x, y, z);
        transformToMoveCube.localPosition = posCube;*/

    }

    // trigger drumhit position if received hit
   public void drumHit(bool isHit)
    {
        drumstickAnimator.SetTrigger("drumhit");
        receivedHit = false;
    }
}
