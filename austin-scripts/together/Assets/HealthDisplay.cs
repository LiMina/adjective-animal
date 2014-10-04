using UnityEngine;
using System.Collections;

public class HealthDisplay : MonoBehaviour {
	
	TextMesh textMesh;
	public string target = "Enemy";
	
	// Use this for initialization
	void Start () {
		textMesh = GetComponent<TextMesh> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		textMesh.text = target + " HP: " + transform.parent.GetComponent<Stats>().health;
	}
}

