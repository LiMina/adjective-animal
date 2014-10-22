using UnityEngine;
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
		public static readonly int DIE_MANA_COST = 5;
		public static readonly int SPLOSIONS_MANA_COST = 10;
		public static int attack = 10;
		public static int DIEAttack = 10 + (int)(transitions.happiness / 2 + 0.5);
		public static int SPLOSIONSAttack = 10 + (int)(transitions.happiness / 4 + 0.5);

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
	
		// Use this for initialization
		void Start ()
		{
				playerHP = Mathf.RoundToInt (transitions.wellbeing * 100);
				playerMP = Mathf.RoundToInt (transitions.grades * 100);
				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
				GameObject secondenemy = GameObject.Find ("Enemy2");
				GameObject firstenemy = GameObject.Find ("Enemy");
				if (transitions.enemyCount == 2) {
						foreach (GameObject e in enemies) {
								print ("more than one enemy");
								e.SetActive (true);
						}
						//secondenemy 
						secondenemy.transform.position = new Vector3 (-3f, 1.2f, 0f);
						secondenemy.transform.localScale = new Vector3 (0.4f, 0.4f, 0.4f);
						firstenemy.transform.position = new Vector3 (3f, 1.2f, 0f);
						
				} 
				if (transitions.enemyCount == 1) {
						firstenemy.transform.position = new Vector3 (0, 1.2f, 0f);
						secondenemy.SetActive (false);
				}
				foreach (GameObject e in enemies) {
						e.GetComponent<SpriteRenderer> ().sprite = transitions.nextImage;
				}

				DIEAttack = 10 + (int)(Mathf.Round (transitions.happiness) / 2 + 0.5);
				SPLOSIONSAttack = 10 + (int)(Mathf.Round (transitions.happiness) / 4 + 0.5);
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
								announcerLine = winLine;
								if (getTurnState () == 2) {
										if (Input.GetButtonDown ("Fire1")) {
												TurnStateMachine.numEnemies = 0;
												whosTurn = 0;
												turnState = 0;
												announcerLine = "";
												Application.LoadLevel ("dialogue");
												transitions.won = true;
										}
										return true;
								}
								nextTurnState ();
								nextTurnState ();
								return true;
						}
				}
				if (playerHP <= 0) {
						announcerLine = loseLine;
						if (getTurnState () == 2) {
								if (Input.GetButtonDown ("Fire1")) {
										TurnStateMachine.numEnemies = 0;
										whosTurn = 0;
										turnState = 0;
										announcerLine = "";
										Application.LoadLevel ("dialogue");
										transitions.won = false;
								}
								return true;
						}
						nextTurnState ();
						nextTurnState ();
						return true;
						
						

				}
				return false;
		}

		// Update is called once per frame
		void Update ()
		{
				print (numEnemies);
				if (CheckAllDead ()) {
						return;
				}
				
				if (turnState == 2) {
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
																if (random <= critChance) {
																		e.GetComponent<Stats> ().health -= attack * 2;
																		announcerLine = attackLine + critSuffix;
																} else if (random <= 1 - missChance) {
																		e.GetComponent<Stats> ().health -= attack;
																		announcerLine = attackLine;
																} else {
																		announcerLine = missLine;
																}
																nextTurnState ();	
																nextTurnState (); //twice b/c no animation
																//TurnStateMachine.nextTurn ();
														} else if (commandSelection == SELECT_TARGET_DIE) {		//Do DIE ability
																float random = Random.value;
																if (random <= critChance) {
																		e.GetComponent<Stats> ().health -= DIEAttack * 2;
																		announcerLine = DIELine + critSuffix;
																} else if (random <= 1 - missChance) {
																		e.GetComponent<Stats> ().health -= DIEAttack;
																		announcerLine = DIELine;
																} else {
																		announcerLine = missLine;
																}
																playerMP -= DIE_MANA_COST;
																commandSelection = SELECT_NONE;

																nextTurnState ();	
																nextTurnState (); //twice b/c no animation
																//TurnStateMachine.nextTurn ();
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

		public static void setAnnouncerLine (string s)
		{
				announcerLine = s;
		}

		public static void castSPLOSIONS ()
		{
				playerMP -= SPLOSIONS_MANA_COST;
				float random = Random.value;

				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
				foreach (GameObject e in enemies) {
						if (random <= critChance) {
								e.GetComponent<Stats> ().health -= SPLOSIONSAttack * 2;
								announcerLine = SPLOSIONSLine + critSuffix;
						} else if (random <= 1 - missChance) {
								e.GetComponent<Stats> ().health -= SPLOSIONSAttack;
								announcerLine = SPLOSIONSLine;
						} else {
								announcerLine = missLine;
						}
				}

				nextTurnState ();	
				nextTurnState (); //twice b/c no animation
				commandSelection = SELECT_NONE;
				//TurnStateMachine.nextTurn ();
		}
}

