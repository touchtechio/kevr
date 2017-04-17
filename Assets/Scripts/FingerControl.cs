using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FingerControl : MonoBehaviour {

    public GameObject[] fingers;
    public Text[] finLeft;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    internal void bendFinger(int i, float fingerBendData)
    {

        fingers[i].transform.localRotation = Quaternion.Euler(0, 0, fingerBendData*-150);
        fingerBendData = fingerBendData * 100;
        finLeft[i].text = ((int)fingerBendData).ToString();

    }
}
