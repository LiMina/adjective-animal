using UnityEngine;
using System.Collections;

public class TurnStateMachine : MonoBehaviour {
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

	/**
	 * Who's turn it is.
	 * 0 = Player's turn
	 * 1 = First enemy's turn
	 * 2 = Second enemy's turn
	 * ...
	 * n = n-th enemy's turn
	 **/
	private static int whosTurn = 0;
	public static int numEnemies = 0;
	
	public static int playerHP = 100;
	public static int playerMP = 25;
	public int deadEnemyCounter = 0;
	
	// Use this for initialization
	void Start () {
		
	}

	void CheckAllDead(){
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		deadEnemyCounter = 0;
		foreach (GameObject e in enemies) {
			if(e.GetComponent<Stats>().health <=0){
				deadEnemyCounter++;
			}
			if(deadEnemyCounter == numEnemies){ // THEY'RE ALL DEAD AHHHHHHH
				TurnStateMachine.numEnemies = 0;
				Application.LoadLevel ("dialogue");
				transitions.won = true;

			}
		}
		if (playerHP <= 0) {
			TurnStateMachine.numEnemies = 0;
			Application.LoadLevel ("dialogue");
			transitions.won = false;

		}
	}

	// Update is called once per frame
	void Update () {
		CheckAllDead ();
		Debug.Log ("asdf " + commandSelection);
		Debug.Log ("whose turn? " + whosTurn);
		if (whosTurn == 0 && isCommandTargeting ()) {
			if (Input.GetButtonDown ("Fire1")) {

				RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
				foreach (RaycastHit2D ray in hits) {

					foreach (GameObject e in enemies) {
						if (ray.collider == e.GetComponent<BoxCollider2D>()) {
							if (e.GetComponent<Stats>().health <= 0) {
								return;
							}
							if (commandSelection == SELECT_TARGET_ATTACK) {		//Do regular attack
								e.GetComponent<Stats>().health -= 15;
								Debug.Log (e.GetComponent<EnemyControl>().ID + " " + e.GetComponent<Stats>().health);
								TurnStateMachine.nextTurn ();
								Debug.Log ("player hp " + playerHP);
							} else if (commandSelection == SELECT_TARGET_DIE) {		//Do DIE ability
								e.GetComponent<Stats>().health -= 30;
								playerMP -= 5;
								commandSelection = SELECT_NONE;
								TurnStateMachine.nextTurn ();
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
	public static bool isCommandTargeting() { return commandSelection > 100; }
	
	public static int getTurn() { return whosTurn; }
	
	public static void nextTurn() {
		whosTurn++;
		if (whosTurn > numEnemies)
			whosTurn = 0;
	}

	public static void castSPLOSIONS() {
		playerMP -= 10;
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject e in enemies) {
			e.GetComponent<Stats>().health -= 20;
		}
		commandSelection = SELECT_NONE;
		TurnStateMachine.nextTurn ();
	}
}

