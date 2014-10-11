using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	
	public int ID;
	
	// Use this for initialization
	void Start () {
		TurnStateMachine.numEnemies++;
		ID = TurnStateMachine.numEnemies;
	}
	
	// Update is called once per frame
	void Update () {
		if (TurnStateMachine.getTurn () == ID) {
			if (this.GetComponent<Stats>().health > 0) {
				TurnStateMachine.playerHP -= this.GetComponent<Stats>().attack;
			}
			TurnStateMachine.nextTurn ();
		}
	}
}
