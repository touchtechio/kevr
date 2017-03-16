using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZoneController : MonoBehaviour
{

    void Awake()
    {

        //  source = GetComponent<AudioSource>();
    }


    int _amount = 4;

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

    public GameObject zone4SliderLeft;
    GameObject zone4SliderRight;
    public GameObject waterGloveLeft;
    public GameObject waterGloveRight;
    public GameObject droneGloveLeft;
    public GameObject droneGloveRight;

    private Color zone1Color;
    Color[] zoneColors = { Color.green, Color.yellow, Color.red, Color.cyan };

    [SerializeField]
    // x range is -0.3 - 0.3
    float[] xPosRange = { -0.35f, -0.10f, 0.5f, 0.10f, 0.30f };

    [SerializeField]
    // y range is 0.2 - 0.85
    public float[] yPosRange = { 0.35f, 0.55f, 0.7f, 0.9f };


    public bool isWaterGloveLeft = false;
    public bool isWaterGloveRight = false;
    public bool isDroneGloveLeft = false;
    public bool isDroneGloveRight = false;
    int waterGloveLeftStatusCount = 0;
    int waterGloveRightStatusCount = 0;
    int droneGloveLeftStatusCount = 0;
    int droneGloveRightStatusCount = 0;
    int lastLeftZoneYState = 0;
    int lastRightZoneYState = 0;
    Vector3 gloveScaleFactor= new Vector3(1.2f, 1, 1.2f);

    void Start()
    {
        zoneLeft = new GameObject[_amount];

        //zoneLeft = new List<GameObject>();

        for (int i = 0; i < (_amount - 1); i++)
        {
            // instantiate fountains based on prefab and then assign to fountain jet array
            // GameObject zone = Instantiate(zoneObject, transform.position + new Vector3(-(displacementX * .5f + i * displacementX ), 0, i * displacementZ), Quaternion.identity) as GameObject;
            GameObject zone = Instantiate(zoneObject, transform.position + new Vector3(-(displacementX * .45f + i * displacementX + 0.05f), 0, i * displacementZ), Quaternion.identity) as GameObject;
            // zoneLeft.Add((GameObject)zone);

            zoneLeft[i] = zone;
        }

        zoneRight = new GameObject[_amount];

        for (int i = 0; i < (_amount - 1); i++)
        {
            // instantiate fountains based on prefab and then assign to fountain jet array
            GameObject zone = Instantiate(zoneObject, transform.position + new Vector3(displacementX * .45f + i * displacementX + 0.05f, 0, i * displacementZ), Quaternion.identity) as GameObject;
            zoneRight[i] = zone;
        }

        // instantiate slider objects
        zone4SliderLeft = Instantiate(zone4SliderLeft, transform.position + new Vector3(-(displacementX * .45f + 3 * displacementX + 0.05f), 0.05f, -0.1f), Quaternion.identity) as GameObject;
        zoneLeft[3] = zone4SliderLeft;
        //zoneLeft.Add((GameObject)zone4SliderLeft);
        zone4SliderRight = Instantiate(zone4SliderLeft, transform.position + new Vector3((displacementX * .45f + 3 * displacementX) + 0.05f, 0.05f, -0.1f), Quaternion.identity) as GameObject;
        zoneRight[3] = zone4SliderRight;

        waterGloveLeft = Instantiate(waterGloveLeft, transform.position + new Vector3(-(displacementX * .8f + 3 * displacementX), displacementY, 0), Quaternion.identity) as GameObject;
        waterGloveRight = Instantiate(waterGloveRight, transform.position + new Vector3((displacementX * .8f + 3 * displacementX), displacementY, 0), Quaternion.identity) as GameObject;
        waterGloveRight.transform.localScale = new Vector3(1, 1, 1); // mirrors 
        droneGloveLeft = Instantiate(droneGloveLeft, transform.position + new Vector3(-(displacementX * .8f + 3 * displacementX), 2 * displacementY, 0), Quaternion.identity) as GameObject;
        droneGloveRight = Instantiate(droneGloveRight, transform.position + new Vector3((displacementX * .8f + 3 * displacementX), 2 * displacementY, 0), Quaternion.identity) as GameObject;
        droneGloveRight.transform.localScale = new Vector3(1, 1, 1);

    }

    // passing left and right zone information to main zone height controller
    private void ZoneHeightLeft(int zone, float yPos)
    {
        //Debug.Log("zone-LEFT:" + zone + " scale value:" + yPos);

        ZoneHeight(zoneLeft[zone], zone, yPos);

        // rightZoneColor(zone);
    }

    private void ZoneHeightRight(int zone, float yPos)
    {
        //Debug.Log("zone-RIGHT:" + zone + " scale value:" + yPos);

        ZoneHeight(zoneRight[zone], zone, yPos);

        // leftZoneColor(zone);
    }

    internal void ZoneHeight(GameObject ZoneObject, int zone, float yPos)
    {
        Transform childTransform = ZoneObject.transform.GetChild(0);
        childTransform.localScale = new Vector3(1, 8 * Mathf.Pow(yPos, 2), 1); // Mathf.Pow(yPos, 2) for square (pow 2)
    }

    // passing information from left slider and right slider
    private void SliderLeft(float zPos)
    {
        SliderMotion(zone4SliderLeft, zPos);
    }

    private void SliderRight(float zPos)
    {

        SliderMotion(zone4SliderRight, zPos);

    }

    private void SliderMotion(GameObject selectedSlider, float zPos)
    {

        // Debug.Log("slider selecting" + selectedZone);

        //  Debug.Log("slider selected");

        Transform moveSlider = selectedSlider.transform.GetChild(0);

        moveSlider.localPosition = new Vector3(0, 0, 0.06f + zPos);

    }

    public void rightZoneColor(int zoneNumber)
    {
        for (int i = 0; i < zoneRight.Length; i++)
        {
            zoneRight[i].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        }
        zoneRight[zoneNumber].GetComponentInChildren<MeshRenderer>().material.color = zoneColors[zoneNumber];

        //Debug.Log("color: " + zoneColors[zoneNumber]);

    }

    public void leftZoneColor(int zoneNumber)
    {
        for (int i = 0; i < zoneLeft.Length; i++)
        //foreach (GameObject zones in zoneLeft)
        {

            zoneLeft[i].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            //Debug.Log(rightZones.Length);
        }


        zoneLeft[zoneNumber].GetComponentInChildren<MeshRenderer>().material.color = zoneColors[zoneNumber];

        //  Debug.Log("color: " + zoneColors[zoneNumber]);



    }

    internal int GetZone(float xPos)
    {

        //// xpos values are -0.3 to 0.3
        for (int i = 0; i < (xPosRange.Length - 1); i++)
        {

            if (xPos > xPosRange[i] && xPos < xPosRange[i + 1])
            {
                // Debug.Log("in zone: " + i);
                return i;
            }
        }
        //  Debug.Log(xPos + "did not fall into zones");
        return -1;
    }

    internal int GetYZone(float yPos)
    {

        //// ypos values are -0.9 to 0.9
        for (int i = 0; i < (yPosRange.Length - 1); i++)
        {

            if (yPos > yPosRange[i] && yPos < yPosRange[i + 1])
            {
                // Debug.Log("in zone: " + i);
                return i;
            }
        }
        //  Debug.Log(xPos + "did not fall into zones");
        return -1;
    }


    internal void UpdateLeftZone(float xPos, float yPos, float zPos)
    {
        Transform waterGloveScale = waterGloveLeft.transform.GetChild(0);
        //waterGloveScale.localScale = new Vector3(1, 1, 1);
        Transform droneGloveScale = droneGloveLeft.transform.GetChild(0);
       // droneGloveScale.localScale = new Vector3(1, 1, 1);
        int selectedYZone = GetYZone(yPos);

        int selectedZone = GetZone(xPos);
        leftZoneColor(selectedZone);

        if (selectedZone < 3)
        {
            ZoneHeightLeft(selectedZone, yPos);

        }
        else
        {
            if (selectedYZone == 0)
            {
                SliderLeft(zPos);
            }
            else if (selectedYZone == 1 && lastLeftZoneYState != 1) // check if it was in zone 1 before
            {

                // to turn left water glove switch on and off
       
                if (!isWaterGloveLeft)
                {

                    isWaterGloveLeft = true;
                    waterGloveScale.localScale = gloveScaleFactor;
                }
                else
                {
                    isWaterGloveLeft = false;
                    waterGloveScale.localScale = new Vector3(1f, 1, 1f);

                }

                Debug.Log("water glove mode " + isWaterGloveLeft);
            }
            else if (selectedYZone == 2 && lastLeftZoneYState != 2) // check if it was in zone 1 before
            {

        
                //Debug.Log("I am in " + selectedYZone + ", ypos: " + yPos);

                // to turn drone glove switch on and off
                if (!isDroneGloveLeft)
                {
                    droneGloveScale.localScale = gloveScaleFactor;
                    isDroneGloveLeft = true;
                }
                else
                {
                    droneGloveScale.localScale = new Vector3(1f, 1, 1f);
                    isDroneGloveLeft = false;
                }
            }
            Debug.Log("I am in " + selectedYZone + ", ypos: " + yPos);
            lastLeftZoneYState = selectedYZone;
        }

        return;
    }

    internal void UpdateRightZone(float xPos, float zPos, float yPos)
    {

        int selectedZone = GetZone(xPos);
        rightZoneColor(selectedZone);

        Transform waterGloveScale = waterGloveRight.transform.GetChild(0);
        //waterGloveScale.localScale = new Vector3(1, 1, 1);
        Transform droneGloveScale = droneGloveRight.transform.GetChild(0);
        //droneGloveScale.localScale = new Vector3(1, 1, 1);
        int selectedYZone = GetYZone(yPos);

        if (selectedZone < 3)
        {
            ZoneHeightRight(selectedZone, yPos);

        }
        else
        {
            if (selectedYZone == 0)
            {
                SliderLeft(zPos);
            }
            else if (selectedYZone == 1 && lastRightZoneYState != 1)// check if it was in zone 1 before)
            {

   
                //Debug.Log("I am in " + selectedYZone + ", ypos: " + yPos);

                // to turn water glove switch on and off

          
                if (!isWaterGloveRight)
                {
                    waterGloveScale.localScale = gloveScaleFactor;
                    isWaterGloveRight = true;
                }
                else
                {
                    waterGloveScale.localScale = new Vector3(1f, 1, 1f);
                    isWaterGloveRight = false;
                }
            }
            else if (selectedYZone == 2 && lastRightZoneYState != 2)
            {

     
                //Debug.Log("I am in " + selectedYZone + ", ypos: " + yPos);

                // to turn drone glove switch on and off

                if (!droneGloveRight)
                {
                    droneGloveScale.localScale = gloveScaleFactor;
                    isDroneGloveRight = true;
                }
                else
                {
                    droneGloveScale.localScale = new Vector3(1f, 1, 1f);
                    isDroneGloveRight = false;
                }
            }
            lastRightZoneYState = selectedYZone;
        }
   
        return;
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




