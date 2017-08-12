using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumAudio : MonoBehaviour {

    AudioSource drumSource;
    public AudioClip taiko1;
    public AudioClip taiko2;
	// Use this for initialization
	void Start () {
        drumSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void playDrum1()
    {
        Debug.Log("play sound" + taiko1);
        drumSource.clip = taiko1;
        drumSource.Play();
    }
}
