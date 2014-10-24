﻿using UnityEngine;
using System.Collections;

public class TurnStateMachine : MonoBehaviour
{
		public static float critChance = (float)0.0625;
		public static float missChance = (float)0.0625;

		/**
	 * What action is currently selected. e.g. this changes after you press the attack button and you're choosing a target.
	 **/
		public static int commandSelection = SELECT_NONE;
		//Not waiting to choose a target (a.k.a. just on the menu)
		public static readonly int SELECT_NONE = 0;
		public static readonly int SELECT_ABILITY = 1;

		//Waiting to choose a target
		public static readonly int SELECT_TARGET_ATTACK = 101;
		public static readonly int SELECT_TARGET_DIE = 102;
		public static int DIE_MANA_COST = 5;
		public static int SPLOSIONS_MANA_COST = 15;
		public static int attack = 6 + (int)(Mathf.Round (transitions.happiness * 100) / 5 + 0.5);
		public static int DIEAttack = 15 + (int)(transitions.happiness * 100 / 4 + 0.5);
		public static int SPLOSIONSAttack = 20 + (int)(transitions.happiness * 100 / 2 + 0.5);

		/**
	 * Who's turn it is.
	 * 0 = Player's turn
	 * 1 = First enemy's turn
	 * 2 = Second enemy's turn
	 * ...
	 * n = n-th enemy's turn
	 **/
		private static int whosTurn = 0;
		/**
	 * The current stage of the turn. Turns consist of up to 3 stages.
	 * 0 = Selecting an attack
	 * 1 = Attack is currently animating / in progress
	 * 2 = Attack outcome string is currently displayed on announcer GUI.
	 * */
		private static int turnState = 0;
		public static int numEnemies = 0;
		public static int playerHP = 100;
		public static int playerMP = 25;
		public int deadEnemyCounter = 0;
		public static string announcerLine;
		public static string critSuffix = " It was a critical hit!";
		public static string missLine = "Your attack missed!";
		public static string attackLine = "You tackled the problem at hand!";
		public static string DIELine = "You sipped your coffee, jolting your tiredness awake!";
		public static string SPLOSIONSLine = "A cold shower snaps away your drowsiness, giving you a literal wake-up call!";
		public static string winLine = "You overcame your adversaries!";
		public static string loseLine = "You feel terribly overwhelmed. You couldn't handle the pressure...";
		public static bool ending = false;
		public static bool showedSpecialEffect = false;
		private static int turnsLeft = -1;
		private static bool poison = false;
		static public TurnStateMachine instance;
	
		// Use this for initialization
		void Start ()
		{
				playerHP = Mathf.RoundToInt (transitions.wellbeing * 100);
				playerMP = Mathf.RoundToInt (transitions.grades * 100);
				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
				GameObject secondenemy = GameObject.Find ("Enemy2");
				GameObject firstenemy = GameObject.Find ("Enemy");
				GameObject thirdenemy = GameObject.Find ("Enemy3");
				if (transitions.enemyCount == 3) {
						foreach (GameObject e in enemies) {
								//print ("more than one enemy");
								e.SetActive (true);
						}
						secondenemy.transform.position = new Vector3 (-3f, 1.2f, 0f);
						secondenemy.transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
						firstenemy.transform.position = new Vector3 (0f, 1.2f, 0f);
						thirdenemy.transform.position = new Vector3 (3f, 1.2f, 0f);
						thirdenemy.transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
			
				}
				if (transitions.enemyCount == 2) {
						/*foreach (GameObject e in enemies) {
								print ("more than one enemy");
								e.SetActive (true);
						}*/
						thirdenemy.SetActive (false);
						//secondenemy 
						secondenemy.transform.position = new Vector3 (-3f, 1.2f, 0f);
						secondenemy.transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
						firstenemy.transform.position = new Vector3 (3f, 1.2f, 0f);
			
				} 
				if (transitions.enemyCount == 1) {
						firstenemy.transform.position = new Vector3 (0, 1.2f, 0f);
						secondenemy.SetActive (false);
						thirdenemy.SetActive (false);
				}
				foreach (GameObject e in enemies) {
						e.GetComponent<SpriteRenderer> ().sprite = transitions.nextImage;
				}

				attack = 6 + (int)(Mathf.Round (transitions.happiness * 100) / 5 + 0.5);
				DIEAttack = 15 + (int)(Mathf.Round (transitions.happiness * 100) / 4 + 0.5);
				SPLOSIONSAttack = 20 + (int)(Mathf.Round (transitions.happiness * 100) / 2 + 0.5);
		}

		bool CheckAllDead ()
		{
				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
				deadEnemyCounter = 0;
				foreach (GameObject e in enemies) {
						if (e.GetComponent<Stats> ().health <= 0) {
								deadEnemyCounter++;
						}
						if (deadEnemyCounter == numEnemies) { // THEY'RE ALL DEAD AHHHHHHH
								if (!ending && getTurnState () != 0) { // Finish the message for your last attack before continuing to the end message
										if (Input.GetButtonDown ("Fire1")) {
												ending = true;
												nextTurn ();
										}
										return true;
								}
								if (getTurnState () == 2) {
										if (Input.GetButtonDown ("Fire1")) {
												resetTurnStateMachine ();
												Application.LoadLevel ("dialogue");
												transitions.won = true;
												/** Post-battle win effects. */
												if (transitions.currBattle == "wakeup") {
														transitions.happiness = Mathf.Min (1, transitions.happiness + 0.1f);
												} else if (textmanage.scene == "school" && transitions.currBattle == "lecture") {
														transitions.grades = Mathf.Min (1, transitions.grades + 0.1f);
												} else if (textmanage.scene == "school" && transitions.currBattle == "test") {
														transitions.happiness = Mathf.Min (1, transitions.happiness + 0.1f);
												} else if (textmanage.scene == "school" && transitions.currBattle == "gym") {
														transitions.wellbeing = Mathf.Min (1, transitions.wellbeing + 0.1f);
														transitions.happiness = Mathf.Min (1, transitions.happiness + 0.05f);
												} else if (textmanage.scene == "school2" && transitions.currBattle == "lecture") {
														transitions.grades = Mathf.Min (1, transitions.grades + 0.1f);
												} else if (textmanage.scene == "school2" && transitions.currBattle == "gym") {
														transitions.wellbeing = Mathf.Min (1, transitions.wellbeing + 0.1f);
														transitions.happiness = Mathf.Min (1, transitions.happiness + 0.05f);
												} else if (textmanage.scene == "school2" && transitions.currBattle == "test") {
														transitions.happiness = Mathf.Min (1, transitions.happiness + 0.1f);
												} else if (textmanage.scene == "school2" && transitions.currBattle == "studying") {
														transitions.wellbeing = Mathf.Min (1, transitions.wellbeing + 0.1f);
														transitions.grades = Mathf.Min (1, transitions.grades + 0.05f);
												} else if (textmanage.scene == "school2" && transitions.currBattle == "lunch") {
														transitions.wellbeing = Mathf.Min (1, transitions.wellbeing + 0.25f);
												}
										}
										return true;
								}
								if (transitions.currBattle != "wakeup" && textmanage.scene != "school" && textmanage.scene != "school2") // To catch extra cases
										announcerLine = winLine;
								/** Win lines */
								if (transitions.currBattle == "wakeup") {
										if (getTurnState () == 0) {
												announcerLine = winLine + " You feel great having started the day off right!";
												nextTurnState ();
										} else if (getTurnState () == 1) {
												if (Input.GetButtonDown ("Fire1")) {
														announcerLine = "Your happiness has increased!";
														nextTurnState ();
												}
										}
								} else if (textmanage.scene == "school" && transitions.currBattle == "lecture") {
										if (getTurnState () == 0) {
												announcerLine = winLine + " You feel like you know today's material pretty well!";
												nextTurnState ();
										} else if (getTurnState () == 1) {
												if (Input.GetButtonDown ("Fire1")) {
														announcerLine = "Your grades have increased!";
														nextTurnState ();
												}
										}
								} else if (textmanage.scene == "school" && transitions.currBattle == "test") {
										if (getTurnState () == 0) {
												announcerLine = winLine + " You don't know your grade, but you feel confident about that exam!";
												nextTurnState ();
										} else if (getTurnState () == 1) {
												if (Input.GetButtonDown ("Fire1")) {
														announcerLine = "Your happiness has increased!";
														nextTurnState ();
												}
										}
								} else if (textmanage.scene == "school" && transitions.currBattle == "gym") {
										if (getTurnState () == 0) {
												announcerLine = winLine + " What a good workout!";
												nextTurnState ();
										} else if (getTurnState () == 1) {
												if (Input.GetButtonDown ("Fire1")) {
														announcerLine = "Your well-being has increased! Your happiness has increased!";
														nextTurnState ();
												}
										}
								} else if (textmanage.scene == "school2" && transitions.currBattle == "lecture") {
										if (getTurnState () == 0) {
												announcerLine = winLine + " You're awake and have pages of notes!";
												nextTurnState ();
										} else if (getTurnState () == 1) {
												if (Input.GetButtonDown ("Fire1")) {
														announcerLine = "Your grades have increased!";
														nextTurnState ();
												}
										}
								} else if (textmanage.scene == "school2" && transitions.currBattle == "gym") {
										if (getTurnState () == 0) {
												announcerLine = winLine + " Yeah yeah! You feel pumped!";
												nextTurnState ();
										} else if (getTurnState () == 1) {
												if (Input.GetButtonDown ("Fire1")) {
														announcerLine = "Your well-being has increased! Your happiness has increased!";
														nextTurnState ();
												}
										}
								} else if (textmanage.scene == "school2" && transitions.currBattle == "test") {
										if (getTurnState () == 0) {
												announcerLine = winLine + " Grades will be out later, but for now you feel great!";
												nextTurnState ();
										} else if (getTurnState () == 1) {
												if (Input.GetButtonDown ("Fire1")) {
														announcerLine = "Your happiness has increased!";
														nextTurnState ();
												}
										}
								} else if (textmanage.scene == "school2" && transitions.currBattle == "studying") {
										if (getTurnState () == 0) {
												announcerLine = winLine + " With some studying done, you feel a little less stressed!";
												nextTurnState ();
										} else if (getTurnState () == 1) {
												if (Input.GetButtonDown ("Fire1")) {
														announcerLine = "Your well-being has increased! You grades have increased!";
														nextTurnState ();
												}
										}
								} else if (textmanage.scene == "school2" && transitions.currBattle == "lunch") {
										if (getTurnState () == 0) {
												announcerLine = winLine + " It's important to eat well! You feel awesome!";
												nextTurnState ();
										} else if (getTurnState () == 1) {
												if (Input.GetButtonDown ("Fire1")) {
														announcerLine = "Your well-being has increased greatly!";
														nextTurnState ();
												}
										}
								}
								if (transitions.currBattle != "wakeup" && textmanage.scene != "school" && textmanage.scene != "school2") { // To catch extra cases
										nextTurnState ();
										nextTurnState ();
								}
								return true;
						}
				}
				if (playerHP <= 0 || turnsLeft == 0) {
						if (turnsLeft != 0) {
								if (!ending) {
										ending = true;
										turnsLeft = -1;
								}
						}
						if (getTurnState () == 2) {
								if (Input.GetButtonDown ("Fire1")) {
										resetTurnStateMachine ();
										Application.LoadLevel ("dialogue");
										transitions.won = false;
										if (transitions.currBattle == "wakeup") { // Post battle effects for waking up
												transitions.wellbeing = Mathf.Max (.01f, transitions.wellbeing - 0.05f);
										} else if (transitions.currBattle == "lecture") {
												transitions.grades = Mathf.Max (0, transitions.grades - 0.05f);
										} else if (transitions.currBattle == "test") {
												transitions.grades = Mathf.Max (0, transitions.grades - 0.05f);
												transitions.wellbeing = Mathf.Max (.01f, transitions.wellbeing - 0.1f);
										} else if (transitions.currBattle == "gym") {
												transitions.wellbeing = Mathf.Max (.01f, transitions.wellbeing - 0.1f);
										} else if (transitions.currBattle == "studying") {
												transitions.happiness = Mathf.Max (0, transitions.happiness - 0.1f);
										} else if (transitions.currBattle == "lunch") {
												transitions.wellbeing = Mathf.Max (.01f, transitions.wellbeing - 0.1f);
										}
								}
								return true;
						}
			
						if (getTurnState () == 1) {
								if (Input.GetButtonDown ("Fire1")) {
										if (transitions.currBattle == "wakeup") {
												announcerLine = "Your well-being decreased!";
										} else if (transitions.currBattle == "lecture") {
												announcerLine = "Your grades has decreased!";
										} else if (transitions.currBattle == "test") {
												announcerLine = "Your grades has decreased! Your well-being has decreased!";
										} else if (transitions.currBattle == "gym") {
												announcerLine = "Your well-being has decreased!";
										} else if (transitions.currBattle == "studying") {
												announcerLine = "Your happiness has decreased!";
										} else if (transitions.currBattle == "lunch") {
												announcerLine = "Your well-being has decreased!";
										}
										nextTurnState ();
								}
						}
						if (getTurnState () == 0) {
								if (turnsLeft == 0) {
										announcerLine = "Out of time! " + loseLine;
								} else {
										announcerLine = loseLine;
								}
								nextTurnState ();
						}
			
						return true;
						
						

				}
				return false;
		}

		// Update is called once per frame
		void Update ()
		{
		/*GameObject[] enemies1 = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject e in enemies1) {
			e.GetComponent<SpriteRenderer> ().sprite = transitions.nextImage;
		}*/
				GameObject.Find ("bg").GetComponent<SpriteRenderer> ().sprite = transitions.bg;	
				/** Start of battle effects */
				if (!showedSpecialEffect) {
						if (transitions.currBattle == "wakeup") {
								announcerLine = "Shut up that alarm!!";
						} else if (transitions.currBattle == "lecture") {
								announcerLine = "The monotony of the lecture puts you in a tired state... You cannot critical hit!";
								critChance = 0f;
						} else if (textmanage.scene == "school" && transitions.currBattle == "test") {
								turnsLeft = 6;
								announcerLine = "You sure hope you can finish the test within the time limit... Finish the battle in " + turnsLeft + " turns!";
						} else if (textmanage.scene == "school" && transitions.currBattle == "gym") {
								announcerLine = "Ultimately, it all comes down to effort and hard work... Attacks are stronger, Abilities are weakened!";
								attack = 16 + (int)(Mathf.Round (transitions.happiness * 100) / 5 + 0.5);
								DIEAttack = 5 + (int)(Mathf.Round (transitions.happiness * 100) / 4 + 0.5);
								SPLOSIONSAttack = 10 + (int)(Mathf.Round (transitions.happiness * 100) / 2 + 0.5);
						} else if (textmanage.scene == "school2" && transitions.currBattle == "gym") {
								announcerLine = "Ultimately, it all comes down to effort and hard work... Attacks are stronger, Abilities are weakened!";
								attack = 21 + (int)(Mathf.Round (transitions.happiness * 100) / 5 + 0.5);
								DIEAttack = 0 + (int)(Mathf.Round (transitions.happiness * 100) / 4 + 0.5);
								SPLOSIONSAttack = 5 + (int)(Mathf.Round (transitions.happiness * 100) / 2 + 0.5);
						} else if (textmanage.scene == "school2" && transitions.currBattle == "test") {
								turnsLeft = 4;
								announcerLine = "You sure hope you can finish the test within the time limit... Finish the battle in " + turnsLeft + " turns!";
						} else if (textmanage.scene == "school2" && transitions.currBattle == "lunch") {
								announcerLine = "You're really starving. If only you could eat the same time each day... Lose well-being each turn!";
								poison = true;
						} else if (textmanage.scene == "school2" && transitions.currBattle == "studying") {
								announcerLine = "Aw crud, this is your worst subject. More reason to study I guess... Abilities cost more grade to use!";
								DIE_MANA_COST = 15;
								SPLOSIONS_MANA_COST = 25;
						} else {
								showedSpecialEffect = true;
						}
						if (Input.GetButtonDown ("Fire1")) {
								showedSpecialEffect = true;
						}
						return;
				}

				if (whosTurn == 0) {
						if (CheckAllDead ())
								return;
				}
				
				if (whosTurn == 0 && turnState == 2) {
						if (Input.GetButtonDown ("Fire1")) {
								TurnStateMachine.nextTurn ();
								return;
						}
				}
				if (whosTurn == 0 && isCommandTargeting ()) {
						if (Input.GetButtonDown ("Fire1")) {

								RaycastHit2D[] hits = Physics2D.RaycastAll (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
								GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
								foreach (RaycastHit2D ray in hits) {

										foreach (GameObject e in enemies) {
												if (ray.collider == e.GetComponent<BoxCollider2D> ()) {
														if (e.GetComponent<Stats> ().health <= 0) {
																return;
														}
														if (commandSelection == SELECT_TARGET_ATTACK) {		//Do regular attack
																float random = Random.value;
																if (random <= critChance) { // you do critical attack
																		e.GetComponent<Stats> ().health -= attack * 2;
																		announcerLine = attackLine + critSuffix;
																		StartCoroutine (attackedAnimation (e));
																} else if (random <= 1 - missChance) { // you do regular attack
																		e.GetComponent<Stats> ().health -= attack;
																		announcerLine = attackLine;
																		StartCoroutine (attackedAnimation (e));
																} else { // you miss
																		announcerLine = missLine;
																}
																if (poison) {
																		announcerLine += " You lost 5 well-being from hunger!";
																		playerHP = Mathf.Max (1, playerHP - 5);
																}
																nextTurnState ();	
																nextTurnState (); //twice b/c no animation
														} else if (commandSelection == SELECT_TARGET_DIE) {		//Do DIE ability
																float random = Random.value;
																if (random <= critChance) { // you do critical attack
																		e.GetComponent<Stats> ().health -= DIEAttack * 2;
																		announcerLine = DIELine + critSuffix;
																		StartCoroutine (attackedAnimation (e));
																} else if (random <= 1 - missChance) { // you do regular attack
																		e.GetComponent<Stats> ().health -= DIEAttack;
																		announcerLine = DIELine;
																		StartCoroutine (attackedAnimation (e));
																} else { // you miss
																		announcerLine = missLine;
																}
																playerMP -= DIE_MANA_COST;
																commandSelection = SELECT_NONE;
																if (poison) {
																		announcerLine += " You lost 5 well-being from hunger!";
																		playerHP = Mathf.Max (1, playerHP - 5);
																}
																nextTurnState ();	
																nextTurnState (); //twice b/c no animation
														}
												}
										}
								}
								if (commandSelection == SELECT_NONE || commandSelection == SELECT_TARGET_ATTACK) {
										commandSelection = SELECT_NONE;
								} else {
										commandSelection = SELECT_ABILITY;
								}
						}
				}
				//player turn
				//enemy turn
		
		}

		IEnumerator attackedAnimation (GameObject e)
		{
				float BATTLETIMEDELAY = 0.2f;
				int i;
				for (i = 0; i < 6; i++) {
						if (i % 2 == 0) {
								e.renderer.enabled = false;
						} else {
								e.renderer.enabled = true;
						}
						yield return new WaitForSeconds (BATTLETIMEDELAY);
						BATTLETIMEDELAY /= 1.5f;
				}
		}

		/** Returns true if commandSelection is waiting to choose a target. */
		public static bool isCommandTargeting ()
		{
				return commandSelection > 100;
		}
	
		public static int getTurn ()
		{
				return whosTurn;
		}
	
		public static void nextTurn ()
		{
				if (whosTurn == 0 && turnsLeft >= 0) {
						turnsLeft -= 1;
				}
				whosTurn++;
				if (whosTurn > numEnemies) {
						whosTurn = 0;
				}
				turnState = 0;
		}

		public static int getTurnState ()
		{
				return turnState;
		}

		public static void nextTurnState ()
		{
				turnState++;
				if (turnState > 2) {
						nextTurn ();
				}
		}

		public static void resetTurnStateMachine ()
		{
				TurnStateMachine.numEnemies = 0;
				whosTurn = 0;
				turnState = 0;
				announcerLine = "";
				ending = false;
				showedSpecialEffect = false;
				critChance = 0.0625f;
				turnsLeft = -1;
				attack = 6 + (int)(Mathf.Round (transitions.happiness * 100) / 5 + 0.5);
				DIEAttack = 15 + (int)(Mathf.Round (transitions.happiness * 100) / 4 + 0.5);
				SPLOSIONSAttack = 20 + (int)(Mathf.Round (transitions.happiness * 100) / 2 + 0.5);
				DIE_MANA_COST = 5;
				SPLOSIONS_MANA_COST = 15;
				poison = false;
		}
	
		public static void setAnnouncerLine (string s)
		{
				announcerLine = s;
		}

		public void castSPLOSIONS ()
		{
				playerMP -= SPLOSIONS_MANA_COST;
				float random = Random.value;

				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
				foreach (GameObject e in enemies) {
						if (random <= critChance) {
								e.GetComponent<Stats> ().health -= SPLOSIONSAttack * 2;
								announcerLine = SPLOSIONSLine + critSuffix;
								StartCoroutine ("attackedAnimation", e);
						} else if (random <= 1 - missChance) {
								e.GetComponent<Stats> ().health -= SPLOSIONSAttack;
								announcerLine = SPLOSIONSLine;
								StartCoroutine ("attackedAnimation", e);
						} else {
								announcerLine = missLine;
						}
				}
				if (poison) {
						announcerLine += " You lost 5 well-being from hunger!";
						playerHP = Mathf.Max (1, playerHP - 5);
				}
				nextTurnState ();	
				nextTurnState (); //twice b/c no animation
				commandSelection = SELECT_NONE;
		}
}

