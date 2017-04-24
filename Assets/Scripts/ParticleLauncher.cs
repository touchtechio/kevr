﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleLauncher : MonoBehaviour
{

    public ParticleSystem[] particleLauncher;
    
   

    // Update is called once per frame
    void Update()
    {
        /*

        if (Input.GetButton("Fire1"))

        {

            ParticleSystem.MainModule psMain = particleLauncher.main; // calling variables from the main module
                                                                       // psMain.startColor = particleColorGradient.Evaluate(UnityEngine.Random.Range(0f, 1f));
            particleLauncher.Emit(1);
        }
        */

    }

    public void launchParticle(int i)
    {

        Debug.Log("launching particle system: " + i);
        ParticleSystem.MainModule psMain = particleLauncher[i].main; // calling variables from the main module
                                                                     //psMain.startColor = particleColorGradient.Evaluate(UnityEngine.Random.Range(0f, 1f));
        particleLauncher[i].Emit(3);
      

    }
}
