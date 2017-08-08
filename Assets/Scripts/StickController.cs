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
        }


        // trigger idle stick position if not hit data received after hit
        if (!receivedHit)
        {
            drumstickAnimator.SetTrigger("stophit");
        }

        
    }


    internal void SetWristAngle(GameObject Hand, int degrees)
    {
        //sets positional transform of the whole hand
        Transform transform = Hand.GetComponent<Transform>();
        //  transform.rotation = Quaternion.Euler(-90, 180, 0) * Quaternion.Euler(0, -degrees - 90, 0);
        transform.GetChild(0).rotation = Quaternion.Euler(0, 0, degrees);


    }

    internal void SetStickAngle(int degrees)
    {
        SetWristAngle(stickObject1, degrees + 25);
        // send wrist rotation data to UI
        //leftRotation.text = ((int)degrees).ToString();
        //leftRotDial.transform.rotation = Quaternion.Euler(0,0,degrees);
        //leftRotDial.fillAmount = ((float)(degrees+30) / 120.0f);
        //Debug.Log("rotation" + degrees);
        //Debug.Log("rotation"+ leftRotDial.fillAmount);
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
