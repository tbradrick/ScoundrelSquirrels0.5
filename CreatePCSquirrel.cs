using UnityEngine;
using System.Collections;

public class CreatePCSquirrel : MonoBehaviour {

	// this script creates the player squirrel, as defined in the SquirrelPlayerCharacter class

	public static SquirrelPlayerCharacter pcSquirrel = new SquirrelPlayerCharacter();

	// to assign clothing to a random squirrel, the item list must be created first
	void Awake () {
		if (MasterObjectList.masterItemList.Count == 0) {
			MasterObjectList.CreateMasterListOfItems ();
		}
	}

	// if there is no squirrel (health = 0), create a new random squirrel
	void Start () {
		if (pcSquirrel.squirrelMaxHealthValue == 0) {
			CreateRandomSquirrel ();
		}
	}

	// create a random squirrel
	public static void CreateRandomSquirrel () {

		int startRun = 2;					// assign default values to attributes
		int startJump = 2;
		int startClimb = 2;
		int startHealth = 4;


		SquirrelPlayerCharacter.setSqPCName (pcSquirrel, SelectRandomSquirrelName ());

		SquirrelPlayerCharacter.setSqPCType (pcSquirrel, Random.Range(1,3));

		SquirrelPlayerCharacter.setSqPCRun (pcSquirrel, startRun);
		SquirrelPlayerCharacter.setSqPCJump (pcSquirrel, startJump);
		SquirrelPlayerCharacter.setSqPCClimb (pcSquirrel, startClimb);
		SquirrelPlayerCharacter.setSqPCHealth (pcSquirrel, startHealth);

		SquirrelPlayerCharacter.setSqPCAcorns (pcSquirrel, 40 + Random.Range(1,21));
		pcSquirrel.squirrelAcornsSaved = 40 + Random.Range(1,21);


		if (MasterObjectList.masterItemList.Count != 0) {
			MasterObjectList.SetRandomClothingItem (pcSquirrel, "Head");
			MasterObjectList.SetRandomClothingItem (pcSquirrel, "Body");
			MasterObjectList.SetRandomClothingItem (pcSquirrel, "Foot");
		}

		pcSquirrel.itemWornOnHeadID = SquirrelPlayerCharacter.getHeadItemWorn(pcSquirrel).itemID;
		pcSquirrel.itemWornOnBodyID = SquirrelPlayerCharacter.getBodyItemWorn(pcSquirrel).itemID;
		pcSquirrel.itemWornOnFeetID = SquirrelPlayerCharacter.getFootItemWorn(pcSquirrel).itemID;
		
		pcSquirrel.squirrelLastWorldX = 7.5f;
		pcSquirrel.squirrelLastWorldY = 2.0f;
		
		pcSquirrel.squirrelBestTimeAdv01 = 3599.0f;			// 3599 = 59 minutes, 59 seconds
		

	} // end create random squirrel

	public static string SelectRandomSquirrelName () {
		int randomName;
		int randomExtension;
		string squirrelName;
		string extensionText;

		// select a random name, and attached 4-digit number
		// these names (except Sparky) were selected from famous squirrels in popular culture
		
		randomName = Random.Range (1, 17);
		randomExtension = Random.Range (0, 10000);
		extensionText = randomExtension.ToString();
		
		switch (randomName) {
		case 1:
			squirrelName = string.Concat ( "Sparky", extensionText) ;
			break;
		case 2:
			squirrelName = string.Concat ( "Rocky", extensionText);
			break;
		case 3:
			squirrelName = string.Concat ( "Scrat", extensionText);
			break;
		case 4:
			squirrelName = string.Concat ( "Morwenna", extensionText);
			break;
		case 5:
			squirrelName = string.Concat ( "Rupert", extensionText);
			break;
		case 6:
			squirrelName = string.Concat ( "Skippy", extensionText);
			break;
		case 7:
			squirrelName = string.Concat ( "Slappy", extensionText);
			break;
		case 8:
			squirrelName = string.Concat ( "Tommy", extensionText);
			break;
		case 9:
			squirrelName = string.Concat ( "Twiggy", extensionText);
			break;
		case 10:
			squirrelName = string.Concat ( "Sally", extensionText);
			break;
		case 11:
			squirrelName = string.Concat ( "Hammy", extensionText);
			break;
		case 12:
			squirrelName = string.Concat ( "Bucky", extensionText);
			break;
		case 13:
			squirrelName = string.Concat ( "Sandy", extensionText);
			break;
		case 14:
			squirrelName = string.Concat ( "Ratatosk", extensionText);
			break;
		case 15:
			squirrelName = string.Concat ( "Shirley", extensionText);
			break;
		default:
			squirrelName = string.Concat ( "Melvin", extensionText);
			break;
		}

		return squirrelName;

	}
	

}
