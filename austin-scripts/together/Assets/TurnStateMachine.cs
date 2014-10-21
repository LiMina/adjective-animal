using UnityEngine;
using System.Collections;

public class TurnStateMachine : MonoBehaviour
{
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

	public static readonly int DIE_MANA_COST = 25;
	public static readonly int SPLOSIONS_MANA_COST = 50;

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
		public static string attackLine = "You tackled the problem at hand for 15 damage!";
		public static string DIELine = "You sipped your coffee, jolting your tiredness awake for 30 damage!";
		public static string SPLOSIONSLine = "A cold shower snaps 25 damage at all your tiredness!";
		public static string winLine = "You overcame your adversaries!";
		public static string loseLine = "You feel terribly overwhelmed. You couldn't handle the pressure...";
	
		// Use this for initialization
		void Start ()
		{
		playerHP = Mathf.CeilToInt(transitions.wellbeing * 100);
		playerMP = Mathf.CeilToInt(transitions.grades * 100);
				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
				foreach (GameObject e in enemies) {
						e.GetComponent<SpriteRenderer> ().sprite = transitions.nextImage;
				}
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
									if (Input.GetButtonDown("Fire1")) {
											TurnStateMachine.numEnemies = 0;
											whosTurn = 0;
											turnState = 0;
											announcerLine = "";
											transitions.wellbeing = ((float)(playerHP)) / 100f;
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
				print (transitions.nextImage);
				if (CheckAllDead ()) {
						return;
				}
				
				if (turnState == 2) {
						if (Input.GetButtonDown("Fire1")) {
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
																e.GetComponent<Stats> ().health -= 15;
																Debug.Log (e.GetComponent<EnemyControl> ().ID + " " + e.GetComponent<Stats> ().health);

																announcerLine = attackLine;
																nextTurnState ();	
																nextTurnState (); //twice b/c no animation
																//TurnStateMachine.nextTurn ();
														} else if (commandSelection == SELECT_TARGET_DIE) {		//Do DIE ability
																e.GetComponent<Stats> ().health -= 30;
																playerMP -= DIE_MANA_COST;
																commandSelection = SELECT_NONE;

																announcerLine = DIELine;
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
				GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
				foreach (GameObject e in enemies) {
						e.GetComponent<Stats> ().health -= 25;
				}

				announcerLine = SPLOSIONSLine;
				nextTurnState ();	
				nextTurnState (); //twice b/c no animation
				commandSelection = SELECT_NONE;
				//TurnStateMachine.nextTurn ();
		}
}

