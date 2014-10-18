using UnityEngine;
using System.Collections;

public class BattleGUI : MonoBehaviour
{
	
		public bool attackLock = false;
		public string playerTurnString = "Please select a move.";
		public string selectTargetString = "Choose a target.";
		public string dialogue;
		public GUIStyle textcolor;
		public int fontSize = 18;
		public GUIStyle styler;
		public Texture2D texture;
	public GUIStyle promptStyler;
	public Texture2D promptTexture;
		public GUIStyle buttonStyler;
		public Texture2D buttonTexture;

		void Start ()
		{
				dialogue = playerTurnString;
		}

		void Update ()
		{
				Camera.main.backgroundColor = new Color (163f / 255f, 203f / 255f, 204f / 255f, 1f);
		}
	
		void OnGUI ()
		{
				textcolor = new GUIStyle (GUI.skin.label);
				textcolor.fontSize = fontSize;
				textcolor.normal.textColor = Color.white;
				float boxHeight = 150;
				float boxWidth = (Screen.width - (Screen.width / 8) - 100) / 3;
				float boxY = Screen.height - 200;
				float boxOneX = 50;
				float statWidth = boxWidth - 40;
				float statHeight = 30;
				float statX = 70;
				float HPTextY = Screen.height - 180;
				float MPTextY = Screen.height - 155;
				float boxTwoX = 50 + (Screen.width - (Screen.width / 8) - 100) / 3 + Screen.width / 16;
				float boxThreeX = 50 + 2 * (Screen.width - (Screen.width / 8) - 100) / 3 + Screen.width / 8;
				float buttonWidth = (Screen.width - (Screen.width / 8) - 100) / 3 - 40;
				float buttonHeight = 25;
				float buttonX = 50 + (Screen.width - (Screen.width / 8) - 100) / 3 + Screen.width / 16 + 20;
				float buttonOneY = Screen.height - 180;
				float buttonTwoY = Screen.height - 150;
				float backButtonY = Screen.height - 90;
				float announcerX = 25;
				float announcerY = 25;
				float announcerWidth = Screen.width - 50;
				float announcerHeight = 50;	
				GUI.skin.box.wordWrap = true;
				texture = new Texture2D (16, 16);
				for (int y = 0; y < texture.height; ++y) {
						for (int x = 0; x < texture.width; ++x) {
								if ((x > 2 && y > 2) && (x < texture.width - 3 && y < texture.height - 3)) {
										Color color = new Color (228f / 255f, 174f / 255f, 198f / 255f, 1f);
										texture.SetPixel (x, y, color);
								} else {
										Color color = new Color (228f / 255f, 200f / 255f, 213f / 255f, 1f);
										texture.SetPixel (x, y, color);
								}
						}
				}
				texture.Apply ();
		
				styler = new GUIStyle (GUI.skin.box);
				styler.normal.textColor = Color.white;
				styler.fontSize = 18;
				styler.normal.background = texture;

				/* For dialogue */
		promptTexture = new Texture2D (Screen.width - 50, 50);
		for (int y = 0; y < promptTexture.height; ++y) {
			for (int x = 0; x < promptTexture.width; ++x) {
				if ((x > 2 && y > 2) && (x < promptTexture.width - 3 && y < promptTexture.height - 3)) {
					Color color = new Color (228f / 255f, 174f / 255f, 198f / 255f, 1f);
					promptTexture.SetPixel (x, y, color);
				} else {
					Color color = new Color (228f / 255f, 200f / 255f, 213f / 255f, 1f);
					promptTexture.SetPixel (x, y, color);
				}
			}
		}
		promptTexture.Apply ();
		
		promptStyler = new GUIStyle (GUI.skin.box);
		promptStyler.normal.textColor = Color.white;
		promptStyler.fontSize = 18;
		promptStyler.normal.background = promptTexture;

				/* For buttons */
				buttonTexture = new Texture2D ((int)buttonWidth, (int)buttonHeight);
				for (int y = 0; y < buttonTexture.height; ++y) {
						for (int x = 0; x < buttonTexture.width; ++x) {
								if ((x > 1 && y > 1) && (x < buttonTexture.width - 2 && y < buttonTexture.height - 2)) {
										Color color = new Color (245f / 255f, 207f / 255f, 148f / 255f, 1f);
										buttonTexture.SetPixel (x, y, color);
								} else {
										Color color = new Color (254f / 255f, 234f / 255f, 174f / 255f, 1f);
										buttonTexture.SetPixel (x, y, color);
								}
						}
				}
				buttonTexture.Apply ();
		
				buttonStyler = new GUIStyle (GUI.skin.box);
				buttonStyler.normal.textColor = Color.white;
				buttonStyler.fontSize = 14;
				buttonStyler.normal.background = buttonTexture;
				/*Announcer
		 * This section will display what attack the enemy used or playerTurnString.
		 */
			
				/*GUI.Box (new Rect (25, 25, Screen.width - 50, 50), "");
			if (!attackLock) {
				GUI.Label (new Rect (25, 25, Screen.width - 50, 50), playerTurnString);
			} else if (TurnStateMachine.isCommandTargeting()) {
				GUI.Label (new Rect (25, 25, Screen.width - 50, 50), selectTargetString);
			}*/

			
				GUI.Box (new Rect (25, 25, Screen.width - 50, 50), dialogue, promptStyler);
				if (TurnStateMachine.getTurnState () == 2) {
					dialogue = TurnStateMachine.announcerLine;
				} else if (!attackLock) {
						dialogue = playerTurnString;
				} else if (TurnStateMachine.isCommandTargeting ()) {
						//print ("called lololol");
						dialogue = selectTargetString;
				}

				//HP/MP display
				GUI.Box (new Rect (boxOneX, boxY, boxWidth, boxHeight), "", styler); 
				GUI.Label (new Rect (statX, HPTextY, statWidth, statHeight),
		           "Well-being : " + TurnStateMachine.playerHP + "/" + transitions.wellbeing * 100, textcolor);
				GUI.Label (new Rect (statX, MPTextY, statWidth, statHeight),
		           "Grades : " + TurnStateMachine.playerMP + "/" + transitions.grades * 100, textcolor);
	
				//Handles whether or not the buttons are locked (due to currently choosing a target)
				GUI.Box (new Rect (boxTwoX, boxY, boxWidth, boxHeight), "", styler);
				if (TurnStateMachine.getTurn () == 0 && !TurnStateMachine.isCommandTargeting ()) {
						attackLock = false;
				} else {	
						attackLock = true;
				}
				GUI.enabled = !attackLock;
				//Handles the menu buttons
				if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_NONE
						|| TurnStateMachine.commandSelection == TurnStateMachine.SELECT_TARGET_ATTACK) {		//If we're not choosing an ability, show the usual buttons.
						if (GUI.Button (new Rect (buttonX, buttonOneY, buttonWidth, buttonHeight), "Tackle", buttonStyler)) {
								TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_ATTACK;
						}
						if (GUI.Button (new Rect (buttonX, buttonTwoY, buttonWidth, buttonHeight), "Abilities", buttonStyler)) {
								TurnStateMachine.commandSelection = TurnStateMachine.SELECT_ABILITY;
						}
				} else {	//Otherwise show the ability menu
						if (TurnStateMachine.playerMP < TurnStateMachine.DIE_MANA_COST) {
								GUI.enabled = false;
						}
						if (GUI.Button (new Rect (buttonX, buttonOneY, buttonWidth, buttonHeight), "Sip Coffee", buttonStyler)) {
								TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_DIE;
						}
						GUI.enabled = !attackLock;

						if (TurnStateMachine.playerMP < TurnStateMachine.SPLOSIONS_MANA_COST) {
								GUI.enabled = false;
						}
						if (GUI.Button (new Rect (buttonX, buttonTwoY, buttonWidth, buttonHeight), "Cold Shower", buttonStyler)) {
								TurnStateMachine.castSPLOSIONS ();
						}
						GUI.enabled = !attackLock;

						if (GUI.Button (new Rect (buttonX, backButtonY, buttonWidth, buttonHeight), "Back", buttonStyler)) {
								TurnStateMachine.commandSelection = TurnStateMachine.SELECT_NONE;
						}
				}
				//GUI.Box (new Rect (50+2*(Screen.width - (Screen.width/8)- 100)/3 + Screen.width/8, Screen.height - 200, (Screen.width - (Screen.width/8)- 100)/3, 150), "");
		}
}