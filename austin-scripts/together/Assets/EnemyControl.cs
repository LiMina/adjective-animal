using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	public float critChance = (float) 0.0625;
	public float missChance = (float) 0.0625;

	public float camDuration = 0.3f;
	public float camMagnitude = 0.3f;
	
	public int ID = 2;
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
					StartCoroutine( Shake ());
				} else if (random <= 1 - missChance) {
					TurnStateMachine.playerHP -= this.GetComponent<Stats>().attack;
					TurnStateMachine.setAnnouncerLine("The enemy attacks! Your well-being takes a hit of " + this.GetComponent<Stats>().attack + " points!");
					StartCoroutine( Shake ());
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

	IEnumerator Shake() {
		float elapsed = 0.0f;
		Vector3 originalCamPos = Camera.main.transform.position;
		while (elapsed < camDuration) {
			elapsed += Time.deltaTime;

			float percentComplete = elapsed / camDuration;
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= camMagnitude * damper;
			y *= camMagnitude * damper;

			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

			yield return null;
		}
		Camera.main.transform.position = originalCamPos;
	}
}
