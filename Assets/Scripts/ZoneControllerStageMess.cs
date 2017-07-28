using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZoneControllerStageMess : MonoBehaviour
{


    [Range(2, 8)]
    public int stageZonesRows = 3;
    public int stageZonesColumns = 4;

    public GameObject zoneObject;
    public GameObject stageZoneObject;

    GameObject[] stageZoneX;
    // List<GameObject> zoneLeft;

    GameObject[] stageZoneY;
    float[] xPosRange;
    float[] zPosRange;
    GameObject[] stageZones;
    // List<GameObject> zoneLeft;
    GameObject[,] stageZonesMatrix;

    // size of zone boxes
    private float zoneXdim = 0;
    private float zoneZdim = 0;
  

    [SerializeField]
    //[Range(-0.3f,1f)]
    float stageHeightOffGround = 0;    // this value should be based on the height of stage off base of basestations
    float zoneCenterYPoint = 1.5f;

    public GameObject zone4SliderLeft;
    public GameObject zone4SliderRight;
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

    String[] guitarTypes = { "Strat Solo", "Strat Chords", "Strat Chords", "Other"};
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
    float zoneXoffset;
    float zoneZoffset;

    int selectedRow;
    int selectedCol;
    float zoneCenterXPoint;
    float zoneCenterZPoint;


    void Start()
    {
        // adds list of guitar names and corresponding zones to hashtable
        for (int i = 0; i < guitarTypes.Length; i++) {
            guitarName.Add(zonesUsed[i], guitarTypes[i]);
        }

        // instantiate zone matrix arrays
        stageZones = new GameObject[stageZonesRows];
        stageZonesMatrix = new GameObject[stageZonesRows, stageZonesColumns];

        xPosRange = new float[stageZonesRows+1];
        zPosRange = new float[stageZonesColumns+1];

        //distanceX = FrontRight.position[0] - FrontLeft.position[0];
        distanceX = FrontRight.position[0] - FrontLeft.position[0];
        distanceZ = FrontLeft.position[2] - BackLeft.position[2];
        //Debug.Log("displacement Z of stage " + distanceZ);

        // set size of each column and row
        zoneXdim = distanceX / stageZonesRows;
        zoneZdim = distanceZ / stageZonesColumns;
        zoneColorMatrix = new Color[4, 4] { { turquoise, Color.white, fuscia, blueness }, { Color.white, Color.white, blueness, turquoise }, { blueness, Color.white, blueness, ocher }, { turquoise, Color.white, fuscia, blueness } };
    
        //new Color(255f / i, 255f / i * 0.8f, 255f / i * 0.6f, 0.2f)
        
        InstantiateZoneObjects();
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

    public void InstantiateZoneObjects()
    {

        FrontLeft.position += new Vector3(0, stageHeightOffGround, 0); // set the stage height for zone starting height;

        for (int i = 0; i < stageZonesRows; i++)
        {
            for (int j = 0; j < stageZonesColumns; j++)
            {
                //Debug.Log("front left position: "+ FrontLeft.position);
                GameObject zone = null;
                zoneXoffset = zoneXdim / 2 + (i) * zoneXdim; // compensates for the object position point being set at the center of object not corner
                zoneZoffset = -zoneZdim / 2 - j * zoneZdim;

                // using the front left corner of the stage as starting point
                zone = Instantiate(stageZoneObject, FrontLeft.position + new Vector3(zoneXoffset, 0, zoneZoffset), Quaternion.identity) as GameObject;
                zone.name = "stage-zone-" + i +"-"+ j + " #"+ GetZoneNumber(i,j);
                zone.transform.parent = transform;
                zone.SetActive(false);

                stageZonesMatrix[i,j] = zone;
                // scales up the size of the prefab to fit the size of zone
                stageZonesMatrix[i,j].transform.localScale = new Vector3(-zoneXdim*10, 0, zoneZdim*10);
                stageZonesMatrix[i,j].GetComponent<Renderer>().material.color = zoneColorMatrix[i,j];

                // calculate front and side point of each zone
                xPosRange[i] = zone.transform.localPosition[0] - 0.5f * zoneXdim;
                zPosRange[j] = zone.transform.localPosition[2] + 0.5f * zoneZdim;

            }
           
        }
        // calculate the back and end point of last zone in row + column
        xPosRange[stageZonesRows] = xPosRange[stageZonesRows - 1] + 0.5f * zoneXdim;
        zPosRange[stageZonesColumns] = zPosRange[stageZonesColumns - 1] - 0.5f * zoneZdim;

        // get each point of zone
        Debug.Log(xPosRange[0] + "," + xPosRange[1] + "," + xPosRange[2] + "," + xPosRange[3]);// + "," + xPosRange[4]);
        Debug.Log(zPosRange[0] + "," + zPosRange[1] + "," + zPosRange[2] + "," + zPosRange[3]);// + "," + zPosRange[4]);

    }

    // returns the zone number as a single int for the initial instantiation
    int GetZoneNumber(int i, int j)
    {
        return (j + i * stageZonesColumns);
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

    // sets the selected zone height and returns rest as zero
    internal void ZoneHeight(GameObject ZoneObject, float yPos)
    {

       // Transform childTransform = ZoneObject.transform.GetChild(0);
        ZoneObject.transform.localScale = new Vector3(-zoneXdim * 10, yPos* 5, zoneZdim * 10); 
        // Mathf.Pow(yPos, 2) for square (pow 2)
     // Debug.Log("zone height" + childTransform.localScale);
    }

    public void ZoneColor(GameObject selectedZoneObject, bool isRSLeft)
    {
        for (int i = 0; i < stageZonesRows; i++)
        //foreach (GameObject zones in zoneLeft)
        {
            for (int j = 0; j < stageZonesColumns; j++)
            {
                stageZonesMatrix[i, j].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
                //Debug.Log(rightZones.Length);
            }

        }

        selectedZoneObject.GetComponentInChildren<MeshRenderer>().material.color = zoneColorMatrix[selectedRow, selectedCol];
        //HUDxPosLeft.color = zoneColors[zoneNumber];
        
        // sets color of the HUD x position element to change colors based on zone changes
        /*
        if (isRSLeft == true)
        {
            HUDxPosLeft.color = zoneColors[zoneNumber];
        } else {

            HUDxPosRight.color = zoneColors[zoneNumber];
            } 
        //  Debug.Log("color: " + zoneColors[zoneNumber]);
        */
    }

    // returns the selected zone object and records the matrix number
    internal GameObject GetStageZone(float xPos, float zPos)
    {

        for (int i = 0; i < xPosRange.Length - 1; i++)
        {
            for (int j = 0; j < zPosRange.Length - 1; j++)
            {
 
                if ((xPos > xPosRange[i]) && (xPos < xPosRange[i + 1]))
                {
                    if ((zPos < zPosRange[j]) && (zPos > zPosRange[j + 1]))
                    {
                       // Debug.Log(xPos + ": in x zone: " + i);
                       // Debug.Log(zPos + ": in z zone: " + j);
                        selectedRow = i;
                        selectedCol = j;

                        return stageZonesMatrix[i, j];

                    }
                }
            }
        }
        //Debug.Log(xPos + "x" + zPos +"z" +": did not fall into zones");
        return null;
    }


    /// <summary>
    /// runs the get stage zone calculation, moved block heights then returns
    /// sets instrument name on HUD
    /// </summary>
    /// <param name="xPos"></param>
    /// <param name="yPos"></param>
    /// <param name="zPos"></param>
    internal void UpdateZone(float xPos, float yPos, float zPos)
    {
        
        GameObject selectedZoneObject = GetStageZone(xPos, zPos);

        String text = (String)guitarName[GetZoneNumber()];
        if (text != null)
        {
            // update text
            InstrumentName.text = text;
        }

        UpdateZoneObjectPosition(selectedZoneObject);

        if (selectedZoneObject == null)
        {
            //Debug.Log(xPos + "," + yPos + "," + zPos + " : yielded no zone");
            return;
        }

        // left zones filled

        ZoneHeightBlock(selectedZoneObject, yPos);
        
        // sets zone color and HUD zone height fill for right hand
        ZoneColor(selectedZoneObject, false);
        //HUDyPosRight.fillAmount = yPos;

        //Vector3 zoneCenter = selectedZoneObject.GetComponent<Renderer>().bounds.center;
        //Debug.Log("zoneCenterPont" + zoneCenter[0]);
        // Debug.Log("ypos: " + yPos);

        return;
    }

    // returns the objects position as the center point of activated zone
    void UpdateZoneObjectPosition(GameObject selectedZoneObject)
    {
        if (selectedZoneObject == null)
        {
            //Debug.Log(xPos + "," + yPos + "," + zPos + " : yielded no zone");
            return;
        }

        zoneCenterXPoint = selectedZoneObject.transform.localPosition[0];
        zoneCenterZPoint = selectedZoneObject.transform.localPosition[2];

    }

    // sets up the ideal position for the zone object
    public Vector3 GetZoneObjectPosition()
    {
        return new Vector3(zoneCenterXPoint, zoneCenterYPoint, zoneCenterZPoint);
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




