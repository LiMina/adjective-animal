using UnityEngine;
using System.Collections;

public class textmanage : MonoBehaviour
{
		public int state = 0;
		public int clickedbutton = 0;
		public bool choosingOption = false;
		public int path = 0;
		public string scene = "room";
		public string dialogue = "WAKE UP!!!!!";
		public int fontSize = 18;
		public int classesTaken = 0;
		public bool lunch = false;
		public float winlose = 0.0f;
		public int extracurriculars = 0;
		public int CLASSLIMIT = 3;
		public int EXTRALIMIT = 2;
		public bool waitActive = false;
		public float BATTLETIMEDELAY = 3.0f;
		public bool wait_called = false;
		// Use this for initialization
		void Start ()
		{
	
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

		void Update ()
		{
				if (Input.GetMouseButtonDown (0) && !choosingOption) {

						if (scene == "room" && state == 0 && path == 2) {
								Reset_statepath ();
						} else if (scene == "room" && state == 0 && path == 1) {
								scene = "breakfast";
								Reset_statepath ();
						} else if (scene == "breakfast" && state == 0 && path != 0) {
								scene = "school";
								Reset_statepath ();
						} else if (scene == "school" && state == 1 && path != 0) {
								scene = "school2";
								Reset_statepath ();
						} else if (scene == "school2" && state == 1 && path != 0 && classesTaken <= CLASSLIMIT) {
								if (!lunch && path == 4) {
										lunch = true;
								}
								classesTaken++;
								Reset_statepath ();
						} else if (scene == "school2" && classesTaken > CLASSLIMIT) {
								scene = "afterschool";
								Reset_statepath ();
						} else if (scene == "afterschool" && state == 1 && path != 0) {
								scene = "afterschool2";
								extracurriculars++;
								Reset_statepath ();
						} else if (scene == "afterschool2" && state == 1 && path != 0 && extracurriculars <= EXTRALIMIT) {
								extracurriculars++;
								Reset_statepath ();
						} else if (scene == "afterschool2" && state == 1 && path != 0 && extracurriculars > EXTRALIMIT) {
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
		}
		//void onMouseClick
		void OnGUI ()
		{
			
				GUI.Box (new Rect (80, Screen.height - 220, Screen.width - 160, 200), dialogue);
				GUI.skin.box.fontSize = fontSize;	
				GUI.skin.box.wordWrap = true;

				/*
				 *ROOOOOMMM SCEEEENNNNEEEE
				 */
				if (scene == "room") {
						if (state == 0 && path == 0) {
								dialogue = "WAKE UP!!!!!";
						} else if (state == 1 && path == 0) {
								dialogue = "You roll over, groan and hit your alarm clock";		
						} else if (state == 2 && path == 0) {
								choosingOption = true;
								dialogue = "What will you do?";
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Get up")) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Don't get up")) {
										clickedbutton = 2;
								}
						} else if (state == 0 && path == 2) { // option 2
								dialogue = "Really?";
						
						} else if (state == 0 && path == 1) { // option 1
								dialogue = "Sighing, you push yourself out of bed and stand shakily while you blink blearily. ";
								//load Battle
								StartCoroutine (Wait (BATTLETIMEDELAY));
								//if (!waitActive) {
								//CallWait (BATTLETIMEDELAY);
								//Application.LoadLevel ("Battle");
								//}
						}
							
				}
				/*
				 * SCHOOL SCENE WOW!
				 */
				else if (scene == "breakfast") {
						if (state == 0 && path == 0) {
								dialogue = "Climbing down the stairs, you find your way to the kitchen, make some food, and shovel some of it into your mouth.";
						}
						if (state == 1 && path == 0) {
								choosingOption = true;
								dialogue = "What do you eat? ";
								
								if (GUI.Button (new Rect (100, Screen.height - 140, Screen.width - 200, 25), "Eggs and Bacon")) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Cereal")) {
										clickedbutton = 2;
								}
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Leftovers from last night")) {
										clickedbutton = 3;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Mysterious Ooze")) {
										clickedbutton = 4;
								}
								
						}
						if (state == 0 && (path == 1 || path == 2)) {
								dialogue = "Sounds good!";
						}
						if (state == 0 && path == 3) {
								dialogue = "Okay, that's cool; dinner for breakfast is great!";			
						}
						if (state == 0 && path == 4) {
								dialogue = "Whatever floats your boat!";
						}
				} else if (scene == "school") {
						if (state == 0 && path == 0) {
								choosingOption = true;
								dialogue = "Welcome to the start of your day. What do you have first?";

								if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Lecture")) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Test")) {
										clickedbutton = 2;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Gym")) {
										clickedbutton = 3;
								}
				
						}
						if (state == 0 && path == 1) {
								dialogue = "Alright, this class should be okay, right? ";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 2) {
								dialogue = "Well, you studied...right? ";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 3) {
								dialogue = "Hope you’re ready to get psyched first thing in the morning!";
								//LOAD BATLE
								winlose = Random.Range (0, 1f);
						}
						/*
						 * win lose states
						 */
						if (state == 1 && path == 1 && winlose <= 0.5f) { // win lecture
								dialogue = "You made it through the class! Alright, what are you doing next?";
						} else if (state == 1 && path == 1 && winlose > 0.5f) {
								dialogue = "Well, the professor called you out when you fell asleep in class, and you couldn’t answer anything about the lecture. ";
						}
						if (state == 1 && path == 2 && winlose <= 0.5f) { // test win{
								dialogue = "Aww yiss, you aced it! Ready for the rest of the day? ";
						} else if (state == 1 && path == 2 && winlose > 0.5f) {
								dialogue = "Wow, you really didn’t study, did you? What a shame. What’s next?";		
						}
						if (state == 1 && path == 3 && winlose <= 0.5f) {// gym win
								dialogue = "You feel rejuvenated and a bit tired, but ready for the next thing in your day.";
						} else if (state == 1 && path == 3 && winlose > 0.5f) {
								dialogue = "You were completely uncoordinated and your classmates glared at you as you struggled to keep up. Too bad you still have the rest of the day to go. ";		
						}

									
				} else if (scene == "school2") {
						/* SCHOOL 2 */
			
						if (state == 0 && path == 0) {
								choosingOption = true;
								dialogue = "Where are you going next?";
				
								if (!lunch) {
										if (GUI.Button (new Rect (100, Screen.height - 170, Screen.width - 200, 25), "Test")) {
												clickedbutton = 1;
										}
										if (GUI.Button (new Rect (100, Screen.height - 140, Screen.width - 200, 25), "Gym")) {
												clickedbutton = 2;
										}
										if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Lecture")) {
												clickedbutton = 3;
										}
										if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Lunch")) {
												clickedbutton = 4;
										}
										if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Studying")) {
												clickedbutton = 5;
										}
								} else {
										if (GUI.Button (new Rect (100, Screen.height - 140, Screen.width - 200, 25), "Test")) {
												clickedbutton = 1;
										}
										if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Gym")) {
												clickedbutton = 2;
										}
										if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Lecture")) {
												clickedbutton = 3;
										}
										if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Studying")) {
												clickedbutton = 4;
										}
								}
						}
						if (!lunch) {
								if (state == 0 && path == 1) {
										dialogue = "Are you ready for this?";
										//LOAD BATTLE
										winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 2) {
										dialogue = "Get pumped!";
										//LOAD BATTLE
										winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 3) {
										dialogue = "How do you feel about this subject?";
										//LOAD BATTLE
										winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 4) {
										dialogue = "So, what's on the menu?";
										//LOAD BATTLE
										//lunch = true;
										winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 5) {
										dialogue = "Time to break out those books!";
										//LOAD BATTLE
										winlose = Random.Range (0, 1f);
								}
						}
						if (lunch) {
								if (state == 0 && path == 1) {
										dialogue = "Are you ready for this?";
										//LOAD BATTLE
										winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 2) {
										dialogue = "Get pumped!";
										//LOAD BATTLE
										winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 3) {
										dialogue = "How do you feel about this subject?";
										//LOAD BATTLE
										winlose = Random.Range (0, 1f);
								}
								if (state == 0 && path == 4) {
										dialogue = "Time to break out those books!";
										//LOAD BATTLE
										winlose = Random.Range (0, 1f);
								}

						}
						if (state == 1 && path == 1 && winlose <= 0.5f) { // won test!
								dialogue = "Aww yiss, you aced it! Ready for the rest of the day?";
					
						} else if (state == 1 && path == 1 && winlose > 0.5f) {
								dialogue = "Wow, you really didn’t study, did you? What a shame. What’s next?";
						}
						if (state == 1 && path == 2 && winlose <= 0.5f) {// gym won!
								dialogue = "You feel rejuvenated and a bit tired, but ready for the next thing in your day.";
						} else if (state == 1 && path == 3 && winlose > 0.5f) {
								dialogue = "You were completely uncoordinated and your classmates glared at you as you struggled to keep up. Too bad you still have the rest of the day to go. ";		
						}
						if (state == 1 && path == 3 && winlose <= 0.5f) { // win lecture
								dialogue = "You made it through the class! Alright, what are you doing next?";
						} else if (state == 1 && path == 3 && winlose > 0.5f) {
								dialogue = "Well, the professor called you out when you fell asleep in class, and you couldn’t answer anything about the lecture. ";
						}
						if (state == 1 && path == 4 && winlose <= 0.5f && !lunch) { // lunch win!
								dialogue = "That was a great lunch! You had a delicious meal, and it was a beautiful day. Time to go! ";
						} else if (state == 1 && path == 4 && winlose > 0.5f && !lunch) {
								dialogue = "You couldn’t decide what to eat and then when you bought something, it was horrible and you didn’t have enough time to finish anyways. You’re still hungry, but you have to go. ";
						} else if (state == 1 && path == 4 && winlose <= 0.5f && lunch) { // study win!
								dialogue = "You really figured out what you weren’t understanding in class, and the concepts turned out to be much easier than you thought they would be. You feel confident that you’ll do well in this class. ";
						} else if (state == 1 && path == 4 && winlose > 0.5f && lunch) {
								dialogue = "Nothing makes sense in this course, and you fell asleep while reading your textbook and got woken up by the librarian asking if you were okay. No. Not really. ";
						}
						//classesTaken++;

				}
		/* AFTER SCHOOL */
		
		else if (scene == "afterschool") {
			
						if (state == 0 && path == 0) {
								choosingOption = true;
								dialogue = "You made it through the day! What are you doing after school?";
				
								if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Club")) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Hanging out with friends")) {
										clickedbutton = 2;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Homework")) {
										clickedbutton = 3;
								}
						}
						if (state == 0 && path == 1) {
								dialogue = "Not all clubs are made equal - how do you like yours?";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 2) {
								dialogue = "Cool, your friends are free! Are you ready to socialize?";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 3) {
								dialogue = "Ugh, you do have to get it done...";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 1 && path == 1 && winlose <= 0.5f) { // won club!
								dialogue = "The club meeting was awesome - everyone listened to what you had to say, and you feel really excited for your next meeting.";
				
						} else if (state == 1 && path == 1 && winlose > 0.5f) {
								dialogue = "The club meeting was a mess. No one wanted to listen to you and the club is going in a direction that you never wanted it to go. Very disappointing.";
						}
						if (state == 1 && path == 2 && winlose <= 0.5f) { // hangout won!
								dialogue = "It was great to catch up with your friends and forget about school for a while.";
						} else if (state == 1 && path == 3 && winlose > 0.5f) {
								dialogue = "Your friends ignored you the entire time, even though they invited you, and you felt uncomfortable that they kept making jokes about the queer community. You didn’t feel like you could speak up, though, and was quietly uncomfortable. ";		
						}
						if (state == 1 && path == 3 && winlose <= 0.5f) { // win lecture
								dialogue = "All right, easy homework! You’re done with it now and don’t have to worry about it for the rest of the night. ";
						} else if (state == 1 && path == 3 && winlose > 0.5f) {
								dialogue = "Your homework was impossibly hard and no one else knows how to do it (or won’t help you).";
						}
						//extracurriculars++;
				}
		
		/* AFTER SCHOOL 2 */
		
		else if (scene == "afterschool2") {
			
						if (state == 0 && path == 0) {
								choosingOption = true;
								dialogue = "What are you going to do now?";
				
								if (GUI.Button (new Rect (100, Screen.height - 140, Screen.width - 200, 25), "Club")) {
										clickedbutton = 1;
								}
								if (GUI.Button (new Rect (100, Screen.height - 110, Screen.width - 200, 25), "Hanging out with friends")) {
										clickedbutton = 2;
								}
								if (GUI.Button (new Rect (100, Screen.height - 80, Screen.width - 200, 25), "Homework")) {
										clickedbutton = 3;
								}
								if (GUI.Button (new Rect (100, Screen.height - 50, Screen.width - 200, 25), "Party")) {
										clickedbutton = 4;
								}
				
						}
						if (state == 0 && path == 1) {
								dialogue = "Sometimes, large groups of people are hard to handle. Are you prepared?";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 2) {
								dialogue = "Hey, your friends asked you to go out with them!";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 3) {
								dialogue = "It's too bad, but homework has to get done eventually.";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 0 && path == 4) {
								dialogue = "You got invited to a party! Time to crack up the music and break out those dance moves!";
								//LOAD BATTLE
								winlose = Random.Range (0, 1f);
						}
						if (state == 1 && path == 1 && winlose <= 0.5f) { // won club!
								dialogue = "The club meeting was awesome - everyone listened to what you had to say, and you feel really excited for your next meeting.";
				
						} else if (state == 1 && path == 1 && winlose > 0.5f) {
								dialogue = "The club meeting was a mess. No one wanted to listen to you and the club is going in a direction that you never wanted it to go. Very disappointing.";
						}
						if (state == 1 && path == 2 && winlose <= 0.5f) { // hangout won!
								dialogue = "It was great to catch up with your friends and forget about school for a while.";
						} else if (state == 1 && path == 2 && winlose > 0.5f) {
								dialogue = "Your friends ignored you the entire time, even though they invited you, and you felt uncomfortable that they kept making jokes about the queer community. You didn’t feel like you could speak up, though, and was quietly uncomfortable. ";		
						}
						if (state == 1 && path == 4 && winlose <= 0.5f) { // win party
								dialogue = "The party had great music and you just might have hooked up with this really hot person. You even got their number!";
						} else if (state == 1 && path == 4 && winlose > 0.5f) {
								dialogue = "It was a horrible mess of people - you could barely move at all and got crushed in the crowd towards the front, and had your hearing blasted out by the speakers. Your ears are still ringing from the music...";
						}
						if (state == 1 && path == 3 && winlose <= 0.5f) { // win lecture
								dialogue = "All right, easy homework! You’re done with it now and don’t have to worry about it for the rest of the night. ";
						} else if (state == 1 && path == 3 && winlose > 0.5f) {
								dialogue = "Your homework was impossibly hard and no one else knows how to do it (or won’t help you).";
						}
			
				} else if (scene == "end") {
						if (state == 0 && path == 0) {
								dialogue = "Phew. It’s been a long day. Time to go to bed and try to sleep.";
								//LOAD BATTLE
						}
				}
		}

}
