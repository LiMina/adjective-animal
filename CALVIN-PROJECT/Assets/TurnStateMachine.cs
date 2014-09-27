using UnityEngine;
using System.Collections;

public class TurnStateMachine : MonoBehaviour {
	//Probably just true, like, all the time
	bool battle = true;
	/**
	 * Who's turn it is.
	 * 0 = Player's turn
	 * 1 = First enemy's turn
	 * 2 = Second enemy's turn
	 * ...
	 * n = n-th enemy's turn
	 **/
	public static int whosTurn = 0;
	public static int numEnemies = 1;
	/**
	 * What action is currently selected. e.g. this changes after you press the attack button and you're choosing a target.
	 * 0 = No selection
	 * 1 = attack
	 **/
	public static int commandSelection = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		while (battle) {
			//player turn
			//enemy turn
		}
	
	}

	public static int getTurn() { return whosTurn; }

	public static void nextTurn() {
		whosTurn++;
		if (whosTurn > numEnemies)
			whosTurn = 0;
	}
}
