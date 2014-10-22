using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	public float critChance = (float) 0.0625;
	public float missChance = (float) 0.0625;
	
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
			float random = Random.value;
			if (this.GetComponent<Stats>().health > 0) {
				if (random <= critChance) {
					TurnStateMachine.playerHP -= this.GetComponent<Stats>().attack * 2;
					TurnStateMachine.setAnnouncerLine("The enemy attacks! Your well-being takes a hit of " + (this.GetComponent<Stats>().attack * 2) + " points! A critical hit!");
				} else if (random <= 1 - missChance) {
					TurnStateMachine.playerHP -= this.GetComponent<Stats>().attack;
					TurnStateMachine.setAnnouncerLine("The enemy attacks! Your well-being takes a hit of " + this.GetComponent<Stats>().attack + " points!");
				} else {
					TurnStateMachine.setAnnouncerLine("The enemy's attack missed!");
				}
			}
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
