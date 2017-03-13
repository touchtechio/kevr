using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneController : MonoBehaviour {
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

    [SerializeField]
    int _amount = 3;

    [SerializeField]
    GameObject zoneObject;

    [SerializeField]
    GameObject[] zoneLeft;

    [SerializeField]
    GameObject[] zoneRight;

    [SerializeField]
    float displacementX = 0.3f;

    [SerializeField]
    int displacementZ = 0;

    Color[] zoneColors = { Color.green, Color.yellow, Color.red };

    void Start()
    {
        zoneLeft = new GameObject[_amount];

        for (int i = 0; i < _amount; i++)
        {
            // instantiate fountains based on prefab and then assign to fountain jet array
            GameObject zone = Instantiate(zoneObject, transform.position + new Vector3(-(displacementX *.5f + i * displacementX), 0, i * displacementZ), Quaternion.identity) as GameObject;
            zoneLeft[i] = zone;
        }

        zoneRight = new GameObject[_amount];

        for (int i = 0; i < _amount; i++)
        {
            // instantiate fountains based on prefab and then assign to fountain jet array
            GameObject zone = Instantiate(zoneObject, transform.position + new Vector3(displacementX * .5f+i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            zoneRight[i] = zone;
        }

    }

    internal void ZoneHeightLeft(int zone, float yPos)
    {
        //Debug.Log("zone-LEFT:" + zone + " scale value:" + yPos);
        ZoneHeight(zoneLeft[zone], zone, yPos);
       // rightZoneColor(zone);
    }

    internal void ZoneHeightRight(int zone, float yPos)
    {
        //Debug.Log("zone-RIGHT:" + zone + " scale value:" + yPos);
        ZoneHeight(zoneRight[zone], zone, yPos);
       // leftZoneColor(zone);
    }

    internal void ZoneHeight(GameObject ZoneObject, int zone, float yPos)
    {
        Transform childTransform = ZoneObject.transform.GetChild(0);
        childTransform.localScale = new Vector3(1, (1 + yPos), 1);
    }


    public void rightZoneColor(int zoneNumber)
    {
        for (int i = 0; i < zoneRight.Length; i++ )
        {
            zoneRight[i].GetComponentInChildren<MeshRenderer>().material.color = Color.white;

      
            //Debug.Log(rightZones.Length);

        }
       zoneRight[zoneNumber].GetComponentInChildren<MeshRenderer>().material.color = zoneColors[zoneNumber];

        Debug.Log("color: "+ zoneColors[zoneNumber]);

    }

    public void leftZoneColor(int zoneNumber)
    {
        for (int i = 0; i < zoneLeft.Length; i++)
        {
            zoneLeft[i].GetComponentInChildren<MeshRenderer>().material.color = Color.white;


            //Debug.Log(rightZones.Length);

        }
        zoneLeft[zoneNumber].GetComponentInChildren<MeshRenderer>().material.color = zoneColors[zoneNumber];

        Debug.Log("color: " + zoneColors[zoneNumber]);



    }

    internal int GetZone(float xPos)
    {

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
    }


}
