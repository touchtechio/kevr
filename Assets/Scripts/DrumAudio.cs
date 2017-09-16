using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrumAudio : MonoBehaviour {

    AudioSource drumSource;
    public AudioClip taiko1;
    public AudioClip taiko2;
    public AudioClip taiko3;
    public AudioClip taiko4;
    public AudioClip taiko5;
    public AudioClip taiko6;
    public AudioClip taiko7;
    public AudioClip taiko8;
    public AudioClip taiko9;
    private AudioClip[] taikoSoundArray;
    AudioManager audioManager;

    // Use this for initialization
    void Start () {
        drumSource = GetComponent<AudioSource>();
        taikoSoundArray = new AudioClip[9] { taiko1, taiko2, taiko3, taiko4, taiko5, taiko6, taiko7, taiko8, taiko9 };
        audioManager = GameObject.FindObjectOfType<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {
      
    }

    public void playDrum(int drumNumber)
    {
        Debug.Log("play sound" + taiko1);
        drumSource.clip = taikoSoundArray[drumNumber];

        drumSource.volume = audioManager.volumeSlider.value;
        Debug.Log("stick volume" + audioManager.volumeSlider.value);
        drumSource.Play();
        ZoneControllerStageRadial ZoneController = FindObjectOfType<ZoneControllerStageRadial>();
        ZoneController.drumName.text = drumSource.clip.name.ToString();
    }
}
