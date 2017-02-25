using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FountainRSControllerVR : MonoBehaviour {
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

    public GameObject[] rightZones;
    public GameObject[] leftZones;
    public GameObject rightZone1;
    public GameObject rightZone2;
    public GameObject rightZone3;
    Color[] zoneColors = {Color.green, Color.yellow, Color.red};

    public GameObject[] fountainJetsLeft;

    void Awake()
    {
        
      //  source = GetComponent<AudioSource>();  
    }



    void Start()
    {


    }


    public void fountainHeightLeft(int i, float fingerBendDataLeft)
    {
        Debug.Log("finger" + i + "bend value" + fingerBendDataLeft);
        fountainJetsLeft[i].transform.localScale = new Vector3 (10,20 + 1000 *fingerBendDataLeft,10);

        
    


    }


    public void fountainHeightRight(int i, float fingerBendDataRight)
    {
        Debug.Log("finger" + i + "bend value" + fingerBendDataRight);
    }
    /*
    public void rsZoneLeftXZ(float rsZoneDataLeftX, float rsZoneDataLeftZ)
    {
        Debug.Log("left hand is at x pos " + rsZoneDataLeftX + " y pos " + rsZoneDataLeftZ);
        //  HandObject.GetComponent<Transform> = new Vector3(rsZoneLeftX, rsZoneLeftY, rsZoneLeftZ);
    }


    public void rsZoneLeftY(float rsZoneDataLeftY)
    {
        Debug.Log("left hand is at y pos " + rsZoneDataLeftY);
    }

    public void rsZoneRightXZ(float rsZoneDataRightX, float rsZoneDataRightZ)
    {
        Debug.Log("right hand is at x pos " + rsZoneDataRightX + " y pos " + rsZoneDataRightZ);
        //  HandObject.GetComponent<Transform> = new Vector3(rsZoneLeftX, rsZoneLeftY, rsZoneLeftZ);
    }


    public void rsZoneRightY(float rsZoneDataRightY)
    {
        Debug.Log("left hand is at y pos " + rsZoneDataRightY);
    }

    public void rightZoneColor(int zoneNumber)
    {
        for (int i = 0; i < rightZones.Length; i++ )
        {
            rightZones[i].GetComponent<Renderer>().sharedMaterial.color = Color.white;
            //Debug.Log(rightZones.Length);
           
        }


    }

    public void leftZoneColor(int zoneNumber)
    {
        for (int i = 0; i < leftZones.Length; i++)
        {
            leftZones[i].GetComponent<Renderer>().sharedMaterial.color = Color.white;
            //Debug.Log(rightZones.Length);

        }
        leftZones[zoneNumber - 1].GetComponent<Renderer>().sharedMaterial.color = zoneColors[zoneNumber - 1];
        Debug.Log(zoneNumber);



    }
    */

}
