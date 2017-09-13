﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDisplay : MonoBehaviour {

    bool extendedScreen = false;
	// Use this for initialization
	void Start () {

        Screen.fullScreen = false;

        if (extendedScreen == true)
        {
            Debug.Log("displays connected: " + Display.displays.Length);
            // Display.displays[0] is the primary, default display and is always ON.
            // Check if additional displays are available and activate each.
            if (Display.displays.Length > 1)
                Display.displays[1].Activate();
            if (Display.displays.Length > 2)
                Display.displays[2].Activate();
            if (Display.displays.Length > 3)
                Display.displays[3].Activate();
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
