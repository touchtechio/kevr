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

    [Range(0,10)]
    public int displacementY = 0;

    public GameObject zone4SliderLeft;
    public GameObject zone4SliderRight;
    // [Header ("hello")]
    // [Space]
    public Image HUDxPosLeft;
    public Image HUDxPosRight;
    public Image HUDyPosLeft;
    public Image HUDyPosRight;


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

    int selectedRowColor;
    int selectedColColor;
    float zoneCenterXPoint;
    float zoneCenterZPoint;

    void Start()
    {
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
        zoneColorMatrix = new Color[4, 4] { { turquoise, ocher, fuscia, blueness }, {  ocher, fuscia, blueness, turquoise }, { blueness, fuscia, blueness, ocher }, { turquoise, ocher, fuscia, blueness } };
    
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
        /*
        for (int i = 0; i < (stageZonesRows); i++)
        {
            GameObject zone = null;
            
            zone = Instantiate(stageZoneObject, FrontLeft.position + new Vector3(displacementX/2 + (i) * displacementX, 0, -displacementX/2), Quaternion.identity) as GameObject;

            zone.name = "zone-left-" + i;
            zone.transform.parent = transform;
            
            stageZones[i] = zone;
            stageZones[i].transform.localScale = new Vector3(-displacementX, 1, 1);
            stageZones[i].GetComponent<Renderer>().material.color = zoneColors[i];
        }
        */

        FrontLeft.position += new Vector3(0, stageHeightOffGround, 0); // set the stage height for zone starting height;

        for (int i = 0; i < stageZonesRows; i++)
        {
            for (int j = 0; j < stageZonesColumns; j++)
            {
                //Debug.Log("front left position: "+ FrontLeft.position);
                GameObject zone = null;
                zoneXoffset = zoneXdim / 2 + (i) * zoneXdim;
                zoneZoffset = -zoneZdim / 2 - j * zoneZdim;

                zone = Instantiate(stageZoneObject, FrontLeft.position + new Vector3(zoneXoffset, 0, zoneZoffset), Quaternion.identity) as GameObject;
                zone.name = "stage-zone-" + i +"-"+ j + " #"+(j+i*stageZonesColumns);
                zone.transform.parent = transform;
                zone.SetActive(false);

                stageZonesMatrix[i,j] = zone;
                stageZonesMatrix[i,j].transform.localScale = new Vector3(-zoneXdim*10, displacementY, zoneZdim*10);
                stageZonesMatrix[i,j].GetComponent<Renderer>().material.color = zoneColorMatrix[i,j];

                xPosRange[i] = zone.transform.localPosition[0] - 0.5f * zoneXdim;
                zPosRange[j] = zone.transform.localPosition[2] + 0.5f * zoneZdim;

            }
           
        }
        xPosRange[stageZonesRows] = xPosRange[stageZonesRows - 1] + 0.5f * zoneXdim;
        zPosRange[stageZonesColumns] = zPosRange[stageZonesColumns - 1] - 0.5f * zoneZdim;

        Debug.Log(xPosRange[0] + "," + xPosRange[1] + "," + xPosRange[2] + "," + xPosRange[3]);// + "," + xPosRange[4]);
        Debug.Log(zPosRange[0] + "," + zPosRange[1] + "," + zPosRange[2] + "," + zPosRange[3]);// + "," + zPosRange[4]);

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
        ZoneHeight(zone, yPos);

    }


    internal void ZoneHeight(GameObject ZoneObject, float yPos)
    {

       // Transform childTransform = ZoneObject.transform.GetChild(0);
        ZoneObject.transform.localScale = new Vector3(-zoneXdim * 10, yPos* 5, zoneZdim * 10); // Mathf.Pow(yPos, 2) for square (pow 2)
                                                                               // Debug.Log("zone height" + childTransform.localScale);
    }
    /*
    internal void ZoneHeight(GameObject ZoneObject, int zone, float yPos)
    {

        Transform childTransform = ZoneObject.transform.GetChild(0);
        childTransform.localScale = new Vector3(1, 8 * Mathf.Pow(yPos, 2), 1); // Mathf.Pow(yPos, 2) for square (pow 2)
       // Debug.Log("zone height" + childTransform.localScale);
    }

    */


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

        selectedZoneObject.GetComponentInChildren<MeshRenderer>().material.color = zoneColorMatrix[selectedRowColor, selectedColColor];
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

    //internal int Get
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
                        selectedRowColor = i;
                        selectedColColor = j;
                        return stageZonesMatrix[i, j];

                    }
                }
            }
        }
        //Debug.Log(xPos + "x" + zPos +"z" +": did not fall into zones");
        return null;
    }

    /*
    /// setting height zones

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
    */

    /// <summary>
    /// runs the get stage zone calculation, moved block heights then returns
    /// 
    /// </summary>
    /// <param name="xPos"></param>
    /// <param name="yPos"></param>
    /// <param name="zPos"></param>
    internal void UpdateZone(float xPos, float yPos, float zPos)
    {
        
        GameObject selectedZoneObject = GetStageZone(xPos, zPos);


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

    void InstantiateSliderObjects()
    {
        // instantiate slider objects
        zone4SliderLeft = Instantiate(zone4SliderLeft, transform.position + new Vector3(-(zoneXdim * .45f + 3 * zoneXdim + 0.05f), 0.05f, -0.1f), Quaternion.identity) as GameObject;
        zone4SliderLeft.name = "zone-left-" + 3;
        zone4SliderLeft.transform.parent = transform;
        stageZones[3] = zone4SliderLeft;
        //zoneLeft.Add((GameObject)zone4SliderLeft);
        zone4SliderRight = Instantiate(zone4SliderLeft, transform.position + new Vector3((zoneXdim * .45f + 3 * zoneXdim) + 0.05f, 0.05f, -0.1f), Quaternion.identity) as GameObject;
        zone4SliderRight.name = "zone-right-" + 3;
        zone4SliderRight.transform.parent = transform;
        
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




