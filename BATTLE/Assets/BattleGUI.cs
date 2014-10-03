using UnityEngine;
using System.Collections;

public class BattleGUI : MonoBehaviour {
	
	public bool attackLock = false;
	private string playerTurnString = "Please select a move.";
	private string selectTargetString = "Choose a target.";
	
	void OnGUI() {
		/*Announcer
		 * This section will display what attack the enemy used or playerTurnString.
		 */
			GUI.Box (new Rect (25, 25, Screen.width - 50, 50), "");
			if (!attackLock) {
				GUI.Label (new Rect (25, 25, Screen.width - 50, 50), playerTurnString);
			} else if (TurnStateMachine.isCommandTargeting()) {
				GUI.Label (new Rect (25, 25, Screen.width - 50, 50), selectTargetString);
			}

		//HP/MP display
			GUI.Box (new Rect (50, Screen.height - 200, 300, 150), ""); 
			GUI.Label (new Rect(75, Screen.height - 180, 300, 25), "HP :" + TurnStateMachine.playerHP);
			GUI.Label (new Rect (75, Screen.height - 165, 300, 25), "MP :" + TurnStateMachine.playerMP);
	
		//Handles whether or not the buttons are locked (due to currently choosing a target)
			GUI.Box (new Rect (375, Screen.height - 200, 300, 150), "");
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
				if (GUI.Button (new Rect (380, Screen.height - 180, 295, 25), "Tackle")) {
					Debug.Log ("tackle!");
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_ATTACK;
				}
				if (GUI.Button (new Rect (380, Screen.height - 150, 295, 25), "Abilities")) {
					Debug.Log ("abilities?");
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_ABILITY;
				}
			} else {	//Otherwise show the ability menu
				if (TurnStateMachine.playerMP < 5) {
					GUI.enabled = false;
				}
				if (GUI.Button (new Rect (380, Screen.height - 180, 295, 25), "DIEDIEDIE")) {
					Debug.Log ("DIEEEEEE");
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_DIE;
				}
				GUI.enabled = !attackLock;
				if (GUI.Button (new Rect (380, Screen.height - 90, 295, 25), "Back")) {
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_NONE;
				}
			}

	}
}