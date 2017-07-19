﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZoneControllerStage : MonoBehaviour
{

    void Awake()
    {

        //  source = GetComponent<AudioSource>();
    }


    int _amount = 3;

    [SerializeField]
    GameObject zoneObject;


    GameObject[] zoneLeft;
    // List<GameObject> zoneLeft;

    GameObject[] zoneRight;

    [SerializeField]
    float displacementX = 0.3f;

    [SerializeField]
    float displacementY = 0.1f;

    [SerializeField]
    int displacementZ = 0;



   // private Color zone1Color;
    static Color ocher = new Color(255/255f, 178/255f, 69/255f, 0.2f);
    static Color fuscia = new Color(255 / 255f, 79 / 255f, 218 / 255f, 0.2f);
    static Color turquoise = new Color(30 / 255f, 219 / 255f, 232 / 255f, 0.2f);
    Color[] zoneColors = { turquoise, ocher, fuscia };

    // x range is -0.3 - 0.3
    float[] xPosLeftRange = { -0.5f, -0.13f, 0.03f, 0.50f };

    // x range is -0.3 - 0.3, 4 zones here
    float[] xPosRightRange = { -0.5f, -0.15f, -0.05f, 0.10f, 0.50f };

    // y range is 0.2 - 0.85
    float[] yPosRange = { 0.2f, 0.4f, 0.6f, 0.95f };

    int lastLeftZoneYState = 0;
    int lastRightZoneYState = 0;
    Vector3 gloveScaleFactor = new Vector3(1.2f, 1, 1.2f);
    public int whichSide = 1; // -1 is right, 1 is left

    void Start()
    {
        zoneLeft = new GameObject[_amount];

        InstantiateZoneObjects();

        //zoneLeft = new List<GameObject>();

        /*
        zoneRight = new GameObject[_amount];
        InstantiateSliderObjects();
        
        */
    }

    private void Update()
    {
        /*
        for (int i = 0; i < _amount; i++)
        {
            ZoneHeight(zoneLeft[i], i, 0);
        }
        */
        
    }

    public void InstantiateZoneObjects()
    {
        
        for (int i = 0; i < (_amount); i++)
        {
            GameObject zone = null;
            if (1 == whichSide) { // LEFT
                zone = Instantiate(zoneObject, transform.position + new Vector3(displacementX + (-i) * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            } else {  // RIGHT
                zone = Instantiate(zoneObject, transform.position + new Vector3(-displacementX + (i) * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            }

        zone.name = "zone-left-" + i;
            zone.transform.parent = transform;
            zoneLeft[i] = zone;
            zoneLeft[i].transform.localScale = new Vector3(1.8f, 1, 1);

        }

    }


    // passing left and right zone information to main zone height controller
    private void ZoneHeightBlock(int zone, float yPos)
    {
        Debug.Log("zone-LEFT:" + zone + " scale value:" + yPos);
        for (int i = 0; i < _amount; i++)
        {
            // resets each zone to zero
            ZoneHeight(zoneLeft[i], i, 0);

        }
        // only change selected zone's height
        ZoneHeight(zoneLeft[zone], zone, yPos);

    }

    
    internal void ZoneHeight(GameObject ZoneObject, int zone, float yPos)
    {

        Transform childTransform = ZoneObject.transform.GetChild(0);
        childTransform.localScale = new Vector3(1, 8 * Mathf.Pow(yPos, 2), 1); // Mathf.Pow(yPos, 2) for square (pow 2)
       // Debug.Log("zone height" + childTransform.localScale);
    }


    private void SliderMotion(GameObject selectedSlider, float zPos)
    {

        // Debug.Log("slider selecting" + selectedZone);

        //  Debug.Log("slider selected");

        Transform moveSlider = selectedSlider.transform.GetChild(0);

        moveSlider.localPosition = new Vector3(0, 0, 0.06f + zPos);

    }


    public void ZoneColor(int zoneNumber, bool isRSLeft)
    {
        for (int i = 0; i < zoneLeft.Length; i++)
        //foreach (GameObject zones in zoneLeft)
        {

            zoneLeft[i].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            //Debug.Log(rightZones.Length);
        }


        zoneLeft[zoneNumber].GetComponentInChildren<MeshRenderer>().material.color = zoneColors[zoneNumber];
        //HUDxPosLeft.color = zoneColors[zoneNumber];
        
        // sets color of the HUD x position element to change colors based on zone changes
        if (isRSLeft == true)
        {
            HUDxPosLeft.color = zoneColors[zoneNumber];
        } else {

            HUDxPosRight.color = zoneColors[zoneNumber];
            } 
        //  Debug.Log("color: " + zoneColors[zoneNumber]);

    }

    internal int GetZone(float xPos, float[] arrayXRange)
    {

        //// xpos values are -0.3 to 0.3
        for (int i = 0; i < (arrayXRange.Length - 1); i++)
        {

            if ((xPos > arrayXRange[i]) && (xPos < arrayXRange[i + 1]))
            {
               // Debug.Log(xPos + ": in zone: " + i);
                return i;
            }
        }
        Debug.Log(xPos + ": did not fall into zones");
        return -1;
    }

    internal int GetYZone(float yPos)
    {

        //// ypos values are 0.4 to 0.9
        for (int i = 0; i < (yPosRange.Length - 1); i++)
        {
            // Debug.Log("zone" + i + "yPosRange"+ yPosRange[i]);
            if ((yPos > yPosRange[i]) && (yPos < yPosRange[i + 1]))
            {

                // Debug.Log("ypos : " + yPos + "in zone: " + i);
                return i;
            }
        }
        //  Debug.Log(xPos + "did not fall into zones");
        return -1;
    }


    internal void UpdateZone(float xPos, float yPos, float zPos, bool isRSLeft)
    {
        int selectedYZone = GetYZone(yPos);

        int selectedZone = GetZone(xPos, xPosLeftRange);

        if (selectedZone == -1)
        {
            Debug.Log(xPos + "," + yPos + "," + zPos + " : yielded no X zone");
            return;
        }

        // left zones filled

        if (isRSLeft == true)
        {
            // sets zone color  and HUD zone height fill  for left hand
            selectedZone = 2 - selectedZone;
            ZoneColor(selectedZone, true);
            HUDyPosLeft.fillAmount = yPos;
        } else
        {
            // sets zone color and HUD zone height fill for right hand
            ZoneColor(selectedZone, false);
            HUDyPosRight.fillAmount = yPos;
        }


        // Debug.Log("ypos: " + yPos);

        if (selectedZone < 3)
        {
            ZoneHeightBlock(selectedZone, yPos);

        }

        return;
    }



    void InstantiateSliderObjects()
    {
        // instantiate slider objects
        zone4SliderLeft = Instantiate(zone4SliderLeft, transform.position + new Vector3(-(displacementX * .45f + 3 * displacementX + 0.05f), 0.05f, -0.1f), Quaternion.identity) as GameObject;
        zone4SliderLeft.name = "zone-left-" + 3;
        zone4SliderLeft.transform.parent = transform;
        zoneLeft[3] = zone4SliderLeft;
        //zoneLeft.Add((GameObject)zone4SliderLeft);
        zone4SliderRight = Instantiate(zone4SliderLeft, transform.position + new Vector3((displacementX * .45f + 3 * displacementX) + 0.05f, 0.05f, -0.1f), Quaternion.identity) as GameObject;
        zone4SliderRight.name = "zone-right-" + 3;
        zone4SliderRight.transform.parent = transform;
        zoneRight[3] = zone4SliderRight;
        /*
        waterGloveLeft = Instantiate(waterGloveLeft, transform.position + new Vector3(-(displacementX * .8f + 3 * displacementX), displacementY, 0), Quaternion.identity) as GameObject;
        waterGloveLeft.name = "water-left-glove";
        waterGloveLeft.transform.parent = transform;
        waterGloveRight = Instantiate(waterGloveRight, transform.position + new Vector3((displacementX * .8f + 3 * displacementX), displacementY, 0), Quaternion.identity) as GameObject;
        waterGloveRight.name = "water-right-glove";
        waterGloveRight.transform.parent = transform;
        waterGloveRight.transform.localScale = new Vector3(1, 1, 1); // mirrors 
        droneGloveLeft = Instantiate(droneGloveLeft, transform.position + new Vector3(-(displacementX * .8f + 3 * displacementX), 2 * displacementY, 0), Quaternion.identity) as GameObject;
        droneGloveLeft.name = "drone-left-glove";
        droneGloveLeft.transform.parent = transform;
        droneGloveRight = Instantiate(droneGloveRight, transform.position + new Vector3((displacementX * .8f + 3 * displacementX), 2 * displacementY, 0), Quaternion.identity) as GameObject;
        droneGloveRight.name = "drone-right -glove";
        droneGloveRight.transform.parent = transform;
        droneGloveRight.transform.localScale = new Vector3(1, 1, 1);
        */
    }
    

}

//// to make 5 zones
/*
if (Math.Abs(xPos) < 0.05)
{
    return 0;
}
else if (Math.Abs(xPos) < 0.15)
{

    // zone middle

    return 1;
}
else
{
    // zone outside
    return 2;
}
*/




