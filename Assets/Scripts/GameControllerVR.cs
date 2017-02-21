using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerVR : MonoBehaviour {
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

}
