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

    GameObject[] stageZones;
    // List<GameObject> zoneLeft;
    GameObject[,] stageZonesMatrix;

    private float displacementX = 0;

    [SerializeField]
    //[Range(-0.3f,1f)]
    float displacementY = 0.1f;

    [SerializeField]
    float displacementZ = 0;


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

    // x range is -0.3 - 0.3
    float[] xPosLeftRange = { -0.5f, -0.13f, 0.03f, 0.50f };

    // x range is -0.3 - 0.3, 4 zones here
    float[] xPosRightRange = { -0.5f, -0.15f, -0.05f, 0.10f, 0.50f };

    // y range is 0.2 - 0.85
    float[] yPosRange = { 0.2f, 0.4f, 0.6f, 0.95f };


    public int whichSide = 1; // -1 is right, 1 is left

    float distanceX;
    float distanceZ;



    void Start()
    {
        stageZones = new GameObject[stageZonesRows];
        stageZonesMatrix = new GameObject[stageZonesRows, stageZonesColumns];

        //distanceX = FrontRight.position[0] - FrontLeft.position[0];
        distanceX = FrontRight.position[0] - FrontLeft.position[0];
        distanceZ = FrontLeft.position[2] - BackLeft.position[2];
        Debug.Log("displacement Y of stage " + distanceZ);

        // set size of each column and row
        displacementX = distanceX / stageZonesRows;
        displacementZ = distanceZ / stageZonesColumns;
        zoneColorMatrix = new Color[4, 4] { { turquoise, ocher, fuscia, blueness }, {  ocher, fuscia, blueness, turquoise }, {Color.white, Color.white, Color.white, Color.white }, { turquoise, ocher, fuscia, blueness } };
    
        //new Color(255f / i, 255f / i * 0.8f, 255f / i * 0.6f, 0.2f)
        
        InstantiateZoneObjects();

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
        for (int i = 0; i < stageZonesRows; i++)
        {
            for (int j = 0; j < stageZonesColumns; j++)
            {

                stageZonesMatrix[i,j] = Instantiate(stageZoneObject, FrontLeft.position + new Vector3(displacementX / 2 + (i) * displacementX, 0, -displacementZ / 2 - j * displacementZ), Quaternion.identity) as GameObject;
                stageZonesMatrix[i,j].transform.localScale = new Vector3(-displacementX, 1, displacementZ);
                stageZonesMatrix[i,j].GetComponent<Renderer>().material.color = zoneColorMatrix[i,j];
                //stageZonesMatrix[i, j].GetComponent<Renderer>().material.color;
            }

        }
    }

    // passing left and right zone information to main zone height controller
    private void ZoneHeightBlock(int zone, float yPos)
    {
        Debug.Log("zone-LEFT:" + zone + " scale value:" + yPos);
        for (int i = 0; i < stageZonesRows; i++)
        {
            // resets each zone to zero
            ZoneHeight(stageZones[i], i, 0);

        }
        // only change selected zone's height
        ZoneHeight(stageZones[zone], zone, yPos);

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
        for (int i = 0; i < stageZonesRows; i++)
        //foreach (GameObject zones in zoneLeft)
        {

            stageZones[i].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
            //Debug.Log(rightZones.Length);
        }


        stageZones[zoneNumber].GetComponentInChildren<MeshRenderer>().material.color = zoneColors[zoneNumber];
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

   
            // sets zone color and HUD zone height fill for right hand
            ZoneColor(selectedZone, false);
            HUDyPosRight.fillAmount = yPos;
        


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
        stageZones[3] = zone4SliderLeft;
        //zoneLeft.Add((GameObject)zone4SliderLeft);
        zone4SliderRight = Instantiate(zone4SliderLeft, transform.position + new Vector3((displacementX * .45f + 3 * displacementX) + 0.05f, 0.05f, -0.1f), Quaternion.identity) as GameObject;
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




