using UnityEngine;
using System.Collections;

public class CreationScreen : MonoBehaviour {
	public GUIStyle styler;
	public Texture2D texture;
	public GUIStyle buttonStyler;
	public Texture2D buttonTexture;
	public Texture2D buttonHoverTexture;
	public int fontSize = 18;
	public bool endCreation = false;
	public int sexNum = 0;
	public int genderNum = 0;
	public int orientationNum = 0;
	public int hairNum = 0;
	public int sexSelectIteration = 0;
	public int genderSelectIteration = 0;
	public int orientationSelectIteration = 0;
	public int hairColorSelectIteration = 0;
	public int hairStyleSelectIteration = 0;
	public int favColorSelectIteration = 0;
	public int operatingSystemSelectIteration = 0;
	static string[] sex = {"Male", "Female", "Intersex"};
	static string[] gender = {"Male", "Female", "Nonbinary"};
	static string[] orientation = {"Heterosexual", "Homosexual", "Bisexual", "Pansexual", "Polysexual", "Grey-Asexual", "Asexual"};
	static string[] haircolor = {"Black", "Brown", "Red", "Blond(e)", "White"};
	static string[] hairstyle = {"Buzzcut", "Bob", "K-Pop Star Hair", "Straight and Long", "Skrillex Sidecut"};
	static string[] favcolor = {"Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Indigo", "Black", "White"};
	static string[] operatingsystem = {"Windows", "OS X", "Linux"};
	public int lineNum = 0;
	public string dialogue = "Create a character.";
	
	public void FinalChecker(int sexSelect, int genderSelect, int orientationSelect, int hairSelect){
		while((sexNum == sexSelect && genderNum == genderSelect && orientationNum == orientationSelect)|| (sexNum == 0 && genderNum == 0 && orientationNum == 0) || (sexNum == 1 && genderNum == 1 && orientationNum == 0)) {
			//prevents game from generating a heterosexual cisgender male or a heterosexual cisgender female or keeping all of the player's choices
			sexNum = (int)Mathf.Round(Random.Range (0,sex.Length-1));
			genderNum = (int)Mathf.Round(Random.Range (0, gender.Length-1));
			orientationNum = (int)Mathf.Round(Random.Range (0, orientation.Length-1));
			}
		hairNum = (int)Mathf.Round(Random.Range (0, haircolor.Length-1));
		Debug.Log (sex[sexNum] + ", " + gender[genderNum] + ", " + orientation[orientationNum] + ", " + haircolor[hairNum]);
		}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//GUI
	void OnGUI () {
		GUI.skin.box.fontSize = fontSize;	
		GUI.skin.box.wordWrap = true;
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
		texture.Apply ();
		
		styler = new GUIStyle (GUI.skin.box);
		styler.normal.textColor = Color.black;
		styler.normal.background = texture;
		
		/* For buttons */
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
		buttonTexture.Apply ();
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
		buttonHoverTexture.Apply ();
		
		buttonStyler = new GUIStyle (GUI.skin.box);
		buttonStyler.normal.textColor = Color.black;
		buttonStyler.fontSize = 14;
		buttonStyler.normal.background = buttonTexture;
		buttonStyler.hover.background = buttonHoverTexture;
		
		//Options
		
		GUI.Label (new Rect(80, 50, Screen.width/2, 25), "Test");
		
		//Buttons
		
		//Bottom Box
		GUI.Box (new Rect (80, Screen.height - 120, Screen.width - 160, 100), dialogue, styler);
		if(lineNum == 1){
			dialogue = "There are some things you can't control in life.";
			}
		else if(lineNum == 2){
			if(operatingSystemSelectIteration == 2){
				dialogue = "Your operating system is not one of those things. Go you.";
			}
			else{
				dialogue = "Look, we're just trying to make a point, okay?";
			}
		}
		else {
			Debug.Log("something");
			}
	}
}
