using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainGUI06 : MonoBehaviour {

	// create a squirrel functions
	
	int makeASquirrelStep;	// this variable manages which section of the creation process is presented
							// it is simply incremented as the steps progress
	int spendPoints;		// points available to spend on attributes
	
	string mySqName;		// currently player selected values

	int mySqRun;
	int mySqJump;
	int mySqClimb;
	int mySqHealth;
	int mySqAcorns;

	float buttonHeight = Screen.height * 0.07f;
	float buttonWidth = Screen.width * 0.07f;
	
	string[] qualityText = new string[6];

	public float textAreaX;			// variables to set up the display box
	public float textAreaY;
	public float textAreaWidth;
	public float textAreaHeight;

	float calcAreaX;
	float calcAreaY;
	float calcAreaWidth;
	float calcAreaHeight;

	int prevTutorialStep;

	bool readyToConfirm = true;		// flag to check if all values selected before confirmation
	bool failedOnName = false;
	bool failedOnStats = false;

	public Texture myBackButton;
	public Texture myNextButton;
	public Texture myConfirmButton;
	public Texture myGraySqButton;
	public Texture myRedSqButton;
	public Texture myAddButton;
	public Texture mySubtractButton;
	public Texture myNameButton;
	public Texture myPointsButton;
	public GUIStyle myButtonStyle;



	// Use this for initialization
	void Start () {
		MasterObjectList.initialStore.Clear();
		MasterObjectList.CheckAndCreateInitialItemLists ();

		SquirrelPlayerCharacter.setSqPCName (CreatePCSquirrel.pcSquirrel, "");
		SquirrelPlayerCharacter.setSqPCPlayer (CreatePCSquirrel.pcSquirrel, "");
		SquirrelPlayerCharacter.setSqPCType (CreatePCSquirrel.pcSquirrel, 1);
		SquirrelPlayerCharacter.setSqPCRun (CreatePCSquirrel.pcSquirrel, 0);
		SquirrelPlayerCharacter.setSqPCJump (CreatePCSquirrel.pcSquirrel, 0);
		SquirrelPlayerCharacter.setSqPCClimb (CreatePCSquirrel.pcSquirrel, 0);
		SquirrelPlayerCharacter.setSqPCHealth (CreatePCSquirrel.pcSquirrel, 0);
		SquirrelPlayerCharacter.setSqPCAcorns (CreatePCSquirrel.pcSquirrel, 0);

		CreatePCSquirrel.pcSquirrel.squirrelAcornsInHand = 0;
		CreatePCSquirrel.pcSquirrel.squirrelAcornsSaved = 40 + Random.Range(1,21);

		SquirrelPlayerCharacter.clearItemsWorn (CreatePCSquirrel.pcSquirrel);

		CreatePCSquirrel.pcSquirrel.squirrelLastWorldX = 7.5f;
		CreatePCSquirrel.pcSquirrel.squirrelLastWorldY = 2.0f;
		
		CreatePCSquirrel.pcSquirrel.squirrelBestTimeAdv01 = 3599.0f; // 3599 seconds = 59 minutes, 59 seconds

		makeASquirrelStep = 0;
		prevTutorialStep = -1;
		InitialStoreFunctions.displayInventoryContents = false;

		mySqName = "";				// default squirrel values
		mySqRun = 2;
		mySqJump = 2;
		mySqClimb = 2;
		mySqHealth = 4;
		mySqAcorns = 50 + Random.Range(1, 6) + Random.Range(1, 6) + Random.Range(1, 6);

		spendPoints = 0;

		qualityText [0] = "Dismal";
		qualityText [1] = "Poor";
		qualityText [2] = "Average";
		qualityText [3] = "Good";
		qualityText [4] = "Excellent";
		qualityText [5] = "Super";
	}

	void Update () {
		calcAreaX = Screen.width * textAreaX;
		calcAreaY = Screen.height * textAreaY;
		calcAreaWidth = Screen.width * textAreaWidth;
		calcAreaHeight = Screen.height * textAreaHeight;
	}

	void OnGUI () {
		GUI.skin.button.alignment = TextAnchor.MiddleCenter;
		GUI.skin.button.richText = true;
		GUI.skin.button.fontSize = Screen.width/1536*36;						
		GUI.skin.button.fontStyle = FontStyle.Normal;
		GUI.skin.button.wordWrap = true;

		GUI.skin.label.fontSize = Screen.width / 1536 * 48;
		GUI.skin.label.alignment = TextAnchor.UpperLeft;
		GUI.skin.label.wordWrap = true;
		GUI.skin.label.fixedWidth = 0;
		GUI.skin.label.richText = true;
		GUI.skin.label.fontStyle = FontStyle.Normal;

		GUI.skin.textField.fontSize = Screen.width / 1536 * 48;
		GUI.skin.textField.fixedWidth = Screen.width*2/10;

		GUILayout.BeginArea(new Rect (calcAreaX, calcAreaY, calcAreaWidth, calcAreaHeight));
		GUI.skin.label.fontSize = Screen.width / 1536 * 64;

		// first step in creating the squirrel - name
		if (makeASquirrelStep == 0) {


			GUILayout.Label ("Name Your New Squirrel!\n");
			GUI.skin.label.fontSize = Screen.width / 1536 * 48;

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Squirrel Name: "); //, GUILayout.Width (350));
			mySqName = GUILayout.TextField (mySqName);
			GUILayout.EndHorizontal ();

			GUILayout.Label ("\nTap the Next button for the next step. If you change your mind, you can come back to this screen by tapping the Back button.");
		}

		//next step in creating the squirrel - choose the model
		if (makeASquirrelStep == 1) {
			GUI.skin.label.fontSize = Screen.width / 1536 * 64;
			GUILayout.Label ("Choose what type of squirrel to play. \n");

			GUILayout.BeginHorizontal ();
			if (GUILayout.Button (myGraySqButton, myButtonStyle)
				) {
				SoundEffects.PlayMechClick ();
				SquirrelPlayerCharacter.setSqPCType (CreatePCSquirrel.pcSquirrel, 1);
			}

			if (GUILayout.Button (myRedSqButton, myButtonStyle)
				) {
				SoundEffects.PlayMechClick ();
				SquirrelPlayerCharacter.setSqPCType (CreatePCSquirrel.pcSquirrel, 2);
			}
			GUILayout.EndHorizontal ();

			GUI.skin.label.fontSize = Screen.width / 1536 * 36;
			GUILayout.Label ("\nMore types will become available after a future expansion.");
			GUI.skin.label.fontSize = Screen.width / 1536 * 48;
		}

		//next step in creating the squirrel - choose abilities
		if (makeASquirrelStep == 2) {

			// turn off inventory screen (if coming here from the back button)
			StartCoroutine (DisplayInventoryAfterFrame(false));

			GUI.skin.label.fontSize = Screen.width / 1536 * 64;
			GUILayout.Label ("Choose the quality of your four stats. \n");
			GUI.skin.label.fontSize = Screen.width / 1536 * 48;

			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Points Remaining:", GUILayout.Width(Screen.width * 0.25f)); //, GUILayout.Width(450));
			GUILayout.Label (spendPoints.ToString(), GUILayout.Width(Screen.width * 0.1f));
			GUILayout.EndHorizontal ();
			
			// begin displaying Run value
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Run:", GUILayout.Width(Screen.width * 0.1f));
			GUILayout.Label(mySqRun.ToString(), GUILayout.Width(Screen.width * 0.05f));

			// +/- buttons
			if (GUILayout.Button (mySubtractButton, myButtonStyle, GUILayout.Height(buttonHeight))
			    ) {
				if ((mySqRun > 1) && (spendPoints < 6)) {
					SoundEffects.PlayMechClick ();
					mySqRun -= 1;
					spendPoints += 1;
				} else {
					SoundEffects.PlayHuhUnh ();
				}
			}

			GUILayout.Space (25);
			if (GUILayout.Button (myAddButton, myButtonStyle, GUILayout.Height(buttonHeight))
				) {
				if ((mySqRun < 4) && (spendPoints > 0)) {
					SoundEffects.PlayMechClick ();
					mySqRun += 1;
					spendPoints -= 1;
				} else {
					SoundEffects.PlayHuhUnh ();
				}			
			}

			GUILayout.Space (25);
			GUILayout.Label(qualityText[mySqRun], GUILayout.Width(Screen.width * 0.25f));
			GUILayout.EndHorizontal ();

			// begin displaying Jump value
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Jump:", GUILayout.Width(Screen.width * 0.1f));
			GUILayout.Label(mySqJump.ToString(), GUILayout.Width(Screen.width * 0.05f));
			
			if (GUILayout.Button (mySubtractButton, myButtonStyle, GUILayout.Height(buttonHeight))
				) {
				if ((mySqJump > 1) && (spendPoints < 6)) {
					SoundEffects.PlayMechClick ();
					mySqJump -= 1;
					spendPoints += 1;
				} else {
					SoundEffects.PlayHuhUnh ();
				}			
			}
			
			GUILayout.Space (25);
			if (GUILayout.Button (myAddButton, myButtonStyle, GUILayout.Height(buttonHeight))
				) {
				if ((mySqJump < 4) && (spendPoints > 0)) {
					SoundEffects.PlayMechClick ();
					mySqJump += 1;
					spendPoints -= 1;
				} else {
					SoundEffects.PlayHuhUnh ();
				}	
			}
			
			GUILayout.Space (25);
			GUILayout.Label(qualityText[mySqJump], GUILayout.Width(Screen.width * 0.25f));
			GUILayout.EndHorizontal ();

			// begin displaying Climb value
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Climb:", GUILayout.Width(Screen.width * 0.1f));
			GUILayout.Label(mySqClimb.ToString(), GUILayout.Width(Screen.width * 0.05f));
			
			if (GUILayout.Button (mySubtractButton, myButtonStyle, GUILayout.Height(buttonHeight))
				) {
				if ((mySqClimb > 1) && (spendPoints < 6)) {
					SoundEffects.PlayMechClick ();
					mySqClimb -= 1;
					spendPoints += 1;
				} else {
					SoundEffects.PlayHuhUnh ();
				}				
			}
			
			GUILayout.Space (25);
			if (GUILayout.Button (myAddButton, myButtonStyle, GUILayout.Height(buttonHeight))
				) {
				if ((mySqClimb < 4) && (spendPoints > 0)) {
					SoundEffects.PlayMechClick ();
					mySqClimb += 1;
					spendPoints -= 1;
				} else {
					SoundEffects.PlayHuhUnh ();
				}
			}
			
			GUILayout.Space (25);
			GUILayout.Label(qualityText[mySqClimb], GUILayout.Width(Screen.width * 0.25f));
			GUILayout.EndHorizontal ();
			
			// begin displaying Health value
			GUILayout.BeginHorizontal ();
			GUILayout.Label ("Health:", GUILayout.Width(Screen.width * 0.1f));
			GUILayout.Label(mySqHealth.ToString(), GUILayout.Width(Screen.width * 0.05f));
			
			if (GUILayout.Button (mySubtractButton, myButtonStyle, GUILayout.Height(buttonHeight))
				) {
				if ((mySqHealth > 1) && (spendPoints < 6)) {
					SoundEffects.PlayMechClick ();
					mySqHealth -= 1;
					spendPoints += 1;
				} else {
					SoundEffects.PlayHuhUnh ();
				}			
			}
			
			GUILayout.Space (25);
			if (GUILayout.Button (myAddButton, myButtonStyle, GUILayout.Height(buttonHeight))
				) {
				if ((mySqHealth < 4) && (spendPoints > 0)) {
					SoundEffects.PlayMechClick ();
					mySqHealth += 1;
					spendPoints -= 1;
				} else {
					SoundEffects.PlayHuhUnh ();
				}
			}
			
			GUILayout.Space (25);
			GUILayout.Label(qualityText[mySqHealth], GUILayout.Width(Screen.width * 0.25f));
			GUILayout.EndHorizontal ();
			GUI.skin.label.fontSize = Screen.width / 1536 * 48;

		} // end choose abilities

		//next step in creating the squirrel - acquire clothes
		if (makeASquirrelStep == 3) {
			
			GUI.skin.label.fontSize = Screen.width / 1536 * 64;
			GUILayout.Label ("Choose your initial clothing. \n");
			GUI.skin.label.fontSize = Screen.width / 1536 * 48;

			StartCoroutine (DisplayInventoryAfterFrame(true));
			
		} // end acquire clothes


		//next step in creating the squirrel - confirm build
		if (makeASquirrelStep == 4) {

			// turn off inventory screen 
			StartCoroutine (DisplayInventoryAfterFrame(false));
			
			GUI.skin.label.fontSize = Screen.width / 1536 * 64;
			GUILayout.Label ("Confirm your squirrel build! \n");
			GUI.skin.label.fontSize = Screen.width / 1536 * 48;
			
			GUILayout.BeginHorizontal ();
			GUILayout.Label("Name:", GUILayout.Width(Screen.width * 0.1f));
			GUILayout.Label(mySqName);
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label("Run:", GUILayout.Width(Screen.width * 0.1f));
			GUILayout.Label(mySqRun.ToString(), GUILayout.Width(Screen.width * 0.05f));
			GUILayout.Label(qualityText[mySqRun], GUILayout.Width(Screen.width * 0.25f));
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			GUILayout.Label("Jump:", GUILayout.Width(Screen.width * 0.1f));
			GUILayout.Label(mySqJump.ToString(), GUILayout.Width(Screen.width * 0.05f));
			GUILayout.Label(qualityText[mySqJump], GUILayout.Width(Screen.width * 0.25f));
			GUILayout.EndHorizontal ();
			
			GUILayout.BeginHorizontal ();
			GUILayout.Label("Climb:", GUILayout.Width(Screen.width * 0.1f));
			GUILayout.Label(mySqClimb.ToString(), GUILayout.Width(Screen.width * 0.05f));
			GUILayout.Label(qualityText[mySqClimb], GUILayout.Width(Screen.width * 0.25f));
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label("Health:", GUILayout.Width(Screen.width * 0.1f));
			GUILayout.Label(mySqHealth.ToString(), GUILayout.Width(Screen.width * 0.05f));
			GUILayout.Label(qualityText[mySqHealth], GUILayout.Width(Screen.width * 0.25f));
			GUILayout.EndHorizontal ();

			GUILayout.BeginHorizontal ();
			GUILayout.Label("Starting Acorns: ", GUILayout.Width(Screen.width * 0.2f));
			GUILayout.Label(mySqAcorns.ToString());
			GUILayout.EndHorizontal ();
			
		} //end confirm

		// error on confirm
		if (makeASquirrelStep == 5) {
			if (failedOnName) {
				GUILayout.Space (Screen.height * 0.125f);
				if (GUILayout.Button (myNameButton, myButtonStyle)
					) {
					SoundEffects.PlayMechClick ();
					makeASquirrelStep = 0;
					mySqName = CreatePCSquirrel.SelectRandomSquirrelName ();
				}
			}
			
			if (failedOnStats && !failedOnName) {
				GUILayout.Space (Screen.height * 0.125f);
				if (GUILayout.Button (myPointsButton, myButtonStyle)
					) {
					SoundEffects.PlayMechClick ();
					makeASquirrelStep = 2;
				}
			}
		}

		GUILayout.EndArea();

		// display back button
		if (makeASquirrelStep > 0) {
			GUILayout.BeginArea(new Rect (Screen.width*3/5, Screen.height*0.9f, Screen.width*1/5-25, Screen.height * 0.085f));

			if (GUILayout.Button (myBackButton, myButtonStyle)
				) {
				SoundEffects.PlayMechClick ();
				makeASquirrelStep -= 1;
			}
			GUILayout.EndArea();

		}
		
		// display next button
		if (makeASquirrelStep < 4) {
			GUILayout.BeginArea(new Rect (Screen.width*4/5, Screen.height*0.9f, Screen.width*1/5-25, Screen.height * 0.085f));

			if (GUILayout.Button (myNextButton, myButtonStyle)
				) {
				SoundEffects.PlayMechClick ();
				makeASquirrelStep += 1;
			}
			GUILayout.EndArea();

		}

		GUI.skin.button.fontSize = Screen.width/1536*48;
		if (makeASquirrelStep == 4) {
			readyToConfirm = true;
			failedOnName = false;
			failedOnStats = false;
			GUILayout.BeginArea(new Rect (Screen.width*4/5, Screen.height*0.9f, Screen.width*1/5-25, Screen.height * 0.085f));

			if (GUILayout.Button (myConfirmButton, myButtonStyle)
				) {

				// check if name is entered
				if (mySqName == "") {
					SoundEffects.PlayHuhUnh ();
					makeASquirrelStep = 5;
					readyToConfirm = false;
					failedOnName = true;
				}

				// check if attribute points spent
				if (spendPoints != 0) {
					SoundEffects.PlayHuhUnh ();
					makeASquirrelStep = 5;
					readyToConfirm = false;
					failedOnStats = true;
				}

				// if everything is good, set the squirrel values, initialize the items lists, save the info and load the first play scene
				if (readyToConfirm) {
					SoundEffects.PlayMechClick ();
					// apply choices to the squirrelPC class
					// -- type applied above				SquirrelPlayerCharacter.setSqPCType (CreatePCSquirrel.pcSquirrel, 0);
					// -- user not yet implemented			SquirrelPlayerCharacter.setSqPCPlayer (CreatePCSquirrel.pcSquirrel, "");
					SquirrelPlayerCharacter.setSqPCName (CreatePCSquirrel.pcSquirrel, mySqName);
					SquirrelPlayerCharacter.setSqPCRun (CreatePCSquirrel.pcSquirrel, mySqRun);
					SquirrelPlayerCharacter.setSqPCJump (CreatePCSquirrel.pcSquirrel, mySqJump);
					SquirrelPlayerCharacter.setSqPCClimb (CreatePCSquirrel.pcSquirrel, mySqClimb);
					SquirrelPlayerCharacter.setSqPCHealth (CreatePCSquirrel.pcSquirrel, mySqHealth);
					SquirrelPlayerCharacter.setSqPCAcorns (CreatePCSquirrel.pcSquirrel, mySqAcorns);

					MasterObjectList.inventory.Clear();
					MasterObjectList.wardrobe.Clear();
					MasterObjectList.vendorStore.Clear();
					MasterObjectList.initialStore.Clear();
					MasterObjectList.CheckAndCreateInitialItemLists ();

					MostRecentPCSquirrel.SaveRecentPCSquirrel ();
					
					MostRecentGamePlaySettings.showTutorialMessages = true;
					for (int i = 0; i < TutorialManager.masterTutorialList.Count; i ++) {
						TutorialManager.masterTutorialList[i].hasBeenPresented = false;
					}
					MostRecentTutorialProgress.SaveRecentTutorialProgress ();
					
					MostRecentGamePlaySettings.SaveRecentSettings ();

					Application.LoadLevel (9);	
				}
			} // end confirm button
			GUILayout.EndArea();

		} // end if makeASquirrelStep = 4

		SetTutorialFeatures (makeASquirrelStep);

	} // end OnGUI

	IEnumerator DisplayInventoryAfterFrame (bool value) {

		// display the inventory list after the frame as completed displaying
		yield return new WaitForEndOfFrame();
		InitialStoreFunctions.displayInventoryContents = value;
	}

	void SetTutorialFeatures (int newStep) {

		if (newStep != prevTutorialStep) {

			prevTutorialStep = newStep;

			// assign a tutorial messaged based on which step is presented
			switch (newStep) {
			case 0:
				TutorialManager.whichTutorialButtonToShow = 0;
				TutorialManager.tutorialMessageKeyword = "Create00";
				break;
			case 2:
				TutorialManager.whichTutorialButtonToShow = 1;
				TutorialManager.tutorialMessageKeyword = "Create02";
				break;
			case 3:
				TutorialManager.whichTutorialButtonToShow = 2;
				TutorialManager.tutorialMessageKeyword = "Create07";
				break;
			default:
				TutorialManager.whichTutorialButtonToShow = -1;
				TutorialManager.tutorialMessageKeyword = "none";
				break;
			} // end switch

			TutorialManager.changeTutorialButton = true;
			TutorialManager.showMessageIfNew = true;

		} // end if
	} // end SetTutorialFeatures

}
