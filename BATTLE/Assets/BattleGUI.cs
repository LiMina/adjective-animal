using UnityEngine;
using System.Collections;

public class BattleGUI : MonoBehaviour {

	// GUI yay
	void OnGUI() {
	//Announcer
		GUI.Box (new Rect (25, 25, Screen.width - 50, 50), "This is the announcer.");

	}
}