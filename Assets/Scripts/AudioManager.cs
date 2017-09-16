using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {

    public Slider volumeSlider;
    // Use this for initialization
    void Start () {
        volumeSlider = GameObject.FindGameObjectWithTag("Slider").GetComponent<Slider>();
        //  drumName = (Text)FindObjectOfType<Text>();
        volumeSlider.value = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            volumeSlider.value += 0.25f;
            Debug.Log("volume value " + volumeSlider.value);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && volumeSlider.value > volumeSlider.minValue)
        {
            volumeSlider.value -= 0.25f;
        }


    }
}
