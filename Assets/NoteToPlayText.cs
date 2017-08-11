using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteToPlayText : MonoBehaviour {

    public int whichSide = 0; // left is 1, right is 0
    public Text[] finZoneText;
    private string[,] NoteArray;
    // Use this for initialization
    void Start () {
        NoteArray = new string[5, 5] {{ "C", "B", "C", "D", "E" } , { "E1", "G1", "A1", "A#1", "E2" }, { "G3", "G#3", "B3", "C4", "E4" }, { "A3", "C4", "D4", "E4", "F4" }, { "E4", "G4", "A4", "B4", "C5" }};

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void ChangePlayingNotes(int fingerNumber)
    {

        Debug.Log("change notes on right hand");
        for (int i = 0; i < finZoneText.Length; i++)
        //foreach (GameObject zones in zoneLeft)
        {
            //  finZoneButtonArray[i].material.SetColor("_EmissionColor", Color.white);
            finZoneText[i].text = NoteArray[fingerNumber, i].ToString();
          //  Debug.Log("right hand" + NoteArray[fingerNumber, i].ToString());
        }

    }

}
