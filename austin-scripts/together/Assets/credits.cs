using UnityEngine;
using System.Collections;

public class credits : MonoBehaviour {
	public int fontSize = 18;
	public GUIStyle promptStyler;
	public Texture2D promptTexture;
	public GUIStyle styler;
	public Texture2D texture;
	public GUIStyle buttonStyler;
	public Texture2D buttonTexture;
	public Texture2D buttonHoverTexture;
	void Start () {
		promptTexture = new Texture2D (Screen.width - 50, 50);
		for (int y = 0; y < promptTexture.height; ++y) {
			for (int x = 0; x < promptTexture.width; ++x) {
				if ((x > 2 && y > 2) && (x < promptTexture.width - 3 && y < promptTexture.height - 3)) {
					Color color = new Color (228f / 255f, 125f / 255f, 171f / 255f, 1f);
					promptTexture.SetPixel (x, y, color);
				} else {
					Color color = new Color (228f / 255f, 200f / 255f, 213f / 255f, 1f);
					promptTexture.SetPixel (x, y, color);
				}
			}
		}

		texture = new Texture2D (128, 128);
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

		buttonTexture = new Texture2D (Screen.width - 200, 25);
		for (int y = 0; y < buttonTexture.height; ++y) {
			for (int x = 0; x < buttonTexture.width; ++x) {
				if ((x > 1 && y > 1) && (x < buttonTexture.width - 2 && y < buttonTexture.height - 2)) {
					Color color = new Color (245f / 255f, 207f / 255f, 148f / 255f, 1f);
					buttonTexture.SetPixel (x, y, color);
				} else {
					Color color = new Color (254f / 255f, 234f / 255f, 174f / 255f, 1f);
					buttonTexture.SetPixel (x, y, color);
				}
			}
		}

		buttonHoverTexture = new Texture2D (Screen.width - 200, 25);
		for (int y = 0; y < buttonHoverTexture.height; ++y) {
			for (int x = 0; x < buttonHoverTexture.width; ++x) {
				if ((x > 1 && y > 1) && (x < buttonHoverTexture.width - 2 && y < buttonHoverTexture.height - 2)) {
					Color color = new Color (254f / 255f, 234f / 255f, 174f / 255f, 1f);
					buttonHoverTexture.SetPixel (x, y, color);
				} else {
					Color color = new Color (254f / 255f, 234f / 255f, 174f / 255f, 1f);
					buttonHoverTexture.SetPixel (x, y, color);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.backgroundColor = new Color (163f / 255f, 203f / 255f, 204f / 255f, 1f);
	}

	void OnGUI () {
		//transitions.happiness=.20f;
		//transitions.grades=.20f;
		//transitions.wellbeing=.5f;
		transitions.nextImage = null;
		//transitions.won = false;
		transitions.outOfBattle = true;
		transitions.nextState = 0;
		transitions.nextPath = 0;
		transitions.nextScene = "room";
		transitions.lunch = false;
		transitions.classesTaken = 0;
		transitions.extra = 0;
		transitions.currBattle = "wakeup";
		transitions.enemyCount = 1;
		transitions.bg = Resources.Load<Sprite> ("processed_bedroom2"); 
		int cameraOffsetX = (int)(Camera.main.transform.position.x * Screen.width / 10);
		int cameraOffsetY = (int)(Camera.main.transform.position.y * Screen.height / 20);
		/* For dialogue */
		/** TEXTURE INITIALIZATION
		promptTexture = new Texture2D (Screen.width - 50, 50);
		for (int y = 0; y < promptTexture.height; ++y) {
			for (int x = 0; x < promptTexture.width; ++x) {
				if ((x > 2 && y > 2) && (x < promptTexture.width - 3 && y < promptTexture.height - 3)) {
					Color color = new Color (228f / 255f, 125f / 255f, 171f / 255f, 1f);
					promptTexture.SetPixel (x, y, color);
				} else {
					Color color = new Color (228f / 255f, 200f / 255f, 213f / 255f, 1f);
					promptTexture.SetPixel (x, y, color);
				}
			}
		} */
		promptTexture.Apply ();
		
		promptStyler = new GUIStyle (GUI.skin.box);
		promptStyler.normal.textColor = Color.black;
		promptStyler.fontSize = 18;
		promptStyler.normal.background = promptTexture;

		GUI.skin.box.fontSize = fontSize;
		GUI.skin.button.fontSize = fontSize;
		GUI.skin.box.wordWrap = true;
		/** TEXTURE INITIALIZATION
		texture = new Texture2D (128, 128);
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
		} */
		texture.Apply ();
		
		styler = new GUIStyle (GUI.skin.box);
		styler.normal.textColor = Color.black;
		styler.normal.background = texture;
		
		/* For buttons */
		/** TEXTURE INITIALIZATION
		buttonTexture = new Texture2D (Screen.width - 200, 25);
		for (int y = 0; y < buttonTexture.height; ++y) {
			for (int x = 0; x < buttonTexture.width; ++x) {
				if ((x > 1 && y > 1) && (x < buttonTexture.width - 2 && y < buttonTexture.height - 2)) {
					Color color = new Color (245f / 255f, 207f / 255f, 148f / 255f, 1f);
					buttonTexture.SetPixel (x, y, color);
				} else {
					Color color = new Color (254f / 255f, 234f / 255f, 174f / 255f, 1f);
					buttonTexture.SetPixel (x, y, color);
				}
			}
		} */
		buttonTexture.Apply ();
		/** TEXTURE INITIALIZATION
		buttonHoverTexture = new Texture2D (Screen.width - 200, 25);
		for (int y = 0; y < buttonHoverTexture.height; ++y) {
			for (int x = 0; x < buttonHoverTexture.width; ++x) {
				if ((x > 1 && y > 1) && (x < buttonHoverTexture.width - 2 && y < buttonHoverTexture.height - 2)) {
					Color color = new Color (254f / 255f, 234f / 255f, 174f / 255f, 1f);
					buttonHoverTexture.SetPixel (x, y, color);
				} else {
					Color color = new Color (254f / 255f, 234f / 255f, 174f / 255f, 1f);
					buttonHoverTexture.SetPixel (x, y, color);
				}
			}
		} */
		buttonHoverTexture.Apply ();
		
		buttonStyler = new GUIStyle (GUI.skin.box);
		buttonStyler.normal.textColor = Color.black;
		buttonStyler.fontSize = 25;
		buttonStyler.normal.background = buttonTexture;
		buttonStyler.hover.background = buttonHoverTexture;

		GUI.Box (new Rect (50 + cameraOffsetX, cameraOffsetY, Screen.width - 100, 30), "Thanks for playing!", promptStyler);

		string wonFinal;
		if (transitions.won) {
			wonFinal = "won";
		} else {
			wonFinal = "lost";
		}
		GUI.Box (new Rect (50 + cameraOffsetX, 75 + cameraOffsetY, Screen.width - 100, 325), "\nYou " + wonFinal + " the final battle! Your final scores:\n" +
		         "_Happiness: " + Mathf.RoundToInt(transitions.happiness * 100) + 
		         "    Well-being: " + Mathf.RoundToInt(transitions.wellbeing * 100) + 
		         "    Grades: " + Mathf.RoundToInt (transitions.grades * 100) + "_\n\n" +
		         "This game was created by:\n\nTracy Lee\nMina Li\nCalvin Lu\nAustin Shyu\n\n" +
		         "If you liked what you played, please get in contact with us!\n\nCheck out our code at https://github.com/LiMina/adjective-animal.", styler);

		if (GUI.Button (new Rect (300 + cameraOffsetX, 450 + cameraOffsetY, Screen.width - 600, 100), "\nReplay?", buttonStyler)) {
			transitions.happiness=.20f;
			transitions.grades=.20f;
			transitions.wellbeing=.5f;
			transitions.won = false;
			Application.LoadLevel ("dialogue");
				}
		}
}
