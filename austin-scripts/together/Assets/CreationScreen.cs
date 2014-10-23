using UnityEngine;
using System.Collections;

public class CreationScreen : MonoBehaviour {

	public bool endCreation = false;
	public int sexNum = (int)Math.Round(Random.Range (1,sex.Length-1));
	public int genderNum = (int)Math.Round(Random.Range (1, gender.Length-1));
	public int orientationNum = (int)Math.Round(Random.Range (1, orientation.Length-1));
	public int hairNum = (int)Math.Round(Random.Range (0, haircolor.Length-1);
	string[] sex = {"Male", "Female", "Intersex"};
	string[] gender = {"Male", "Female", "Nonbinary"};
	string[] orientation = {"Heterosexual", "Homosexual", "Bisexual", "Pansexual", "Polysexual", "Grey-Asexual", "Asexual"};
	string[] haircolor = {"Black", "Brown", "Red", "Blond(e)", "White"};
	string[] hairstyle = {"Buzzcut", "Bob", "K-Pop Star Hair", "Straight and Long", "Skrillex Sidecut"};
	string[] favcolor = {"Red", "Orange", "Yellow", "Green", "Blue", "Purple", "Indigo", "Black", "White"};
	string[] operatingsystem = {"Windows", "OS X", "Linux"};
	public int lineNum = 0;
	public string dialogue = "Create a character." //"There are some things you can't control in life. Look, we're just trying to make a point, okay?"
	//if operatingsytemNum = 2 {dialogue = "There are some things you can't control in life. Except your operating system. Go you.";}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	//GUI
	void OnGUI () {
	}
}
