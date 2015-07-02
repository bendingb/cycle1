using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Timer : MonoBehaviour {

	public Test2 values;
	public OSCConnectionExample oscex;

	public bool timerOn = true;
	public float timeLeft = 3.0f;

	public float valueMellow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (timerOn) {

			timeLeft -= Time.deltaTime;

			if (timeLeft <= 0.0f)
			{
				//End the level here. 
				GetComponent<GUIText>().text = "You ran out of time";
				Debug.Log ("Your ran out of time");
				timerOn = false;
			}
			else
			{
				GetComponent<GUIText>().text = "Time left = " + (int)timeLeft + " seconds";
				Debug.Log (timeLeft);
				Debug.Log (oscex.mellowScore);
				}
			}
		}
}
