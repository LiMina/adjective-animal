
using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {
	
	public int health = 80;
	public int mana = 10;
	public int maxHealth = 80;
	public int attack = 5;
	public GUIStyle styler;
	public Vector2 pos = new Vector2(50,17);
	public Vector2 size = new Vector2(100,25);
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	public GUIStyle textcolor;
	public GUIStyle styler2;
	public Texture2D texture;
	public GUIStyle statsStyle;
	public Texture2D statsTexture;
	public GUIStyle barStyle;
	public Texture2D barTexture;

	void OnGUI() {
		styler.normal.textColor = Color.red;
		styler.fontSize = 18;
		var p = Camera.main.WorldToScreenPoint(transform.position);
		if (health > 0) {
			GUI.Label (new Rect (p.x - 30, Screen.height - p.y - 140,300, 25), "hp:" + health, styler);
		} else {
			GUI.Label (new Rect (p.x - 30, Screen.height - p.y - 140,300, 25), "  K.O.", styler);
		}
		// ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
		/*textcolor = new GUIStyle (GUI.skin.label);
		textcolor.normal.textColor = Color.black;
		/*For the stats bg*/
		/*texture = new Texture2D (128, 128);
		for (int y = 0; y < texture.height; ++y) {
			for (int x = 0; x < texture.width; ++x) {
				if ((x > 2 && y > 2) && (x < texture.width-3 && y < texture.height-3)) {
					Color color = new Color(167f/255f, 174f/255f, 216f/255f, 1f);
					texture.SetPixel (x, y, color);
				} else {
					Color color = new Color(190f/255f, 193f/255f, 216f/255f, 1f);
					texture.SetPixel (x, y, color);
				}
			}
		}
		texture.Apply();*/
		/*styler2 = new GUIStyle (GUI.skin.box);
		styler2.normal.background = texture;
		
		/*For the stats health bars*/
		/*statsTexture = new Texture2D ((int)size.x, (int)size.y);
		for (int y = 0; y < statsTexture.height; ++y) {
			for (int x = 0; x < statsTexture.width; ++x) {
				if ((x > 0 && y > 0) && (x < statsTexture.width-1 && y < statsTexture.height-1)) {
					Color color = new Color(136f/255f, 154f/255f, 204f/255f, 0.5f);
					statsTexture.SetPixel (x, y, color);
				} else {
					Color color = new Color(190f/255f, 193f/255f, 216f/255f, 0.5f);
					statsTexture.SetPixel (x, y, color);
				}
			}
		}
		statsTexture.Apply();
		statsStyle = new GUIStyle (GUI.skin.box);
		statsStyle.normal.textColor = Color.black;
		statsStyle.fontSize = 14;
		statsStyle.normal.background = statsTexture;
		/* bars themselves */
		/*barTexture = new Texture2D ((int)size.x, (int)size.y);
		for (int y = 0; y < barTexture.height; ++y) {
			for (int x = 0; x < barTexture.width; ++x) {
				if ((x > 0 && y > 0) && (x < barTexture.width-1 && y < barTexture.height-1)) {
					Color color = new Color(89f/255f, 113f/255f, 181f/255f, 0.5f);
					barTexture.SetPixel (x, y, color);
				} else {
					Color color = new Color(190f/255f, 193f/255f, 216f/255f, 0.5f);
					barTexture.SetPixel (x, y, color);
				}
			}
		}
		barTexture.Apply();
		barStyle = new GUIStyle (GUI.skin.box);
		barStyle.normal.textColor = Color.black;
		barStyle.normal.background = barTexture;
		
		GUI.Box (new Rect (10, 10, 180, 100),"", styler2);
		/*
		 * Happiness
		 */
		// draw the background:
		/*GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y), "", textcolor);
		GUI.Box (new Rect (0,0, size.x, size.y), "Happiness", statsStyle);*/
		
		// draw the filled-in part:
		//GUI.BeginGroup (new Rect (pos.x, pos.y, size.x * health, size.y)/*, "  Happiness",textcolor*/);
		//GUI.Box (new Rect (pos.x,pos.y, size.x, size.y),progressBarFull, barStyle);
		//GUI.EndGroup ();
		
		//GUI.EndGroup ();


	}
	
	// Use this for initialization
	void Start () {
		float random = Random.value;
		if (transitions.currBattle == "wakeup") {
						health = 60;
						attack = 10;
				} else if (textmanage.scene == "school" && transitions.currBattle == "lecture") {
						health = 60;
						attack = 8;
				} else if (textmanage.scene == "school" && transitions.currBattle == "test") {
						health = 70;
						attack = 5;
				} else if (textmanage.scene == "school" && transitions.currBattle == "gym") {
						health = 30;
						attack = 5;
				} else if (textmanage.scene == "school2" && transitions.currBattle == "lecture") {
						if (random <= 0.33)
								health = 60;
						else if (random <= 0.66)
								health = 70;
						else
								health = 80;
						attack = 8;
				} else if (textmanage.scene == "school2" && transitions.currBattle == "gym") {
						if (random <= 0.33)
								health = 30;
						else if (random <= 0.66)
								health = 35;
						else
								health = 40;
						attack = 5;
				} else if (textmanage.scene == "school2" && transitions.currBattle == "test") {
						if (random <= 0.33)
								health = 60;
						else if (random <= 0.66)
								health = 70;
						else
								health = 80;
						attack = 7;
				} else if (textmanage.scene == "school2" && transitions.currBattle == "studying") {
						if (random <= 0.33)
								health = 30;
						else if (random <= 0.66)
								health = 35;
						else
								health = 40;
						attack = 7;
				} else if (textmanage.scene == "school2" && transitions.currBattle == "lunch") {
						if (random <= 0.33)
								health = 40;
						else if (random <= 0.66)
								health = 50;
						else
								health = 60;
						attack = 10;
				} else if (textmanage.scene == "afterschool" && transitions.currBattle == "club") {
						if (random <= 0.33)
								health = 30;
						else if (random <= 0.66)
								health = 40;
						else
								health = 50;
						attack = 8;
				} else if (textmanage.scene == "afterschool" && transitions.currBattle == "friends") {
						if (random <= 0.33)
								health = 110;
						else if (random <= 0.66)
								health = 120;
						else
								health = 130;
						attack = 5;
				} else if (textmanage.scene == "afterschool" && transitions.currBattle == "hw") {
						if (random <= 0.33)
								health = 80;
						else if (random <= 0.66)
								health = 90;
						else
								health = 100;
						attack = 6;
				} else if (textmanage.scene == "afterschool2" && transitions.currBattle == "club") {
						if (random <= 0.33)
								health = 40;
						else if (random <= 0.66)
								health = 50;
						else
								health = 60;
						attack = 9;
				} else if (textmanage.scene == "afterschool2" && transitions.currBattle == "friends") {
						if (random <= 0.33)
								health = 120;
						else if (random <= 0.66)
								health = 130;
						else
								health = 140;
						attack = 5;
				} else if (textmanage.scene == "afterschool2" && transitions.currBattle == "hw") {
						if (random <= 0.33)
								health = 90;
						else if (random <= 0.66)
								health = 100;
						else
								health = 110;
						attack = 8;
				} else if (textmanage.scene == "afterschool2" && transitions.currBattle == "party") {
						if (random <= 0.33) {
								health = 40;
								maxHealth = 40;
						} else if (random <= 0.66) {
								health = 50;
								maxHealth = 50;
						} else {
								health = 60;
								maxHealth = 60;
						}
						attack = 5;
				} else { 	//Final Boss: Sleeping
						health = 160;
						attack = 10;
				}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void onGUI(){


		
		//GUI.EndGroup ();
	}
}