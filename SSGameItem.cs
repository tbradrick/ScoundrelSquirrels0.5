using UnityEngine;
using System.Collections;

[System.Serializable]
public class SSGameItem {
	 // Scoundrel Squirrels Game Item

	// game item definition data	

	public int itemID;						// a unique ID number for the object; 1000s digit denotes usage: 1=head, 2=body, 3=feet, 4=food
	public string itemPicName;
	public string itemName;					// a human readable name	
	public int itemCost;					// how much the item costs in acorns
	public Sprite itemDressupSprite;		// the picture of the object for the squirrel dressup scenes 
	public Texture2D itemButtonPic;			// the picture of the object for the various stores
	public string itemBenefit;				// if the object grants a bonus to a stat in game, what stat

	public SSGameItem () {

		}


	public SSGameItem (int myID, string myPicName, string objectName, int myCost, string myBenefit) {
		itemID = myID;
		itemPicName = myPicName;
		itemName = objectName;
		itemCost = myCost;
		itemDressupSprite = Resources.Load<Sprite> ("du-" + myPicName);
		itemButtonPic = Resources.Load<Texture2D> ("btn-" + myPicName);
		itemBenefit = myBenefit;


	}
	
}
