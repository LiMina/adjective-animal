using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour
{

		public float critChance = (float)0.0625;
		public float missChance = (float)0.0625;
		public float camDuration = 0.3f;
		public float camMagnitude = 0.3f;
		public int ID = 2;
		public bool selectedAttack = false;
		// Use this for initialization
		void Start ()
		{
				if (transitions.enemyCount != 1) {
						TurnStateMachine.numEnemies++;
						ID = TurnStateMachine.numEnemies;
				} else {
						TurnStateMachine.numEnemies = 1;
						ID = TurnStateMachine.numEnemies;
				}

<<<<<<< HEAD
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (TurnStateMachine.getTurn () == ID && TurnStateMachine.getTurnState () == 0) {
						float random = Random.value;
						if (this.GetComponent<Stats> ().health > 0) {
								if (random <= critChance) {
										TurnStateMachine.playerHP -= this.GetComponent<Stats> ().attack * 2;
										TurnStateMachine.setAnnouncerLine ("The enemy attacks! Your well-being takes a hit of " + (this.GetComponent<Stats> ().attack * 2) + " points! A critical hit!");
										StartCoroutine (Shake ());
								} else if (random <= 1 - missChance) {
										TurnStateMachine.playerHP -= this.GetComponent<Stats> ().attack;
										TurnStateMachine.setAnnouncerLine ("The enemy attacks! Your well-being takes a hit of " + this.GetComponent<Stats> ().attack + " points!");
										StartCoroutine (Shake ());
								} else {
										TurnStateMachine.setAnnouncerLine ("The enemy's attack missed!");
								}
						}
						TurnStateMachine.nextTurnState ();
						TurnStateMachine.nextTurnState (); //twice b/c no animation yet.
						//TurnStateMachine.nextTurn ();
				}
=======
	public float camDuration = 0.5f;
	public float camMagnitude = 0.1f;
	
	public int ID = 2;
	public bool selectedAttack = false;

	private float timeSinceShake = 0;
	private int currentAttackDamage = 0;

	// Use this for initialization
	void Start () {
		TurnStateMachine.numEnemies++;
		ID = TurnStateMachine.numEnemies;
	}
	
	// Update is called once per frame
	void Update () {
		timeSinceShake += Time.deltaTime;
		if (TurnStateMachine.getTurn () == ID && TurnStateMachine.getTurnState () == 0) {
			float random = Random.value;
			if (this.GetComponent<Stats>().health > 0) {
				if (random <= critChance) {
					TurnStateMachine.setAnnouncerLine("");
					StartCoroutine( Shake ());
					currentAttackDamage = this.GetComponent<Stats>().attack * 2;
					timeSinceShake = 0;
				} else if (random <= 1 - missChance) {
					TurnStateMachine.setAnnouncerLine("");
					StartCoroutine( Shake ());
					currentAttackDamage = this.GetComponent<Stats>().attack;
					timeSinceShake = 0;
				} else {
					TurnStateMachine.setAnnouncerLine("The enemy's attack missed!");
					TurnStateMachine.nextTurnState ();
				}
			}
			TurnStateMachine.nextTurnState ();
		}
		if (TurnStateMachine.getTurn () == ID && TurnStateMachine.getTurnState () == 1 && timeSinceShake > 0.5) {
			TurnStateMachine.playerHP -= currentAttackDamage;
			if (currentAttackDamage == this.GetComponent<Stats>().attack * 2) {
				TurnStateMachine.setAnnouncerLine("The enemy attacks! Your well-being takes a hit of " + currentAttackDamage + " points! A critical hit!");
			} else if (currentAttackDamage == this.GetComponent<Stats>().attack) {
				TurnStateMachine.setAnnouncerLine("The enemy attacks! Your well-being takes a hit of " + this.GetComponent<Stats>().attack + " points!");
			}
			TurnStateMachine.nextTurnState ();
>>>>>>> baa7a1f6de228f3582e3cab6fc3214f504ee2b1e
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
