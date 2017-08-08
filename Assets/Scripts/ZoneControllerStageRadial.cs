using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZoneControllerStageRadial : MonoBehaviour
{


    [Range(2, 8)]
    public int stageZonesRows = 3;
    public int stageZonesColumns = 4;
    public int stageZoneSlices = 6;

    public GameObject drumObject;
    public GameObject[] stageDrums;
    public float percentageTilt;

    GameObject[] stageZoneX;
    // List<GameObject> zoneLeft;

    GameObject[] stageZoneY;
    float[] xPosRange;
    float[] zPosRange;
    GameObject[] stageZonesArray;
    // List<GameObject> zoneLeft;
    GameObject[,] stageZonesMatrix;

    // size of zone boxes
    private float zoneXdim = 0;
    private float zoneZdim = 0;


    [SerializeField]
    //[Range(-0.3f,1f)]
    float stageHeightOffGround = 0;    // this value should be based on the height of stage off base of basestations
    float zoneCenterYPoint = 1.5f;

    // [Header ("hello")]
    // [Space]
    [Header("Zone UI Elements")]
    public Image HUDxPosLeft;
    public Image HUDxPosRight;
    public Image HUDyPosLeft;
    public Image HUDyPosRight;

    public Text YPos;
    public Text InstrumentName;
    public Text ZPos;
    public GloveController gloveController;
    public bool fixedYHeight = true;

    String[] guitarTypes = { "Strat Solo", "Strat Chords", "Strat Chords", "Other" };
    int[] zonesUsed = { 0, 2, 6, 8 };

    Hashtable guitarName = new Hashtable();


    public Transform FrontLeft;
    public Transform FrontRight;
    public Transform BackLeft;
    public Transform BackRight;

    // private Color zone1Color;
    static Color ocher = new Color(255 / 255f, 178 / 255f, 69 / 255f, 0.2f);
    static Color fuscia = new Color(255 / 255f, 79 / 255f, 218 / 255f, 0.2f);
    static Color turquoise = new Color(30 / 255f, 219 / 255f, 232 / 255f, 0.2f);
    static Color blueness = new Color(0, 150 / 255f, 242 / 255f, 0.2f);
    Color[] zoneColors = { turquoise, ocher, fuscia, blueness };

    Color[,] zoneColorMatrix;

    public int whichSide = 1; // -1 is right, 1 is left

    float distanceX; // full width of stage
    float distanceZ; // full depth of stage
    float drumRadius;
    float zoneZoffset;

    int selectedRow;
    int selectedCol;
    float zoneCenterXPoint;
    float zoneCenterZPoint;
    private float xOffsetRadial;
    private float zOffsetRadial;


    void Start()
    {
        // adds list of guitar names and corresponding zones to hashtable
        for (int i = 0; i < guitarTypes.Length; i++) {
            guitarName.Add(zonesUsed[i], guitarTypes[i]);
        }

        // instantiate zone matrix arrays
        stageZonesArray = new GameObject[stageZoneSlices];

        zoneColorMatrix = new Color[4, 4] { { turquoise, Color.white, fuscia, blueness }, { Color.white, Color.white, blueness, turquoise }, { blueness, Color.white, blueness, ocher }, { turquoise, Color.white, fuscia, blueness } };

        //new Color(255f / i, 255f / i * 0.8f, 255f / i * 0.6f, 0.2f)

        InstantiateZoneDrumObjects();
        Debug.Log("drum stage data in");
    }
    /*
    private void Update()
    {
        for (int i = 0; i < stageZonesRows; i++)
        {
            for (int j = 0; j < stageZonesColumns; j++)
            {
                ZoneHeight(zoneLeft[i], i, 0);
        }
        
        
    }*/

    public void InstantiateZoneDrumObjects()
    {
        Debug.Log("instantiating" + stageZoneSlices + "objects");
        int radialDegrees = 360 / stageZoneSlices;
        //  FrontLeft.position += new Vector3(0, stageHeightOffGround, 0); // set the stage height for zone starting height;

        for (int i = 0; i < stageZoneSlices; i++)
        {

            GameObject zone = null;
            drumRadius = 1f; // distance from center
            float rad = radialDegrees * Mathf.Deg2Rad;

            // transform in a radial array
            xOffsetRadial = drumObject.transform.position[0] + Mathf.Cos(rad * i) * drumRadius;
            zOffsetRadial = drumObject.transform.position[2] + Mathf.Sin(rad * i) * drumRadius;

            // sin(pi) is one loop
            float tiltDegreesX = Mathf.Rad2Deg * Mathf.Sin(Mathf.PI / (stageZoneSlices / 2) * i);
            float tiltDegreesZ = Mathf.Rad2Deg * Mathf.Cos(Mathf.PI / (stageZoneSlices / 2) * i);
            percentageTilt = 0.3f;
            // using the front left corner of the stage as starting point
            zone = Instantiate(drumObject, new Vector3(xOffsetRadial, 0, zOffsetRadial), Quaternion.Euler(-percentageTilt * tiltDegreesX, 0, percentageTilt * tiltDegreesZ)) as GameObject;
            zone.name = "stage-zone-" + i;
            zone.transform.parent = transform;
            zone.SetActive(true);

            stageZonesArray[i] = zone;


        }

    }

    void setZoneWithKeypress()
    {
        if (Input.GetKeyDown("q"))
        {

        }
    }

    void GetRadialZone()
    {
        // check input x,y position from syncphony and calculate the angle, compare againsts angles above
        // if mathf.arctan() < angle etc
        // return zone


    }

    // returns the zone number based on the selected zone at the time
    int GetZoneNumber()
    {
        return GetZoneNumber(selectedRow, selectedCol);
    }

    // passing left and right zone information to main zone height controller
    private void ZoneHeightBlock(GameObject zone, float yPos)
    {
        // Debug.Log("zone-LEFT:" + zone + " scale value:" + yPos);
        for (int i = 0; i < stageZonesRows; i++)
        {
            // resets each zone to zero
            for (int j = 0; j < stageZonesColumns; j++)
            {

                stageZonesMatrix[i, j].SetActive(false);
                ZoneHeight(stageZonesMatrix[i, j], 0);
            }

        }

        // only change selected zone's height
        zone.SetActive(true);

        if (fixedYHeight)
        {
            yPos = 1.0f;
        }

        ZoneHeight(zone, yPos);

    }
}



