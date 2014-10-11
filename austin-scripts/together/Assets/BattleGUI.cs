using UnityEngine;
using System.Collections;

public class BattleGUI : MonoBehaviour {
	
	public bool attackLock = false;
	public string playerTurnString = "Please select a move.";
	public string selectTargetString = "Choose a target.";
	public string dialogue;

	void Start(){
		dialogue = playerTurnString;
	}
	
	void OnGUI() {
		/*Announcer
		 * This section will display what attack the enemy used or playerTurnString.
		 */
			
			/*GUI.Box (new Rect (25, 25, Screen.width - 50, 50), "");
			if (!attackLock) {
				GUI.Label (new Rect (25, 25, Screen.width - 50, 50), playerTurnString);
			} else if (TurnStateMachine.isCommandTargeting()) {
				GUI.Label (new Rect (25, 25, Screen.width - 50, 50), selectTargetString);
			}*/

			
			GUI.Box (new Rect (25, 25, Screen.width - 50, 50), dialogue);
			if (!attackLock) {
				dialogue = playerTurnString;
			} else if (TurnStateMachine.isCommandTargeting()) {
			//print ("called lololol");
				dialogue = selectTargetString;
			}

		//HP/MP display
			GUI.Box (new Rect (50, Screen.height - 200, (Screen.width - (Screen.width/8) - 100)/3, 150), ""); 
			GUI.Label (new Rect(70, Screen.height - 180, (Screen.width - (Screen.width/8) - 100)/3-40, 25),
		           "Well-being :" + TurnStateMachine.playerHP + "/" + transitions.wellbeing * 100);
			GUI.Label (new Rect (70, Screen.height - 165, (Screen.width - (Screen.width/8) - 100)/3-40, 25),
		           "Grades :" + TurnStateMachine.playerMP + "/" + transitions.grades * 100);
	
		//Handles whether or not the buttons are locked (due to currently choosing a target)
			GUI.Box (new Rect (50+(Screen.width - (Screen.width/8)- 100)/3 + Screen.width/16, Screen.height - 200, (Screen.width - (Screen.width/8)- 100)/3, 150), "");
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
					if (GUI.Button (new Rect (50+(Screen.width - (Screen.width/8) - 100)/3 + Screen.width/16 + 20, Screen.height - 180, (Screen.width - (Screen.width/8) - 100)/3 - 40, 25),
			                "Tackle")) {
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_ATTACK;
				}
					if (GUI.Button (new Rect (50+(Screen.width - (Screen.width/8)- 100)/3 + Screen.width/16 + 20, Screen.height - 150, (Screen.width - (Screen.width/8) - 100)/3 - 40, 25),
			                "Abilities")) {
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_ABILITY;
				}
			} else {	//Otherwise show the ability menu
				if (TurnStateMachine.playerMP < TurnStateMachine.DIE_MANA_COST) {
					GUI.enabled = false;
				}
					if (GUI.Button (new Rect (50+(Screen.width - (Screen.width/8)- 100)/3 + Screen.width/16 + 20, Screen.height - 180, (Screen.width - (Screen.width/8)- 100)/3 - 40, 25),
			                "Sip Coffee")) {
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_DIE;
				}
				GUI.enabled = !attackLock;

				if (TurnStateMachine.playerMP < TurnStateMachine.SPLOSIONS_MANA_COST) {
					GUI.enabled = false;
				}
					if (GUI.Button (new Rect (50+(Screen.width - (Screen.width/8)- 100)/3 + Screen.width/16 + 20, Screen.height - 150, (Screen.width - (Screen.width/8)- 100)/3 - 40, 25),
			                "Cold Shower")) {
					TurnStateMachine.castSPLOSIONS();
				}
				GUI.enabled = !attackLock;

					if (GUI.Button (new Rect (50+(Screen.width - (Screen.width/8)- 100)/3 + Screen.width/16 + 20, Screen.height - 90, (Screen.width - (Screen.width/8)- 100)/3 - 40, 25),
			                "Back")) {
					TurnStateMachine.commandSelection = TurnStateMachine.SELECT_NONE;
				}
			}
			//GUI.Box (new Rect (50+2*(Screen.width - (Screen.width/8)- 100)/3 + Screen.width/8, Screen.height - 200, (Screen.width - (Screen.width/8)- 100)/3, 150), "");
	}
}