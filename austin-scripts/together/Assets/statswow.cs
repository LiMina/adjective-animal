using UnityEngine;
using System.Collections;

public class statswow : MonoBehaviour {
	public float happiness = 0;
	public float grades = 0;
	public float wellbeing = 0;
	public Vector2 pos = new Vector2(50,17);
	public Vector2 size = new Vector2(100,25);
	public Texture2D progressBarEmpty;
	public Texture2D progressBarFull;
	public GUIStyle textcolor;
	public GUIStyle styler;
	public Texture2D texture;
	public GUIStyle statsStyle;
	public Texture2D statsTexture;
	public GUIStyle barStyle;
	public Texture2D barTexture;
	// Use this for initialization
	void Start () {
		/*happiness = transitions.happiness;
		grades = transitions.grades;
		wellbeing = transitions.wellbeing;*/
	}
	
	// Update is called once per frame
	void Update () {
		happiness = transitions.happiness;
		grades = transitions.grades;
		wellbeing = transitions.wellbeing;

		if (transitions.happiness > 1f) {
			transitions.happiness = 1f;
		}
		if (transitions.grades > 1f) {
			transitions.grades = 1f;
		}
		if (transitions.wellbeing > 1f) {
			transitions.wellbeing = 1f;		
		}
	}
	void OnGUI(){
		textcolor = new GUIStyle (GUI.skin.label);
		textcolor.normal.textColor = Color.white;
		/*For the stats bg*/
		texture = new Texture2D (128, 128);
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
		texture.Apply();
		styler = new GUIStyle (GUI.skin.box);
		styler.normal.background = texture;

		/*For the stats health bars*/
		statsTexture = new Texture2D ((int)size.x, (int)size.y);
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
		statsStyle.normal.textColor = Color.white;
		statsStyle.fontSize = 14;
		statsStyle.normal.background = statsTexture;
		/* bars themselves */
		barTexture = new Texture2D ((int)size.x, (int)size.y);
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
		barStyle.normal.textColor = Color.white;
		barStyle.normal.background = barTexture;

		GUI.Box (new Rect (10, 10, 180, 100),"", styler);
		/*
		 * Happiness
		 */
		// draw the background:
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y), "", textcolor);
		GUI.Box (new Rect (0,0, size.x, size.y), "Happiness", statsStyle);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * happiness, size.y)/*, "  Happiness",textcolor*/);
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarFull, barStyle);
		GUI.EndGroup ();
		
		GUI.EndGroup ();

		/*
		 * Wellbeing
		 */ 
		GUI.BeginGroup (new Rect (pos.x, pos.y+30, size.x, size.y), "",textcolor);
		GUI.Box (new Rect (0,0, size.x, size.y), "Well-being", statsStyle);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * wellbeing, size.y));
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarFull, barStyle);
		GUI.EndGroup ();
		
		GUI.EndGroup ();

		/*
		 * Grades
		 */
		
		GUI.BeginGroup (new Rect (pos.x, pos.y+60, size.x, size.y), "",textcolor);
		GUI.Box (new Rect (0,0, size.x, size.y), "Grades", statsStyle);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * grades, size.y));
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarFull, barStyle);
		GUI.EndGroup ();
		
		GUI.EndGroup ();

	}
}
