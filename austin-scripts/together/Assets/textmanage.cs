using UnityEngine;
using System.Collections;

public class textmanage : MonoBehaviour
{
		public int state = 0;
		public int clickedbutton = 0;
		public bool choosingOption = false;
		public int path = 0;
		public static string scene = "room";
		public string dialogue = "WAKE UP!!!!!";
		public int fontSize = 18;
		public int classesTaken = 0;
		public bool lunch = false;
		public float winlose = 0.0f;
		public int extracurriculars = 0;
		public int CLASSLIMIT = 2;
		public int EXTRALIMIT = 2;
		public bool waitActive = false;
		public float BATTLETIMEDELAY = 2.0f;
		public bool wait_called = false;
		public bool stats_upped = false;
		public GUIStyle styler;
		public Texture2D texture;
		public GUIStyle buttonStyler;
		public Texture2D buttonTexture;
		public Texture2D buttonHoverTexture;
		public bool addedClass = false;
		public float rand;
		public bool set = false;
		//Sprite clock;
		// Use this for initialization
		void Start ()
		{
				//styler = ;
				//clock = (Sprite)Resources.Load ("clock");
				state = transitions.nextState;
				path = transitions.nextPath;
				if (transitions.nextScene != null) {
						scene = transitions.nextScene;
				} else {
						scene = "afterschool2";
				}
				lunch = transitions.lunch;
				classesTaken = transitions.classesTaken;
				extracurriculars = transitions.extra;
		}

		void Reset_statepath ()
		{
				state = 0;
				path = 0;
		}
		// Update is called once per frame
		IEnumerator Wait (float wait_time)
		{	
				//if (!waitActive) {
				waitActive = true;
				yield return new WaitForSeconds (wait_time);
				waitActive = false;
				Application.LoadLevel ("Battle");
			
				//}
				//inScene = true;
		}

		void SetTransition (int state, int path, string scene, Sprite nextImage, int enemyCount = 1)
		{
				transitions.nextState = state;
				transitions.nextPath = path;
				transitions.nextScene = scene;
				transitions.nextImage = nextImage;
				transitions.enemyCount = enemyCount;
		}

		void Update ()
		{
				if (!set) {
						rand = Random.value;
						set = true;
				}
				GameObject.Find ("bg").GetComponent<SpriteRenderer> ().sprite = transitions.bg;	
				//Camera camera = (Camera) GameObject.Find ("Main Camera");
				Camera.main.backgroundColor = new Color (163f / 255f, 203f / 255f, 204f / 255f, 1f);
				print (transitions.nextScene);
				
				if (Input.GetMouseButtonDown (0) && !choosingOption && !waitActive) {

						if (scene == "room" && state == 0 && path == 2) {
								Reset_statepath ();
						} else if (scene == "room" && state == 1 && path == 1) {
								scene = "breakfast";
								Reset_statepath ();
						} else if (scene == "breakfast" && state == 0 && path != 0) {
								scene = "school";
								Reset_statepath ();
						} else if (scene == "school" && state == 2 && path != 0) {
								scene = "school2";
								Reset_statepath ();
						} else if (scene == "school2" && state == 2 && path != 0 && classesTaken <= CLASSLIMIT) {
								if (!lunch && path == 4) {
										lunch = true;
										transitions.lunch = true;
								}
								addedClass = false;
								//classesTaken++;
								//transitions.classesTaken++;
								Reset_statepath ();
						} else if (scene == "school2" && classesTaken > CLASSLIMIT) {
								scene = "afterschool";
								Reset_statepath ();
						} else if (scene == "afterschool" && state == 2 && path != 0) {
								scene = "afterschool2";
								//extracurriculars++;
								transitions.extra++;
								Reset_statepath ();
						} else if (scene == "afterschool2" && state == 2 && path != 0 && extracurriculars <= EXTRALIMIT) {
								transitions.extra++;
								//extracurriculars++;
								Reset_statepath ();
						} else if (scene == "afterschool2" && extracurriculars > EXTRALIMIT) {
								scene = "end";
								Reset_statepath ();
						} else {
								state++;
						}
				}
				if (clickedbutton == 1) {
						choosingOption = false;
						clickedbutton = 0;
						path = 1;
						state = 0;
				} else if (clickedbutton == 2) {
						choosingOption = false;
						clickedbutton = 0;
						path = 2;
						state = 0;
				} else if (clickedbutton == 3) {
						choosingOption = false;
						clickedbutton = 0;
						path = 3;
						state = 0;
				} else if (clickedbutton == 4) {
						choosingOption = false;
						clickedbutton = 0;
						path = 4;
						state = 0;
				} else if (clickedbutton == 5) {
						choosingOption = false;
						clickedbutton = 0;
						path = 5;
						state = 0;
				}
				//print ("nextPath: " + transitions.nextPath + "; nextScene: " + transitions.nextScene + ". nextState: " + transitions.nextState);
		}
		//void onMouseClick
		void OnGUI ()
		{
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
		
				GUI.Box (new Rect (80, Screen.height - 220, Screen.width - 160, 200), dialogue, styler);
				/*
				 *ROOOOOMMM SCEEEENNNNEEEE
				 */
				if (scene == "room") {
						transitions.bg = Resources.Load<Sprite> ("processed_bedroom2");
						if (state == 0 && path == 0) {
								dialogue = "...WAKE UP!!!!!";
						} else if (state == 1 && path == 0) {
								dialogue = "You roll over, groan and hit your alarm clock";		
						} else if (state == 2 && path == 0) {
								choosingOption = true;
								dialogue = "What will you do?";
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Get up", buttonStyler)) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Don't get up", buttonStyler)) {
										clickedbutton = 2;
								}
						} else if (state == 0 && path == 2) { // option 2
								dialogue = "Really?";
						
						} else if (state == 0 && path == 1) { // option 1
								dialogue = "Sighing, you try to sit up while you blink blearily. You don't want to be late to class again, right?\nCarpe Diem!";
								//load Battle
								//StartCoroutine (Wait (BATTLETIMEDELAY));
								
								/*transitions.nextState = 0;
								transitions.nextPath = 0;
								transitions.nextScene = "breakfast";*/
								//if (!waitActive) {
								//CallWait (BATTLETIMEDELAY);
								//Application.LoadLevel ("Battle");
								//}
						} else if (state == 1 && path == 1) {
								transitions.currBattle = "wakeup";
								Application.LoadLevel ("Battle");
								SetTransition (0, 0, "breakfast", Resources.Load<Sprite> ("clock"), 1);
						}
						
							
				}
				/*
				 * SCHOOL SCENE WOW!
				 */
				else if (scene == "breakfast") {
						
						if (state == 0 && path == 0) {
				transitions.bg = Resources.Load<Sprite> ("processed_stairs");
								dialogue = "Climbing down the stairs, you find your way to the kitchen, make some food, and shovel some of it into your mouth.";
						}
						if (state == 1 && path == 0) {
				transitions.bg = Resources.Load<Sprite> ("processed_breakfast");
								choosingOption = true;
								dialogue = "What do you eat? ";
								
								if (GUI.Button (new Rect (100, Screen.height - 140, Screen.width - 200, 25), "Eggs and Bacon", buttonStyler)) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Cereal", buttonStyler)) {
										clickedbutton = 2;
								}
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Leftovers from last night", buttonStyler)) {
										clickedbutton = 3;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Mysterious Ooze", buttonStyler)) {
										clickedbutton = 4;
								}
								
						}
						if (state == 0 && path == 1) {
								if (!stats_upped) {
										transitions.wellbeing += 0.20f;
										stats_upped = true;
								}
								dialogue = "Bacon? The internet approves.\nYour well-being increased.";
						}
						if (state == 0 && path == 2) {
								if (!stats_upped) {
										transitions.happiness += 0.1f;
										stats_upped = true;
								}
								dialogue = "Your favorite cereal! You're so glad you bought this.\nYour happiness increased.";
						}
						if (state == 0 && path == 3) {
								if (!stats_upped) {
										transitions.grades += 0.1f;
										stats_upped = true;
								}
								dialogue = "Okay, that's cool; dinner for breakfast is great! While it sits in the microwave, you spend some time looking over your notes.\nYour grades increased.";			
						}
						if (state == 0 && path == 4) {
								if (!stats_upped) {
										transitions.wellbeing += 0.1f;
										transitions.grades += 0.05f;
										transitions.happiness -= 0.05f;
										stats_upped = true;
								}
								dialogue = "You quickly eat it before it starts moving ...again. It's not very appetizing, but you feel rejuvenated and alert.\nYour happiness decreased. Your well-being and grades increased.\n...What was in that?";
						}
				
				} else if (scene == "school") {
						
						if (state == 0 && path == 0) {
				transitions.bg = Resources.Load<Sprite> ("processed_background3");
								stats_upped = false;
								choosingOption = true;
								dialogue = "Welcome to the start of your day. What do you have first?";

								if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Lecture", buttonStyler)) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Test", buttonStyler)) {
										clickedbutton = 2;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Gym", buttonStyler)) {
										clickedbutton = 3;
								}
				
						}
						if (state == 0 && path == 1) {
								transitions.bg = Resources.Load<Sprite> ("processed_lecture");
								dialogue = "8 AM classes are the worst. but you should be okay, right?";
								//LOAD BATTLE
								//StartCoroutine (Wait (BATTLETIMEDELAY));
								//winlose = Random.Range (0, 1f);
								//SetTransition (1, 1, "school", Resources.Load<Sprite> ("clock"));
						}
						if (state == 0 && path == 2) {
								transitions.bg = Resources.Load<Sprite> ("processed_lecturehall");
								dialogue = "Well, you studied...right?";
								//LOAD BATTLE
								//StartCoroutine (Wait (BATTLETIMEDELAY));
								//winlose = Random.Range (0, 1f);
								//SetTransition (1, 2, "school", Resources.Load<Sprite> ("clock"));
						}
						if (state == 0 && path == 3) {
								transitions.bg = Resources.Load<Sprite> ("processed_gym");
								dialogue = "You told yourself you would start hitting the gym more often. Are you ready to feel the burn?";
								//LOAD BATLE
								//StartCoroutine (Wait (BATTLETIMEDELAY));
								//SetTransition (1, 3, "school", Resources.Load<Sprite> ("clock"));

								//winlose = Random.Range (0, 1f);
						}
						if (state == 1 && path == 1) {
								transitions.currBattle = "lecture";
								Application.LoadLevel ("Battle");
								SetTransition (2, 1, "school", Resources.Load<Sprite> ("chalkboard"));
						}
						if (state == 1 && path == 2) {
								transitions.currBattle = "test";
								Application.LoadLevel ("Battle");
								SetTransition (2, 2, "school", Resources.Load <Sprite> ("book"));
						}
						if (state == 1 && path == 3) {
								transitions.currBattle = "gym";
								Application.LoadLevel ("Battle");
								SetTransition (2, 3, "school", Resources.Load<Sprite> ("gym"), 2);
						}
						/*
						 * win lose states
						 */
						if (state == 2 && path == 1 && transitions.won) { // win lecture
								dialogue = "You made it through the class! Alright, what are you doing next?";
						} else if (state == 2 && path == 1 && !transitions.won) {
								dialogue = "Well, the professor called you out when you fell asleep in class, and you couldn’t answer anything about the lecture. What's next?";
						}
						if (state == 2 && path == 2 && transitions.won) { // test win{
								dialogue = "Aww yiss, you aced it! Ready for the rest of the day? ";
						} else if (state == 2 && path == 2 && !transitions.won) {
								dialogue = "Wow, you really didn’t study, but curves, am I right? What’s next?";		
						}
						if (state == 2 && path == 3 && transitions.won) {// gym win
								dialogue = "You feel rejuvenated and a bit tired, but ready for the next thing in your day.";
						} else if (state == 2 && path == 3 && !transitions.won) {
								dialogue = "You were completely uncoordinated and you got some odd looks. Too bad you still have the rest of the day to go.";		
						}

									
				} else if (scene == "school2") {
						/* SCHOOL 2 */
			
						if (state == 0 && path == 0) {
								if (transitions.classesTaken == 0) {
										transitions.bg = Resources.Load<Sprite> ("processed_background1");
								} else if (transitions.classesTaken == 1) {
										transitions.bg = Resources.Load<Sprite> ("processed_background4");
								} else if (transitions.classesTaken == 2) {
										transitions.bg = Resources.Load<Sprite> ("processed_background5");
								} else if (transitions.classesTaken == 3) {
										transitions.bg = Resources.Load<Sprite> ("processed_background6");
								}
								choosingOption = true;
								dialogue = "Where are you going next?";
				
								if (!lunch) {
										if (GUI.Button (new Rect (100, Screen.height - 170, Screen.width - 200, 25), "Test", buttonStyler)) {
												clickedbutton = 1;
										}
										if (GUI.Button (new Rect (100, Screen.height - 140, Screen.width - 200, 25), "Gym", buttonStyler)) {
												clickedbutton = 2;
										}
										if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Lecture", buttonStyler)) {
												clickedbutton = 3;
										}
										if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Lunch", buttonStyler)) {
												clickedbutton = 4;
										}
										if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Studying", buttonStyler)) {
												clickedbutton = 5;
										}
								} else {
										if (GUI.Button (new Rect (100, Screen.height - 140, Screen.width - 200, 25), "Test", buttonStyler)) {
												clickedbutton = 1;
										}
										if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Gym", buttonStyler)) {
												clickedbutton = 2;
										}
										if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Lecture", buttonStyler)) {
												clickedbutton = 3;
										}
										if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Studying", buttonStyler)) {
												clickedbutton = 4;
										}
								}
						}
						if (!lunch) {
								if (state == 0 && path == 1) {
										transitions.bg = Resources.Load<Sprite> ("processed_lecturehall");
										if (rand <= 0.5) {
												dialogue = "Isn't it weird that some midterms aren't actually in the middle of the semester?";
										} else {
												dialogue = "You're worried today's the day the professor makes 'C' the answer to every question.";
										}
										//LOAD BATTLE
										//winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 2) {
										transitions.bg = Resources.Load<Sprite> ("processed_gym");
										if (rand <= 0.5) {
												dialogue = "Are you really working out now? Hope you're ready to be sweaty for the whole day.";
										} else {
												dialogue = "Sometimes you wonder how effect walking on the treadmill is.";
										}
										//LOAD BATTLE
										//winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 3) {
										transitions.bg = Resources.Load<Sprite> ("processed_lecture");
										if (rand <= 0.5) {
												dialogue = "Sometimes you feel like your professor should be teaching meditation instead of mathematics.";
										} else {
												dialogue = "Should you sit in the back and fall asleep? Or in the front and fall asleep?";
										}
										//LOAD BATTLE
										//winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 4) {
										transitions.bg = Resources.Load<Sprite> ("processed_pieology");
										if (rand <= 0.5) {
												dialogue = "You remind yourself to eat right!";
										} else {
												dialogue = "Healthy body, healthy mind.";
										}
										//LOAD BATTLE
										//lunch = true;
										//winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 5) {
										if (rand <= 0.5) {
												dialogue = "Don't you wish you could just absorb the book via osmosis?";
										} else {
												dialogue = "Sometimes it feels like you're reading the same line over and over again. Sometimes it feels like you're reading the same line over and over again.";
										}
										//LOAD BATTLE
										//winlose = Random.Range (0, 1f);
								}
						}
						if (lunch) {
								if (state == 0 && path == 1) {
										transitions.bg = Resources.Load<Sprite> ("processed_lecturehall");
										if (rand <= 0.5) {
												dialogue = "Isn't it weird that some midterms aren't actually in the middle of the semester?";
										} else {
												dialogue = "You're worried today's the day the professor makes 'C' the answer to every question.";
										}
										//LOAD BATTLE
										//winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 2) {
										transitions.bg = Resources.Load<Sprite> ("processed_gym");
										if (rand <= 0.5) {
												dialogue = "Are you really working out now? Hope you're ready to be sweaty for the whole day.";
										} else {
												dialogue = "Sometimes you wonder how effective walking on the treadmill is.";
										}
										//LOAD BATTLE
										//winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 3) {
										transitions.bg = Resources.Load<Sprite> ("processed_lecture");
										if (rand <= 0.5) {
												dialogue = "Sometimes you feel like your professor should be teaching meditation instead of mathematics.";
										} else {
												dialogue = "Should you sit in the back and fall asleep? Or in the front and fall asleep?";
										}
										//LOAD BATTLE
										//winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 4) {
										if (rand <= 0.5) {
												dialogue = "Don't you wish you could just absorb the book via osmosis?";
										} else {
												dialogue = "Sometimes it feels like you're reading the same line over and over again. Sometimes it feels like you're reading the same line over and over again.";
										}
										//LOAD BATTLE
										//winlose = Random.Range (0, 1f);
								}

						}


						if (state == 1 && path == 1) { // test
								if (!addedClass) {
										classesTaken++;
										transitions.classesTaken++;
										addedClass = true;
								}
								transitions.currBattle = "test";
								Application.LoadLevel ("Battle");
								SetTransition (2, 1, "school2", Resources.Load <Sprite> ("book"));
								set = false;
						}
						if (state == 1 && path == 2) { //gym
								if (!addedClass) {
										classesTaken++;
										transitions.classesTaken++;
										addedClass = true;
								}
								transitions.currBattle = "gym";
								Application.LoadLevel ("Battle");
								SetTransition (2, 2, "school2", Resources.Load<Sprite> ("gym"));
								set = false;
						}
						if (state == 1 && path == 3) { // lecture
								if (!addedClass) {
										classesTaken++;
										transitions.classesTaken++;
										addedClass = true;
								}
								transitions.currBattle = "lecture";
								Application.LoadLevel ("Battle");
								SetTransition (2, 3, "school2", Resources.Load<Sprite> ("chalkboard"));
								set = false;
						}
						if (state == 1 && path == 4 && !lunch) { // lunch
								if (!addedClass) {
										classesTaken++;
										transitions.classesTaken++;
										addedClass = true;
								}
								//load lunch
								transitions.currBattle = "lunch";
								Application.LoadLevel ("Battle");
								SetTransition (2, 4, "school2", Resources.Load<Sprite> ("lunch"));
								set = false;
						}
						if (((state == 1 && path == 4 && lunch) || (state == 1 && path == 5 && !lunch))) { //studying
								if (!addedClass) {
										classesTaken++;
										transitions.classesTaken++;
										addedClass = true;
								}				
								transitions.currBattle = "studying";
								Application.LoadLevel ("Battle");
								SetTransition (2, 5, "school2", Resources.Load<Sprite> ("book"));
								set = false;
						}


						if (state == 2 && path == 1 && transitions.won) { // won test!
								dialogue = "Aww yiss, you aced it! Ready for the rest of the day?";
						} else if (state == 2 && path == 1 && !transitions.won) {
								dialogue = "Wow, you really didn’t study, but curves, am I right? What’s next?";
						}
						if (state == 2 && path == 2 && transitions.won) {// gym won!
								dialogue = "You feel rejuvenated and a bit tired, but ready for the next thing in your day.";
						} else if (state == 2 && path == 2 && !transitions.won) {
								dialogue = "You were completely uncoordinated and you got some odd looks. Too bad you still have the rest of the day to go.";		
						}
						if (state == 2 && path == 3 && transitions.won) { // win lecture
								dialogue = "You made it through the class! Alright, what are you doing next?";
						} else if (state == 2 && path == 3 && !transitions.won) {
								dialogue = "Well, the professor called you out when you fell asleep in class, and you couldn’t answer anything about the lecture. What's next?";
						}
						if (state == 2 && path == 4 && transitions.won) { // lunch win!
								dialogue = "That was a great lunch! You had a delicious meal, and it was a beautiful day. Time to go! ";
						} else if (state == 2 && path == 4 && !transitions.won) {
								dialogue = "You couldn’t decide what to eat and then when you bought something, it was horrible and you didn’t have enough time to finish anyways. You’re still hungry, but you have to go. ";
						} else if (state == 2 && path == 5 && transitions.won) { // study win!
								dialogue = "You really figured out what you weren’t understanding in class, and the concepts turned out to be much easier than you thought they would be. You feel confident that you’ll do well in this class. ";
						} else if (state == 2 && path == 5 && !transitions.won) {
								dialogue = "Nothing makes sense in this course, and you fell asleep while reading your textbook and got woken up by the librarian asking if you were okay. No. Not really. ";
						}
						//classesTaken++;

				}
		/* AFTER SCHOOL */
		
		else if (scene == "afterschool") {
						
						if (state == 0 && path == 0) {
				transitions.bg = Resources.Load<Sprite> ("processed_sunset1");
								choosingOption = true;
								dialogue = "You made it through the day! What are you doing after school?";
				
								if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Club Meeting", buttonStyler)) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Hanging out with friends", buttonStyler)) {
										clickedbutton = 2;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Homework", buttonStyler)) {
										clickedbutton = 3;
								}
						}
						if (state == 0 && path == 1) {
								transitions.bg = Resources.Load<Sprite> ("processed_clubroom");
								dialogue = "You hear some new members are joining today and you're excited to meet them!";
								//LOAD BATTLE
								//winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 2) {
								transitions.bg = Resources.Load<Sprite> ("processed_frands");
								dialogue = "Cool, your friends are free! You're super excited to see them!";
								//LOAD BATTLE
								//winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 3) {
								dialogue = "Ugh, necessary evils...";
								//LOAD BATTLE
								//winlose = Random.Range (0, 1f);
						}

						if (state == 1 && path == 1) { // club
								transitions.currBattle = "club";
								Application.LoadLevel ("Battle");
								SetTransition (2, 1, "afterschool", Resources.Load <Sprite> ("person1"));
						}
						if (state == 1 && path == 2) { // friends
								transitions.currBattle = "friends";
								Application.LoadLevel ("Battle");
								SetTransition (2, 2, "afterschool", Resources.Load <Sprite> ("person2"));
						}
						if (state == 1 && path == 3) { // hw
								transitions.currBattle = "hw";
								Application.LoadLevel ("Battle");
								SetTransition (2, 3, "afterschool", Resources.Load <Sprite> ("book"));
						}

						if (state == 2 && path == 1 && transitions.won) { // won club!
								dialogue = "The club meeting was awesome - everyone had a good time, and you feel really excited for your next meeting.";
				
						} else if (state == 2 && path == 1 && !transitions.won) {
								dialogue = "The club meeting was a mess. You were pretty awkward. Actually, everyone was pretty awkward.";
						}
						if (state == 2 && path == 2 && transitions.won) { // hangout won!
								dialogue = "It was great to catch up with your friends and forget about school for a while.";
						} else if (state == 2 && path == 2 && !transitions.won) {
								dialogue = "It was nice seeing your friends; you just wish they would let you speak up a little more.";		
						}
						if (state == 2 && path == 3 && transitions.won) { // win lecture
								dialogue = "All right, easy homework! You’re done with this assignment and will have more free time.";
						} else if (state == 2 && path == 3 && !transitions.won) {
								dialogue = "Your homework was impossibly hard and no one else knows how to do it (or won’t help you).";
						}
						//extracurriculars++;
				}
		
		/* AFTER SCHOOL 2 */
		
		else if (scene == "afterschool2") {
			
						if (state == 0 && path == 0) {
								transitions.bg = Resources.Load<Sprite> ("processed_sunset2");
								choosingOption = true;
								dialogue = "What are you going to do now?";
				
								if (GUI.Button (new Rect (100, Screen.height - 140, Screen.width - 200, 25), "Club Meeting", buttonStyler)) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Hanging out with friends", buttonStyler)) {
										clickedbutton = 2;
								}
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Homework", buttonStyler)) {
										clickedbutton = 3;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Party", buttonStyler)) {
										clickedbutton = 4;
								}
				
						}
						if (state == 0 && path == 1) {
								transitions.bg = Resources.Load<Sprite> ("processed_clubroom");
								dialogue = "You hear some new members are joining today and you're excited to meet them!";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 2) {
								transitions.bg = Resources.Load<Sprite> ("processed_frands");
								dialogue = "Cool, your friends are free! You're super excited to see them!";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 3) {
				transitions.bg = Resources.Load<Sprite> ("processed_room");
								dialogue = "Ugh, necessary evils...";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 4) {
				transitions.bg = Resources.Load<Sprite> ("processed_nightstreet");
								dialogue = "You got invited to a party! The party don't start 'til you walk in!";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						
						if (state == 1 && path == 1) { // club
								transitions.currBattle = "club";
								Application.LoadLevel ("Battle");
								SetTransition (2, 1, "afterschool2", Resources.Load <Sprite> ("person1"));
						}
						if (state == 1 && path == 2) { // friends
								transitions.currBattle = "friends";
								Application.LoadLevel ("Battle");
								SetTransition (2, 2, "afterschool2", Resources.Load <Sprite> ("person2"));
						}
						if (state == 1 && path == 3) { // hw
								transitions.currBattle = "hw";
								Application.LoadLevel ("Battle");
								SetTransition (2, 3, "afterschool2", Resources.Load <Sprite> ("book"));
						}
						if (state == 1 && path == 4) { // party
								transitions.currBattle = "party";
								Application.LoadLevel ("Battle");
								SetTransition (2, 4, "afterschool2", Resources.Load <Sprite> ("crowd"));
						}
						

						if (state == 2 && path == 1 && transitions.won) { // won club!
								dialogue = "The club meeting was awesome - everyone had a good time, and you feel really excited for your next meeting.";
							
						} else if (state == 2 && path == 1 && !transitions.won) {
								dialogue = "The club meeting was a mess. You were pretty awkward. Actually, everyone was pretty awkward.";
						}
						if (state == 2 && path == 2 && transitions.won) { // hangout won!
								dialogue = "It was great to catch up with your friends and forget about school for a while.";
						} else if (state == 2 && path == 2 && !transitions.won) {
								dialogue = "It was nice seeing your friends; you just wish they would let you speak up a little more.";
						}
						if (state == 2 && path == 4 && transitions.won) { // win party
								dialogue = "The party had great music and you met a lot of cool people!";
						} else if (state == 2 && path == 4 && !transitions.won) {
								dialogue = "It was a horrible mess of people - you could barely move at all and got crushed in the crowd.";
						}
						if (state == 2 && path == 3 && transitions.won) { // win lecture
								dialogue = "All right, easy homework! You’re done with this assignment and will have more free time.";
						} else if (state == 2 && path == 3 && !transitions.won) {
								dialogue = "Your homework was impossibly hard and no one else knows how to do it (or won’t help you).";
						}
			
				} else if (scene == "end") {
						if (state == 0 && path == 0) {
				transitions.bg = Resources.Load<Sprite> ("processed_bedroom3");
								dialogue = "Phew. It’s been a long day. Time to go to bed and try to sleep.";
								//LOAD BATTLE
						}
						if (state == 1 && path == 0) {
								transitions.currBattle = "sleep";
								Application.LoadLevel ("Battle");
								SetTransition (2, 0, "end", Resources.Load<Sprite> ("bed"));
						}
						if (state == 2 && path == 0) {
								dialogue = "Congratulations on surviving another day in Hard Mode. See you tomorrow.";
						}
				}
		}

}
