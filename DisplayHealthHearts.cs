using UnityEngine;
using System.Collections;

public class DisplayHealthHearts : MonoBehaviour {

	// show the correct number and type of hearts in the main play screens
	// this script is assigned to each of the seven heart objects 
	// in the Unity editor, the heart objects are named "Health Heart #"

	int heartNumber;

	Texture healthyHeartImage;
	Texture damagedHeartImage;
	Texture bonusHeartImage;

	GUITexture heartObjectImage;


	// Use this for initialization
	void Start () {

		// get the number of this heart: "Health Heart #"
		// set heart names as 
		// 14th character, index 13
		heartNumber = int.Parse (gameObject.name.Substring (13)); 

		heartObjectImage = GetComponent<GUITexture> ();
	
		healthyHeartImage = Resources.Load<Texture> ("icon-heart-healthy");
		damagedHeartImage = Resources.Load<Texture> ("icon-heart-damaged");
		bonusHeartImage = Resources.Load<Texture> ("icon-heart-bonus");
	
	}
	
	// Update is called once per frame
	void Update () {

		if ((heartNumber <= CreatePCSquirrel.pcSquirrel.squirrelCurrentHealthValue) && 
		    (heartNumber > CreatePCSquirrel.pcSquirrel.squirrelMaxHealthValue)) 
				heartObjectImage.texture = bonusHeartImage;											// set bonus heart- current > max
			else if (heartNumber <= CreatePCSquirrel.pcSquirrel.squirrelCurrentHealthValue) 
					heartObjectImage.texture = healthyHeartImage;									// set healthy heart- current = max
				 else if (heartNumber <= CreatePCSquirrel.pcSquirrel.squirrelMaxHealthValue) 
						heartObjectImage.texture = damagedHeartImage;								// set damaged heart- current < max
					else heartObjectImage.texture = null;											// set null icon for slots above max

	} // end update
}
