using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerControl : MonoBehaviour {

    public GameObject[] fingers;
    public Text[] finText;
    public Image[] finDial;
   
  

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
    
    
}
