
using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public int health = 80;
	public int mana = 10;

	void OnGUI() {
		var p = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Label (new Rect (p.x - 30, Screen.height - p.y - 40,300, 25), "hp:" + health);
	}
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}