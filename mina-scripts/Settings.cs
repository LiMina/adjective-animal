using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {
	
	void OnGUI () {
		GUI.Box (new Rect (Screen.width - 60,10,50,50), "SETTINGS");
	}
}