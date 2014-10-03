using UnityEngine;
using System.Collections;

public class textmanage : MonoBehaviour
{
		public int state = 0;
		public int clickedbutton = 0;
		public bool choosingOption = false;
		public int path = 0;
		public string scene = "room";
		public string dialogue = "WAKE UP!!!!!";
		public int fontSize = 18;
	public int classesTaken = 0;
	public bool lunch = false;
		// Use this for initialization
		void Start ()
		{
	
		}

		void Reset_statepath ()
		{
				state = 0;
				path = 0;
		}
		// Update is called once per frame
		void Update ()
		{
				if (Input.GetMouseButtonDown (0) && !choosingOption) {

						if (scene == "room" && state == 0 && path == 2) {
								Reset_statepath ();
						} else if (scene == "room" && state == 0 && path == 1) {
								scene = "breakfast";
								Reset_statepath ();
						} else if (scene == "breakfast" && state == 0 && path != 0) {
								scene = "school";
								Reset_statepath ();
						} else {
								state++;
						}
				}
				if (clickedbutton == 1) {
						choosingOption = false;
						clickedbutton = 0;
						path = 1;
						state = 0;
				} else if (clickedbutton == 2) {
						choosingOption = false;
						clickedbutton = 0;
						path = 2;
						state = 0;
				} else if (clickedbutton == 3) {
						choosingOption = false;
						clickedbutton = 0;
						path = 3;
						state = 0;
				} else if (clickedbutton == 4) {
						choosingOption = false;
						clickedbutton = 0;
						path = 4;
						state = 0;
				} 
		}
		//void onMouseClick
		void OnGUI ()
		{
			
				GUI.Box (new Rect (80, Screen.height - 270, Screen.width - 160, 250), dialogue);
				GUI.skin.box.fontSize = fontSize;	
				GUI.skin.box.wordWrap = true;

				/*
				 *ROOOOOMMM SCEEEENNNNEEEE
				 */
				if (scene == "room") {
						if (state == 0 && path == 0) {
								dialogue = "WAKE UP!!!!!";
						} else if (state == 1 && path == 0) {
								dialogue = "You roll over, groan and hit your alarm clock";		
						} else if (state == 2 && path == 0) {
								choosingOption = true;
								dialogue = "What will you do?";
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Get up")) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Don't get up")) {
										clickedbutton = 2;
								}
						} else if (state == 0 && path == 2) { // option 2
								dialogue = "Really?";
						
						} else if (state == 0 && path == 1) { // option 1
								dialogue = "Sighing, you push yourself out of bed and stand shakily while you blink blearily. ";
						}
							
				}
				/*
				 * SCHOOL SCENE WOW!
				 */
				else if (scene == "breakfast") {
						if (state == 0 && path == 0) {
								dialogue = "Climbing down the stairs, you find your way to the kitchen, make some food, and shovel some of it into your mouth.";
						}
						if (state == 1 && path == 0) {
								choosingOption = true;
								dialogue = "What do you eat? ";
								
								if (GUI.Button (new Rect (100, Screen.height - 140, Screen.width - 200, 25), "Eggs and Bacon")) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Cereal")) {
										clickedbutton = 2;
								}
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Leftovers from last night")) {
										clickedbutton = 3;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Mysterious Oozer")) {
										clickedbutton = 4;
								}
								
						}
						if (state == 0 && (path == 1 || path == 2)) {
								dialogue = "Sounds good!";
						}
						if (state == 0 && path == 3) {
								dialogue = "Okay, that's cool; dinner for breakfast is great!";			
						}
						if (state == 0 && path == 4) {
								dialogue = "Whatever floats your boat!";
						}
				} else if (scene == "school") {
							
				}
		}

}
