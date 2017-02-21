using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[System.Serializable]
public class Player{
	public Image panel;
	public Text text;
	public Button button;
}

[System.Serializable]
public class PlayerColor{
	public Color panelColor;
	public Color textColor;
}

public class GameControllerTTT : MonoBehaviour {

	public Text[] buttonList;
	private string playerSide;
	public GameObject gameOverPanel;
	public Text gameOverText;
	private int moveCount;
	public GameObject restartButton;
	public Player playerX;
	public Player playerO;
	public PlayerColor activePlayerColor;
	public PlayerColor inactivePlayerColor;
	public GameObject startInfo;

	void Awake() {
		SetGameControllerReferenceOnButtons ();
		gameOverPanel.SetActive (false);
		moveCount = 0;
		restartButton.SetActive (false);

	}

	//Check each item in the Button List and 
	//set the GameController reference in the GridSpace 
	//component on the parent GameObject
	void SetGameControllerReferenceOnButtons() { 
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList [i].GetComponentInParent<GridSpace> ().SetGameControllerReference (this);
		}
	}

	public void SetStartingSide(string startingSide){ // gets the string from button selected
		Debug.Log (startingSide);
		playerSide = startingSide;
		if (playerSide == "X") {
			//playerSide = "X";

			SetPlayerColors (playerX, playerO);
			//SetPlayerColors (playerO, playerX);
		} else {
			//playerSide = "O";

			SetPlayerColors (playerO, playerX);
		}
		StartGame();
	}

	void StartGame() {
		SetBoardInteractable (true);
		SetPlayerButtons (false);
		startInfo.SetActive (false);
	}
	public string GetPlayerSide (){
		return playerSide;
	}

	void SetPlayerButtons(bool toggle){
		playerX.button.interactable = toggle;
		playerO.button.interactable = toggle;  
	}



	public void EndTurn() {
		moveCount++;
		if (buttonList [0].text == playerSide && buttonList [1].text == playerSide && buttonList [2].text == playerSide) {
			GameOver (playerSide);
		}

		else if (buttonList [3].text == playerSide && buttonList [4].text == playerSide && buttonList [5].text == playerSide) {
			GameOver (playerSide);
		}

		else if (buttonList [6].text == playerSide && buttonList [7].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);
		}

		else if (buttonList [0].text == playerSide && buttonList [3].text == playerSide && buttonList [6].text == playerSide) {
			GameOver (playerSide);
		}

		else if (buttonList [1].text == playerSide && buttonList [4].text == playerSide && buttonList [7].text == playerSide) {
			GameOver (playerSide);
		}

		else if (buttonList [2].text == playerSide && buttonList [5].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);
		}

		else if (buttonList [0].text == playerSide && buttonList [4].text == playerSide && buttonList [8].text == playerSide) {
			GameOver (playerSide);
		}

		else if (buttonList [2].text == playerSide && buttonList [7].text == playerSide && buttonList [6].text == playerSide) {
			GameOver (playerSide);
		}
		else if (moveCount >= 9) {
			GameOver ("draw");
		} else {
			ChangeSides();
		}

	}

	void ChangeSides(){
		playerSide = (playerSide == "X") ? "O" : "X";
		if (playerSide == "X") {

			SetPlayerColors (playerX, playerO); //active player, inactive player

		} else {
			SetPlayerColors (playerO, playerX);
		}

	}



	void GameOver(string winningPlayer) {
		SetBoardInteractable (false);
		restartButton.SetActive (true);
		if (winningPlayer == "draw") {
			SetGameOverText ("It's a draw!");
			SetPlayerColorsInactive ();
		} else {
			SetGameOverText (winningPlayer + " Wins!");
		}
	}

	void SetGameOverText(string value)  {
		gameOverPanel.SetActive (true);
		gameOverText.text = value;
	}

	public void RestartGame() {
		playerSide = "X";
		moveCount = 0;
		gameOverPanel.SetActive (false);

		for (int i = 0; i < buttonList.Length; i++) {
			buttonList[i].text = "";
		}
		restartButton.SetActive (false);
		SetPlayerColors (playerX, playerO);
		SetPlayerButtons (true);
		SetPlayerColorsInactive ();
		startInfo.SetActive (true);
	}

	void SetBoardInteractable(bool toggle){
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList [i].GetComponentInParent<Button> ().interactable = toggle;
		}
	}

	void SetPlayerColors(Player newPlayer, Player oldPlayer) {
		newPlayer.panel.color = activePlayerColor.panelColor;
		newPlayer.text.color = activePlayerColor.textColor;
		oldPlayer.panel.color = inactivePlayerColor.panelColor;
		oldPlayer.text.color = inactivePlayerColor.textColor;
	}

	void SetPlayerColorsInactive ()
	{
		playerX.panel.color = inactivePlayerColor.panelColor;
		playerX.text.color = inactivePlayerColor.textColor;
		playerO.panel.color = inactivePlayerColor.panelColor;
		playerO.text.color = inactivePlayerColor.textColor;
	}
}