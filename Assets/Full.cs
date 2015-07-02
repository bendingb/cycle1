using UnityEngine;
using System.Collections;

public class Full : MonoBehaviour {
	public int numberOfPlayers = 0;
	public int gameTurn = 0;

	// COMMANDS - Types
	public string[] commandType = new string[] {"mellow","headtilts","blink"};

	// SOUND - Turn on/off
	public bool playSoundInstructions = true;			// DONE
	public bool playSoundCommandPos = true;				// DONE
	public bool playSoundCommandNeg = true;
	public bool playSoundEndOfFeedbackPosPos = true;
	public bool playSoundEndOfFeedbackPos = true;
	public bool playSoundEndOfFeedbackNeg = true;
	public bool playSoundEndOfFeedbackNegNeg = true;

	// SOUND - Folders
	public AudioClip[] robotInstructions;				// DONE
	public AudioClip[] robotCommandMellow;				// DONE
	public AudioClip[] robotCommandHeadTilts;			// DONE
	public AudioClip[] robotCommandBlink;				// DONE
	public AudioClip[] indvCommandFeedbackPos;			// DONE
	public AudioClip[] indvCommandFeedbackNeg;
	public AudioClip[] endOfGameFeedbackPosPos;
	public AudioClip[] endOfGameFeedbackPos;
	public AudioClip[] endOfGameFeedbackNeg;
	public AudioClip[] endOfGameFeedbackNegNeg;

	// Use this for initialization
	void Start () {

		// INSTRUCTIONS 
		robotInstructions = new AudioClip[]{
			Resources.Load ("instructions") as AudioClip,
		};

		if (!GetComponent<AudioSource>().playOnAwake) {
			GetComponent<AudioSource>().clip = robotInstructions[0];
			GetComponent<AudioSource>().Play();
		}

		/// COMMANDS
		robotCommandMellow = new AudioClip[]{
			Resources.Load ("robotComand_01") as AudioClip,
			Resources.Load ("robotComand_02") as AudioClip,
			Resources.Load ("robotComand_03") as AudioClip,
		};
		
		if (!GetComponent<AudioSource>().playOnAwake) {
			GetComponent<AudioSource>().clip = robotCommandMellow[Random.Range (0, robotCommandMellow.Length)];
			GetComponent<AudioSource>().Play();
		}

		// FEEDBACK - Individual Command Feedback Positive
		indvCommandFeedbackPos = new AudioClip[]{
			Resources.Load ("indvfeedbackp_01") as AudioClip,
			Resources.Load ("indvfeedbackp_02") as AudioClip,
			Resources.Load ("indvfeedbackp_03") as AudioClip,
		};

		if (!GetComponent<AudioSource>().playOnAwake) {
			GetComponent<AudioSource>().clip = indvCommandFeedbackPos [Random.Range (0, indvCommandFeedbackPos.Length)];
			GetComponent<AudioSource>().Play();
		}

		// FEEDBACK - Individual Command Feedback Negative

		// END FEEDBACK - Overall Feedback Positive Positive

		// END FEEDBACK - Overall Feedback Positive

		// END FEEDBACK - Overall Feedback Negative

		// END FEEDBACK - Overall Feedback Negative Negative
	}


	// Update is called once per frame
	void Update () {
	// RANDOM COMMAND TYPE

	// Player Selector 
	if (Input.GetKeyDown (KeyCode.Alpha1)) {
			numberOfPlayers = 1;
			Debug.Log ("Number of Players is: " + numberOfPlayers);
		}
	else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			numberOfPlayers = 2;
			Debug.Log ("Number of Players is: " + numberOfPlayers);
		}
	else if (Input.GetKeyDown (KeyCode.Alpha3)) {
		numberOfPlayers = 3;
		Debug.Log ("Number of Players is: " + numberOfPlayers);
	}
	else if (Input.GetKeyDown (KeyCode.Alpha4)) {
		numberOfPlayers = 4;
		Debug.Log ("Number of Players is: " + numberOfPlayers);
	}
	
	if (Input.GetKeyDown (KeyCode.Return)) {
			gameTurn++;
			Debug.Log ("This is turn number: " + gameTurn);
		}

		// INSTRUCTIONS
		if (Input.GetKeyDown(KeyCode.Return)) {
			playSoundInstructions = true;
		}
		else {
			playSoundInstructions = false;
		}

		if (playSoundInstructions) {
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = robotInstructions[0];
				GetComponent<AudioSource>().Play();
			}

		// INDIVIDUAL COMMAND FEEDBACK POSITIVE
		if (Input.GetKeyDown(KeyCode.Space)) {
			playSoundCommandPos = true;
		}
		else {
			playSoundCommandPos = false;
		}

		if (playSoundCommandPos) {
			if (!GetComponent<AudioSource>().isPlaying) {
				GetComponent<AudioSource>().clip = indvCommandFeedbackPos[Random.Range (0,indvCommandFeedbackPos.Length)];
				GetComponent<AudioSource>().Play();	
			}
		}

		}
	}
}
