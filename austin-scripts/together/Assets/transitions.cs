using UnityEngine;
using System.Collections;

public class transitions : MonoBehaviour {
	public static float happiness=.20f;
	public static float grades=.20f;
	public static float wellbeing=.50f;
	public static Sprite nextImage = null;
	public static bool won = false;
	public static bool outOfBattle = true;
	public static int nextState = 0;
	public static int nextPath = 0;
	public static string nextScene;
	public static bool lunch = false;
	public static int classesTaken = 0;
	public static int extra = 0;
	public static string currBattle = "wakeup";
	public static int enemyCount = 1;
	public static Sprite bg; 

	public static int hap = 0;
	public static int grad = 0;
	public static int well = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		print (nextImage);
	}
}
