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
		public Texture2D buttonHoverTexture;

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
				GUI.skin.label.wordWrap = true;
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
										Color color = new Color (228f / 255f, 125f / 255f, 171f / 255f, 1f);
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
				buttonHoverTexture = new Texture2D (Screen.width - 200, 25);
				for (int y = 0; y < buttonHoverTexture.height; ++y) {
						for (int x = 0; x < buttonHoverTexture.width; ++x) {
								if ((x > 1 && y > 1) && (x < buttonHoverTexture.width - 2 && y < buttonHoverTexture.height - 2)) {
										Color color = new Color (254f / 255f, 234f / 255f, 174f / 255f, 1f);
										buttonHoverTexture.SetPixel (x, y, color);
								} else {
										Color color = new Color (254f / 255f, 234f / 255f, 174f / 255f, 1f);
										buttonHoverTexture.SetPixel (x, y, color);
								}
						}
				}
				buttonHoverTexture.Apply ();
		
				buttonStyler = new GUIStyle (GUI.skin.box);
				buttonStyler.normal.textColor = Color.black;
				buttonStyler.fontSize = 14;
				buttonStyler.normal.background = buttonTexture;
				buttonStyler.hover.background = buttonHoverTexture;
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
				if (TurnStateMachine.getTurn () == 0 && !TurnStateMachine.isCommandTargeting () && TurnStateMachine.getTurnState () != 2) {
						attackLock = false;
				} else {	
						attackLock = true;
				}
				GUI.enabled = !attackLock;
				//Handles the menu buttons
				string phys = "Tackle";
				string spec1 = "Sip Coffee";
				string spec2 = "Cold Shower";
				if (transitions.currBattle == "wakeup") { //This is actually our first wake up battle here
						phys = "Pound the alarm";
						spec1 = "Sip Coffee";
						spec2 = "Cold Shower";
						DescriptionBox.ATTACK_DESC = "Shut off that alarm!";
						DescriptionBox.DIE_DESC = "Invigorate yourself with a cuppa joe. Deals high damage to a single target.\nRequires a grade of 25.";
						DescriptionBox.SPLOSIONS_DESC = "An icy shower strikes down lethargy. Deals moderate damage to all enemies.\nRequires a grade of 50.";
						TurnStateMachine.attackLine = "You slam your hand on the alarm for 15 damage!";
				}
				if (transitions.currBattle == "lecture") {
						phys = "Answer";
						spec1 = "Sip Coffee";
						spec2 = "Bathroom Break";
						DescriptionBox.ATTACK_DESC = "Solve that tricky problem on the board.";
						DescriptionBox.DIE_DESC = "Invigorate yourself with a cuppa joe. Deals high damage to a single target.\nRequires a grade of 25.";
						DescriptionBox.SPLOSIONS_DESC = "Splash your face with some water in the bathroom to attain better focus. Deals moderate damage to all enemies.\nRequires a grade of 50.";
				}
				if (transitions.currBattle == "test") {
						phys = "Tackle";
						spec1 = "Bonus Question";
						spec2 = "Extra Time";
						DescriptionBox.ATTACK_DESC = "Tackle the problem at hand.";
						DescriptionBox.DIE_DESC = "Solve the bonus question for extra credit. Deals high damage to a single target.\nRequires a grade of 25.";
						DescriptionBox.SPLOSIONS_DESC = "The professor has a change of heart and extends the time. Deals moderate damage to all enemies.\nRequires a grade of 50.";
				}
				if (transitions.currBattle == "gym") {
						phys = "Throw";
						spec1 = "Hydration";
						spec2 = "Too Cool For You";
						DescriptionBox.ATTACK_DESC = "Throw your weight into it!";
						DescriptionBox.DIE_DESC = "Hydrate yourself and replenish your energy.\nRequires a grade of 25.";
						DescriptionBox.SPLOSIONS_DESC = "Make heads turn with how fabulous you are. Deals moderate damage to all enemies.\nRequires a grade of 50.";
				}
				if (transitions.currBattle == "lunch") {
						phys = "Devour";
						spec1 = "Dessert";
						spec2 = "Healthy Reminders";
						DescriptionBox.ATTACK_DESC = "Go ahead, take a bite.";
						DescriptionBox.DIE_DESC = "Treat yo' self. Deals high damage to a single target.\nRequires a grade of 25.";
						DescriptionBox.SPLOSIONS_DESC = "Your body needs food, and it gives you the energy you need. Deals moderate damage to all enemies.\nRequires a grade of 50.";
				}
				if (transitions.currBattle == "studying") {
						phys = "Flick";
						spec1 = "Study Buddy";
						spec2 = "Highlight";
						DescriptionBox.ATTACK_DESC = "Flick the page to the next section. Progress!";
						DescriptionBox.DIE_DESC = "Grab a friend who helps you through a tough subject. Deals high damage to a single target.\nRequires a grade of 25.";
						DescriptionBox.SPLOSIONS_DESC = "Mark the sections that are most important. Deals moderate damage to all enemies.\nRequires a grade of 50.";
				}
				if (transitions.currBattle == "club") {
						phys = "Speak Up!";
						spec1 = "Call Them Out";
						spec2 = "RAINBOWS";
						DescriptionBox.ATTACK_DESC = "Make your voice heard!";
						DescriptionBox.DIE_DESC = "Call out someone for insensitive language. Deals high damage to a single target.\nRequires a grade of 25.";
						DescriptionBox.SPLOSIONS_DESC = "Flaunt your pride! Deals moderate damage to all enemies.\nRequires a grade of 50.";
				}
				if (transitions.currBattle == "friends") {
						phys = "Banter";
						spec1 = "Call Them Out";
						spec2 = "Coming Out";
						DescriptionBox.ATTACK_DESC = "Make a sassy comeback to make everyone laugh.";
						DescriptionBox.DIE_DESC = "Call out your friend and educate them about the insensitive use of language. Deals high damage to a single target.\nRequires a grade of 25.";
						DescriptionBox.SPLOSIONS_DESC = "You decide to come out to them so you can really be yourself with them. Deals moderate damage to all enemies.\nRequires a grade of 50.";
				}
				if (transitions.currBattle == "hw") {
						phys = "Answer";
						spec1 = "Study Buddy";
						spec2 = "Extra Time";
						DescriptionBox.ATTACK_DESC = "Solve that tricky homework problem.";
						DescriptionBox.DIE_DESC = "Grab a friend who helps you through a tough subject. Deals high damage to a single target.\nRequires a grade of 25.";
						DescriptionBox.SPLOSIONS_DESC = "The professor has a change of heart and extends the deadline. Deals moderate damage to all enemies.\nRequires a grade of 50.";
				}
				if (transitions.currBattle == "party") {
						phys = "DANCE";
						spec1 = "Snappy Comeback";
						spec2 = "Queering the Binary";
						DescriptionBox.ATTACK_DESC = "Break out those killer dance moves!";
						DescriptionBox.DIE_DESC = "Someone insults your fashion choices, and you SHUT THEM DOWN. Deals high damage to a single target.\nRequires a grade of 25.";
						DescriptionBox.SPLOSIONS_DESC = "Make everyone re-question their notion of the gender binary. Deals moderate damage to all enemies.\nRequires a grade of 50.";
				}
				if (transitions.currBattle == "sleep") {
						phys = "Throw";
						spec1 = "Candle Light";
						spec2 = "Soothing Sound";
						DescriptionBox.ATTACK_DESC = "Throw down the pillows and get yourself settled in.";
						DescriptionBox.DIE_DESC = "Bring in a little light and make the room smell nice too. Deals high damage to a single target.\nRequires a grade of 25.";
						DescriptionBox.SPLOSIONS_DESC = "Music, white noise, whatever helps you sleep. Deals moderate damage to all enemies.\nRequires a grade of 50.";
				}
				if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_NONE
						|| TurnStateMachine.commandSelection == TurnStateMachine.SELECT_TARGET_ATTACK) {		//If we're not choosing an ability, show the usual buttons.
						if (GUI.Button (new Rect (buttonX, buttonOneY, buttonWidth, buttonHeight), phys, buttonStyler)) {
								TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_ATTACK;
						}
						if (GUI.Button (new Rect (buttonX, buttonTwoY, buttonWidth, buttonHeight), "Abilities", buttonStyler)) {
								TurnStateMachine.commandSelection = TurnStateMachine.SELECT_ABILITY;
						}
				} else {	//Otherwise show the ability menu
						if (TurnStateMachine.playerMP < TurnStateMachine.DIE_MANA_COST) {
								GUI.enabled = false;
						}
						if (GUI.Button (new Rect (buttonX, buttonOneY, buttonWidth, buttonHeight), spec1, buttonStyler)) {
								TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_DIE;
						}
						GUI.enabled = !attackLock;

						if (TurnStateMachine.playerMP < TurnStateMachine.SPLOSIONS_MANA_COST) {
								GUI.enabled = false;
						}
						if (GUI.Button (new Rect (buttonX, buttonTwoY, buttonWidth, buttonHeight), spec2, buttonStyler)) {
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