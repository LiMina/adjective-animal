using UnityEngine;
using System.Collections;

public class BattleGUI : MonoBehaviour {
	
	public bool attackLock = false;
	private string playerTurnString = "Please select a move.";
	
	void OnGUI() {
		/*Announcer
		 * This section will display what attack the enemy used or playerTurnString.
		 */
			GUI.Box (new Rect (25, 25, Screen.width - 50, 50), "");
			if(!attackLock)
				GUI.Label (new Rect(25, 25, Screen.width - 50, 50), playerTurnString);

		//HP/MP display
			GUI.Box (new Rect (50, Screen.height - 200, 300, 150), ""); 
	
		//Attack Menu
			GUI.Box (new Rect (375, Screen.height - 200, 300, 150), "");
			if (TurnStateMachine.whosTurn == 0) {
				attackLock = false;
			} 
			else {	
				attackLock = true;
			}
			GUI.enabled = !attackLock;
			if(GUI.Button(new Rect(380, Screen.height - 180, 295, 25), "Tackle")){
				Debug.Log ("tackle!");
				TurnStateMachine.commandSelection = 1;
			}

	}
}