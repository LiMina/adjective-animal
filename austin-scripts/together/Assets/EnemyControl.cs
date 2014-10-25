using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour
{
	public float critChance = 0.0625f;
	public float missChance = 0.0625f;
	public float camDuration = 0.5f;
	public float camMagnitude = 0.1f;
	
	public int ID = 2;
	public bool selectedAttack = false;

	private float timeSinceShake = 0;
	private int currentAttackDamage = 0;

	private bool charge = false;
	private int turnsUntilHeal = -1;

	// Use this for initialization
	void Start () {
		if (transitions.enemyCount != 1) {
			TurnStateMachine.numEnemies++;
			ID = TurnStateMachine.numEnemies;
		} else {
			TurnStateMachine.numEnemies = 1;
			ID = TurnStateMachine.numEnemies;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (textmanage.scene == "afterschool" && transitions.currBattle == "club") {
			critChance = 0.5f;
			missChance = 0.5f;
		} else {
			critChance = 0.0625f;
			missChance = 0.0625f;
		}
		/** First check if I'm dead. */
		if (TurnStateMachine.getTurn () == ID && this.GetComponent<Stats> ().health <= 0) {
			TurnStateMachine.nextTurn ();
			return;
		}

		if (transitions.currBattle == "party") {
			if (turnsUntilHeal < 0) {
				turnsUntilHeal = 3;
			} else if (turnsUntilHeal == 0) {
				this.GetComponent<Stats>().health = Mathf.Min(this.GetComponent<Stats>().health + 20, this.GetComponent<Stats>().maxHealth);
				turnsUntilHeal = 3;
			}
		}

		if (transitions.currBattle == "wakeup") {
			if (TurnStateMachine.getTurn () == ID && TurnStateMachine.getTurnState () == 0 && !charge) {
				TurnStateMachine.setAnnouncerLine ("The enemy hits snooze and is preparing to attack!");
				TurnStateMachine.nextTurnState ();
				TurnStateMachine.nextTurnState ();
				return;
			} else if (TurnStateMachine.getTurn () == ID && TurnStateMachine.getTurnState () == 2 && !charge) {
				if (Input.GetButtonDown ("Fire1")) {
					charge = true;
					TurnStateMachine.nextTurnState ();
				}
				return;
			}
		}
		timeSinceShake += Time.deltaTime;
		if (TurnStateMachine.getTurn () == ID && TurnStateMachine.getTurnState () == 0) {
			float random = Random.value;
			if (this.GetComponent<Stats> ().health > 0) {
				if (random <= critChance) {
					TurnStateMachine.setAnnouncerLine ("The enemy attacks!");
					StartCoroutine (Shake ());
					currentAttackDamage = this.GetComponent<Stats> ().attack * 2;
					timeSinceShake = 0;
				} else if (random <= 1 - missChance) {
					TurnStateMachine.setAnnouncerLine ("The enemy attacks!");
					StartCoroutine (Shake ());
					currentAttackDamage = this.GetComponent<Stats> ().attack;
					timeSinceShake = 0;
				} else {
					TurnStateMachine.setAnnouncerLine ("The enemy's attack missed!");
					TurnStateMachine.nextTurnState ();
				}
				if (transitions.currBattle == "party") {
					turnsUntilHeal--;
				}
			}
			TurnStateMachine.nextTurnState ();
		} else if (TurnStateMachine.getTurn () == ID && TurnStateMachine.getTurnState () == 1 && timeSinceShake > 0.5) {
			if (Input.GetButtonDown ("Fire1")) {			
				TurnStateMachine.playerHP -= currentAttackDamage;
				if (currentAttackDamage == this.GetComponent<Stats> ().attack * 2) {
					TurnStateMachine.setAnnouncerLine ("A critical hit! Your well-being takes a hit of " + currentAttackDamage + " points!");
				} else if (currentAttackDamage == this.GetComponent<Stats> ().attack) {
					TurnStateMachine.setAnnouncerLine ("Your well-being takes a hit of " + this.GetComponent<Stats> ().attack + " points!");
				}
				TurnStateMachine.nextTurnState ();
			}
		} else if (TurnStateMachine.getTurn () == ID && TurnStateMachine.getTurnState () == 2) {
			if (Input.GetButtonDown ("Fire1")) {
				TurnStateMachine.nextTurnState();
				charge = false;
			}
		}
	}

		void OnMouseEnter ()
		{
				string highlight = transitions.nextImage.name + "_hl";
				print (highlight);
				if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_TARGET_ATTACK || TurnStateMachine.commandSelection == TurnStateMachine.SELECT_TARGET_DIE) {
						this.gameObject.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> (highlight);
				}
		}

		void OnMouseExit ()
		{
				this.gameObject.GetComponent<SpriteRenderer> ().sprite = transitions.nextImage;
		}

		IEnumerator Shake ()
		{
				float elapsed = 0.0f;
				Vector3 originalCamPos = Camera.main.transform.position;
				while (elapsed < camDuration) {
						elapsed += Time.deltaTime;

						float percentComplete = elapsed / camDuration;
						float damper = 1.0f - Mathf.Clamp (4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

						float x = Random.value * 2.0f - 1.0f;
						float y = Random.value * 2.0f - 1.0f;
						x *= camMagnitude * damper;
						y *= camMagnitude * damper;

						Camera.main.transform.position = new Vector3 (x, y, originalCamPos.z);

						yield return null;
				}
				Camera.main.transform.position = originalCamPos;
		}
}
