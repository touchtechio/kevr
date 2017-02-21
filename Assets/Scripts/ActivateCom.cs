using UnityEngine;
using System.Collections;
//using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using System.Text;
using System.Threading;
using System.Linq;

public class ActivateCom : MonoBehaviour
{
    [AddComponentMenu("UniOSC/OSCConnection")]


    public UnityEngine.MonoBehaviour script;


    // Use this for initialization
    void Start()
    {

        script = GetComponent<UniOSC.UniOSCConnection>();


    }

    // Update is called once per frame
    void Update()
    {

        script.enabled = true;
    }
}
