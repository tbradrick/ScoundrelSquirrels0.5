using UnityEngine;
using System.Collections;

public class DropAnApple : MonoBehaviour {

	// script assigned to the apple tree to drop an apple when the squirrel collides with it

	int applesAddedInWorld = 0;
	public GameObject droppedApple;
	GameObject newApple;

	public Transform appleStartLoc;
	public Transform appleDropLoc;

	bool moveNewApple;
	bool readytoAddNewApple = true;

	public Sprite[] numberSprite;
	public GameObject numberIndicator;

	// Update is called once per frame
	void Update () {

		// move the apple from the tree to the ground
		if ((moveNewApple) && (newApple != null)) {
			newApple.transform.position = Vector3.MoveTowards(newApple.transform.position, appleDropLoc.position, 0.25f);
		}

		// allow a new apple to drop if the previous one is finished falling
		if ((applesAddedInWorld > 0) && (newApple != null)) {
			if (newApple.transform.position == appleDropLoc.position) {
				moveNewApple =  false;
				readytoAddNewApple = true;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D thisCollision) {

		// drop an apple on collision
		if ((thisCollision.gameObject.tag == "Player") && (applesAddedInWorld < 5) && (readytoAddNewApple)) {
			newApple = (GameObject) Instantiate(droppedApple, appleStartLoc.position, Quaternion.identity);
			applesAddedInWorld ++;
			moveNewApple =  true;
			readytoAddNewApple = false;
			newApple.transform.position = Vector3.MoveTowards(newApple.transform.position, appleDropLoc.position, 0.125f);
			numberIndicator.GetComponent<SpriteRenderer> ().sprite = numberSprite [5-applesAddedInWorld];

		}
	}


}
