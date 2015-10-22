using UnityEngine;
using System.Collections;

public class CreditsAsOverlay : MonoBehaviour {

	// show the credits screen as an overlay object inside of a scroll box

	Vector2 scrollPosition;
	Touch myTouch;
	float screeWidthDivisor = 20.0f;

	public float creditsAreaXPos;
	public float creditsAreaYPos;
	public float creditsAreaWidth;
	public float creditsAreaHeight;

	float creditsAreaX;
	float creditsAreaY;
	float creditsAreaCalcWidth;
	float creditsAreaCalcHeight;


	// Use this for initialization
	void Start () {
		SetAreaDimensions ();
	}

	void OnGUI () {
		GUI.skin.label.alignment = TextAnchor.UpperLeft;
		GUI.skin.label.fixedWidth = 0;
		GUI.skin.label.fontStyle = FontStyle.Normal;
		GUI.skin.label.fontSize = Screen.width / 1536 * 64;
		GUI.skin.label.richText = true;

		GUI.skin.button.richText = true;
		GUI.skin.button.fontStyle = FontStyle.Normal;
		GUI.skin.button.wordWrap = true;
		GUI.skin.button.alignment = TextAnchor.UpperLeft;
		GUI.skin.button.fixedWidth = 0;	
		GUI.skin.button.fontSize = Screen.width/1536*36;

		// ********* Begin ScrollView
		GUILayout.BeginArea (new Rect(creditsAreaX, creditsAreaY, creditsAreaCalcWidth, creditsAreaCalcHeight));
		GUILayout.BeginVertical ();

		GUI.color = Color.red;
		GUILayout.Label ("Credits", GUILayout.MinHeight(50), GUILayout.MaxHeight(150));
		GUI.skin.label.fontSize = Screen.width/1536*36;	
		GUI.color = Color.white;

		GUILayout.Label ("Timothy Bradrick - Full Sail University - August 2015\n"); 

		GUI.skin.verticalScrollbar.fixedWidth = Screen.width * 0.05f;
		GUI.skin.verticalScrollbarThumb.fixedWidth = Screen.width * 0.05f;
		scrollPosition = GUILayout.BeginScrollView (scrollPosition);

		GUI.color = Color.white;
		GUILayout.Label ("Game, all artwork and sounds, except as noted below, are copyright Timothy Bradrick 2015\n"); 

		GUILayout.Label ("Audio");
		
		if (GUILayout.Button ("clfox. (2015 February 25). <i>bird sounds.wav.</i> Retrieved from http://www.freesound.org/ people/ clfox/ sounds/ 265043/"))
			Application.OpenURL("http://www.freesound.org/people/clfox/sounds/265043/");
		GUILayout.Space (Screen.height / screeWidthDivisor);

		if (GUILayout.Button ("davidworksonline. (2008 June 9). <i>crow1.wav.</i> Retrieved from http://www.freesound.org/ people/ davidworksonline/ sounds/ 54973/"))
			Application.OpenURL("http://www.freesound.org/people/davidworksonline/sounds/54973/");
		GUILayout.Space (Screen.height / screeWidthDivisor);

		if (GUILayout.Button ("hoersturz. (2010 August 17). <i>Soda Butte Squirrel.</i> Retrieved from http://www.freesound.org/ people/ hoersturz/ sounds/ 103140/"))
			Application.OpenURL("http://www.freesound.org/people/hoersturz/sounds/103140/");
		GUILayout.Space (Screen.height / screeWidthDivisor);

		if (GUILayout.Button ("jessepash. (2011 December 28). <i>Crowd.Yay.Applause.25ppl.Long.wav.</i> Retrieved from http://www.freesound.org/ people/ jessepash/ sounds/ 139973/"))
			Application.OpenURL("http://www.freesound.org/people/jessepash/sounds/139973/");
		GUILayout.Space (Screen.height / screeWidthDivisor);
		
		if (GUILayout.Button ("JoelAudio. (2011 December 4). <i>ELECTRIC_ZAP_001.wav</i> Retrieved from http://www.freesound.org/ people/ JoelAudio/ sounds/ 136542/"))
			Application.OpenURL("http://www.freesound.org/people/JoelAudio/sounds/136542/");
		GUILayout.Space (Screen.height / screeWidthDivisor);
		
		if (GUILayout.Button ("Koenig, M. (2011 June 4). <i>Hawk Screeching Sound.</i> Retrieved from http://soundbible.com/ 1844-Hawk-Screeching.html"))
			Application.OpenURL("http://soundbible.com/1844-Hawk-Screeching.html");
		GUILayout.Space (Screen.height / screeWidthDivisor);

		if (GUILayout.Button ("philberts. (2011 January 4). <i>Red Squirrel Chatter.</i> Retrieved from http://www.freesound.org/ people/ philberts/ sounds/ 111393/"))
			Application.OpenURL("http://www.freesound.org/people/philberts/sounds/111393/");
		GUILayout.Space (Screen.height / screeWidthDivisor);

		if (GUILayout.Button ("PsychoBird. (2010 July 3). <i>Hawk Call 2x Sound.</i> Retrieved from http://soundbible.com/ 1481-Hawk-Call-2x.html"))
			Application.OpenURL("http://soundbible.com/1481-Hawk-Call-2x.html");
		GUILayout.Space (Screen.height / screeWidthDivisor);
		
		if (GUILayout.Button ("Richmond, K. (2014 April 28). <i> Basket of Apples.</i> On 'Easy Listening - On the Move' [Audio file]. Ontario, Canada: Westar Music Publishing"))
			Application.OpenURL("http://westarmusic.sourceaudio.com/#!details?id=6856943");
		GUILayout.Space (Screen.height / screeWidthDivisor);

		if (GUILayout.Button ("superEGsonic, K. (2014 April 28). <i>small_person_animal(s)_in_leaves.wav</i> Retrieved from http://www.freesound.org/ people/ superEGsonic/ sounds/ 167572/"))
			Application.OpenURL("http://www.freesound.org/people/superEGsonic/sounds/167572/");
		GUILayout.Space (Screen.height / screeWidthDivisor);
		
		GUILayout.Label ("APIs");
		
		if (GUILayout.Button ("Skared Creations. (2014, March 16). <i>combü</i> Retrieved from http://skaredcreations.com/ wp/ products/ combu/ #sthash.pJB4xhxb.dpbs"))
			Application.OpenURL("http://skaredcreations.com/wp/products/combu/#sthash.pJB4xhxb.dpbs");
		GUILayout.Space (Screen.height / screeWidthDivisor);

		if (GUILayout.Button ("Unity Technologies. (2014, November 13). <i>Unity Ads.</i> Retrieved from https://www.assetstore.unity3d.com/ en/ #!/ content/ 21027"))
			Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/content/21027");
		GUILayout.Space (Screen.height / screeWidthDivisor);

		GUILayout.EndVertical ();
		GUILayout.EndScrollView ();

		GUILayout.EndArea ();
	}

	// allow scrolling in other parts of the screen besides the scrollbar
	void Update () {
		if (Input.touchCount > 0) {
			
			myTouch = Input.touches [0];
			if (myTouch.phase == TouchPhase.Moved) {
				scrollPosition.y += myTouch.deltaPosition.y * 7;
			}
		}
	}

	void SetAreaDimensions () {
		
		creditsAreaX = Screen.width * creditsAreaXPos;
		creditsAreaY = Screen.height * creditsAreaYPos;
		creditsAreaCalcWidth = Screen.width * creditsAreaWidth;
		creditsAreaCalcHeight = Screen.height * creditsAreaHeight;

	}
	

}
