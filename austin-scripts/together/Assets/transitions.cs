﻿using UnityEngine;
using System.Collections;

public class transitions : MonoBehaviour {
	public static float happiness=.50f;
	public static float grades=.50f;
	public static float wellbeing=.9f;
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
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		print (nextImage);
	}
}
