
using UnityEngine;
using System.Collections;

public class opening : MonoBehaviour {
	public int fontSize = 50;
	public GUIStyle promptStyler;
	public Texture2D promptTexture;
	public GUIStyle styler;
	public Texture2D texture;
	public GUIStyle flasher;
	public Texture2D flashTexture;
	private bool blink = false;
	private int counter = 0;
	private int blinkSpeed = 30;

	void Start () {
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
	}
	
	// Update is called once per frame
	void Update () {
		Camera.main.backgroundColor = new Color (163f / 255f, 203f / 255f, 204f / 255f, 1f);
		if (counter < blinkSpeed) {
						blink = true;
				} else if (counter < blinkSpeed * 2) {
						blink = false;
				} else {
			counter = 0;
				}
		counter++;
		if(Input.anyKeyDown) {
			Application.LoadLevel ("CreationScreen");
		}
	}
	
	void OnGUI () {
		int cameraOffsetX = (int)(Camera.main.transform.position.x * Screen.width / 10);
		int cameraOffsetY = (int)(Camera.main.transform.position.y * Screen.height / 20);

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
		styler.fontSize = 100;
		styler.normal.background = texture;

		flasher = new GUIStyle (GUI.skin.label);
		flasher.normal.textColor = Color.white;
		flasher.fontSize = 30;
		
		GUI.Box (new Rect (100 + cameraOffsetX, Screen.height / 2 - 200, Screen.width - 200, 400), "\nHard Mode 101", styler);
		if(blink)
			flasher.normal.textColor = Color.white;
		else
			flasher.normal.textColor = Color.black;
		GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 100, 500, 400), "~*click anywhere to begin*~", flasher);

	}
}
