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
		GUI.Box (new Rect (10, 10, 180, 100),"");
		/*
		 * Happiness
		 */
		// draw the background:
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y), "    Happiness",textcolor);
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * happiness, size.y)/*, "  Happiness",textcolor*/);
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();

		/*
		 * Grades
		 */

		GUI.BeginGroup (new Rect (pos.x, pos.y+30, size.x, size.y), "      Grades",textcolor);
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * grades, size.y));
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();

		/*
		 * Wellbeing
		 */ 
		GUI.BeginGroup (new Rect (pos.x, pos.y+60, size.x, size.y), "    Well-being",textcolor);
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarEmpty);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * wellbeing, size.y));
		GUI.Box (new Rect (0,0, size.x, size.y),progressBarFull);
		GUI.EndGroup ();
		
		GUI.EndGroup ();

	}
}
