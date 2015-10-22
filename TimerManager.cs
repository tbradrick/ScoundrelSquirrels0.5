using UnityEngine;
using System.Collections;

public class TimerManager : MonoBehaviour {

	// keep track of the time in the adventure

	// flags to keep track of what part of the race is currently happening
	public static bool startTimer = false;	
	bool continueTiming = false;
	public static bool endTimer = false;

	// flags accessed in AwardsAdventure script to choose awards
	public static bool raceOver = false;
	public static float racingTime;

	float startTime;
	float endTime;
	public GUIText timerDisplay;

	float elapsedTime;


	// Update is called once per frame
	void Update () {

		if (startTimer) {
			startTime = Time.time;
			startTimer = false;
			continueTiming = true;

		}

		if (continueTiming) {
			elapsedTime = Time.time - startTime;
			timerDisplay.text = ConvertSecondsToClockString (elapsedTime);		
		}

		if (endTimer) {
			continueTiming = false;
			endTimer = false;
			raceOver = true;

			endTime = Time.time;
			racingTime = endTime - startTime;

			if (racingTime < CreatePCSquirrel.pcSquirrel.squirrelBestTimeAdv01) {
				CreatePCSquirrel.pcSquirrel.squirrelBestTimeAdv01 = racingTime;
			}
			BestsInfo.SetBestAdv01Time ();

			
		}
	
	}

	public static string ConvertSecondsToClockString (float givenTimeInSeconds) {

		// convert the time value to a clock-like text string

		string timerText;
		int minutes;
		int seconds;

		minutes = Mathf.FloorToInt(givenTimeInSeconds / 60);
		seconds = Mathf.FloorToInt(givenTimeInSeconds - minutes * 60);

		if (minutes < 10) {
			timerText = "0" + minutes.ToString();
		} else {
			timerText = minutes.ToString();
		}
		
		timerText += ":";
		
		if (seconds < 10) {
			timerText += "0" + seconds.ToString();
		} else {
			timerText += seconds.ToString();
		}

		return timerText;

	}


}
