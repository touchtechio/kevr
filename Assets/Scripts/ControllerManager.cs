using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerManager : MonoBehaviour {

    private VRControllerState_t controllerState;

    private SteamVR_TrackedController device;
    void Start()
    {
        device = GetComponent<SteamVR_TrackedController>();
        device.TriggerClicked += Trigger;
    }

    void Trigger(object sender, ClickedEventArgs e)
    {

        Debug.Log("Trigger has been pressed");

    }

    //// Update is called once per frame
    //void Update () {
    //    var system = OpenVR.System;
    //    if (system != null && system.GetControllerState(device.controllerIndex, ref controllerState))
    //    {
    //        ulong trigger = controllerState.ulButtonPressed & (1UL << ((int)EVRButtonId.k_EButton_SteamVR_Trigger));
    //        float value = controllerState.rAxis1.x;
    //        Debug.Log("f:"+ value);

    //    }


    //}
}
