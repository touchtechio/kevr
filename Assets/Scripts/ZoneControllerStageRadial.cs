using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZoneControllerStageRadial : MonoBehaviour
{

    public StickController stickController;
    public GameObject drumstick;


    public DrumAudio[] drumAudioAray;
    

    [Range(2, 10)]
    public int stageZoneSlices = 6;

    public GameObject drumObject;
    public GameObject[] stageDrums;
    public float percentageTilt;

    GameObject[] stageZoneX;
    // List<GameObject> zoneLeft;

    GameObject[] stageZoneY;
    float[] xPosRange;
    float[] zPosRange;
    public GameObject[] stageZonesArray;
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
    Color[] zoneColors = { turquoise, ocher, fuscia, blueness, turquoise, ocher, fuscia, blueness };

    public int whichSide = 1; // -1 is right, 1 is left

    float distanceX; // full width of stage
    float distanceZ; // full depth of stage
    public float drumRadius = 0.5f; // distance from center
  
    float zoneZoffset;

    public int selectedZone= 0;
    int selectedCol;
    float zoneCenterXPoint;
    float zoneCenterZPoint;
    private float xOffsetRadial;
    private float zOffsetRadial;
    public float[] xOffsetRadialArray;
    public float[] zOffsetRadialArray;
    private string[] keypresses = { "q", "w", "e", "r", "t", "y", "u", "i" };
    

    void Start()
    {
        // adds list of guitar names and corresponding zones to hashtable
        for (int i = 0; i < guitarTypes.Length; i++) {
            guitarName.Add(zonesUsed[i], guitarTypes[i]);
        }

        // instantiate zone matrix arrays
        stageZonesArray = new GameObject[stageZoneSlices];
        xOffsetRadialArray = new float[stageZoneSlices];
        zOffsetRadialArray = new float[stageZoneSlices];

        //new Color(255f / i, 255f / i * 0.8f, 255f / i * 0.6f, 0.2f)

        InstantiateZoneDrumObjects();

        Debug.Log("drum stage data in");
    }
    
    private void Update()
    {
        setZoneWithKeypress();
       
    }

    public void InstantiateZoneDrumObjects()
    {
        Debug.Log("instantiating" + stageZoneSlices + "objects");
        int radialDegrees = 360 / stageZoneSlices;
        //  FrontLeft.position += new Vector3(0, stageHeightOffGround, 0); // set the stage height for zone starting height;

        for (int i = 0; i < stageZoneSlices; i++)
        {

            GameObject zone = null;

            float rad = radialDegrees * Mathf.Deg2Rad;

            // transform in a radial array
            xOffsetRadial = drumObject.transform.position[0] + Mathf.Sin(rad * i) * drumRadius;
            zOffsetRadial = drumObject.transform.position[2] + Mathf.Cos(rad * i) * drumRadius;
            xOffsetRadialArray[i] = xOffsetRadial;
            zOffsetRadialArray[i] = zOffsetRadial;
            // sin(pi) is one loop
            float tiltDegreesX = Mathf.Rad2Deg * Mathf.Cos(Mathf.PI / (stageZoneSlices / 2) * i);
            float tiltDegreesZ = Mathf.Rad2Deg * Mathf.Sin(Mathf.PI / (stageZoneSlices / 2) * i);
            percentageTilt = 0.3f;
            // using the front left corner of the stage as starting point
            zone = Instantiate(drumObject, new Vector3(xOffsetRadial, 0, zOffsetRadial), Quaternion.Euler(-percentageTilt * tiltDegreesX, 0, percentageTilt * tiltDegreesZ)) as GameObject;
            zone.name = "stage-zone-" + i;
            zone.transform.parent = transform;
            zone.SetActive(true);
         
            stageZonesArray[i] = zone;

        }

    }



    // passing left and right zone information to main zone height controller
    private void ZoneHeightBlock(GameObject zone, float yPos)
    {
        // Debug.Log("zone-LEFT:" + zone + " scale value:" + yPos);
        for (int i = 0; i < stageZoneSlices; i++)
        {
            stageZonesArray[i].SetActive(false);
            //  ZoneHeight(stageZonesMatrix[i, j], 0);
        }
            // only change selected zone's height
            zone.SetActive(true);

            if (fixedYHeight)
            {
                yPos = 1.0f;
            }

            ZoneHeight(zone, yPos);  
    }

    // sets the selected zone height and returns rest as zero
    internal void ZoneHeight(GameObject ZoneObject, float yPos)
    {

        // Transform childTransform = ZoneObject.transform.GetChild(0);
        ZoneObject.transform.localScale = new Vector3(-zoneXdim * 10, yPos * 5, zoneZdim * 10);
        // Mathf.Pow(yPos, 2) for square (pow 2)
        // Debug.Log("zone height" + childTransform.localScale);
    }

    public void RadialZoneColor(GameObject selectedZoneObject)
    {
        for (int i = 0; i < stageZoneSlices; i++)
        {
            //foreach (GameObject zones in zoneLeft)

            stageZonesArray[i].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            //Debug.Log(rightZones.Length);
        }

        selectedZoneObject.GetComponentInChildren<MeshRenderer>().material.color = zoneColors[selectedZone];
    }

    public void setZoneWithKeypress()
    {
        for (int i = 0; i < stageZoneSlices; i++)
        {
            if (Input.GetKeyDown(keypresses[i]))
            {
                selectedZone = i;
                //Debug.Log(selectedZone);
                GameObject selectedZoneObject = stageZonesArray[i];
                RadialZoneColor(selectedZoneObject);
                stickController.SetStickPosition(xOffsetRadialArray[i] / 2, 0, zOffsetRadialArray[i] / 2);
                stickController.SetStickAngle(drumstick, i * 360 / stageZoneSlices);
                return;
            }

        }
         

    }
    // returns zone to the stck controller
    public int GetZone()
    {
        Debug.Log(selectedZone);
        return selectedZone;

    }
    // figures out which zone the stick has landed in and returns that drum and zone number
    internal GameObject GetStageRadialZone(float xPos, float zPos)
    {
        // TO DO!!!
        // check input x,z position from syncphony and calculate the angle, compare againsts angles above
        // if mathf.arctan() < angle etc
        // return zone

        for (int i = 0; i < xPosRange.Length - 1; i++)
        {

            if ((xPos > xPosRange[i]) && (xPos < xPosRange[i + 1]))
            {
                // Debug.Log(xPos + ": in x zone: " + i);
                // Debug.Log(zPos + ": in z zone: " + j);
                selectedZone = i;
                return stageZonesArray[i];
            }
        }

        //Debug.Log(xPos + "x" + zPos +"z" +": did not fall into zones");
        return null;
    }


    /// <summary>
    /// called from OSC message receiver
    /// runs the get stage zone calculation, moved block heights then returns
    /// Change the color of the drum
    /// sets instrument name on HUD
    /// </summary>
    /// <param name="xPos"></param>
    /// <param name="yPos"></param>
    /// <param name="zPos"></param>
    internal void UpdateZone(float xPos, float yPos, float zPos)
    {
        // work out which zone is affected and get the new stick position
        GameObject selectedZoneObject = GetStageRadialZone(xPos, zPos);

        // do the calculation of where the stick should be located based on the selected zone
        UpdateStickPosition(selectedZone);

        if (selectedZoneObject == null)
        {
            //Debug.Log(xPos + "," + yPos + "," + zPos + " : yielded no zone");
            return;
        }

        // left zones filled

        ZoneHeightBlock(selectedZoneObject, yPos);

        // sets zone color and HUD zone height fill for right hand
        RadialZoneColor(selectedZoneObject);
        //HUDyPosRight.fillAmount = yPos;

        //Vector3 zoneCenter = selectedZoneObject.GetComponent<Renderer>().bounds.center;
        //Debug.Log("zoneCenterPont" + zoneCenter[0]);
        // Debug.Log("ypos: " + yPos);

        return;
    }

    // returns the objects position with a radial offset
    void UpdateStickPosition(int selectedZone)
    {

        zoneCenterXPoint = xOffsetRadialArray[selectedZone];
        zoneCenterZPoint = zOffsetRadialArray[selectedZone];

    }

    // sets up the ideal position for the zone object using the positions from above
    public Vector3 GetStickPosition()
    {
        return new Vector3(zoneCenterXPoint, zoneCenterYPoint, zoneCenterZPoint);
    }

}




