using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ZoneControllerSingleAny : MonoBehaviour
{

    void Awake()
    {

        //  source = GetComponent<AudioSource>();
    }


    int _amount = 3;

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
    /*public GameObject waterGloveLeft;
    public GameObject waterGloveRight;
    public GameObject droneGloveLeft;
    public GameObject droneGloveRight;*/

   // private Color zone1Color;
    static Color ocher = new Color(255/255f, 178/255f, 69/255f, 0.2f);
    static Color fuscia = new Color(255 / 255f, 79 / 255f, 218 / 255f, 0.2f);
    static Color turquoise = new Color(30 / 255f, 219 / 255f, 232 / 255f, 0.2f);
    //Color[] zoneColors = { Color.cyan, Color.grey, Color.magenta };
    Color[] zoneColors = { turquoise, ocher, fuscia };

    // x range is -0.3 - 0.3
    float[] xPosLeftRange = { -0.5f, -0.13f, 0.03f, 0.50f };

    // x range is -0.3 - 0.3, 4 zones here
    float[] xPosRightRange = { -0.5f, -0.15f, -0.05f, 0.10f, 0.50f };

    // y range is 0.2 - 0.85
    float[] yPosRange = { 0.2f, 0.4f, 0.6f, 0.95f };


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
    Vector3 gloveScaleFactor = new Vector3(1.2f, 1, 1.2f);
    public int whichSide = 1; // -1 is right, 1 is left

    void Start()
    {
        zoneLeft = new GameObject[_amount];

        InstantiateZoneObjects();

        //zoneLeft = new List<GameObject>();

        /*
        zoneRight = new GameObject[_amount];
        InstantiateSliderObjects();
        
        */
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
       // Debug.Log("getting touchOSC");
        // whether the realsense is on left or right
        // left == -1, right == 1

        
        for (int i = 0; i < (_amount); i++)
        {
            // GameObject zone = Instantiate(zoneObject, transform.position + new Vector3(displacementX * .45f + i * displacementX + 0.05f, 0, i * displacementZ), Quaternion.identity) as GameObject;
            // GameObject zone = Instantiate(zoneObject, transform.position + new Vector3((-whichSide) * displacementX + (whichSide) * i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            GameObject zone = null;
            if (1 == whichSide) { // LEFT
                zone = Instantiate(zoneObject, transform.position + new Vector3(displacementX + (-i) * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            } else {  // RIGHT
                zone = Instantiate(zoneObject, transform.position + new Vector3(-displacementX + (i) * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            }

        zone.name = "zone-left-" + i;
            zone.transform.parent = transform;
            zoneLeft[i] = zone;
            zoneLeft[i].transform.localScale = new Vector3(1.8f, 1, 1);

        }

    }


    // passing left and right zone information to main zone height controller
    private void ZoneHeightLeft(int zone, float yPos)
    {
        //Debug.Log("zone-LEFT:" + zone + " scale value:" + yPos);
        for (int i = 0; i < _amount; i++)
        {
            ZoneHeight(zoneLeft[i], i, 0);
        }
        ZoneHeight(zoneLeft[zone], zone, yPos);



    }
    /*
    private void ZoneHeightRight(int zone, float yPos)
    {
        // Debug.Log("zone-RIGHT:" + zone + " scale value:" + yPos);

        ZoneHeight(zoneRight[zone], zone, yPos);

    }
    */

    internal void ZoneHeight(GameObject ZoneObject, int zone, float yPos)
    {

        Transform childTransform = ZoneObject.transform.GetChild(0);
        childTransform.localScale = new Vector3(1, 8 * Mathf.Pow(yPos, 2), 1); // Mathf.Pow(yPos, 2) for square (pow 2)
       // Debug.Log("zone height" + childTransform.localScale);
    }

    // passing information from left slider and right slider
    private void SliderLeft(float zPos)
    {
       // SliderMotion(zone4SliderLeft, zPos);
    }

    private void SliderRight(float zPos)
    {

       // SliderMotion(zone4SliderRight, zPos);

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


    internal void UpdateLeftZone(float xPos, float yPos, float zPos, bool isRSLeft)
    {
        //   Transform waterGloveScale = waterGloveLeft.transform.GetChild(0);
        //waterGloveScale.localScale = new Vector3(1, 1, 1);
        //     Transform droneGloveScale = droneGloveLeft.transform.GetChild(0);
        // droneGloveScale.localScale = new Vector3(1, 1, 1);
        int selectedYZone = GetYZone(yPos);

        int selectedZone = GetZone(xPos, xPosLeftRange);

        if (selectedZone == -1)
        {
            Debug.Log(xPos + "," + yPos + "," + zPos + " : yielded no X zone");
            return;
        }

        // left zones filled

        if (isRSLeft == true)
        {
            selectedZone = 2 - selectedZone;
            leftZoneColor(selectedZone);
        } else
        {
            leftZoneColor(selectedZone);
        }


        // Debug.Log("ypos: " + yPos);

        if (selectedZone < 3)
        {
            ZoneHeightLeft(selectedZone, yPos);

        }

        return;
    }

    internal void UpdateRightZone(float xPos, float yPos, float zPos)
    {
		UpdateLeftZone (xPos, yPos, zPos, false);

		/*
		 * 
        int selectedXZone = GetZone(xPos, xPosRightRange);

        if (selectedXZone == -1)
        {
            Debug.Log(xPos + "," + yPos + "," + zPos + " : yielded no X zone");
            return;
        }

        rightZoneColor(selectedXZone);



       // Transform waterGloveScale = waterGloveRight.transform.GetChild(0);
        //waterGloveScale.localScale = new Vector3(1, 1, 1);
      //  Transform droneGloveScale = droneGloveRight.transform.GetChild(0);
        //droneGloveScale.localScale = new Vector3(1, 1, 1);
        int selectedYZone = GetYZone(yPos);
        //  Debug.Log("which y zone" + selectedYZone + "last y zone state " +lastRightZoneYState);

        if (selectedXZone < 3)
        {
            ZoneHeightRight(selectedXZone, yPos);

        }
       */

        return;
    }
    /*
    void InstantiateSliderObjects()
    {
        // instantiate slider objects
        zone4SliderLeft = Instantiate(zone4SliderLeft, transform.position + new Vector3(-(displacementX * .45f + 3 * displacementX + 0.05f), 0.05f, -0.1f), Quaternion.identity) as GameObject;
        zone4SliderLeft.name = "zone-left-" + 3;
        zone4SliderLeft.transform.parent = transform;
        zoneLeft[3] = zone4SliderLeft;
        //zoneLeft.Add((GameObject)zone4SliderLeft);
        zone4SliderRight = Instantiate(zone4SliderLeft, transform.position + new Vector3((displacementX * .45f + 3 * displacementX) + 0.05f, 0.05f, -0.1f), Quaternion.identity) as GameObject;
        zone4SliderRight.name = "zone-right-" + 3;
        zone4SliderRight.transform.parent = transform;
        zoneRight[3] = zone4SliderRight;

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
    }
    */

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




