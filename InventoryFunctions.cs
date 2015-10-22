using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InventoryFunctions : MonoBehaviour {

	// show the squirrel's inventory on the info screen

	// the functions of tapping differ based on where this screen is opened up
	public enum InventoryAcitonType {
		Transfer,
		Use,
		Sell
	}

	public InventoryAcitonType typeOfAction;
	string actionMessage;

	bool saleMade = false;
	bool performStashAcorns = false;
	SSGameItem itemSold;
	SSGameItem prevItemWorn;

	List <SSGameItem> invListUsed = new List<SSGameItem>();

	public static bool displayInventoryContents = false;

	GameObject myPCSquirrel;

	public float msgWinXPos;
	public float msgWinYPos;
	public float msgWinWidth;
	public float msgWinHeight;
	
	float msgWindowX;
	float msgWindowY;
	float msgWindowWidth;
	float msgWindowHeight;
	
	Vector2 scrollPosition;
	Touch myTouch;

	public float invWinXPos;
	public float invWinYPos;
	public float invWinWidth;
	public float invWinHeight;

	float inventoryWindowX;
	float inventoryWindowY;
	float inventoryWindowWidth;
	float inventoryWindowHeight;

	GameObject newApple;
	public GameObject applePrefab;
	float facingFactor = 1.0f;

	float buttonSize = Screen.height/8;

	int i;

	string itemNameForList;
	string costMessage;
	string benefitMessage;

	// Use this for initialization
	void Start () {

		CalculateWindowAreas ();

		invListUsed = MasterObjectList.inventory;
		invListUsed.Sort ((a,b) => a.itemID.CompareTo(b.itemID));

		myPCSquirrel = GameObject.FindWithTag ("Player");

		// display message differs according the scene
		switch (typeOfAction) {
		case InventoryAcitonType.Sell:
			actionMessage = "Tap an item to sell it.";
			break;
		case InventoryAcitonType.Transfer:
			actionMessage = "Tap item to transfer it to the wardrobe.";
			break;
		case InventoryAcitonType.Use:
			actionMessage = "Tap an item to use or wear it.";
			break;
		default:
			Debug.Log("error choosing an action type");
			break;
			
		}

	}

	// Update is called once per frame
	void Update () {

		CalculateWindowAreas ();

		// allow scrolling from other parts of the screen beside the scrollbar
		if (displayInventoryContents) { 
			if (Input.touchCount > 0) {									
				
				myTouch = Input.touches [0];
				if (myTouch.phase == TouchPhase.Moved) {
					scrollPosition.y += myTouch.deltaPosition.y * 7;
				}
			} // end scroll on screen
		}

		// sell an item in the inventory
		if (saleMade) {
			MasterObjectList.inventory.Remove (itemSold);
			MasterObjectList.vendorStore.Add (itemSold);
			CreatePCSquirrel.pcSquirrel.squirrelAcornsInHand += itemSold.itemCost;
			saleMade = false;
		}

		// stash the acorns in the home
		if (performStashAcorns) {
			performStashAcorns = false;
			CreatePCSquirrel.pcSquirrel.squirrelAcornsSaved  += CreatePCSquirrel.pcSquirrel.squirrelAcornsInHand;
			CreatePCSquirrel.pcSquirrel.squirrelAcornsInHand = 0;
			SoundEffects.PlaySquirrelStashed ();
			
			if (!MasterObjectList.wardrobe.Contains (MasterObjectList.masterItemList.Find (item => item.itemName == "My Acorn Stash"))) {
				MasterObjectList.wardrobe.Add (MasterObjectList.masterItemList.Find (item => item.itemName == "My Acorn Stash"));
				MasterObjectList.wardrobe.Sort ((a,b) => a.itemID.CompareTo(b.itemID));
			}

			invListUsed.Remove (MasterObjectList.masterItemList.Find (item => item.itemName == "Stash Acorns"));
		}


	} // end update


	void OnGUI () {

		GUI.skin.label.alignment = TextAnchor.MiddleCenter;
		GUI.skin.label.richText = true;
		GUI.skin.label.fontSize = Screen.width/1536*54;	
		GUI.skin.label.fontStyle = FontStyle.Normal;

		GUI.skin.button.alignment = TextAnchor.MiddleCenter;

		GUI.skin.button.richText = true;
		GUI.skin.button.fontStyle = FontStyle.Normal;

		if (displayInventoryContents) {

			GUI.skin.button.fontSize = Screen.width / 1536 * 36;						
			GUI.skin.verticalScrollbar.fixedWidth = Screen.width * 0.05f;
			GUI.skin.verticalScrollbarThumb.fixedWidth = Screen.width * 0.05f;
			GUI.skin.label.alignment = TextAnchor.MiddleLeft;


			GUILayout.BeginArea (new Rect (msgWindowX, msgWindowY, msgWindowWidth, msgWindowHeight )); //message Window
			GUI.skin.label.fixedWidth = 0;
			GUI.skin.label.fontSize = Screen.width / 1536 * 48;
			GUI.skin.label.alignment = TextAnchor.UpperCenter;
			GUI.skin.label.wordWrap = true;
			GUI.skin.label.richText = true;
			GUI.skin.label.fontStyle = FontStyle.Normal;

			GUI.color = Color.red;
			GUILayout.Label(actionMessage, GUILayout.MinHeight(50), GUILayout.MaxHeight(150));

			GUI.color = Color.white;
			GUILayout.EndArea();

			GUI.skin.label.fontSize = Screen.width / 1536 * 32;	



			scrollPosition = GUI.BeginScrollView (new Rect (inventoryWindowX, inventoryWindowY, inventoryWindowWidth, inventoryWindowHeight), 
			                                      scrollPosition, 
			                                      new Rect (0, 0, inventoryWindowWidth -  Screen.width * 0.05f - 1, Mathf.Max (inventoryWindowHeight, invListUsed.Count * (buttonSize + 5))), 
			                                      false, true);

			GUILayout.BeginVertical ();

				for (i = 0; i < invListUsed.Count; i++) {

				// determine message next to button
				itemNameForList = invListUsed[i].itemName;

				if ((invListUsed [i].itemCost != 0) && (typeOfAction == InventoryAcitonType.Sell)) {
					costMessage = "\nSell price: " + invListUsed [i].itemCost;
				} else {
					costMessage = "";
				}
				
				if (invListUsed [i].itemBenefit == "none") {
					benefitMessage = "";
				} else {
					benefitMessage = "\nBenefit: " + invListUsed[i].itemBenefit;
				}

				// make the list of buttons
				GUILayout.BeginHorizontal ();
				if (GUILayout.Button (invListUsed[i].itemButtonPic, GUILayout.Height (buttonSize), GUILayout.Width (buttonSize))) {

					if (invListUsed [i].itemName == "Stash Acorns") {
						performStashAcorns = true;						//  see section in Update method
					}

					// if its the remove hat button (and the hat slot is not empty)
					if ((invListUsed [i].itemID == 2000) && (SquirrelPlayerCharacter.getHeadItemWorn(CreatePCSquirrel.pcSquirrel).itemID != 2000)) {
						prevItemWorn = SquirrelPlayerCharacter.getHeadItemWorn(CreatePCSquirrel.pcSquirrel);
						SquirrelPlayerCharacter.removeItemBoosts(CreatePCSquirrel.pcSquirrel, prevItemWorn); 	// remove item boosts
						invListUsed.Add (prevItemWorn);															// add worn item to inventory
						SquirrelPlayerCharacter.setItemWorn (CreatePCSquirrel.pcSquirrel, invListUsed [i]);		// empty out the slot
					} 

					// if its the remove shirt button (and the slot is not empty)
					if ((invListUsed [i].itemID == 3000) && (SquirrelPlayerCharacter.getBodyItemWorn(CreatePCSquirrel.pcSquirrel).itemID != 3000)) {
						prevItemWorn = SquirrelPlayerCharacter.getBodyItemWorn(CreatePCSquirrel.pcSquirrel);
						SquirrelPlayerCharacter.removeItemBoosts(CreatePCSquirrel.pcSquirrel, prevItemWorn); 	// remove item boosts
						invListUsed.Add (prevItemWorn);															// add worn item to inventory
						SquirrelPlayerCharacter.setItemWorn (CreatePCSquirrel.pcSquirrel, invListUsed [i]);		// empty out the slot
					} 

					// if its the remove shoes button (and the slot is not empty)
					if ((invListUsed [i].itemID == 4000) && (SquirrelPlayerCharacter.getFootItemWorn(CreatePCSquirrel.pcSquirrel).itemID != 4000)) {
						prevItemWorn = SquirrelPlayerCharacter.getFootItemWorn(CreatePCSquirrel.pcSquirrel);
						SquirrelPlayerCharacter.removeItemBoosts(CreatePCSquirrel.pcSquirrel, prevItemWorn); 	// remove item boosts
						invListUsed.Add (prevItemWorn);															// add worn item to inventory
						SquirrelPlayerCharacter.setItemWorn (CreatePCSquirrel.pcSquirrel, invListUsed [i]);		// empty out the slot
					} 


					if ((invListUsed [i].itemID != 2000) && (invListUsed [i].itemID != 3000)  		// if its not one of the inventory managaement buttons
					    && (invListUsed [i].itemID != 4000) && invListUsed [i].itemID != 2) {

						switch (typeOfAction) {														// do something with it
						case InventoryAcitonType.Sell:
							SellItem (invListUsed[i]);
							break;

						case InventoryAcitonType.Transfer:
							TransferItem (invListUsed[i]);
							break;

						case InventoryAcitonType.Use:
							UseItem (invListUsed[i]);
							break;

						default:
							break;
						}  //end switch (typeOfAction)
					}

					// resort the inventory list after performing the actions
					invListUsed.Sort ((a,b) => a.itemID.CompareTo(b.itemID));

				} // end button functions

				GUI.skin.label.fixedWidth = Screen.width * (inventoryWindowWidth - 0.05f) - buttonSize;
				GUI.skin.label.alignment = TextAnchor.MiddleLeft;

				GUILayout.Label (itemNameForList + costMessage + benefitMessage, GUILayout.MinHeight (buttonSize));
				GUI.skin.label.fixedWidth = 0;

				GUILayout.EndHorizontal ();

			} // end for loop

			GUILayout.EndVertical ();
			GUI.EndScrollView ();

		} // end display inventory contents 

	} // end OnGUI

	void SellItem (SSGameItem myItem) {
		saleMade = true;						// see section in Update method
		itemSold = myItem;
		SoundEffects.PlayNutsDropping ();
	}

	void TransferItem (SSGameItem myItem) {
		MasterObjectList.wardrobe.Add (myItem);
		MasterObjectList.inventory.Remove (myItem);
		SoundEffects.PlaySquirrelStashed ();
	}

	void UseItem (SSGameItem myItem) {
		switch (myItem.itemID) {
			case 1001: // chocolate doughnut
			// in order to eat a doughnut, the character needs to be in the neighborhood or adventure, 
			// and not currently under the effect of another doughnut
			if ((Application.loadedLevel == 11) && (!ItemEffectConsumeDoughnut.isTimerRunning)) {
					ItemEffectConsumeDoughnut.timerDoughnutTypeString = "c";
					ItemEffectConsumeDoughnut.startDoughnutTimer = true; 
					invListUsed.Remove (myItem); 
				} else {
					SoundEffects.PlayHuhUnh ();
				}
			break;
				
			case 1002: // jelly doughnut
			if ((Application.loadedLevel == 11) && (!ItemEffectConsumeDoughnut.isTimerRunning)) {
				ItemEffectConsumeDoughnut.timerDoughnutTypeString = "j";
				ItemEffectConsumeDoughnut.startDoughnutTimer = true; 
				invListUsed.Remove (myItem); 
				} else {
					SoundEffects.PlayHuhUnh ();
				}
			break;
				
			case 1003: // sugar doughnut
			if (((Application.loadedLevel == 10) || (Application.loadedLevel == 11)) && (!ItemEffectConsumeDoughnut.isTimerRunning)) {
					ItemEffectConsumeDoughnut.timerDoughnutTypeString = "s";
					ItemEffectConsumeDoughnut.startDoughnutTimer = true; 
				 	invListUsed.Remove (myItem); 
				} else {
					SoundEffects.PlayHuhUnh ();
				}
			break;
				
			case 1004: // grapes
				if (CreatePCSquirrel.pcSquirrel.squirrelCurrentHealthValue < 7) {		
					ItemEffectHealthyGrapes.GrapesEffect ();
					invListUsed.Remove (myItem); 
				} else {
					SoundEffects.PlayHuhUnh ();
				}
				break;
				
			case 1005: // apple
				if ((Application.loadedLevel == 10) || (Application.loadedLevel == 11)) {
					if (Squirrel_Controller.facingRight) {									
						facingFactor = 1.0f;
					} else {
						facingFactor = -1.0f;
					}
					Instantiate(applePrefab, new Vector3(myPCSquirrel.transform.position.x + 1.5f * facingFactor, myPCSquirrel.transform.position.y - 1.0f , transform.position.z), Quaternion.identity);
					invListUsed.Remove (myItem); 
					SoundEffects.PlayDroppedApple ();
				} else {
					SoundEffects.PlayHuhUnh ();
				}
				break;
				
			case 1006: // banana
				if (CreatePCSquirrel.pcSquirrel.squirrelCurrentHealthValue <  CreatePCSquirrel.pcSquirrel.squirrelMaxHealthValue) {
					ItemEffectHealthyBanana.BananaEffect();
					invListUsed.Remove (myItem); 
				} else {
					SoundEffects.PlayHuhUnh ();
				}
				
				break;

			case 5001: // climb star
				CreatePCSquirrel.pcSquirrel.squirrelClimbValue ++;
				invListUsed.Remove (myItem); 
				break;

			case 5002: // health star
				CreatePCSquirrel.pcSquirrel.squirrelMaxHealthValue ++;
				CreatePCSquirrel.pcSquirrel.squirrelCurrentHealthValue ++;
				SoundEffects.PlaySoundOnHeal ();
				invListUsed.Remove (myItem); 
				break;
				
			case 5003: // jump star
				CreatePCSquirrel.pcSquirrel.squirrelJumpValue ++;
				invListUsed.Remove (myItem); 
				break;
				
			case 5004: // run star
				CreatePCSquirrel.pcSquirrel.squirrelRunValue ++;
				invListUsed.Remove (myItem); 
				break;
				
			default: // clothing
				// add previously worn item to inventory
				if ((myItem.itemID > 2000) && (myItem.itemID < 3000)) {
					prevItemWorn = SquirrelPlayerCharacter.getHeadItemWorn(CreatePCSquirrel.pcSquirrel);
					if (prevItemWorn.itemID != 2000) {
						SquirrelPlayerCharacter.removeItemBoosts(CreatePCSquirrel.pcSquirrel, prevItemWorn); 	// remove item boosts
						invListUsed.Add (SquirrelPlayerCharacter.getHeadItemWorn(CreatePCSquirrel.pcSquirrel));	// add worn item to inventory
						}
				} else if ((myItem.itemID > 3000) && (myItem.itemID < 4000)) {
					prevItemWorn = SquirrelPlayerCharacter.getBodyItemWorn(CreatePCSquirrel.pcSquirrel);
					if (prevItemWorn.itemID != 3000) {
						SquirrelPlayerCharacter.removeItemBoosts(CreatePCSquirrel.pcSquirrel, prevItemWorn); 	// remove item boosts
						invListUsed.Add (SquirrelPlayerCharacter.getBodyItemWorn(CreatePCSquirrel.pcSquirrel));	// add worn item to inventory
						}
				} else if ((myItem.itemID > 4000) && (myItem.itemID < 5000)) {
					prevItemWorn = SquirrelPlayerCharacter.getFootItemWorn(CreatePCSquirrel.pcSquirrel);
					if (prevItemWorn.itemID != 4000) {
						SquirrelPlayerCharacter.removeItemBoosts(CreatePCSquirrel.pcSquirrel, prevItemWorn); 	// remove item boosts
						invListUsed.Add (SquirrelPlayerCharacter.getFootItemWorn(CreatePCSquirrel.pcSquirrel));	// add worn item to inventory
					}
				}
				
				// set the worn item slot to the selected item
				SquirrelPlayerCharacter.setItemWorn (CreatePCSquirrel.pcSquirrel, myItem);
				
				// remove the item from the list 
				invListUsed.Remove (myItem); 
				
				// sort the list
				invListUsed.Sort ((a,b) => a.itemID.CompareTo(b.itemID));
				
				break; // default clothing
			} // end switch statement
		} // end use item method

	void CalculateWindowAreas () {

		inventoryWindowX = Screen.width * invWinXPos;
		inventoryWindowY = Screen.height * invWinYPos;
		inventoryWindowWidth = Screen.width * invWinWidth;
		inventoryWindowHeight = Screen.height * invWinHeight;
		
		msgWindowX = Screen.width * msgWinXPos;
		msgWindowY = Screen.height * msgWinYPos;
		msgWindowWidth = Screen.width * msgWinWidth;
		msgWindowHeight = Screen.height * msgWinHeight;

	}


}
