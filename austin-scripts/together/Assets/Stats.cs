
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
		float random = Random.value;
		if (transitions.currBattle == "wakeup") {
						health = 60;
						attack = 10;
				} else if (textmanage.scene == "school" && transitions.currBattle == "lecture") {
						health = 70;
						attack = 10;
				} else if (textmanage.scene == "school" && transitions.currBattle == "test") {
						health = 100;
						attack = 5;
				} else if (textmanage.scene == "school" && transitions.currBattle == "gym") {
						health = 50;
						attack = 5;
				} else if (textmanage.scene == "school2" && transitions.currBattle == "lecture") {
						if (random <= 0.33)
								health = 70;
						else if (random <= 0.66)
								health = 90;
						else
								health = 110;
						attack = 12;
				} else if (textmanage.scene == "school2" && transitions.currBattle == "gym") {
						if (random <= 0.33)
								health = 40;
						else if (random <= 0.66)
								health = 50;
						else
								health = 60;
						attack = 8;
				} else if (textmanage.scene == "school2" && transitions.currBattle == "test") {
						if (random <= 0.33)
								health = 100;
						else if (random <= 0.66)
								health = 120;
						else
								health = 140;
						attack = 10;
				} else if (textmanage.scene == "school2" && transitions.currBattle == "studying") {
						if (random <= 0.33)
								health = 50;
						else if (random <= 0.66)
								health = 60;
						else
								health = 70;
						attack = 9;
				} else if (textmanage.scene == "school2" && transitions.currBattle == "lunch") {
						if (random <= 0.33)
								health = 70;
						else if (random <= 0.66)
								health = 80;
						else
								health = 90;
						attack = 10;
				} else if (textmanage.scene == "afterschool" && transitions.currBattle == "club") {
						if (random <= 0.33)
								health = 30;
						else if (random <= 0.66)
								health = 40;
						else
								health = 50;
						attack = 12;
				} else if (textmanage.scene == "afterschool" && transitions.currBattle == "friends") {
						if (random <= 0.33)
								health = 170;
						else if (random <= 0.66)
								health = 190;
						else
								health = 210;
						attack = 8;
				} else if (textmanage.scene == "afterschool" && transitions.currBattle == "hw") {
						if (random <= 0.33)
								health = 110;
						else if (random <= 0.66)
								health = 130;
						else
								health = 150;
						attack = 8;
				} else if (textmanage.scene == "afterschool2" && transitions.currBattle == "club") {
						if (random <= 0.33)
								health = 60;
						else if (random <= 0.66)
								health = 70;
						else
								health = 80;
						attack = 12;
				} else if (textmanage.scene == "afterschool2" && transitions.currBattle == "friends") {
						if (random <= 0.33)
								health = 200;
						else if (random <= 0.66)
								health = 220;
						else
								health = 240;
						attack = 9;
				} else if (textmanage.scene == "afterschool2" && transitions.currBattle == "hw") {
						if (random <= 0.33)
								health = 160;
						else if (random <= 0.66)
								health = 180;
						else
								health = 200;
						attack = 10;
				} else if (textmanage.scene == "afterschool2" && transitions.currBattle == "party") {
						if (random <= 0.33)
								health = 80;
						else if (random <= 0.66)
								health = 100;
						else
								health = 120;
						attack = 6;
				} else { 	//Final Boss: Sleeping
						health = 220;
						attack = 14;
				}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}