using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MasterObjectList : MonoBehaviour {

	// create the various item lists used throughout the game, based on the item definitions
	// created in the SSGameItem class
	
	public static List <SSGameItem> masterItemList = new List<SSGameItem>();
	public static List <SSGameItem> initialStore = new List<SSGameItem>();
	public static List <SSGameItem> inventory = new List<SSGameItem>();
	public static List <SSGameItem> wardrobe = new List<SSGameItem>();
	public static List <SSGameItem> vendorStore = new List<SSGameItem>();
	public static List <SSGameItem> prizeBox = new List<SSGameItem>();

	SSGameItem myGameItem;

	// number and types of items to facilitate random generation 
	public static int removeHeadItemIndex;
	public static int removeBodyItemIndex;
	public static int removeFootItemIndex;
	public static int starItemsStartIndex;

	public static int consumablesStartIndex;
	public static int totalConsumableItems;

	public static int totalHeadSlotItems;
	public static int totalMundaneHeadItems;

	public static int totalBodySlotItems;
	public static int totalMundaneBodyItems;

	public static int totalFootSlotItems;
	public static int totalMundaneFootItems;

	public static int totalStarItems;

	// Use this for initialization
	void Awake () {
		// populate the lists when game is first started
		CheckAndCreateInitialItemLists ();
	}

	public static void CreateMasterListOfItems () {

		// define the base list of items

		// a unique ID number for the object; 1000s digit denotes usage: 0=acorns, 1=food, 2=head, 3=body, 4=feet, 5=star

										//  ID  	Picture Name        Object Name  			Cost 	Benefit
		// acorn items
		masterItemList.Add (new SSGameItem (1, 		"Brown Acorn", 		"My Acorn Stash", 		0, 		"none"));			// inventory management item
		masterItemList.Add (new SSGameItem (2, 		"Stash Acorns", 	"Stash Acorns", 		0, 		"none"));			// inventory management item
		masterItemList.Add (new SSGameItem (3, 		"Brown Acorn", 		"Small Acorn Award", 	0, 		"none"));			
		masterItemList.Add (new SSGameItem (4, 		"Brown Acorn", 		"Medium Acorn Award", 	0, 		"none"));
		masterItemList.Add (new SSGameItem (5, 		"Brown Acorn", 		"Large Acorn Award", 	0, 		"none"));

		// consumables
		consumablesStartIndex = 5;
		masterItemList.Add (new SSGameItem (1001, "Chocolate Doughnut", "Chocolate Doughnut", 10, "+1 Jump; 10 sec"));		// index 5
		masterItemList.Add (new SSGameItem (1002, "Jelly Doughnut", "Jelly Doughnut", 5, "+1 Climb; 10 sec"));
		masterItemList.Add (new SSGameItem (1003, "Sugar Doughnut", "Sugar Doughnut", 15, "+1 Run; 10 sec"));
		masterItemList.Add (new SSGameItem (1004, "Green Grapes", "Green Grapes", 15, "+1 Health; Max. 7"));
		masterItemList.Add (new SSGameItem (1005, "Red Apple", "Red Apple", 5, "Push Objects"));
		masterItemList.Add (new SSGameItem (1006, "Yellow Banana", "Yellow Banana", 10, "Heal one Heart"));
		totalConsumableItems = 6;
		
		// head items
		removeHeadItemIndex = consumablesStartIndex + totalConsumableItems;
		masterItemList.Add (new SSGameItem (2000, "Remove Head Item", "Remove Head Item", 0, "none"));						// index 11 // inventory management item
		masterItemList.Add (new SSGameItem (2001, "Red Cap", "Red Cap", 15, "none"));
		masterItemList.Add (new SSGameItem (2002, "Pink Hat", "Pink Hat", 20, "none"));
		masterItemList.Add (new SSGameItem (2003, "Rose Specs", "Rose Specs", 1750, "none"));
		masterItemList.Add (new SSGameItem (2004, "White Stetson", "White Stetson", 1500, "none"));
		masterItemList.Add (new SSGameItem (2005, "Yellow Rain Hat", "Yellow Rain Hat", 1600, "none"));	
		masterItemList.Add (new SSGameItem (2501, "Rose Specs", "Climber's Specs", 9500, "+1 Climb"));
		masterItemList.Add (new SSGameItem (2502, "Yellow Rain Hat", "Puddle Jumping Hat", 10000, "+1 Jump"));	
		masterItemList.Add (new SSGameItem (2503, "White Stetson", "Rancher's Hat", 12500, "+1 Run"));
		totalHeadSlotItems = 9;
		totalMundaneHeadItems = 6;

		// body items
		removeBodyItemIndex = removeHeadItemIndex + totalHeadSlotItems;
		masterItemList.Add (new SSGameItem (3000, "Remove Body Item", "Remove Body Item", 0, 		"none"));				// index 20 // inventory management item
		masterItemList.Add (new SSGameItem (3001, "Orange Jacket", "Orange Jacket", 15, "none"));
		masterItemList.Add (new SSGameItem (3002, "Green Shirt", "Green Shirt", 25, "none"));
		masterItemList.Add (new SSGameItem (3003, "Yellow Raincoat", "Yellow Raincoat", 1650, "none"));
		masterItemList.Add (new SSGameItem (3004, "Black Dress", "Black Dress", 50, "none"));				
		masterItemList.Add (new SSGameItem (3005, "Brown Apron","Brown Apron", 1350, "none"));
		masterItemList.Add (new SSGameItem (3501, "Green Shirt", "Hero's Shirt", 50000, "+1 All"));
		masterItemList.Add (new SSGameItem (3502, "Black Dress", "Hero's Dress", 50000, "+1 All"));	
		masterItemList.Add (new SSGameItem (3503, "Orange Jacket", "Jacket of Endurance", 15000, "+1 Health"));

		totalBodySlotItems = 9;
		totalMundaneBodyItems = 6;

		// foot items
		removeFootItemIndex = removeBodyItemIndex + totalBodySlotItems;
		masterItemList.Add (new SSGameItem (4000, "Remove Foot Item", "Remove Foot Item", 0, 		"none"));				// index 28 // inventory management item
		masterItemList.Add (new SSGameItem (4001, "Blue Sneakers", "Blue Sneakers", 50, "none"));
		masterItemList.Add (new SSGameItem (4002, "Red Sneakers", "Red Sneakers", 15, "none"));
		masterItemList.Add (new SSGameItem (4003, "Brown Boots", "Brown Boots", 35, "none"));				
		masterItemList.Add (new SSGameItem (4004, "Black Boots", "Black Boots", 1500, "none"));
		masterItemList.Add (new SSGameItem (4005, "Purple Flats", "Purple Flats", 850, "none"));
		masterItemList.Add (new SSGameItem (4501, "Red Sneakers", "Running Sneakers", 12500, "+1 Run"));
		masterItemList.Add (new SSGameItem (4502, "Blue Sneakers", "Jumping Sneakers", 10000, "+1 Jump"));
		masterItemList.Add (new SSGameItem (4503, "Brown Boots", "Climbing Boots", 8000, "+1 Climb"));
		totalFootSlotItems = 9;
		totalMundaneFootItems = 6;

		// stars
		starItemsStartIndex = removeFootItemIndex + totalFootSlotItems;
		masterItemList.Add (new SSGameItem (5001, "Star-Climb", "Bonus Star: Climb", 250, "+1 Climb; Perm"));				// index 37
		masterItemList.Add (new SSGameItem (5002, "Star-Health", "Bonus Star: Health", 250, "+1 Health; Perm"));
		masterItemList.Add (new SSGameItem (5003, "Star-Jump", "Bonus Star: Jump", 250, "+1 Jump; Perm"));
		masterItemList.Add (new SSGameItem (5004, "Star-Run", "Bonus Star: Run", 250, "+1 Run; Perm"));
		totalStarItems = 4;


	}

	public static void SetRandomClothingItem (SquirrelPlayerCharacter mySquirrel, string mySlot) {

		// random clothing generator for initial squirrels

		if (mySlot == "Head") {
			switch (Random.Range (0, 3)) {
			case 0:
				SquirrelPlayerCharacter.setItemWorn (mySquirrel, masterItemList.Find (item => item.itemName == "Red Cap")); // head slot item
				break;
			case 1:
				SquirrelPlayerCharacter.setItemWorn (mySquirrel, masterItemList.Find (item => item.itemName == "Black Dress")); // head slot item
				break;
			default:
				SquirrelPlayerCharacter.setItemWorn (mySquirrel, masterItemList.Find (item => item.itemName == "Remove Head Item")); // head slot item
				break;
			}
		}
		if (mySlot == "Body") {
			switch (Random.Range (0, 3)) {
			case 0:
				SquirrelPlayerCharacter.setItemWorn (mySquirrel, masterItemList.Find (item => item.itemName == "Green Shirt")); // body slot item
				break;
			case 1:
				SquirrelPlayerCharacter.setItemWorn (mySquirrel, masterItemList.Find (item => item.itemName == "Pink Hat")); // body slot item
				break;
			default:
				SquirrelPlayerCharacter.setItemWorn (mySquirrel, masterItemList.Find (item => item.itemName == "Remove Body Item")); // body slot item
				break;
			}
		}

		if (mySlot ==  "Foot") {
			switch (Random.Range (0, 3)) {
			case 0:
				SquirrelPlayerCharacter.setItemWorn (mySquirrel, masterItemList.Find (item => item.itemName == "Blue Sneakers")); // foot slot item
				break;
			case 1:
				SquirrelPlayerCharacter.setItemWorn (mySquirrel, masterItemList.Find (item => item.itemName == "Brown Boots")); // foot slot item
				break;
			default:
				SquirrelPlayerCharacter.setItemWorn (mySquirrel, masterItemList.Find (item => item.itemName == "Remove Foot Item")); // foot slot item
				break;
			}
		}
	} // end SetRandomClothingItem

																		// use the Object Name (the second entry listed above) 
	public static void generateInitialStoreList () {

		// stating list when creating a new squirrel
		initialStore.Add (masterItemList.Find (item => item.itemName == "Remove Head Item"));	 
		initialStore.Add (masterItemList.Find (item => item.itemName == "Red Cap"));	
		initialStore.Add (masterItemList.Find (item => item.itemName == "Pink Hat"));	
		
		initialStore.Add (masterItemList.Find (item => item.itemName == "Remove Body Item"));	
		initialStore.Add (masterItemList.Find (item => item.itemName == "Green Shirt"));
		initialStore.Add (masterItemList.Find (item => item.itemName == "Black Dress"));
		
		initialStore.Add (masterItemList.Find (item => item.itemName == "Remove Foot Item"));
		initialStore.Add (masterItemList.Find (item => item.itemName == "Blue Sneakers"));
		initialStore.Add (masterItemList.Find (item => item.itemName == "Brown Boots"));
		initialStore.Sort ((a,b) => a.itemID.CompareTo(b.itemID));
	}

	public static void generateWardrobe () {

		// starting wardrobe of the player
		wardrobe.Add (masterItemList.Find (item => item.itemName == "Remove Head Item"));
		wardrobe.Add (masterItemList.Find (item => item.itemName == "Remove Body Item"));
		wardrobe.Add (masterItemList.Find (item => item.itemName == "Orange Jacket"));

		wardrobe.Add (masterItemList.Find (item => item.itemName == "Remove Foot Item"));
		wardrobe.Add (masterItemList.Find (item => item.itemName == "Red Sneakers"));
		wardrobe.Sort ((a,b) => a.itemID.CompareTo(b.itemID));

	}

	public static void generateInventory () {

		// starting inventory of the player
		inventory.Add (masterItemList.Find (item => item.itemName == "Red Apple"));
		inventory.Add (masterItemList.Find (item => item.itemName == "Red Apple"));

		inventory.Add (masterItemList.Find (item => item.itemName == "Yellow Banana"));
		inventory.Add (masterItemList.Find (item => item.itemName == "Remove Head Item"));
		inventory.Add (masterItemList.Find (item => item.itemName == "Remove Body Item"));
		inventory.Add (masterItemList.Find (item => item.itemName == "Remove Foot Item"));
		inventory.Sort ((a,b) => a.itemID.CompareTo(b.itemID));

	}

	public static void generateVendorStore () {

		// random items for the store
		int itemIndex;
		itemIndex = Random.Range (consumablesStartIndex, totalHeadSlotItems + totalBodySlotItems + totalFootSlotItems);
		if ((itemIndex == removeHeadItemIndex) || (itemIndex == removeBodyItemIndex) || (itemIndex == removeFootItemIndex)) {
			vendorStore.Add (masterItemList.Find (item => item.itemName == "Green Grapes"));
		} else {
			vendorStore.Add (masterItemList[itemIndex]);
		}

		AddOneOfEachClothing (vendorStore, true);
		AddOneOfEachClothing (vendorStore, true);

		vendorStore.Add (masterItemList.Find (item => item.itemName == "Yellow Banana"));
		vendorStore.Add (masterItemList.Find (item => item.itemName == "Yellow Banana"));
		
		vendorStore.Add (masterItemList.Find (item => item.itemName == "Red Apple"));
		
		vendorStore.Sort ((a,b) => a.itemID.CompareTo(b.itemID));
	}

	public static void CheckAndCreateInitialItemLists () {
		if (masterItemList.Count == 0) {
			CreateMasterListOfItems ();
		}
		
		if (initialStore.Count == 0) {
			generateInitialStoreList ();
		}
		
		if (wardrobe.Count == 0) {
			generateWardrobe ();
		}
		
		if (inventory.Count == 0) {
			generateInventory ();
		}
		
		if (vendorStore.Count == 0) {
			generateVendorStore ();
		}

		// the prize box is generated at the completion of the adventure
	}

	public static void generatePrizeBoxList (string awardLevel) {

		// generate the prize list when the adventure is completed
		// based on the award level

		bool addStar = false;

		prizeBox.Clear ();

		// items which everyone can choose from
		prizeBox.Add (masterItemList.Find (item => item.itemName == "Small Acorn Award")); 						// add two acorn awards
		prizeBox.Find (item => item.itemName == "Small Acorn Award").itemCost = Random.Range (70, 80);

		AddOneOfEachClothing (prizeBox, false);																	// one items of each clothing type
		
		prizeBox.Add (masterItemList[consumablesStartIndex + Random.Range (0, totalConsumableItems)]);			// add two consumables
		prizeBox.Add (masterItemList[consumablesStartIndex + Random.Range (0, totalConsumableItems)]);

		// additional items from which to choose based on completion level
		switch (awardLevel) {
		case "Beginner":
			// normally very rare, each beginning player will recieve one star after their first adventure
			addStar = true;
			break;

		case "Low":
			Debug.Log ("generating low prize");
			prizeBox.Add (masterItemList.Find (item => item.itemName == "Medium Acorn Award")); 
			prizeBox.Find (item => item.itemName == "Medium Acorn Award").itemCost = Random.Range (135, 160);

			prizeBox.Add (masterItemList[consumablesStartIndex + Random.Range (0, totalConsumableItems)]);

			if (Random.Range (0, 100) == 0) {																		// a one percent chance of a star
				addStar = true;
			}

			break;

		case "High":
			Debug.Log ("generating high prize");
			prizeBox.Add (masterItemList.Find (item => item.itemName == "Medium Acorn Award")); 
			prizeBox.Find (item => item.itemName == "Medium Acorn Award").itemCost = Random.Range (135, 160);

			prizeBox.Add (masterItemList.Find (item => item.itemName == "Large Acorn Award")); 
			prizeBox.Find (item => item.itemName == "Large Acorn Award").itemCost = Random.Range (250, 275);

			AddOneOfEachClothing (prizeBox, false);																	

			prizeBox.Add (masterItemList[consumablesStartIndex + Random.Range (0, totalConsumableItems)]);

			if (Random.Range (0, 10) == 0) {																		// a ten percent chance of a star
				addStar = true;
			}
			break;
		default:
			Debug.Log ("error choosing prizes");
			break;
		} // end switch

		prizeBox.Sort ((a,b) => a.itemID.CompareTo(b.itemID));

		if (addStar) {
			prizeBox.Insert (0, masterItemList [starItemsStartIndex + Random.Range (0, totalStarItems)]); 				// add one star item at top of the list
		}

	}

	public static void AddOneOfEachClothing (List<SSGameItem> myInvList, bool includeSpecials) {

		// add inventory items to the store when entered later
		int searchRangeHead;
		int searchRangeBody;
		int searchRangeFoot;

		if (includeSpecials) {
			searchRangeHead = totalHeadSlotItems;
			searchRangeBody = totalBodySlotItems;
			searchRangeFoot = totalFootSlotItems;
		} else {
			searchRangeHead = totalMundaneHeadItems;
			searchRangeBody = totalMundaneBodyItems;
			searchRangeFoot = totalMundaneFootItems;
		}

		myInvList.Add (masterItemList [removeHeadItemIndex + Random.Range (1, searchRangeHead)]);
		myInvList.Add (masterItemList[removeBodyItemIndex + Random.Range (1, searchRangeBody)]);
		myInvList.Add (masterItemList[removeFootItemIndex + Random.Range (1, searchRangeFoot)]);
	}

} // end MasterObjectList class
