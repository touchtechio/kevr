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
    public Text leftHitVelocity;
    public Text rightHitVelocity;
    public OSCSenderMidi oscSenderObject;
    int[] midiNote = { 60, 61, 62, 63, 64, 65, 66, 67, 68, 69 };
    string[] midiChannels = { "midi/1", "midi/2", "midi/3", "midi/4", "midi/5", "midi/6", "midi/7", "midi/8" };

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
    public void SetLeftStickPositionWithZone(Vector3 rawPosition, Vector3 ZonePosition)
    {

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
        Transform transformToMove = stickObjectRight.GetComponent<Transform>();
        Vector3 pos;
        pos = new Vector3(x, y, z); // passed in floats from OSC
        transformToMove.localPosition = pos;

    }


    // trigger drumhit position if received hit

    public void drumHitLeft(bool isHit, int hitVel, int channel)
    {
        int zoneNumber = zoneControllerStageRadial.selectedLeftZone;
        Animator drumstickAnimator = drumstickAnimatorLeft;
        drumHit(isHit, hitVel, channel, drumstickAnimator, zoneNumber);
        leftHitVelocity.text = hitVel.ToString();
    }

    public void drumHitRight(bool isHit, int hitVel, int channel)
    {
        int zoneNumber = zoneControllerStageRadial.selectedRightZone;
        Animator drumstickAnimator = drumstickAnimatorRight;
        drumHit(isHit, hitVel, channel, drumstickAnimator, zoneNumber);
        rightHitVelocity.text = hitVel.ToString();
        
    }

    ///
    public void drumHit(bool isHit, int hitVel, int channel, Animator drumstickAnimator, int zoneNumber)
    {
        Debug.Log("animation speed " + drumstickAnimator.speed);
        drumstickAnimator.speed = Map(hitVel, 0, 127, 3f, 5f);
        drumstickAnimator.SetTrigger("drumhit");

        Debug.Log("drumhit");
  
        // receivedHit = false;

         zoneControllerStageRadial.stageZonesArray[zoneNumber].GetComponent<DrumAudio>().playDrum(zoneNumber);
        // oscSenderObject.SendOSCTaikoMidi("midi/1", midiNote[zoneNumber], hitVel, channel);
        oscSenderObject.SendOSCTaikoMidi(midiChannels[zoneNumber], midiNote[zoneNumber], hitVel, channel);
        // transform.rotation =  Quaternion.Euler( 30 * Time.deltaTime , 0, 0);
        receivedHit = false;
    }



    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }
}
