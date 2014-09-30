using UnityEngine;
using System.Collections;

public class TurnStateMachine : MonoBehaviour {
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
	/**
	 * What action is currently selected. e.g. this changes after you press the attack button and you're choosing a target.
	 * 0 = No selection
	 * 1 = attack
	 **/
	public static int commandSelection = 0;
	
	public static int playerHP = 100;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (whosTurn == 0 && commandSelection == 1) {
			if (Input.GetButtonDown ("Fire1")) {

				RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
				foreach (RaycastHit2D ray in hits) {
					foreach (GameObject e in enemies) {
						if (ray.collider == e.GetComponent<BoxCollider2D>()) {
							if (e.GetComponent<Stats>().health <= 0) {
								return;
							}
							e.GetComponent<Stats>().health -= 15;
							Debug.Log (e.GetComponent<EnemyControl>().ID + " " + e.GetComponent<Stats>().health);
							TurnStateMachine.nextTurn ();
							Debug.Log ("player hp " + playerHP);
						}
				    }
				}

				commandSelection = 0;
			}
		}
		//player turn
		//enemy turn
		
	}
	
	public static int getTurn() { return whosTurn; }
	
	public static void nextTurn() {
		whosTurn++;
		if (whosTurn > numEnemies)
			whosTurn = 0;
	}
}

