﻿
using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public int health = 80;
	public int mana = 10;
	public int attack = 5;
	public GUIStyle styler;

	void OnGUI() {
		styler.normal.textColor = Color.red;
		styler.fontSize = 18;
		var p = Camera.main.WorldToScreenPoint(transform.position);
		if (health > 0) {
			GUI.Label (new Rect (p.x - 30, Screen.height - p.y - 70,300, 25), "hp:" + health, styler);
		} else {
			GUI.Label (new Rect (p.x - 30, Screen.height - p.y - 70,300, 25), "  K.O.", styler);
		}
	}
	
	// Use this for initialization
	void Start () {
		if (textmanage.scene == "school") {
			health = 120;
			attack = 10;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}