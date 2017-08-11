﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerControl : MonoBehaviour {

    public GameObject[] fingers;
    public Text[] finText;
    public Image[] finDial;
    public Text[] finZoneText;
    public Image[] finZoneButtonArray;

    public GameObject thumb;
    public GameObject index;
    public GameObject middle;
    public GameObject ring;
    public GameObject pinkie;

    GameObject[] zoneArray;
    public int whichSide = 1; // 0 is right, 1 is left
    public bool isGuitar = false;
    public NoteToPlayText NotesToPlay;

    public GameObject guitarZoneObject;

    // Use this for initialization
    void Start () {
        if (isGuitar)
        {
         //   InstantiateGuitarZoneObjects();
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("z"))
        {
           //ebug.Log("pressed");
            FingerZoneTrigger(1);
        }
	}

    internal void bendFinger(int i, float fingerBendData)
    {

        fingers[i].transform.localRotation = Quaternion.Euler(0, 0, fingerBendData*-130);
        fingers[i].transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, fingerBendData * -90);
        fingers[i].transform.GetChild(0).GetChild(0).localRotation = Quaternion.Euler(0, 0, fingerBendData * -90);

        // update data readouts
        fingerBendData = fingerBendData * 100;
        finText[i].text = ((int)fingerBendData).ToString();
        finDial[i].fillAmount = fingerBendData / 100;
 
    }


    internal void bendFingerIMU(float[] fingerBendData)
    {
        thumb = fingers[0];
        index = fingers[1];
        middle = fingers[2];
        ring = fingers[3];
        pinkie = fingers[4];
        thumb.transform.localRotation = Quaternion.Euler(0, 0, -fingerBendData[1]);
        index.transform.localRotation = Quaternion.Euler(0, -14, -fingerBendData[3]);
        middle.transform.localRotation = Quaternion.Euler(0, 0, -fingerBendData[6]);
        ring.transform.localRotation = Quaternion.Euler(0, 0, -fingerBendData[9]);
        pinkie.transform.localRotation = Quaternion.Euler(0, 0, -fingerBendData[12]);
        thumb.transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, -fingerBendData[2]);
        index.transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, -fingerBendData[4]);
        index.transform.GetChild(0).GetChild(0).localRotation = Quaternion.Euler(0, -14, -fingerBendData[5]);
        middle.transform.GetChild(0).localRotation = Quaternion.Euler(0, -14, -fingerBendData[7]);
        middle.transform.GetChild(0).GetChild(0).localRotation = Quaternion.Euler(0, 0, -fingerBendData[8]);
        ring.transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, -fingerBendData[10]);
        ring.transform.GetChild(0).GetChild(0).localRotation = Quaternion.Euler(0, 0, -fingerBendData[11]);
        pinkie.transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, -fingerBendData[13]);
        pinkie.transform.GetChild(0).GetChild(0).localRotation = Quaternion.Euler(0, 0, -fingerBendData[14]);
        
    }

    public void InstantiateGuitarZoneObjects()
    {
        for (int i = 0; i < 5; i++)
        {
            // GameObject zone = Instantiate(zoneObject, transform.position + new Vector3(displacementX * .45f + i * displacementX + 0.05f, 0, i * displacementZ), Quaternion.identity) as GameObject;
            // GameObject zone = Instantiate(zoneObject, transform.position + new Vector3((-whichSide) * displacementX + (whichSide) * i * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            GameObject zone = null;
            if (1 == whichSide)
            { // LEFT
              //zone = Instantiate(guitarZoneObject, transform.position + new Vector3(displacementX + (-i) * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            }
            else
            {  // RIGHT
              //zone = Instantiate(guitarZoneObject, transform.position + new Vector3(-displacementX + (i) * displacementX, 0, i * displacementZ), Quaternion.identity) as GameObject;
            }

            zone.name = "guitar-zone-left-" + i;
            zone.transform.parent = transform;
            zoneArray[i] = zone;
            zoneArray[i].transform.localScale = new Vector3(1.8f, 1, 1);

        }
    }

    internal void FingerZoneTrigger(int fingerNumber)
    {
        for (int i = 0; i < finZoneButtonArray.Length; i++)
        {
            finZoneButtonArray[i].color = Color.white;
            //  finZoneButtonArray[i].material.SetColor("_EmissionColor", Color.white);
            //finZoneText[i].text = "off";
            // Debug.Log("left hand" +fingerNumber);
        }

        // finZoneText[fingerNumber].text = "on";
        finZoneButtonArray[fingerNumber].color = Color.grey;

        if(whichSide ==0)
        {
            Debug.Log("right finger controller");
            finZoneButtonArray[fingerNumber].color = Color.grey;
        } else
        {
            Debug.Log("left finger controller");
        }
        if (whichSide == 1)
        {
            if (fingerNumber > 0)
            {
                NotesToPlay.ChangePlayingNotes(fingerNumber);
            }
            else
            {
                return;
            }
            // finZoneButtonArray[fingerNumber].material.SetColor("_EmissionColor", Color.grey);
        }

    }

  
}
