using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {

	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(80,Screen.height - 270,Screen.width - 160,250), "Dialogue");
		
		// Buttons
		if(GUI.Button(new Rect(100,Screen.height - 140,Screen.width - 200,20), "Level 1")) {
			//Application.LoadLevel(1);
		}
		if(GUI.Button(new Rect(100,Screen.height - 110,Screen.width - 200,20), "Level 2")) {
			//Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect(100,Screen.height - 80,Screen.width - 200,20), "Level 3")) {
			//Application.LoadLevel(2);
		}
		if(GUI.Button(new Rect(100,Screen.height - 50,Screen.width - 200,20), "Level 4")) {
			//Application.LoadLevel(2);
		}

		GUI.Box (new Rect (10,10,150,100), "STATS");
		GUI.Box (new Rect (Screen.width - 60,10,50,50), "SETTINGS");
	}
}
