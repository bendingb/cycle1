using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	// SPACE - Next Turn
	// F - Indv Feedback
	// T - Final score/feedback

	public OSCConnectionExample oscex;							// To access Muse data from OSC

	public int currentTurn; 
	public int gameScore;
	public int puffScore;

	public bool canStartGame = false;			// True ONLY if player passed instructions - needed for Muse calibration

	public bool isBlinking = false;
	public bool isNoBlinking = false;
	public bool isMellow = false;

	public string[] commandType = new string[] {
		"mellow",
		"headtiltup",
		"headtiltdown",
		"headtiltleft",
		"headtiltright",
		"blink",
		"noblink",
		"speak",
	};

	public string randomCommandType = null;

	public string[] finalResult = new string[] {
		"fullWin",
		"win",
		"lose",
		"fullLose",
	};

	public int valueHeadTiltUpDown = 0;					//0 = neutral, 1 = up, 2 = down
	public int valueHeadTiltLeftRight = 0;				//0 = neutral, 1 = left, 2 = right

	// Puff Punk Magic
	public int puffHeadTiltUp = 0;
	public int puffHeadTiltDown = 0;
	public int puffHeadTiltLeft = 0;
	public int puffHeadTiltRight = 0;
	public int puffBlinking = 0;	
	public int puffNoBlinking = 0;
	public int puffMellow = 0;

	public float valueMellow;

	// INSTRUCTIONS
	public AudioClip[] robotInstructions;

	// COMMAND
	public AudioClip[] robotCommandMellow;
	public AudioClip[] robotCommandHeadTiltsDown;
	public AudioClip[] robotCommandHeadTiltsLeft;
	public AudioClip[] robotCommandHeadTiltsRight;
	public AudioClip[] robotCommandHeadTiltsUp;
	public AudioClip[] robotCommandBlink;
	public AudioClip[] robotCommandNoBlink;
	public AudioClip[] robotCommandSpeak;

	//FEEDBACK
	public AudioClip[] singleFeedback;
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

		singleFeedback = new AudioClip[]{
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
	
		valueHeadTiltUpDown = oscex.upDownDirection;
		valueHeadTiltLeftRight = oscex.leftRightDirection;
		valueMellow = oscex.mellowScore;

		if (currentTurn == 0 && Input.GetKeyDown (KeyCode.Return)) {
			canStartGame = true;

			// PLAYS BEGINNING
			Debug.Log ("Instructions are being played");
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = robotInstructions[0];
				GetComponent<AudioSource>().Play();
			}
		}

		if (currentTurn < 20 && canStartGame && Input.GetKeyDown (KeyCode.Space)) {
			CommandType ();
			
			if (randomCommandType == "mellow") {
				Mellow ();

				Debug.Log ("Do Mellow!");
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandMellow[Random.Range (0,robotCommandMellow.Length)];
					GetComponent<AudioSource>().Play();
				}
				SendtoPeter();
			}	
			else if (randomCommandType == "headtiltup") {
				HeadTiltUp ();

				Debug.Log ("Head Up!");
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandHeadTiltsUp[Random.Range (0,robotCommandHeadTiltsUp.Length)];
					GetComponent<AudioSource>().Play();	
				}
				SendtoPeter();
			}	
			else if (randomCommandType == "headtiltdown") {
				HeadTiltDown ();

				Debug.Log ("Head down!");
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandHeadTiltsDown[Random.Range (0,robotCommandHeadTiltsDown.Length)];
					GetComponent<AudioSource>().Play();	
				}	
				SendtoPeter();
			}	
			else if (randomCommandType == "headtilteleft") {
				HeadTiltLeft ();

				Debug.Log ("Head Left!");
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandHeadTiltsLeft[Random.Range (0,robotCommandHeadTiltsLeft.Length)];
					GetComponent<AudioSource>().Play();	
				}	
				SendtoPeter();
			}	
			else if (randomCommandType == "headtiltright") {
				HeadTiltRight ();

				Debug.Log ("Head Right!");
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandHeadTiltsRight[Random.Range (0,robotCommandHeadTiltsRight.Length)];
					GetComponent<AudioSource>().Play();	
				}	
				SendtoPeter();
			}	
			else if (randomCommandType == "blink") {
				Blinking();

				Debug.Log ("Blink!");
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandBlink[Random.Range (0,robotCommandBlink.Length)];
					GetComponent<AudioSource>().Play();	
				}
				SendtoPeter();
			}
			else if (randomCommandType == "noblink") {
				NoBlinking();

				Debug.Log ("No Blink!");
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandNoBlink[Random.Range (0,robotCommandNoBlink.Length)];
					GetComponent<AudioSource>().Play();	
				}
				SendtoPeter();
			}
			else if (randomCommandType == "speak") {
				currentTurn++;

				Debug.Log ("Speak!");
				if (!GetComponent<AudioSource>().isPlaying) {
					GetComponent<AudioSource>().clip = robotCommandSpeak[Random.Range (0,robotCommandSpeak.Length)];
					GetComponent<AudioSource>().Play();	
				}	
				SendtoPeter();
			}
		}

		if (Input.GetKeyDown (KeyCode.F)) {
			RandomFeedback();
		}

		if (currentTurn == 20 && Input.GetKeyDown (KeyCode.T)) {
			FinalScore ();
		}
	}

	public void SendtoPeter() {
		//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/mellow", new List<float> { valueMellow });
		//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/turn", new List<int> { currentTurn });
		//OSCHandler.Instance.SendMessageToClient(OSCConnectionExample.ClientName, "/creature/score", new List<int> { gameScore });
	}

	// SELECTORS - RANDOM COMMANDS
	public void CommandType() {
		randomCommandType = commandType[Random.Range(0, commandType.Length)];		// Randomly chosen command type
	}

	public void RandomFeedback() {
		if (!GetComponent<AudioSource>().isPlaying) {
			GetComponent<AudioSource>().clip = singleFeedback[Random.Range (0,singleFeedback.Length)];
			GetComponent<AudioSource>().Play();	
		}	
	}
	
	// COMMANDS - HEAD TILT UP
	public void HeadTiltUp() {
		puffHeadTiltUp = Random.Range (0,2);

		if (valueHeadTiltUpDown == 1 || puffHeadTiltUp == 1) {
			gameScore++;
			currentTurn++;
		}
		else if (valueHeadTiltUpDown == 2 || puffHeadTiltUp == 2) {
			currentTurn++;
		}
		else {
			currentTurn++;
		}
	}

	// COMMANDS - HEAD TILT DOWN
	public void HeadTiltDown() {
		puffHeadTiltDown = Random.Range (0,2);

		if (valueHeadTiltUpDown == 2 || puffHeadTiltDown == 2) {
			gameScore++;
			currentTurn++;
		}
		else if (valueHeadTiltUpDown == 1 || puffHeadTiltDown == 1) {
			currentTurn++;
		}
		else {
			currentTurn++;
		}
	}

	// COMMANDS - HEAD TILT LEFT
	public void HeadTiltLeft() {
		puffHeadTiltLeft = Random.Range (0,2);
		
		if (valueHeadTiltLeftRight == 1 || puffHeadTiltLeft == 1) {
			gameScore++;
			currentTurn++;
		}
			else if (valueHeadTiltLeftRight == 2 || puffHeadTiltLeft == 2) {
			currentTurn++;
		}
		else {
			currentTurn++;
		}
	}
		
	// COMMANDS - HEAD TILT RIGHT
	public void HeadTiltRight() {
		puffHeadTiltRight = Random.Range (0,2);
			
		if (valueHeadTiltLeftRight == 2 || puffHeadTiltRight == 2) {
			gameScore++;
			currentTurn++;
		}
		else if (valueHeadTiltLeftRight == 1 || puffHeadTiltRight == 1) {
			currentTurn++;
		}
		else {
			currentTurn++;
		}
	}

	// COMMANDS - BLINKING
	public void Blinking() {
		puffBlinking = Random.Range(0,2);

		if (puffBlinking == 1) {
			isBlinking = true;
		}
		else {
			isBlinking = false;
		}

		if (isBlinking) {
			gameScore++;
			currentTurn++;
		}
		else {
			currentTurn++;
		}
	}

	// COMMANDS - NO BLINKING
	public void NoBlinking() {
		puffNoBlinking = Random.Range(0,2);
		
		if (puffNoBlinking == 1) {
			isNoBlinking = false;
		}
		else {
			isNoBlinking = true;
		}

		if (!isNoBlinking) {
			gameScore++;
			currentTurn++;
		}
		else {
			currentTurn++;
		}
	}

	// COMMANDS - MELLOW
	public void Mellow() {
		puffMellow = Random.Range(0,2);

		if (puffMellow == 1) {
			isMellow = true;
		}
		else {
			isMellow = false;
		}
		if (isMellow) {
			gameScore++;
			currentTurn++;
		}
		else {
			currentTurn++;
		}
	}// FEEDBACK - FINAL SCORE
	public void FinalScore() {
		puffScore = Random.Range (0,21);
	
		if (puffScore == 0 || puffScore == 1) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[0];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 2) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[1];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 3) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[2];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 4) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[3];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 5) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[4];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 6) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[5];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 7) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[6];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 8) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[7];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 9) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[8];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 10) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[9];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 11) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[10];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 12) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[11];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 13) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[12];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 14) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[13];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 15) {
			Debug.Log ("END SCORE: " + gameScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[14];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 16) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[15];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 17) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[16];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 18) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[17];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 19) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[18];
				GetComponent<AudioSource>().Play();	
			}
		}
		
		if (puffScore == 20) {
			Debug.Log ("END SCORE: " + puffScore);
			
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = overallTotal[19];
				GetComponent<AudioSource>().Play();	
			}
		}
	}
}
