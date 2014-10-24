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

		private GameObject turnStateMachine;

		void Start ()
		{
				dialogue = playerTurnString;
				turnStateMachine = GameObject.FindGameObjectWithTag ("TurnStateMachine");
		}

		void Update ()
		{
				Camera.main.backgroundColor = new Color (163f / 255f, 203f / 255f, 204f / 255f, 1f);
		}
	
		void OnGUI ()
		{
				textcolor = new GUIStyle (GUI.skin.label);
				textcolor.fontSize = fontSize;
				textcolor.normal.textColor = Color.black;
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

				int cameraOffsetX = (int)(Camera.main.transform.position.x * Screen.width / 10);
				int cameraOffsetY = (int)(Camera.main.transform.position.y * Screen.height / 10);

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
				styler.normal.textColor = Color.black;
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
				promptStyler.normal.textColor = Color.black;
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

			
				GUI.Box (new Rect (25 + cameraOffsetX, 25 + cameraOffsetY, Screen.width - 50, 50), dialogue, promptStyler);
				if (TurnStateMachine.getTurnState () != 0 || !TurnStateMachine.showedSpecialEffect) {
						dialogue = TurnStateMachine.announcerLine;
				} else if (!attackLock) {
						dialogue = playerTurnString;
				} else if (TurnStateMachine.isCommandTargeting ()) {
						dialogue = selectTargetString;
				}

				//HP/MP display
				GUI.Box (new Rect (boxOneX + cameraOffsetX, boxY + cameraOffsetY, boxWidth, boxHeight), "", styler); 
				GUI.Label (new Rect (statX + cameraOffsetX, HPTextY + cameraOffsetY, statWidth, statHeight),
		           "Well-being : " + TurnStateMachine.playerHP + "/" + Mathf.RoundToInt (transitions.wellbeing * 100), textcolor);
				GUI.Label (new Rect (statX + cameraOffsetX, MPTextY + cameraOffsetY, statWidth, statHeight),
		           "Grades : " + TurnStateMachine.playerMP + "/" + Mathf.RoundToInt (transitions.grades * 100), textcolor);
	
				//Handles whether or not the buttons are locked (due to currently choosing a target)
				GUI.Box (new Rect (boxTwoX + cameraOffsetX, boxY + cameraOffsetY, boxWidth, boxHeight), "", styler);
				if (TurnStateMachine.getTurn () == 0 && !TurnStateMachine.isCommandTargeting () && TurnStateMachine.getTurnState () != 2 && TurnStateMachine.showedSpecialEffect && !TurnStateMachine.ending) {
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
						DescriptionBox.DIE_DESC = "Invigorate yourself with a cuppa joe. Deals MODERATE damage to a single target.\nRequires a grade of " + TurnStateMachine.DIE_MANA_COST + ".";
						DescriptionBox.SPLOSIONS_DESC = "An icy shower strikes down lethargy. Deals HIGH damage to ALL enemies.\nRequires a grade of " + TurnStateMachine.SPLOSIONS_MANA_COST + ".";
						TurnStateMachine.attackLine = "You slam your hand on the alarm! Today's not the day to wake up to Miley Cyrus.";
						TurnStateMachine.DIELine = "You feel your fingers twitching as the caffeine hits your system. That's normal, right?";
						TurnStateMachine.SPLOSIONSLine = "The cold shower wakes you up ...and your singing wakes up everyone else.";
				}
				if (transitions.currBattle == "lecture") {
						phys = "Take Notes";
						spec1 = "More Coffee";
						spec2 = "Raise hand";
						DescriptionBox.ATTACK_DESC = "Scribble down some notes.";
						DescriptionBox.DIE_DESC = "A double-whip, low-fat, soy, mocha frappa-lappa-chino. Deals MODERATE damage to a single target.\nRequires a grade of " + TurnStateMachine.DIE_MANA_COST + ".";
						DescriptionBox.SPLOSIONS_DESC = "Break the awkward silence by being the one to answer the professor's question. Deals HIGH damage to ALL enemies.\nRequires a grade of " + TurnStateMachine.SPLOSIONS_MANA_COST + ".";
						TurnStateMachine.attackLine = "You copy down the slides word-for-word. Didn't hear the lecturer, but that's okay.";
						TurnStateMachine.DIELine = "You're having yet another coffee...but you can totally stop anytime you want! You just don't want to.";
						TurnStateMachine.SPLOSIONSLine = "You solved the question! You got 99 problems but this won't be one!";		
				}
				if (transitions.currBattle == "test") {
						phys = "Tackle";
						spec1 = "Bonus Question";
						spec2 = "Cheat Sheet";
						DescriptionBox.ATTACK_DESC = "Tackle the problem at hand.";
						DescriptionBox.DIE_DESC = "Solve the bonus question for extra credit. Deals MODERATE damage to a single target.\nRequires a grade of " + TurnStateMachine.DIE_MANA_COST + ".";
						DescriptionBox.SPLOSIONS_DESC = "Whip out your meticulously prepared cheat sheet. Deals HIGH damage to ALL enemies.\nRequires a grade of " + TurnStateMachine.SPLOSIONS_MANA_COST + ".";
						TurnStateMachine.attackLine = "Take-down! Figuratively, of course. If only you could solve all your problems by tackling.";
						TurnStateMachine.DIELine = "You solve the bonus question! Now you can afford to get more questions wrong!";
						TurnStateMachine.SPLOSIONSLine = "You found what you needed in the corner of your cheat sheet. You put away your magnifying glass.";		
				}
				if (transitions.currBattle == "gym") {
						phys = "Throw";
						spec1 = "Hydration";
						spec2 = "Too Cool For You";
						DescriptionBox.ATTACK_DESC = "Throw your weight into it!";
						DescriptionBox.DIE_DESC = "Hydrate yourself with a cool drink. Deals MODERATE damage to a single target.\nRequires a grade of " + TurnStateMachine.DIE_MANA_COST + ".";
						DescriptionBox.SPLOSIONS_DESC = "Make heads turn with how fabulous you are. Deals HIGH damage to ALL enemies.\nRequires a grade of " + TurnStateMachine.SPLOSIONS_MANA_COST + ".";
						TurnStateMachine.attackLine = "You throw your weight into your workout, but you don't throw your weights. Because that's dangerous.";
						TurnStateMachine.DIELine = "You refresh yourself with an energy drink! How come you never sweat colors like in Gatorade commercials?";
						TurnStateMachine.SPLOSIONSLine = "You rock your favorite shirt and feel the eyes of jealous on-lookers.";		
				}
				if (transitions.currBattle == "lunch") {
						phys = "Devour";
						spec1 = "Dessert";
						spec2 = "Healthy Reminders";
						DescriptionBox.ATTACK_DESC = "Go ahead, take a bite.";
						DescriptionBox.DIE_DESC = "Treat yo' self. Deals MODERATE damage to a single target.\nRequires a grade of " + TurnStateMachine.DIE_MANA_COST + ".";
						DescriptionBox.SPLOSIONS_DESC = "Your body needs food, and it gives you the energy you need. Deals HIGH damage to ALL enemies.\nRequires a grade of " + TurnStateMachine.SPLOSIONS_MANA_COST + ".";
						TurnStateMachine.attackLine = "You devour a satisfying mouthful of food!";
						TurnStateMachine.DIELine = "Looks like you CAN have your cake and eat it too!";
						TurnStateMachine.SPLOSIONSLine = "A healthy meal is good for your body and your self-esteem!";		
				}
				if (transitions.currBattle == "studying") {
						phys = "Flick";
						spec1 = "Study Buddy";
						spec2 = "Highlight";
						DescriptionBox.ATTACK_DESC = "Flick the page to the next section. Progress!";
						DescriptionBox.DIE_DESC = "Grab a friend who helps you through a tough subject. Deals MODERATE damage to a single target.\nRequires a grade of " + TurnStateMachine.DIE_MANA_COST + ".";
						DescriptionBox.SPLOSIONS_DESC = "Good thing you bought that pack of 20 different colored highlighters. Deals HIGH damage to ALL enemies.\nRequires a grade of " + TurnStateMachine.SPLOSIONS_MANA_COST + ".";
						TurnStateMachine.attackLine = "Slowly but surely, you make your way through the textbook!";
						TurnStateMachine.DIELine = "Two heads are better than one! Unless you end up watching internet cat videos.";
						TurnStateMachine.SPLOSIONSLine = "Not sure if highlighting each line in a different color is useful, but it looks awesome!";		
				}
				if (transitions.currBattle == "club") {
						phys = "Socialize!";
						spec1 = "Hold Discussion";
						spec2 = "RAINBOWS";
						DescriptionBox.ATTACK_DESC = "Do a bit of mingling.";
						DescriptionBox.DIE_DESC = "Politely point out some problematic language. Deals MODERATE damage to a single target.\nRequires a grade of " + TurnStateMachine.DIE_MANA_COST + ".";
						DescriptionBox.SPLOSIONS_DESC = "Flaunt your pride! Deals HIGH damage to ALL enemies.\nRequires a grade of " + TurnStateMachine.SPLOSIONS_MANA_COST + ".";
						TurnStateMachine.attackLine = "You talk with old friends and make some new ones.";
						TurnStateMachine.DIELine = "People agree that this kind of language should be avoided.";
						TurnStateMachine.SPLOSIONSLine = "A boost in your pride blows away the haters!";		
				}
				if (transitions.currBattle == "friends") {
						phys = "Banter";
						spec1 = "Criticism";
						spec2 = "Coming Out";
						DescriptionBox.ATTACK_DESC = "Make a sassy comeback to make everyone laugh.";
						DescriptionBox.DIE_DESC = "Point out some insensitive language. Deals MODERATE damage to a single target.\nRequires a grade of " + TurnStateMachine.DIE_MANA_COST + ".";
						DescriptionBox.SPLOSIONS_DESC = "You decide to come out so you can really be yourself around them. Deals HIGH damage to ALL enemies.\nRequires a grade of " + TurnStateMachine.SPLOSIONS_MANA_COST + ".";
						TurnStateMachine.attackLine = "You are incredibly witty and charming! And smart! And popular! You are just the best.";
						TurnStateMachine.DIELine = "Your friends are willing to listen.";
						TurnStateMachine.SPLOSIONSLine = "A moment of pride and self-acceptance!";		
				}
				if (transitions.currBattle == "hw") {
						phys = "Answer";
						spec1 = "Study Buddy";
						spec2 = "Extra Credit";
						DescriptionBox.ATTACK_DESC = "Solve that tricky homework problem.";
						DescriptionBox.DIE_DESC = "Grab a friend who helps you through a tough subject. Deals MODERATE damage to a single target.\nRequires a grade of " + TurnStateMachine.DIE_MANA_COST + ".";
						DescriptionBox.SPLOSIONS_DESC = "Spend a little more time for a few more points. Deals HIGH damage to ALL enemies.\nRequires a grade of " + TurnStateMachine.SPLOSIONS_MANA_COST + ".";
						TurnStateMachine.attackLine = "You solved the question! You got 99 problems but this won't be one!";
						TurnStateMachine.DIELine = "Two heads are better than one! Unless you end up watching internet owl videos.";
						TurnStateMachine.SPLOSIONSLine = "Extra points! That means you can slack off on your next homework!";		
				}
				if (transitions.currBattle == "party") {
						phys = "DANCE";
						spec1 = "Snappy Comeback";
						spec2 = "Queering the Binary";
						DescriptionBox.ATTACK_DESC = "Break out those killer dance moves!";
						DescriptionBox.DIE_DESC = "Someone insults your fashion choices, and you SHUT THEM DOWN. Deals MODERATE damage to a single target.\nRequires a grade of " + TurnStateMachine.DIE_MANA_COST + ".";
						DescriptionBox.SPLOSIONS_DESC = "Make everyone re-question their notion of the gender binary. Deals HIGH damage to ALL enemies.\nRequires a grade of " + TurnStateMachine.SPLOSIONS_MANA_COST + ".";
						TurnStateMachine.attackLine = "UNTS UNTS UNTS UNTS";
						TurnStateMachine.DIELine = "You make a snappy come back at someone because you are FABULOUS! Snappier than a crocodile!";
						TurnStateMachine.SPLOSIONSLine = "You break down those gender binaries like a wrecking ball!";		
				}
				if (transitions.currBattle == "sleep") {
						phys = "Throw";
						spec1 = "Candle Light";
						spec2 = "Soothing Sounds";
						DescriptionBox.ATTACK_DESC = "Throw down the pillows and get yourself settled in.";
						DescriptionBox.DIE_DESC = "Bring in a little light and make the room smell nice too. Deals MODERATE damage to a single target.\nRequires a grade of " + TurnStateMachine.DIE_MANA_COST + ".";
						DescriptionBox.SPLOSIONS_DESC = "Music, white noise, whatever helps you sleep. Deals HIGH damage to ALL enemies.\nRequires a grade of " + TurnStateMachine.SPLOSIONS_MANA_COST + ".";
						TurnStateMachine.attackLine = "It's time for a throw-down! A relaxing, soothing throw-down.";
						TurnStateMachine.DIELine = "You bring in a little light, because your big one won't fit through the door.";
						TurnStateMachine.SPLOSIONSLine = "A calming sound track makes you feel like you're sinking into your bed!";		
				}
				if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_NONE
						|| TurnStateMachine.commandSelection == TurnStateMachine.SELECT_TARGET_ATTACK) {		//If we're not choosing an ability, show the usual buttons.
						if (GUI.Button (new Rect (buttonX + cameraOffsetX, buttonOneY + cameraOffsetY, buttonWidth, buttonHeight), phys, buttonStyler)) {
								TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_ATTACK;
						}
						if (GUI.Button (new Rect (buttonX + cameraOffsetX, buttonTwoY + cameraOffsetY, buttonWidth, buttonHeight), "Abilities", buttonStyler)) {
								TurnStateMachine.commandSelection = TurnStateMachine.SELECT_ABILITY;
						}
				} else {	//Otherwise show the ability menu
						if (TurnStateMachine.playerMP < TurnStateMachine.DIE_MANA_COST) {
								GUI.enabled = false;
						}
						if (GUI.Button (new Rect (buttonX + cameraOffsetX, buttonOneY + cameraOffsetY, buttonWidth, buttonHeight), spec1, buttonStyler)) {
								TurnStateMachine.commandSelection = TurnStateMachine.SELECT_TARGET_DIE;
						}
						GUI.enabled = !attackLock;

						if (TurnStateMachine.playerMP < TurnStateMachine.SPLOSIONS_MANA_COST) {
								GUI.enabled = false;
						}
						if (GUI.Button (new Rect (buttonX + cameraOffsetX, buttonTwoY + cameraOffsetY, buttonWidth, buttonHeight), spec2, buttonStyler)) {
								turnStateMachine.GetComponent<TurnStateMachine>().castSPLOSIONS ();
						}
						GUI.enabled = !attackLock;

						if (GUI.Button (new Rect (buttonX + cameraOffsetX, backButtonY + cameraOffsetY, buttonWidth, buttonHeight), "Back", buttonStyler)) {
								TurnStateMachine.commandSelection = TurnStateMachine.SELECT_NONE;
						}
				}
				//GUI.Box (new Rect (50+2*(Screen.width - (Screen.width/8)- 100)/3 + Screen.width/8, Screen.height - 200, (Screen.width - (Screen.width/8)- 100)/3, 150), "");
		}
}