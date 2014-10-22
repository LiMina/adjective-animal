using UnityEngine;
using System.Collections;

public class DescriptionBox : MonoBehaviour
{

		public string BLANK_DESC = "";
		public static string ATTACK_DESC = "Tackle the problem at hand.";
		public static string DIE_DESC = "Invigorate yourself with a cuppa joe. Deals high damage to a single target.\nRequires a grade of 25.";
		public string ABILITY_DESC = "Perform a special action only available to the studious.";
		public static string SPLOSIONS_DESC = "An icy shower strikes down lethargy. Deals moderate damage to all enemies.\nRequires a grade of 50.";
		public string BACK_DESC = "Plot twist: a moment of indecision!";
		public string currentDesc = "";
		public GUIStyle styler;
		public Texture2D texture;

		// Use this for initialization
		void Start ()
		{
				GetComponent<MeshRenderer> ().enabled = false;
		}

		// Update is called once per frame
		void Update ()
		{
				// If selecting a target, show the description for the attack at hand.
				if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_TARGET_ATTACK) {
						currentDesc = ATTACK_DESC;
						return;
				} else if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_TARGET_DIE) {
						currentDesc = DIE_DESC;
						return;
				}
				// If hovering over a menu button
				RaycastHit2D[] hits = Physics2D.RaycastAll (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
				foreach (RaycastHit2D ray in hits) {
						if (ray.collider == GetComponent<BoxCollider2D> ()) {
								// If hovering over the top button:
								if (new Rect (50 + (Screen.width - (Screen.width / 8) - 100) / 3 + Screen.width / 16 + 20,
				              Screen.height - 180,
				              (Screen.width - (Screen.width / 8) - 100) / 3 - 40,
				              25).Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))) {
										if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_NONE) {
												currentDesc = ATTACK_DESC;
												return;
										} else if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_ABILITY) {
												currentDesc = DIE_DESC;
												return;
										}
										// If hovering over the 2nd button:
								} else if (new Rect (50 + (Screen.width - (Screen.width / 8) - 100) / 3 + Screen.width / 16 + 20,
				                     Screen.height - 150,
				                     (Screen.width - (Screen.width / 8) - 100) / 3 - 40,
				                     25).Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))) {
										if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_NONE) {
												currentDesc = ABILITY_DESC;
												return;
										} else if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_ABILITY) {
												currentDesc = SPLOSIONS_DESC;
												return;
										}
								} else if (new Rect (50 + (Screen.width - (Screen.width / 8) - 100) / 3 + Screen.width / 16 + 20,
				                     Screen.height - 90,
				                     (Screen.width - (Screen.width / 8) - 100) / 3 - 40,
				                     25).Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))) {
										if (TurnStateMachine.commandSelection == TurnStateMachine.SELECT_ABILITY) {
												currentDesc = BACK_DESC;
												return;
										}
								}
						}
				}
				currentDesc = BLANK_DESC;
		}

		void OnGUI ()
		{
				int cameraOffsetX = (int) (Camera.main.transform.position.x * Screen.width / 10);
				int cameraOffsetY = (int) (Camera.main.transform.position.y * Screen.height / 10);

				texture = new Texture2D (16, 16);
				for (int y = 0; y < texture.height; ++y) {
						for (int x = 0; x < texture.width; ++x) {
								if ((x > 2 && y > 2) && (x < texture.width - 3 && y < texture.height - 3)) {
										Color color = new Color (228f / 255f, 174f / 255f, 198f / 255f, 1f);
										texture.SetPixel (x, y, color);
								} else {
										Color color = new Color (228f / 255f, 200f / 255f, 213f / 255f, 1f);
										texture.SetPixel (x, y, color);
								}
						}
				}
				texture.Apply ();
		
				styler = new GUIStyle (GUI.skin.box);
				styler.normal.textColor = Color.white;
				styler.fontSize = 18;
				styler.normal.background = texture;
				GUI.skin.box.wordWrap = true;
				GUI.Box (new Rect (50 + 2 * (Screen.width - (Screen.width / 8) - 100) / 3 + Screen.width / 8 + cameraOffsetX, Screen.height - 200 + cameraOffsetY, (Screen.width - (Screen.width / 8) - 100) / 3, 150), currentDesc, styler);
		}
}
