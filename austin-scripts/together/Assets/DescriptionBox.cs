using UnityEngine;
using System.Collections;

public class DescriptionBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<MeshRenderer>().enabled = false;
	}

	void OnMouseOver() {
		GetComponent<MeshRenderer>().enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		GUI.Box (new Rect (50+2*(Screen.width - (Screen.width/8)- 100)/3 + Screen.width/8, Screen.height - 200, (Screen.width - (Screen.width/8)- 100)/3, 150), "");
	}
}
