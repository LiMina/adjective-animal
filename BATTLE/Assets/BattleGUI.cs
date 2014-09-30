using UnityEngine;
using System.Collections;

public class BattleGUI : MonoBehaviour {
	
	public bool attackLock = false;
	private string playerTurnString = "Please select a move.";
	
	void OnGUI() {
	/*Announcer
	 * This section will display what attack the enemy used or playerTurnString.
	 */
	
	if (!attackLock)
		GUI.Box (new Rect (25, 25, Screen.width - 50, 50), playerTurnString);

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
		GUI.Button(new Rect(380, Screen.height - 180, 295, 25), "Tackle");

	}
}