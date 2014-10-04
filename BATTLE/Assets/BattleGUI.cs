using UnityEngine;
using System.Collections;

public class BattleGUI : MonoBehaviour {
	
	public bool attackLock = false;
	public string playerTurnString = "Please select a move.";
	public string selectTargetString = "Choose a target.";
	public string dialogue;
	public string tackleDesc = "A basic physical tackle attack. Attacks one enemy.";
	public string abilityOneDesc = "DIEDIEDIE. Attacks one enemy.";
	public string abilityTwoDesc = "Thing go boom. Attacks every enemy.";

	void Start(){
		dialogue = playerTurnString;
	}
	
	void OnGUI() {
		float boxHeight = 150;
		float boxWidth = (Screen.width - (Screen.width/8) - 100)/3;
		float boxY = Screen.height - 200;
		float boxOneX = 50;
		float statWidth = boxWidth-40;
		float statHeight = 25;
		float statX = 70;
		float HPTextY = Screen.height - 180;
		float MPTextY = Screen.height - 165;
		float boxTwoX = 50+(Screen.width - (Screen.width/8)- 100)/3 + Screen.width/16;
		float boxThreeX = 50+2*(Screen.width - (Screen.width/8)- 100)/3 + Screen.width/8;
		float buttonWidth = (Screen.width - (Screen.width/8) - 100)/3 - 40;
		float buttonHeight = 25;
		float buttonX = 50+(Screen.width - (Screen.width/8) - 100)/3 + Screen.width/16 + 20;
		float buttonOneY = Screen.height - 180;
		float buttonTwoY = Screen.height - 150;
		float backButtonY = Screen.height - 90;
		float announcerX = 25;
		float announcerY = 25;
		float announcerWidth = Screen.width - 50;
		float announcerHeight = 50;
		/*Announcer
		 * This section will display what attack the enemy used or playerTurnString.
		 */
			
			/*GUI.Box (new Rect (25, 25, Screen.width - 50, 50), "");
			if (!attackLock) {
				GUI.Label (new Rect (25, 25, Screen.width - 50, 50), playerTurnString);
			} else if (TurnStateMachine.isCommandTargeting()) {
				GUI.Label (new Rect (25, 25, Screen.width - 50, 50), selectTargetString);
			}*/

			
			GUI.Box (new Rect (announcerX, announcerY, announcerWidth, announcerHeight), dialogue);
			if (!attackLock) {
				dialogue = playerTurnString;
			} else if (TurnStateMachine.isCommandTargeting()) {
			//print ("called lololol");
				dialogue = selectTargetString;
			}

		//HP/MP display
			GUI.Box (new Rect (boxOneX, boxY, boxWidth, boxHeight), ""); 
			GUI.Label (new Rect(statX, HPTextY, statWidth, statHeight), "HP :" + TurnStateMachine.playerHP);
			GUI.Label (new Rect (statX, MPTextY, statWidth, statHeight), "MP :" + TurnStateMachine.playerMP);
			
		//Attack descriptions
			GUI.Box (new Rect (boxThreeX, boxY, boxWidth, boxHeight), "");
	
		//Handles whether or not the buttons are locked (due to currently choosing a target)
			GUI.Box (new Rect (boxTwoX, boxY, boxWidth, boxHeight), "");
			if (TurnStateMachine.getTurn () == 0 && !TurnStateMachine.isCommandTargeting()) {
				attackLock = false;
			} 
			else {	
				attackLock = true;
			}
			GUI.enabled = !attackLock;
		//Handles the menu buttons
			if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_NONE
	    			|| TurnStateMachine.commandSelection == TurnStateMachine.SELECT_TARGET_ATTACK) {		//If we're not choosing an ability, show the usual buttons.
					if (GUI.Button (new Rect (buttonX, buttonOneY, buttonWidth, buttonHeight), "Tackle")) {
					Debug.Log ("tackle!");
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_ATTACK;
				}
					if (GUI.Button (new Rect (buttonX, buttonTwoY, buttonWidth, buttonHeight), "Abilities")) {
					Debug.Log ("abilities?");
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_ABILITY;
				}
			} else {	//Otherwise show the ability menu
				if (TurnStateMachine.playerMP < 5) {
					GUI.enabled = false;
				}
					if (GUI.Button (new Rect (buttonX, buttonOneY, buttonWidth, buttonHeight), "DIEDIEDIE")) {
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_DIE;
				}
				GUI.enabled = !attackLock;

				if (TurnStateMachine.playerMP < 10) {
					GUI.enabled = false;
				}
					if (GUI.Button (new Rect (buttonX, buttonTwoY, buttonWidth, buttonHeight), "SPLOSIONS")) {
					TurnStateMachine.castSPLOSIONS();
				}
				GUI.enabled = !attackLock;

					if (GUI.Button (new Rect (buttonX, backButtonY, buttonWidth, buttonHeight), "Back")) {
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_NONE;
				}
			}
	}
}