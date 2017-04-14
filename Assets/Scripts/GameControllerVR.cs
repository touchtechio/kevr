﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerVR : MonoBehaviour
{
    /*
    public float timerMax;

    bool[] timerStart = { false, false, false, false, false, false }; // timer for tutorial icons
    float[] timer = { 0F, 0F, 0F, 0F, 0F, 0F };


    private Screen currentScreen;
    private Screen previousScreen;


    Vector3 iconScale = new Vector3(1, 1);
    Vector3 startingPosition;
    int toggleCount;
    int textMessageScrollCount;

    public AudioClip selectSound;
    public AudioClip backSound;
    public AudioClip upSound;
    public AudioClip downSound;

    public AudioSource source;
    private float volume = 0.6f;
    */

    public Text YPosLeft;
    public Text YPosRight;

    private GloveController gloveController;



    void Awake()
    {

        //  source = GetComponent<AudioSource>();
    }

    enum Screen
    {
        TUTORIAL,
        APP_VIEW,
        TEXT_LIST,
        TEXT_DETAIL,
        PHOTO_ALBUM,
        INTERRUPT,
    }


    void Start()
    {
        // GUI is rendered with last camera.
        // As we want it to end up in the main screen, make sure main camera is the last one drawn.
        Debug.Log("displays connected: " + Display.displays.Length);
        // Display.displays[0] is the primary, default display and is always ON.
        // Check if additional displays are available and activate each.
        if (Display.displays.Length > 1)
            Display.displays[1].Activate();
        if (Display.displays.Length > 2)
            Display.displays[2].Activate();
        if (Display.displays.Length > 3)
            Display.displays[3].Activate();

        if (null == gloveController)
        {
            gloveController = GameObject.Find("GameController").GetComponent<GloveController>(); // finds the component anywhere in game
            if(null == gloveController)
            {
                Debug.Log("ERROR: couldn't find ZoneController");
            }

        }
       // YPosLeft.text = leftPos.ToString();

    }

    void Update()
    {

        Vector3 leftPosition = gloveController.GetLeftPosition();
        UpdateLeftZone(leftPosition.x, leftPosition.y, leftPosition.z);

        Vector3 rightPosition = gloveController.GetRightPosition();
        UpdateRightZone(rightPosition.x, rightPosition.y, rightPosition.z);

        /*
        private void keyboardData()
    {
        if (Input.GetKeyDown(KeyCode.F)) // check if it was in zone 1 before
        {

            // to turn left water glove switch on and off

            if (!ZoneController.isWaterGloveLeft)
            {

                ZoneController.isWaterGloveLeft = true;

            }
            else
            {
                ZoneController.isWaterGloveLeft = false;


            }

          //  Debug.Log("water glove mode " + ZoneController.isWaterGloveLeft);
        }

        if (Input.GetKeyDown(KeyCode.G)) // check if it was in zone 1 before
        {

            // to turn left water glove switch on and off

            if (!ZoneController.isWaterGloveRight)
            {

                ZoneController.isWaterGloveRight = true;

            }
            else
            {
                ZoneController.isWaterGloveRight = false;


            }

           // Debug.Log("water glove mode " + isWaterGloveLeft);
        }
    }
    */
    }

    private void UpdateLeftZone(float xpos, float ypos, float zpos)
    {
        ypos = (int)(ypos * 100);

        YPosLeft.text = ypos.ToString();

    }
    private void UpdateRightZone(float xpos, float ypos, float zpos)
    {
        ypos = (int)(ypos * 100);

        YPosRight.text = ypos.ToString();

    }
}


