using UnityEngine;
using System.Collections;

public class movieStart : MonoBehaviour {
    public GameObject moviePlane;
    
	// Use this for initialization
	void Start () {
		Renderer r = GetComponent<Renderer> ();
		MovieTexture fountain = (MovieTexture)r.material.mainTexture;
       // MovieTexture fountain = (MovieTexture)GetComponent<Renderer>().material.mainTexture;
		fountain.Play ();
        moviePlane.GetComponent<AudioSource>().Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
