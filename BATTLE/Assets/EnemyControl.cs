using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	
	public int ID = 1;
	int actionSelect = 0;
	
	// Use this for initialization
	void Start () {
		TurnStateMachine.numEnemies++;
	}
	
	// Update is called once per frame
	void Update () {
		if (TurnStateMachine.getTurn () == ID) {
			TurnStateMachine.playerHP -= 5;
			TurnStateMachine.nextTurn ();
		}
	}
}
