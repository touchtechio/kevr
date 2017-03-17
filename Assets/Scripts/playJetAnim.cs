using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playJetAnim : MonoBehaviour {

    public GameObject mainProjectile;
    public ParticleSystem mainParticleSystem;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            mainProjectile.SetActive(true);
        }
        if (mainParticleSystem.IsAlive() == false)
            mainProjectile.SetActive((false)); 
	}
}
