using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Valve.VR;


public class RealsensePlacement : MonoBehaviour {


    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }
    private SteamVR_TrackedObject trackedObj;

    public bool dUp = false;
    public bool dDown = false;
    public bool dLeft = false;
    public bool dRight = false;

    void Start()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    void Update()
    {
        if (controller == null)
        {
            Debug.Log("Controller not initialized");
            return;
        }

        if (controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log(trackedObj.index + " Trigger Movement: " +
                controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).x);
        }

        if (controller.GetPress(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(trackedObj.index + " Trigger Grip: " );
        }

        if (controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y > 0.5f)
            {
                dUp = true;
                Debug.Log(trackedObj.index + " Movement Dpad Up");
            }

            if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).y < -0.5f)
            {
                dDown = true;
                Debug.Log(trackedObj.index + " Movement Dpad Down");
            }

            if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x > 0.5f)
            {
                dRight = true;
                Debug.Log(trackedObj.index + " Movement Dpad Right");
            }

            if (controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad).x < -0.5f)
            {
                dLeft = true;
                Debug.Log(trackedObj.index + " Movement Dpad Left");
            }
        }
    }
}

