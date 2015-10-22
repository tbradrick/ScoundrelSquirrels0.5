using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WardrobeFunctions : MonoBehaviour {

	// show the wardrobe list when in the home

	List <SSGameItem> invListUsed = new List<SSGameItem>();

	public static bool displayWardrobeContents = false;
	bool showTutorialFirstTimeOpened = true;

	public GameObject invBGObject;
	public GameObject invBGSprite;
	public GameObject exitButtonObject;
	public GameObject exitButtonSprite;

	public float invWinXPos;
	public float invWinYPos;
	public float invWinWidth;
	public float invWinHeight;

	Vector2 scrollPosition;
	Touch myTouch;
	int objectListSize;

	float inventoryWindowX;
	float inventoryWindowY;
	float inventoryWindowWidth;
	float inventoryWindowHeight;

	float buttonSize = Screen.height/9.0f;

	int i;
	bool isItInTheList;
	SSGameItem itemImLookingFor;
	SSGameItem prevItemWorn;

	string itemNameForList;
	string benefitMessage;

	// Use this for initialization
	void Start () {
		inventoryWindowX = Screen.width * invWinXPos;
		inventoryWindowY = Screen.height * invWinYPos;
		inventoryWindowWidth = Screen.width * invWinWidth;
		inventoryWindowHeight = Screen.height * invWinHeight;

		invListUsed = MasterObjectList.wardrobe;
		if (CreatePCSquirrel.pcSquirrel.squirrelAcornsSaved == 0) {
			invListUsed.Remove (MasterObjectList.masterItemList.Find (item => item.itemName == "My Acorn Stash"));
		}
		invListUsed.Sort ((a,b) => a.itemID.CompareTo(b.itemID));
	}

	// Update is called once per frame
	void Update () {

		if (InventoryFunctions.displayInventoryContents) {
			displayWardrobeContents = false;
		}

		if (displayWardrobeContents) { 

			// show the related tutorial message if it has not been shown before
			if (showTutorialFirstTimeOpened) {
				if (MostRecentGamePlaySettings.showTutorialMessages) {
					TutorialManager.tutorialMessageKeyword = "Wardrobe00";
					TutorialManager.showMessageIfNew = true;
				}
				showTutorialFirstTimeOpened = false;
			}

			// allow scrolling from other parts of the screen beside the scrollbar
			if (Input.touchCount > 0) {									
				
				myTouch = Input.touches [0];
				if (myTouch.phase == TouchPhase.Moved) {
					scrollPosition.y += myTouch.deltaPosition.y * 7;
				}
			} // end scroll on screen
		}

	} // end update


	void OnGUI () {

		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.skin.label.richText = true;
		GUI.skin.label.fontSize = Screen.width/1536*82;	
		GUI.skin.label.fontStyle = FontStyle.Normal;
		GUI.skin.label.fixedWidth = Screen.width-20;	
		
		GUI.skin.button.alignment = TextAnchor.MiddleCenter;
		GUI.skin.button.richText = true;
		GUI.skin.button.fontStyle = FontStyle.Normal;

		if (displayWardrobeContents) {
			invBGObject.GetComponent<SpriteRenderer> ().sprite = invBGSprite.GetComponent<SpriteRenderer> ().sprite;
			exitButtonObject.GetComponent<SpriteRenderer> ().sprite = exitButtonSprite.GetComponent<SpriteRenderer> ().sprite;

			GUI.skin.button.fontSize = Screen.width / 1536 * 36;						
			GUI.skin.label.alignment = TextAnchor.MiddleLeft;
			GUI.skin.label.fontSize = Screen.width / 1536 * 28;	
			GUI.skin.verticalScrollbar.fixedWidth = Screen.width * 0.05f;
			GUI.skin.verticalScrollbarThumb.fixedWidth = Screen.width * 0.05f;

			scrollPosition = GUI.BeginScrollView (new Rect (inventoryWindowX, inventoryWindowY, inventoryWindowWidth, inventoryWindowHeight), 
			                                      scrollPosition, 
			                                      new Rect (0, 0, inventoryWindowWidth -  Screen.width * 0.05f - 1, Mathf.Max (inventoryWindowHeight, invListUsed.Count * (buttonSize + 5))), 
			                                      false, true);

			GUILayout.BeginVertical ();

				for (i = 0; i < invListUsed.Count; i++) {

				// deterime button text for list
				itemNameForList = invListUsed[i].itemName;

				if (invListUsed [i].itemBenefit == "none") {
					benefitMessage = "";
				} else {
					benefitMessage = "\nBenefit: " + invListUsed[i].itemBenefit;
				}

				if (invListUsed [i].itemName == "My Acorn Stash") {
					benefitMessage = "\nTotal: " + CreatePCSquirrel.pcSquirrel.squirrelAcornsSaved;
				}
				
				// begin listing the buttons in a scroll box
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (invListUsed[i].itemButtonPic, GUILayout.Height (buttonSize), GUILayout.Width (buttonSize))) {

					if (invListUsed [i].itemName == "My Acorn Stash") {
						CreatePCSquirrel.pcSquirrel.squirrelAcornsInHand += CreatePCSquirrel.pcSquirrel.squirrelAcornsSaved;
						CreatePCSquirrel.pcSquirrel.squirrelAcornsSaved = 0;

						if (!MasterObjectList.inventory.Contains (MasterObjectList.masterItemList.Find (item => item.itemName == "Stash Acorns"))) {
							MasterObjectList.inventory.Add (MasterObjectList.masterItemList.Find (item => item.itemName == "Stash Acorns"));
						}
					}

					// when button is tapped...

					// if the item is a consumable, add the item to character's inventory
					if ((invListUsed [i].itemID >= 1000) && (invListUsed [i].itemID < 2000)) {
						MasterObjectList.inventory.Add (invListUsed [i]);	
					}

					// if button is remove item, remove that item and add it to the wardrobe
					if ((invListUsed [i].itemID == 2000) && (CreatePCSquirrel.pcSquirrel.itemWornOnHeadID != 2000)) {
						prevItemWorn = SquirrelPlayerCharacter.getHeadItemWorn(CreatePCSquirrel.pcSquirrel);
						SquirrelPlayerCharacter.removeItemBoosts(CreatePCSquirrel.pcSquirrel, prevItemWorn); 	// remove item boosts
						invListUsed.Add (prevItemWorn);															// add worn item to inventory
						SquirrelPlayerCharacter.setItemWorn (CreatePCSquirrel.pcSquirrel, MasterObjectList.masterItemList.Find(item => item.itemName == "Remove Head Item"));
					}
					if ((invListUsed [i].itemID == 3000) && (CreatePCSquirrel.pcSquirrel.itemWornOnBodyID != 3000)) {
						prevItemWorn = SquirrelPlayerCharacter.getBodyItemWorn(CreatePCSquirrel.pcSquirrel);
						SquirrelPlayerCharacter.removeItemBoosts(CreatePCSquirrel.pcSquirrel, prevItemWorn); 	// remove item boosts
						invListUsed.Add (prevItemWorn);															// add worn item to inventory
						SquirrelPlayerCharacter.setItemWorn (CreatePCSquirrel.pcSquirrel, MasterObjectList.masterItemList.Find(item => item.itemName == "Remove Body Item"));
					}
					if ((invListUsed [i].itemID == 4000) && (CreatePCSquirrel.pcSquirrel.itemWornOnFeetID != 4000)) {
						prevItemWorn = SquirrelPlayerCharacter.getFootItemWorn(CreatePCSquirrel.pcSquirrel);
						SquirrelPlayerCharacter.removeItemBoosts(CreatePCSquirrel.pcSquirrel, prevItemWorn); 	// remove item boosts
						invListUsed.Add (prevItemWorn);															// add worn item to inventory
						SquirrelPlayerCharacter.setItemWorn (CreatePCSquirrel.pcSquirrel, MasterObjectList.masterItemList.Find(item => item.itemName == "Remove Foot Item"));
					}

					// if item is clothing,  check to see if its slot is empty
						// if so, add it as an item worn
						// otherwise add it to the character's inventory
					if ((invListUsed [i].itemID > 2000) && (invListUsed [i].itemID < 3000)) {
						if (CreatePCSquirrel.pcSquirrel.itemWornOnHeadID == 2000) {
							SquirrelPlayerCharacter.setItemWorn (CreatePCSquirrel.pcSquirrel, invListUsed [i]);
						} else {
							MasterObjectList.inventory.Add (invListUsed [i]);	
						}
					} 
					if ((invListUsed [i].itemID > 3000) && (invListUsed [i].itemID < 4000)) {
						if (CreatePCSquirrel.pcSquirrel.itemWornOnBodyID == 3000) {
							SquirrelPlayerCharacter.setItemWorn (CreatePCSquirrel.pcSquirrel, invListUsed [i]);
						} else {
							MasterObjectList.inventory.Add (invListUsed [i]);	
						}
					} 
					if ((invListUsed [i].itemID > 4000) && (invListUsed [i].itemID < 5000)) {
						if (CreatePCSquirrel.pcSquirrel.itemWornOnFeetID == 4000) {
							SquirrelPlayerCharacter.setItemWorn (CreatePCSquirrel.pcSquirrel, invListUsed [i]);
						} else {
							MasterObjectList.inventory.Add (invListUsed [i]);	
						}
					}

					// if item is a special item (ie- a star) 
					// add the item to character's inventory
					if (invListUsed [i].itemID >= 5000) {
						MasterObjectList.inventory.Add (invListUsed [i]);	// add item item to character's inventory
					}

					// remove the item from the list (unless it's a remove item button)
					if ((invListUsed [i].itemName != "Remove Head Item") &&  (invListUsed [i].itemName != "Remove Body Item") && (invListUsed [i].itemName != "Remove Foot Item")) {
						invListUsed.Remove (invListUsed[i]); 
					}

					// sort the list
					invListUsed.Sort ((a,b) => a.itemID.CompareTo(b.itemID));


				} // end button functions



				GUILayout.Label (itemNameForList + benefitMessage, GUILayout.MinHeight (buttonSize));

				GUILayout.EndHorizontal ();

			}

			GUILayout.EndVertical ();
			GUI.EndScrollView ();

		} else {
			invBGObject.GetComponent<SpriteRenderer> ().sprite = null;
			exitButtonObject.GetComponent<SpriteRenderer> ().sprite = null;
		}

	}
}
