using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	int ID = 0;
	int actionSelect = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (TurnStateMachine.getTurn () == ID) {
			if (ID == 0) {
				if (Input.GetButtonDown ("Fire1")) {
					RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
					foreach (RaycastHit2D ray in hits) {
					}
				}
			}
		}

		/**
		if (Input.GetButtonDown ("Fire1")) {
			RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
			foreach (RaycastHit2D ray in hits) {
				if (ray.collider == transform.parent.GetComponent<BoxCollider2D>()) {
					if (TurnStateMachine.whosTurn == 0) {
						health -= 10;
						TurnStateMachine.nextTurn();
					}
				}
			}
		}
		if (TurnStateMachine.whosTurn == 1) {
			float random = Random.value;
			if (random < 0.5) {
				Stats.health -= 3;
				TurnStateMachine.nextTurn ();
			} else {
				Stats.health -= 7;
				TurnStateMachine.nextTurn ();
			}
		}
		*/
	}
}
