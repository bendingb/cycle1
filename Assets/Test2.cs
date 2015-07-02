using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Test2 : MonoBehaviour {
	//TO BE CHANGED - KEYBOARD SHORTCUTS
	// ENTER: Start Game
	// SPACE: Next Turn
	// E: Calculating
	// R: Total Score
	// T: Feedback

	public OSCConnectionExample oscex;			// To access Muse data from OSC
	public Timer timedevent;			// To access Muse data from OSC

	public bool timerOn = true;
	public float timeLeft = 3.0f;

	//GAME TRACKERS
	public int number_of_turns = 0;
	public int current_score = 0;

	//SWITCHES
	public bool canStartGame = false;			// True ONLY if player passed instructions - needed for Muse calibration
	public bool playInstructions = false;
	public bool selectCommandType = false; 	
	public bool endGame = false;
	public bool trigger;

	public bool playCommandMellow = false;
	public bool playCommandHeadTilts = false;
	public bool playCommandHeadTiltDown = false;
	public bool playCommandHeadTiltLeft = false;
	public bool playCommandHeadTiltRight = false;
	public bool playCommandHeadTiltUp = false;
	public bool playCommandBlink = false;
	public bool playCommandNoBlink = false;
	public bool playCommandSpeak = false;

	//VALUES
	public float valueMellow;
	public int valueBlink;

	public int isValueBlink;

	//INSTRUCTIONS
	public AudioClip[] robotInstructions;

	//COMMANDS
	public string[] commandType = new string[] {"mellow","headtiltdown","headtiltup","headtiltleft","headtiltright","blink","noblink","speak"};
	public string randomCommandType = null;

	public AudioClip[] robotCommandMellow;
	public AudioClip[] robotCommandHeadTiltsDown;
	public AudioClip[] robotCommandHeadTiltsLeft;
	public AudioClip[] robotCommandHeadTiltsRight;
	public AudioClip[] robotCommandHeadTiltsUp;
	public AudioClip[] robotCommandBlink;
	public AudioClip[] robotCommandNoBlink;
	public AudioClip[] robotCommandSpeak;

	//PLAYER ANS
	public bool playerAnsMellow;
	public bool playerAnsHeadTilts;
	public bool playerAnsBlink;

	//FEEDBACK
	public AudioClip[] singleFeedbackPos;
	public AudioClip[] singleFeedbackNeg;
	public AudioClip[] totalFeedback;
	
	//TOTAL 
	public AudioClip[] overallCalculating;
	public AudioClip[] overallTotal;
	
	// Use this for initialization
	void Start () {
		// INSTRUCTIONS
		robotInstructions = new AudioClip[]{
			Resources.Load ("instructions") as AudioClip,
		};

		// COMMANDS
		robotCommandMellow = new AudioClip[]{
			Resources.Load ("commandMellow_01") as AudioClip,
			Resources.Load ("commandMellow_02") as AudioClip,
			Resources.Load ("commandMellow_03") as AudioClip,
			Resources.Load ("commandMellow_04") as AudioClip,
			Resources.Load ("commandMellow_05") as AudioClip,
			Resources.Load ("commandMellow_06") as AudioClip,
			Resources.Load ("commandMellow_07") as AudioClip,
			Resources.Load ("commandMellow_08") as AudioClip,
			Resources.Load ("commandMellow_09") as AudioClip,
			Resources.Load ("commandMellow_10") as AudioClip,
			Resources.Load ("commandMellow_11") as AudioClip,
			Resources.Load ("commandMellow_12") as AudioClip,
			Resources.Load ("commandMellow_13") as AudioClip,
			Resources.Load ("commandMellow_14") as AudioClip,
			Resources.Load ("commandMellow_15") as AudioClip,
		};

		robotCommandHeadTiltsUp = new AudioClip[]{
			Resources.Load ("commandTiltUp_01") as AudioClip,
			Resources.Load ("commandTiltUp_02") as AudioClip,
			Resources.Load ("commandTiltUp_03") as AudioClip,
			Resources.Load ("commandTiltUp_04") as AudioClip,
			Resources.Load ("commandTiltUp_05") as AudioClip,
			Resources.Load ("commandTiltUp_06") as AudioClip,
		};

		robotCommandHeadTiltsDown = new AudioClip[]{
			Resources.Load ("commandTiltDown_01") as AudioClip,
			Resources.Load ("commandTiltDown_02") as AudioClip,
			Resources.Load ("commandTiltDown_03") as AudioClip,
			Resources.Load ("commandTiltDown_04") as AudioClip,
			Resources.Load ("commandTiltDown_05") as AudioClip,
			Resources.Load ("commandTiltDown_06") as AudioClip,
			Resources.Load ("commandTiltDown_07") as AudioClip,
			Resources.Load ("commandTiltDown_08") as AudioClip,
		};

		robotCommandHeadTiltsLeft = new AudioClip[]{
			Resources.Load ("commandTiltLeft_01") as AudioClip,
			Resources.Load ("commandTiltLeft_02") as AudioClip,
			Resources.Load ("commandTiltLeft_03") as AudioClip,
			Resources.Load ("commandTiltLeft_04") as AudioClip,
			Resources.Load ("commandTiltLeft_05") as AudioClip,
		};

		robotCommandHeadTiltsRight = new AudioClip[]{
			Resources.Load ("commandTiltRight_01") as AudioClip,
			Resources.Load ("commandTiltRight_02") as AudioClip,
			Resources.Load ("commandTiltRight_03") as AudioClip,
			Resources.Load ("commandTiltRight_04") as AudioClip,
			Resources.Load ("commandTiltRight_05") as AudioClip,
		};

		robotCommandBlink = new AudioClip[]{
			Resources.Load ("commandBlink_01") as AudioClip,
			Resources.Load ("commandBlink_02") as AudioClip,
			Resources.Load ("commandBlink_03") as AudioClip,
		};

		robotCommandNoBlink = new AudioClip[]{
			Resources.Load ("commandNoBlink_04") as AudioClip,
			Resources.Load ("commandNoBlink_05") as AudioClip,
			Resources.Load ("commandNoBlink_06") as AudioClip,
		};
		
		robotCommandSpeak = new AudioClip[]{
			Resources.Load ("commandSpeak_01") as AudioClip,
			Resources.Load ("commandSpeak_02") as AudioClip,
			Resources.Load ("commandSpeak_03") as AudioClip,
			Resources.Load ("commandSpeak_04") as AudioClip,
			Resources.Load ("commandSpeak_05") as AudioClip,
			Resources.Load ("commandSpeak_06") as AudioClip,
		};

		singleFeedbackPos = new AudioClip[]{
			Resources.Load ("feedbackPos_01") as AudioClip,
			Resources.Load ("feedbackPos_02") as AudioClip,
			Resources.Load ("feedbackPos_03") as AudioClip,
			Resources.Load ("feedbackPos_04") as AudioClip,
			Resources.Load ("feedbackPos_05") as AudioClip,
			Resources.Load ("feedbackPos_06") as AudioClip,
			Resources.Load ("feedbackPos_07") as AudioClip,
			Resources.Load ("feedbackPos_08") as AudioClip,
			Resources.Load ("feedbackPos_09") as AudioClip,
			Resources.Load ("feedbackPos_10") as AudioClip,
			Resources.Load ("feedbackPos_11") as AudioClip,
			Resources.Load ("feedbackPos_12") as AudioClip,
			Resources.Load ("feedbackPos_13") as AudioClip,
			Resources.Load ("feedbackPos_14") as AudioClip,
		};

		singleFeedbackNeg = new AudioClip[]{
			Resources.Load ("feedbackNeg_01") as AudioClip,
			Resources.Load ("feedbackNeg_02") as AudioClip,
			Resources.Load ("feedbackNeg_03") as AudioClip,
			Resources.Load ("feedbackNeg_04") as AudioClip,
			Resources.Load ("feedbackNeg_05") as AudioClip,
			Resources.Load ("feedbackNeg_06") as AudioClip,
			Resources.Load ("feedbackNeg_07") as AudioClip,
			Resources.Load ("feedbackNeg_08") as AudioClip,
			Resources.Load ("feedbackNeg_09") as AudioClip,
			Resources.Load ("feedbackNeg_10") as AudioClip,
			Resources.Load ("feedbackNeg_11") as AudioClip,
			Resources.Load ("feedbackNeg_12") as AudioClip,
			Resources.Load ("feedbackNeg_13") as AudioClip,
			Resources.Load ("feedbackNeg_14") as AudioClip,
			Resources.Load ("feedbackNeg_15") as AudioClip,
			Resources.Load ("feedbackNeg_16") as AudioClip,
		};

		totalFeedback = new AudioClip[]{
			Resources.Load ("endFeedbackPosPOs") as AudioClip,
			Resources.Load ("endFeedbackPos") as AudioClip,
			Resources.Load ("endFeedbackNeg") as AudioClip,
			Resources.Load ("endFeedbackNegNeg") as AudioClip,
		};

		overallCalculating = new AudioClip[]{
			Resources.Load ("calculating") as AudioClip,
		};

		overallTotal = new AudioClip[]{
			Resources.Load ("totalScore_01") as AudioClip,
			Resources.Load ("totalScore_02") as AudioClip,
			Resources.Load ("totalScore_03") as AudioClip,
			Resources.Load ("totalScore_04") as AudioClip,
			Resources.Load ("totalScore_05") as AudioClip,
			Resources.Load ("totalScore_06") as AudioClip,
			Resources.Load ("totalScore_07") as AudioClip,
			Resources.Load ("totalScore_08") as AudioClip,
			Resources.Load ("totalScore_09") as AudioClip,
			Resources.Load ("totalScore_10") as AudioClip,
			Resources.Load ("totalScore_11") as AudioClip,
			Resources.Load ("totalScore_12") as AudioClip,
			Resources.Load ("totalScore_13") as AudioClip,
			Resources.Load ("totalScore_14") as AudioClip,
			Resources.Load ("totalScore_15") as AudioClip,
			Resources.Load ("totalScore_16") as AudioClip,
			Resources.Load ("totalScore_17") as AudioClip,
			Resources.Load ("totalScore_18") as AudioClip,
			Resources.Load ("totalScore_19") as AudioClip,
			Resources.Load ("totalScore_20") as AudioClip,
		};
	}
	
	// Update is called once per frame
	void Update () {

			valueBlink = oscex.blink;
			valueMellow = oscex.mellowScore;

		Feedback();

	}		// VOID UPDATE

//		public IEnumerator Timer() {
//		yield return new WaitForSeconds (2.0f);
//		Debug.Log ("Hello World");
//	}

	public void Blink() {
		isValueBlink = Random.Range (0,1);
		
		if (isValueBlink == 1) {
			Debug.Log ("Blinked");
			current_score++;
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = singleFeedbackPos[Random.Range (0,singleFeedbackPos.Length)];
				GetComponent<AudioSource>().Play();	
			}
		}
	}
	
	public void Feedback() {
		//Debug.Log ("upval is " + oscex.upValue);
		if (number_of_turns == 0 && Input.GetKeyDown (KeyCode.Return)) {
			playInstructions = true;
			canStartGame = true;
			
			Debug.Log ("Instructions here");
			
			if (playInstructions) {
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotInstructions[0];
					GetComponent<AudioSource>().Play();
				}
			}
			else if (Input.GetKeyDown (KeyCode.Space)){
				GetComponent<AudioSource>().Pause ();
			}
		}
		else if (Input.GetKeyDown (KeyCode.X)) {
			playInstructions = false;
		}
		
		if (number_of_turns  <= 19 && canStartGame == true && endGame == false && Input.GetKeyDown (KeyCode.Space)) {
			selectCommandType = true;													// Turns on randomly generated command type
			
			randomCommandType = commandType[Random.Range(0, commandType.Length)];		// Stores randomly chosen command type
			
			if (randomCommandType == "mellow") {
				playCommandMellow = true;
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandMellow[Random.Range (0,robotCommandMellow.Length)];
					GetComponent<AudioSource>().Play();	
				}	

				current_score++;
				number_of_turns++;
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/turn", new List<int> { number_of_turns });
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/score", new List<int> { current_score });
			}
			else if (randomCommandType == "headtiltdown") {
				playCommandHeadTilts = true;
				number_of_turns++;
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandHeadTiltsDown[Random.Range (0,robotCommandHeadTiltsDown.Length)];
					GetComponent<AudioSource>().Play();	
				}	
				current_score++;
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/turn", new List<int> { number_of_turns });
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/score", new List<int> { current_score });
			}
			else if (randomCommandType == "headtiltup") {
				playCommandHeadTilts = true;
				number_of_turns++;
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandHeadTiltsUp[Random.Range (0,robotCommandHeadTiltsUp.Length)];
					GetComponent<AudioSource>().Play();	
				}	
				current_score++;
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/turn", new List<int> { number_of_turns });
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/score", new List<int> { current_score });
			}
			else if (randomCommandType == "headtiltleft") {
				playCommandHeadTilts = true;
				number_of_turns++;
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandHeadTiltsLeft[Random.Range (0,robotCommandHeadTiltsLeft.Length)];
					GetComponent<AudioSource>().Play();	
				}	
				current_score++;
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/turn", new List<int> { number_of_turns });
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/score", new List<int> { current_score });
			}
			else if (randomCommandType == "headtiltright") {
				playCommandHeadTilts = true;
				number_of_turns++;
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandHeadTiltsRight[Random.Range (0,robotCommandHeadTiltsRight.Length)];
					GetComponent<AudioSource>().Play();	
				}	
				current_score++;
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/turn", new List<int> { number_of_turns });
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/score", new List<int> { current_score });
			}
			else if (randomCommandType == "blink") {
				playCommandBlink = true;
				number_of_turns++;
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandBlink[Random.Range (0,robotCommandBlink.Length)];
					GetComponent<AudioSource>().Play();	
				}

				Blink ();
				//Debug.Log(oscex.blink); 
				//}
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/turn", new List<int> { number_of_turns });
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/score", new List<int> { current_score });
			}
			else if (randomCommandType == "noblink") {
				playCommandNoBlink = true;
				number_of_turns++;
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandNoBlink[Random.Range (0,robotCommandNoBlink.Length)];
					GetComponent<AudioSource>().Play();	
				}

				if (valueBlink == 0) {
					current_score++;
				}
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/turn", new List<int> { number_of_turns });
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/score", new List<int> { current_score });
			}
			else if (randomCommandType == "speak") {
				playCommandSpeak = true;
				number_of_turns++;
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandSpeak[Random.Range (0,robotCommandSpeak.Length)];
					GetComponent<AudioSource>().Play();	
				}		
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/turn", new List<int> { number_of_turns });
				//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/score", new List<int> { current_score });
			}
			
			//TEST
			Debug.Log ("TURN# " + number_of_turns + " | " + "TYPE: " + randomCommandType + " | " + "SCORE: " + current_score);
		}
		
		selectCommandType = false;
		
		// Is it the end of the game?
		if (number_of_turns == 20) {
			endGame = true;
		}
		else {
			endGame = false;
		}
		
		if (endGame && Input.GetKeyDown (KeyCode.E)) {
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallCalculating[0];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (endGame && Input.GetKeyDown (KeyCode.T)) {
			//FEEDBACK
			if (current_score > 14) {
				Debug.Log ("Full Win");
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = totalFeedback[0];
					GetComponent<AudioSource>().Play();	
				}
			}
			else if (current_score < 15 && current_score > 9) {
				Debug.Log ("Medium Win");
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = totalFeedback[1];
					GetComponent<AudioSource>().Play();	
				}
			}
			else if (current_score < 11 && current_score > 4) {
				Debug.Log ("Medium Lose");
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = totalFeedback[2];
					GetComponent<AudioSource>().Play();	
				}
			}
			else if (current_score < 6 && current_score >= 0) {
				Debug.Log ("Full lose");
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = totalFeedback[3];
					GetComponent<AudioSource>().Play();	
				}
			}
		}
		
		if (endGame && Input.GetKeyDown (KeyCode.R)) {
			if (current_score == 0 || current_score == 1) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[0];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 2) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[1];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 3) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[2];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 4) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[3];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 5) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[4];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 6) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[5];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 7) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[6];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 8) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[7];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 9) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[8];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 10) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[9];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 11) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[10];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 12) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[11];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 13) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[12];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 14) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[13];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 15) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[14];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 16) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[15];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 17) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[16];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 18) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[17];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 19) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[18];
					GetComponent<AudioSource>().Play();	
				}
			}
			
			if (current_score == 20) {
				Debug.Log ("END SCORE: " + current_score);
				
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = overallTotal[19];
					GetComponent<AudioSource>().Play();	
				}
			}
		}
	}
}
	