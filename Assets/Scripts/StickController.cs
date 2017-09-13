using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniOSC;

public class StickController : MonoBehaviour
{

    public GameObject stickObjectLeft;
    public GameObject stickObjectRight;

    // public GameObject StageBoundaries;
    Animator drumstickAnimatorLeft;
    Animator drumstickAnimatorRight;
    bool receivedHit = false;
    public bool stickUsingRaw = false;
    private int degrees;
    public ZoneControllerStageRadial zoneControllerStageRadial;
    public Text hitVelocity;
    public OSCSenderMidi oscSenderObject;
    int[] midiNote = { 60, 61, 62, 63, 64, 65, 66, 67, 68, 69 };

    // Use this for initialization
    void Start()
    {
        drumstickAnimatorLeft = GameObject.Find("DrumsticksLeft").GetComponent<Animator>();
        drumstickAnimatorRight = GameObject.Find("DrumsticksRight").GetComponent<Animator>();
        zoneControllerStageRadial = FindObjectOfType<ZoneControllerStageRadial>();
    }

    // Update is called once per frame
    void Update()
    {
        
     
        if (Input.GetKeyDown(GameControllerVR.HOTKEY_GLOVE))
        {

            stickObjectLeft.SetActive(!stickObjectLeft.activeSelf);

        }

        if (Input.GetKeyDown(GameControllerVR.HOTKEY_STICK_LEFT))
        {

            drumHitLeft(true, 90, 1);
        }

        if (Input.GetKeyDown(GameControllerVR.HOTKEY_STICK_RIGHT))
        {

            drumHitRight(true, 90, 1);
        }



        // trigger idle stick position if not hit data received after hit
        if (Input.GetKeyUp(GameControllerVR.HOTKEY_STICK_LEFT) | !receivedHit)
        {
            drumstickAnimatorLeft.SetTrigger("stophit");
        }

        if (Input.GetKeyUp(GameControllerVR.HOTKEY_STICK_RIGHT) | !receivedHit)
        {
            drumstickAnimatorRight.SetTrigger("stophit");
        }


    }


    public void SetLeftStickAngle(GameObject stickObject, int degrees)
    {
        //sets positional transform of the whole hand
        Transform transform = stickObject.GetComponent<Transform>();
        //  transform.rotation = Quaternion.Euler(-90, 180, 0) * Quaternion.Euler(0, -degrees - 90, 0);
        transform.rotation = Quaternion.Euler(90, degrees, 0); // add 90 to start it at same position as zone 1
     //   Debug.Log("stick rotated");
    }

    public void SetRightStickAngle(GameObject stickObject, int degrees)
    {
        //sets positional transform of the whole hand
        Transform transform = stickObject.GetComponent<Transform>();
        //  transform.rotation = Quaternion.Euler(-90, 180, 0) * Quaternion.Euler(0, -degrees - 90, 0);
        transform.rotation = Quaternion.Euler(90, degrees, 0); // add 90 to start it at same position as zone 1
                                                               //   Debug.Log("stick rotated");
    }



    // get position of stick based on position coordinates from OSC
    public Vector3 GetLeftStickPosition()
    {
        Transform transformToMove = stickObjectLeft.GetComponent<Transform>();
        return transformToMove.localPosition;
    }

    public Vector3 GetRightStickPosition()
    {
        Transform transformToMove = stickObjectRight.GetComponent<Transform>();
        return transformToMove.localPosition;
    }

    // sets position of stick either within a zone or using raw UWB
    public void SetLeftStickPositionWithZone( Vector3 rawPosition, Vector3 ZonePosition) {

        Vector3 position = rawPosition;

        if (!stickUsingRaw)
        {
            position = ZonePosition;
        }
        SetLeftStickPosition(position[0], position[1], position[2]);
        //Debug.Log("stage-pos-right: " + position[0] + " " + position[1] + " " + position[2]);
        return;

    }

    public void SetRightStickPositionWithZone(Vector3 rawPosition, Vector3 ZonePosition)
    {

        Vector3 position = rawPosition;

        if (!stickUsingRaw)
        {
            position = ZonePosition;
        }
        SetRightStickPosition(position[0], position[1], position[2]);
        //Debug.Log("stage-pos-right: " + position[0] + " " + position[1] + " " + position[2]);
        return;

    }

    // position stick in game based on position coordinates from OSC
    // to do pass in rotation!!!
    public void SetLeftStickPosition(float x, float y, float z)
    {
        Transform transformToMove = stickObjectLeft.GetComponent<Transform>();
        Vector3 pos;
        pos = new Vector3(x, y, z); // passed in floats from OSC
        transformToMove.localPosition = pos;

    }

    // position stick in game based on position coordinates from OSC
    // to do pass in rotation!!!
    public void SetRightStickPosition(float x, float y, float z)
    {
        Transform transformToMove = stickObjectLeft.GetComponent<Transform>();
        Vector3 pos;
        pos = new Vector3(x, y, z); // passed in floats from OSC
        transformToMove.localPosition = pos;

    }

    // trigger drumhit position if received hit
    public void drumHitLeft(bool isHit, int hitVel, int channel)
    {

        Debug.Log("animation speed " + drumstickAnimatorLeft.speed);
        drumstickAnimatorLeft.speed = Map(hitVel, 0, 127, 0f, 3f);
        drumstickAnimatorLeft.SetTrigger("drumhit");
        
        Debug.Log("drumhit");
        hitVelocity.text = hitVel.ToString();
        // receivedHit = false;
        int zoneNumber = zoneControllerStageRadial.GetZone();
        zoneControllerStageRadial.stageZonesArray[zoneNumber].GetComponent<DrumAudio>().playDrum(zoneNumber);
        oscSenderObject.SendOSCTaikoMidi("midi/1", midiNote[zoneNumber], hitVel, channel);
        // transform.rotation =  Quaternion.Euler( 30 * Time.deltaTime , 0, 0);
        receivedHit = false;
    }

    // trigger drumhit position if received hit
    public void drumHitRight(bool isHit, int hitVel, int channel)
    {

        Debug.Log("animation speed " + drumstickAnimatorRight.speed);
        drumstickAnimatorRight.speed = Map(hitVel, 0, 127, 0f, 3f);
        drumstickAnimatorRight.SetTrigger("drumhit");

        Debug.Log("drumhit");
        hitVelocity.text = hitVel.ToString();
        // receivedHit = false;
        int zoneNumber = zoneControllerStageRadial.GetZone();
        zoneControllerStageRadial.stageZonesArray[zoneNumber].GetComponent<DrumAudio>().playDrum(zoneNumber);
        oscSenderObject.SendOSCTaikoMidi("midi/1", midiNote[zoneNumber], hitVel, channel);
        // transform.rotation =  Quaternion.Euler( 30 * Time.deltaTime , 0, 0);
        receivedHit = false;
    }

    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
