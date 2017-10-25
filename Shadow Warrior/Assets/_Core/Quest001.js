import UnityEngine.UI;

var TextBoxOnCheck : int = 0;
var MessageBox : GameObject;
var TextBox : GameObject;
var TextMessage : String;
var QuestBox : GameObject;
var QuestText : GameObject;
var QuestName : String;

function OnMouseDown () {
		if (TextBoxOnCheck == 0)  {
			TextBoxOnCheck = 1;
			MessageBox.SetActive(true);
			TextBox.GetComponent.<Text>().text = TextMessage;
			QuestName = "Active Quest: 'Stop the Invasion'";
			QuestText.GetComponent.<Text>().text = QuestName;
		}

		else {
			TextBoxOnCheck = 0;
			MessageBox.SetActive(false);
			TextMessage = "Villager: Get going then!";
			}
}

function Update () {
	if (Input.GetButtonDown("Submit")) {
		if (TextBoxOnCheck == 1) {
			MessageBox.SetActive(false);
			TextBoxOnCheck = 0;
			TextMessage = "Villager: Get going then!";
	}

	}

		if (Input.GetButtonDown("Cancel")) {
			if (TextBoxOnCheck == 1) {
				MessageBox.SetActive(false);
				TextBoxOnCheck = 0;
				TextMessage = "Villager: Get going then!";
	}

	}

}
