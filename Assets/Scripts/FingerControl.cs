using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerControl : MonoBehaviour {

    public GameObject[] fingers;
    public Text[] finText;
    public Image[] finDial;
    public GameObject thumb;
    public GameObject index;
    public GameObject middle;
    public GameObject ring;
    public GameObject pinkie;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
        
     

        // update data readouts
       
       // finText[i].text = ((int)fingerBendData).ToString();
       // finDial[i].fillAmount = fingerBendData / 100;

    }


}
