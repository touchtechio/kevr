using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZoneController : MonoBehaviour
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
  

    public GameObject[] rightZones;
    public GameObject[] leftZones;
    public GameObject rightZone1;
    public GameObject rightZone2;
    public GameObject rightZone3;
   
      */
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

    int _amount = 4;

    [SerializeField]
    GameObject zoneObject;


   GameObject[] zoneLeft;
   // List<GameObject> zoneLeft;

    GameObject[] zoneRight;

    [SerializeField]
    float displacementX = 0.3f;

    [SerializeField]
    float displacementY = 0.2f;

    [SerializeField]
    int displacementZ = 0;

    public GameObject zone4SliderLeft;
    GameObject zone4SliderRight;
    public GameObject waterGloveLeft;
    GameObject waterGloveRight;
    public GameObject droneGloveLeft;
    GameObject droneGloveRight;

    Color[] zoneColors = { Color.green, Color.yellow, Color.red, Color.cyan };

    [SerializeField]
    float[] xPosRange = { -0.35f, -0.15f, 0f, 0.15f, 0.35f };

    void Start()
    {
        zoneLeft = new GameObject[_amount];
        //zoneLeft = new List<GameObject>();

        for (int i = 0; i < (_amount-1); i++)
        {
            // instantiate fountains based on prefab and then assign to fountain jet array
            GameObject zone = Instantiate(zoneObject, transform.position + new Vector3(-(displacementX * .5f + i * displacementX), 0, i * displacementZ), Quaternion.identity) as GameObject;
           // zoneLeft.Add((GameObject)zone);
      
            zoneLeft[i] = zone;
        }

        zoneRight = new GameObject[_amount];

        for (int i = 0; i < (_amount-1); i++)
        {
            // instantiate fountains based on prefab and then assign to fountain jet array
            GameObject zone = Instantiate(zoneObject, transform.position + new Vector3(displacementX * .5f + i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            zoneRight[i] = zone;
        }

        // instantiate slider objects
        zone4SliderLeft = Instantiate(zone4SliderLeft, transform.position + new Vector3(-(displacementX * .5f + 3 * displacementX), 0.05f, -0.1f), Quaternion.identity) as GameObject;
        zoneLeft[3] = zone4SliderLeft;
        //zoneLeft.Add((GameObject)zone4SliderLeft);
        zone4SliderRight = Instantiate(zone4SliderLeft, transform.position + new Vector3((displacementX * .5f + 3 * displacementX), 0.05f, -0.1f), Quaternion.identity) as GameObject;
        zoneRight[3] = zone4SliderRight;

        waterGloveLeft = Instantiate(waterGloveLeft, transform.position + new Vector3(-(displacementX * .5f + 3 * displacementX), displacementY, 0), Quaternion.identity) as GameObject;
        waterGloveRight = Instantiate(waterGloveLeft, transform.position + new Vector3((displacementX * .5f + 3 * displacementX), displacementY, 0), Quaternion.identity) as GameObject;
        waterGloveRight.transform.localScale = new Vector3(-1, 1, 1);
        droneGloveLeft = Instantiate(droneGloveLeft, transform.position + new Vector3(-(displacementX * .5f + 3 * displacementX), 2 * displacementY, 0), Quaternion.identity) as GameObject;
        droneGloveRight = Instantiate(droneGloveLeft, transform.position + new Vector3((displacementX * .5f + 3 * displacementX), 2 * displacementY, 0), Quaternion.identity) as GameObject;
        droneGloveRight.transform.localScale = new Vector3(-1, 1, 1);

    }

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
        childTransform.localScale = new Vector3(1, 5* yPos, 1);
    }

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

        moveSlider.localPosition = new Vector3(0, 0, 0.06f+ zPos);

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

    internal void UpdateLeftZone(float xPos, float zPos, float yPos)
    {

        int selectedZone = GetZone(xPos);
        leftZoneColor(selectedZone);

        if (selectedZone < 3)
        {
            ZoneHeightLeft(selectedZone, yPos);
          
        }
        else
        {
            
            SliderLeft(zPos);
        }

        return;
    }

    internal void UpdateRightZone(float xPos, float zPos, float yPos)
    {

        int selectedZone = GetZone(xPos);
        rightZoneColor(selectedZone);

        if (selectedZone < 3)
        {
            ZoneHeightRight(selectedZone, yPos);
            
        }
        else
        {
            
            SliderRight(zPos);
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




