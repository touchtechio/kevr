using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour {

	public Button button;
	public Text buttonText;
	//public string playerSide;

	private GameControllerTTT gameController;

	// Use this for initialization
	public void SetGameControllerReference (GameControllerTTT controller) {
		gameController = controller;
	}

	public void SetSpace () {
		//buttonText.text = playerSide;
		buttonText.text = gameController.GetPlayerSide();
		button.interactable = false;
		gameController.EndTurn ();
	}
}
