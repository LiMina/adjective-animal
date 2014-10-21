using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {
	
	public int ID;
	public bool selectedAttack = false;
	// Use this for initialization
	void Start () {
		TurnStateMachine.numEnemies++;
		ID = TurnStateMachine.numEnemies;
	}
	
	// Update is called once per frame
	void Update () {
		if (TurnStateMachine.getTurn () == ID && TurnStateMachine.getTurnState () == 0) {
			if (this.GetComponent<Stats>().health > 0) {
				TurnStateMachine.playerHP -= this.GetComponent<Stats>().attack;
			}
			TurnStateMachine.setAnnouncerLine("Your well-being takes a hit of " + this.GetComponent<Stats>().attack + " points!");
			TurnStateMachine.nextTurnState ();
			TurnStateMachine.nextTurnState (); //twice b/c no animation yet.
			//TurnStateMachine.nextTurn ();
		}
	}
	void OnMouseEnter() {
		string highlight = transitions.nextImage.name + "_hl";
		print (highlight);
		if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_TARGET_ATTACK || TurnStateMachine.commandSelection == TurnStateMachine.SELECT_TARGET_DIE ) {
						this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (highlight);
				}
	}
	void OnMouseExit(){
		this.gameObject.GetComponent<SpriteRenderer>().sprite = transitions.nextImage;
	}
}
