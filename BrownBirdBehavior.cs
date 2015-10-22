using UnityEngine;
using System.Collections;

public class BrownBirdBehavior : MonoBehaviour {

	// behavior of the brown bird

	public GameObject theSquirrel;

	public Transform wayPoint0;				// the brown birds fly in a circuit of way points
	public Transform wayPoint1;
	public Transform wayPoint2;
	public Transform wayPoint3;
	public Transform rightExitLocation;		// after attacking, they fly to the exit locations
	public Transform leftExitLocation;
	Transform chosenExitLocation;

	public float cruisingSpeed;
	public float attackSpeed;

	Transform nextCruisingGoalLoc;

	int pathLeg;

	bool facingRight = true;				// flags to keep track of current behavior
	bool brownBirdCruisingArea;
	bool brownBirdAttacking;
	bool brownBirdAttackComplete;

	float DistanceToPoint;

	public AudioSource hawkScreechSource;
	public AudioClip hawkScreechClip;

	void Start (){

		gameObject.transform.position = wayPoint0.position;
		pathLeg = 1;
		nextCruisingGoalLoc = wayPoint1;
		brownBirdCruisingArea = true;
		brownBirdAttacking = false;
		brownBirdAttackComplete = false;

	}

	void OnTriggerEnter2D (Collider2D enteringCollider) {			// if the squirrel enters the bird's trigger area, 
																	// screech and attempt to attack
		
		if (enteringCollider.gameObject.tag == "Player") {
			hawkScreechSource.PlayOneShot(hawkScreechClip, 1.0f);
			
			
			if (!BrownBirdConfusionZones.squirrelHiddenInTrees) {
				brownBirdAttacking = true;
			} else {
				brownBirdAttacking = false;
			}
		}
	}

	void Update () {

		// if simply cruising, fly to next way point

		if (brownBirdCruisingArea) {
			CheckIfNeedToFlipBird (nextCruisingGoalLoc.position.x);
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, nextCruisingGoalLoc.position, cruisingSpeed * Time.deltaTime);
			DistanceToPoint = Mathf.Sqrt (Mathf.Pow ((gameObject.transform.position.x - nextCruisingGoalLoc.position.x), 2) + Mathf.Pow ((gameObject.transform.position.y - nextCruisingGoalLoc.position.y), 2));
			if (DistanceToPoint < 1.0f) {				// if it gets to way point, assign new pathLeg
					pathLeg ++;
					if (pathLeg > 3) {
							pathLeg = 0;
					}
					SelectNextPath (pathLeg);
			}

			if (brownBirdAttacking) {					// turn off cruising if attacking
				brownBirdCruisingArea = false;

			}
		} // end cruising

		// if bird or squirrel are in the trees/poles "confusion" areas, continue cruising
		if (BrownBirdConfusionZones.squirrelHiddenInTrees // || brownBirdConfused
		    ) {
			brownBirdAttacking = false;
			brownBirdCruisingArea = true;
		}

		// if attacking, move towards the squirrel
		if (brownBirdAttacking) {
			CheckIfNeedToFlipBird (theSquirrel.transform.position.x);
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, 
			                                                     new Vector3(theSquirrel.transform.position.x, theSquirrel.transform.position.y - 1.0f, theSquirrel.transform.position.z), 
			                                                     attackSpeed * Time.deltaTime);

			// if bird or squirrel are in the trees/poles "confusion" areas, continue cruising
			if (BrownBirdConfusionZones.squirrelHiddenInTrees // || brownBirdConfused
			    ) {
				brownBirdAttacking = false;
				brownBirdCruisingArea = true;
			}

			// if the bird hits the squirrel, stop attack behavior
			if (brownBirdAttackComplete) {
				brownBirdAttacking = false;
			}
		} // end attacking

		// if the bird is done attacking, move to an exit location
		// if it is reached, continue cruising behavior
		if (brownBirdAttackComplete) {
			CheckIfNeedToFlipBird (chosenExitLocation.position.x);
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, chosenExitLocation.position, attackSpeed * Time.deltaTime);
			if (Vector3.Distance(gameObject.transform.position, chosenExitLocation.position) <= 0.05f) {
				brownBirdCruisingArea = true;
				brownBirdAttacking = false;
				brownBirdAttackComplete = false;
				foreach (Collider2D childCollider in gameObject.GetComponents<Collider2D>()) {
					childCollider.enabled = true;
				}

			}
		} // end complete/exit behavior


	}  // end update

	void OnCollisionEnter2D (Collision2D enteringCollider) {
		int throwXDirection = 1;
		int throwYDirection = 1;

		// if the bird hits the squirrel
		if (enteringCollider.gameObject.tag == "Player") {
			CreatePCSquirrel.pcSquirrel.squirrelCurrentHealthValue -= 1;									// damage the squirrel
			SoundEffects.PlaySoundOnDamage ();

			// choose the bird's exit target based on which direction it attacked
			if (gameObject.transform.position.x > enteringCollider.gameObject.transform.position.x) {
				throwXDirection = -1;
				if (gameObject.transform.position.y > enteringCollider.gameObject.transform.position.y) {
					throwYDirection = -1;
					chosenExitLocation = leftExitLocation;
				} else {
					chosenExitLocation = rightExitLocation;
				}
			} else {
				if (gameObject.transform.position.y > enteringCollider.gameObject.transform.position.y) {
					throwYDirection = -1;
					chosenExitLocation = rightExitLocation;
				} else {
					chosenExitLocation = leftExitLocation;
				}
			} // end choose exit point

			// bump the squirrel when hit
			theSquirrel.GetComponent<Rigidbody2D>().AddForce (new Vector2 (30.0f * throwXDirection, 30.0f * throwYDirection), ForceMode2D.Impulse);

			// complete the attack
			brownBirdAttackComplete = true;

			// disable the bird's colliders (and its abilty to damage the squirrel further)
			foreach (Collider2D childCollider in gameObject.GetComponents<Collider2D>()) {
				childCollider.enabled = false;
			}

		}
	}


	public void SelectNextPath (int pathLeg) {

		// choose the next path in the cruising circuit
		switch (pathLeg) {
				case 0:
					nextCruisingGoalLoc = wayPoint0;
						break;
				case 1: 						// select second leg
					nextCruisingGoalLoc = wayPoint1;
						break;
				case 2:
					nextCruisingGoalLoc = wayPoint2;
						break;
				case 3:
					nextCruisingGoalLoc = wayPoint3;
					break;
				}

	} // select next path




	void CheckIfNeedToFlipBird (float targetXPos) {

		bool flip = false;

		// check to see if the facing of the bird is correct
		if ((gameObject.transform.position.x < targetXPos) && (!facingRight)) {
			flip = true;
		}

		if ((gameObject.transform.position.x > targetXPos) && (facingRight)) {
			flip = true;
		}

		// if it needs to be flipped, change sign of x dimention on bird
		if (flip) {
			facingRight = !facingRight;
			Vector3 tempScale = transform.localScale; 	
			tempScale.x *= -1;							
			transform.localScale = tempScale;
			flip = false;
		}

		
		
	}
	
}


