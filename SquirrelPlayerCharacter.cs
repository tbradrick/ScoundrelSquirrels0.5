using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SquirrelPlayerCharacter  {

	// squirrel definition data 

	// essential data (saved on app exit)
	public string squirrelName;
	public int squirrelType; 			// 1 = gray, 2 = red
	public int squirrelRunValue;
	public int squirrelJumpValue;
	public int squirrelClimbValue;
	public int squirrelMaxHealthValue;
	public int squirrelAcornsSaved;
	public int squirrelAcornsInHand;

	public int itemWornOnHeadID;
	public int itemWornOnBodyID;
	public int itemWornOnFeetID;

	public float squirrelLastWorldX;
	public float squirrelLastWorldY;

	public float squirrelBestTimeAdv01;

	// squirrel inventory is also being saved
	// squirrel wardrobe is also being saved

	// additional into (not saved, possible implemented later)
	public string squirrelPlayerName;	// the username of the player

	public SSGameItem itemWornOnHead;
	public SSGameItem itemWornOnBody;
	public SSGameItem itemWornOnFeet;

	public Sprite spriteWornOnHead;
	public Sprite spriteWornOnBody;
	public Sprite spriteWornOnFeet;

	public int squirrelCurrentHealthValue;
	public PositionState positionState;

	public int tempRunBoost;
	public int tempJumpBoost;
	public int tempClimbBoost;

	public int clothingRunBoost;
	public int clothingJumpBoost;
	public int clothingClimbBoost;
	public int clothingHealthBoost;

	// best adventure acorns collected

	public enum PositionState {
		OnGround,
		OnAscender,
		OnPlatform,
		InFreefall
	}

	public SquirrelPlayerCharacter () {

	}

	public void initializeSquirrelPC () {
		this.squirrelName = "none";
		this.squirrelPlayerName = "none";			// the username of the player
		this.squirrelType = 0; 						// 1 = gray, 2 = red
		this.squirrelRunValue = 2;
		this.squirrelJumpValue = 2;
		this.squirrelClimbValue = 2;
		this.squirrelMaxHealthValue = 4;
		this.squirrelCurrentHealthValue = this.squirrelMaxHealthValue;
		this.squirrelAcornsInHand = 50;
		this.spriteWornOnHead = null;
		this.spriteWornOnBody = null;
		this.spriteWornOnFeet = null;

		this.positionState = PositionState.OnGround;
	}

	public static void setSqPCName (SquirrelPlayerCharacter mySquirrel, string mySqName) {
		mySquirrel.squirrelName = mySqName;
	}

	public static void setSqPCPlayer (SquirrelPlayerCharacter mySquirrel, string mySqPlayer){
		mySquirrel.squirrelPlayerName = mySqPlayer;	// the username of the player
	}
	
	public static void setSqPCType (SquirrelPlayerCharacter mySquirrel, int mySqType) {
		mySquirrel.squirrelType = mySqType; 			// 1 = gray, 2 = red
	}
	
	public static void setSqPCRun (SquirrelPlayerCharacter mySquirrel, int mySqRun) {
		mySquirrel.squirrelRunValue = mySqRun;
	}
	
	public static void setSqPCJump (SquirrelPlayerCharacter mySquirrel, int mySqJump) {
		mySquirrel.squirrelJumpValue = mySqJump;
	}
	
	public static void setSqPCClimb (SquirrelPlayerCharacter mySquirrel, int mySqClimb) {
		mySquirrel.squirrelClimbValue = mySqClimb;
	}
	
	public static void setSqPCHealth (SquirrelPlayerCharacter mySquirrel, int mySqHealth) {
		mySquirrel.squirrelMaxHealthValue = mySqHealth;
		mySquirrel.squirrelCurrentHealthValue = mySqHealth;
	}
	
	public static void setSqPCAcorns (SquirrelPlayerCharacter mySquirrel, int mySqAcorns) {
		mySquirrel.squirrelAcornsInHand = mySqAcorns;
	}


	public static void setItemWorn (SquirrelPlayerCharacter mySquirrel, SSGameItem myItem) {

		if ((myItem.itemID >= 2000) && (myItem.itemID < 3000)) {
			mySquirrel.itemWornOnHeadID = myItem.itemID;
			mySquirrel.itemWornOnHead = myItem;
			mySquirrel.spriteWornOnHead = myItem.itemDressupSprite;
			ReSkinHeadGearAnimation.headGearSpriteSheetName = "sprite-worn-" + myItem.itemPicName;
		}

		if ((myItem.itemID >= 3000) && (myItem.itemID < 4000)) {
			mySquirrel.itemWornOnBodyID = myItem.itemID;
			mySquirrel.itemWornOnBody = myItem;
			mySquirrel.spriteWornOnBody = myItem.itemDressupSprite;
			ReSkinBodyGearAnimation.bodyGearSpriteSheetName = "sprite-worn-" + myItem.itemPicName;
		}
		
		if ((myItem.itemID >= 4000) && (myItem.itemID < 5000)) {
			mySquirrel.itemWornOnFeetID = myItem.itemID;
			mySquirrel.itemWornOnFeet = myItem;
			mySquirrel.spriteWornOnFeet = myItem.itemDressupSprite;
			ReSkinFootGearAnimation.footGearSpriteSheetName = "sprite-worn-" + myItem.itemPicName;
		}

		if (myItem.itemBenefit != "none") {

			string statToBoost;
			int amountToBoost;

			statToBoost = myItem.itemBenefit.Substring (3,1);
			amountToBoost = int.Parse(myItem.itemBenefit.Substring (1,1));

			switch (statToBoost) {
			case "A":
				CreatePCSquirrel.pcSquirrel.clothingRunBoost += amountToBoost;
				CreatePCSquirrel.pcSquirrel.clothingJumpBoost += amountToBoost;
				CreatePCSquirrel.pcSquirrel.clothingClimbBoost += amountToBoost;
				CreatePCSquirrel.pcSquirrel.squirrelMaxHealthValue += amountToBoost;
				CreatePCSquirrel.pcSquirrel.squirrelCurrentHealthValue += amountToBoost;
				SoundEffects.PlaySoundOnHeal ();
				break;
			case "C":
				CreatePCSquirrel.pcSquirrel.clothingClimbBoost += amountToBoost;
				SoundEffects.PlaySoundOnPickUp ();
				break;
			case "H":
				CreatePCSquirrel.pcSquirrel.squirrelMaxHealthValue += amountToBoost;
				CreatePCSquirrel.pcSquirrel.squirrelCurrentHealthValue += amountToBoost;
				SoundEffects.PlaySoundOnHeal ();
				break;
			case "J":
				CreatePCSquirrel.pcSquirrel.clothingJumpBoost += amountToBoost;
				SoundEffects.PlaySoundOnPickUp ();
				break;
			case "R":
				CreatePCSquirrel.pcSquirrel.clothingRunBoost += amountToBoost;
				SoundEffects.PlaySoundOnPickUp ();
				break;
			default:
				Debug.Log ("Error adding clothing boost");
				break;
			}
		}
	}

	public static void removeItemBoosts (SquirrelPlayerCharacter mySquirrel, SSGameItem myItem) {
		if (myItem.itemBenefit != "none") {
			
			string statToReduce;
			int amountToReduce;
			
			statToReduce = myItem.itemBenefit.Substring (3,1);
			amountToReduce = int.Parse(myItem.itemBenefit.Substring (1,1));

			switch (statToReduce) {
			case "A":
				CreatePCSquirrel.pcSquirrel.clothingRunBoost -= amountToReduce;
				CreatePCSquirrel.pcSquirrel.clothingJumpBoost -= amountToReduce;
				CreatePCSquirrel.pcSquirrel.clothingClimbBoost -= amountToReduce;
				CreatePCSquirrel.pcSquirrel.squirrelMaxHealthValue -= amountToReduce;
				CreatePCSquirrel.pcSquirrel.squirrelCurrentHealthValue -= amountToReduce;
				break;
			case "C":
				CreatePCSquirrel.pcSquirrel.clothingClimbBoost -= amountToReduce;
				break;
			case "H":
				CreatePCSquirrel.pcSquirrel.squirrelMaxHealthValue -= amountToReduce;
				CreatePCSquirrel.pcSquirrel.squirrelCurrentHealthValue -= amountToReduce;
				break;
			case "J":
				CreatePCSquirrel.pcSquirrel.clothingJumpBoost -= amountToReduce;
				break;
			case "R":
				CreatePCSquirrel.pcSquirrel.clothingRunBoost -= amountToReduce;
				break;
			default:
				Debug.Log ("Error removing clothing boost");
				break;
			}
		}

	}

	public static void clearItemsWorn (SquirrelPlayerCharacter mySquirrel) {
		setItemWorn (mySquirrel, MasterObjectList.masterItemList.Find(item => item.itemName == "Remove Head Item"));
		setItemWorn (mySquirrel, MasterObjectList.masterItemList.Find(item => item.itemName == "Remove Body Item"));
		setItemWorn (mySquirrel, MasterObjectList.masterItemList.Find(item => item.itemName == "Remove Foot Item"));
	}


	public static void increaseHealth (SquirrelPlayerCharacter mySquirrel) {
		mySquirrel.squirrelMaxHealthValue ++;
		mySquirrel.squirrelCurrentHealthValue ++;
	}

	public static void decreaseHealth (SquirrelPlayerCharacter mySquirrel) {
		mySquirrel.squirrelMaxHealthValue --;
		mySquirrel.squirrelCurrentHealthValue --;


	}

	public static void InflictHeartDamage (SquirrelPlayerCharacter mySquirrel) {
		mySquirrel.squirrelCurrentHealthValue --;
		
		
	}

	public static int getSqPCAcorns (SquirrelPlayerCharacter mySquirrel) {
		return mySquirrel.squirrelAcornsInHand;
	}


	public static Sprite getSpriteWornOnHead (SquirrelPlayerCharacter mySquirrel) {
		return mySquirrel.spriteWornOnHead;
	}

	public static Sprite getSpriteWornOnBody (SquirrelPlayerCharacter mySquirrel) {
		return mySquirrel.spriteWornOnBody;
	}
	
	public static Sprite getSpriteWornOnFeet (SquirrelPlayerCharacter mySquirrel) {
		return mySquirrel.spriteWornOnFeet;
	}


	public static SSGameItem getHeadItemWorn(SquirrelPlayerCharacter mySquirrel) {
		return mySquirrel.itemWornOnHead;
	}
	
	public static SSGameItem getBodyItemWorn(SquirrelPlayerCharacter mySquirrel) {
		return mySquirrel.itemWornOnBody;
	}
	
	public static SSGameItem getFootItemWorn(SquirrelPlayerCharacter mySquirrel) {
		return mySquirrel.itemWornOnFeet;
	}

	public static void clearTempBoosts (SquirrelPlayerCharacter mySquirrel) {
		mySquirrel.tempRunBoost = 0;
		mySquirrel.tempJumpBoost = 0;
		mySquirrel.tempClimbBoost = 0;
		}
	
}
