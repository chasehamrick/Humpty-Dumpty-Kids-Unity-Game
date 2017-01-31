using UnityEngine;
using System.Collections;

public class DialogueScript2 : MonoBehaviour {

	bool start;
	bool start2;
	bool start3;
	bool start4;
	bool start5;
	bool start6;
	bool start7;
	bool start8;
	bool start9;
	bool start10;
	bool start11;
	bool close;
	bool highScore;
	bool movePeasant;
	Rect rect = new Rect(675F, 200F, 700, 400);
	Rect rect2 = new Rect (675F, 200F, 700, 500);
	GameObject talkingPeasant;
	GameObject player;
	public float minDist;
	public float moveSpeed;
	public Font myFont;

	string timerText;
	private float levelTimer;
	private float bestTime;
	private bool updateTimer;
	// Use this for initialization
	void Start () {
		updateTimer = true;
		levelTimer = 0.0F;
		talkingPeasant = GameObject.FindGameObjectWithTag ("TalkingPeasant2");
		player = GameObject.FindGameObjectWithTag ("Player");
		if (PlayerPrefs.GetFloat("besttime") == 0.0F) {
			PlayerPrefs.SetFloat ("besttime", 1000.0F);
			PlayerPrefs.Save ();
		} else {
			bestTime = PlayerPrefs.GetFloat ("besttime");
		}
	}

	void Update() {
		bool held = Input.GetKeyDown(KeyCode.Space);
		if (held) {
			start = false;
			close = !close;
		}
		if (updateTimer) {
			levelTimer += Time.deltaTime * 1;
			timerText = levelTimer.ToString ();
		}
		if (movePeasant == true) {
			if (Vector3.Distance (talkingPeasant.transform.position, player.transform.position) > minDist) {
				talkingPeasant.transform.position += transform.right * moveSpeed * Time.deltaTime;
			} else if (Vector3.Distance (talkingPeasant.transform.position, player.transform.position) <= minDist) {
				start = true;
				movePeasant = false;
			}
		} 
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			other.GetComponent<PlayerController> ().enabled = false;
			updateTimer = false;
			movePeasant = true;
		}
	}

	void OnGUI() {
		GUIStyle myStyle = new GUIStyle ();
		myStyle.font = myFont;
		myStyle.alignment = TextAnchor.UpperCenter;
		myStyle.fontSize = 30;
		myStyle.normal.background = GUI.skin.textField.normal.background;
		myStyle.active.background = GUI.skin.textField.active.background;
		myStyle.hover.background = GUI.skin.textField.hover.background;
		myStyle.normal.textColor = Color.white;
		myStyle.active.textColor = Color.white;
		myStyle.hover.textColor = Color.white;
		myStyle.focused.background = GUI.skin.textField.focused.background;
		Vector2 myVector = new Vector2 (0, 70);
		myStyle.contentOffset = myVector;
		if (start == true) {
			if (levelTimer < bestTime) {
				bestTime = levelTimer;
				highScore = true;
				PlayerPrefs.SetFloat ("besttime", bestTime);
				PlayerPrefs.Save ();
			}
			GUI.TextField (rect, "Peasants on ground: \n\nOwwwww....!!!! \n\n[Press to close]", myStyle);
			start2 = true;
		}
		if (start == false && start2 == true && close == true) {
			GUI.TextField (rect, "Humpty: \n\nI am truly sorry... \n\nBut I must knock down all the walls in the Kingdom!!\n\n[Press to close]", myStyle);
			start3 = true;
		}
		if (start3 == true && close == false) {
			GUI.TextField (rect, "Humpty: \n\nI'm tired of cracking my shell for the sake of the king at my expense... \n\nThey never put me back together right again...\n\n[Press to close]", myStyle);
			start2 = false;
			start4 = true;
		}
		if (start4 == true && close == true) {
			GUI.TextField (rect, "Head Peasant: \n\nBut the King told us you were going to destroy our village...\n\n[Press to close]", myStyle);
			start3 = false;
			start5 = true;
		}
		if (start5 == true && close == false) {
			GUI.TextField (rect, "Head Peasant: \n\nIn that case....\n\n[Press to close]", myStyle);
			start4 = false;
			start6 = true;
		}
		if (start6 == true && close == true) {
			GUI.TextField (rect2, "Head Peasant: \n\nWe never liked those walls anyway! \n\n Never made any sense for there to be so many walls \n\nso close together!! \n\n It's suffocating!! \n\n[Press to close]", myStyle);
			start5 = false;
			start7 = true;
		}
		if (start7 == true && close == false) {
			GUI.TextField (rect, "Humpty: \n\nThe King is misguided! \n\n I'll make him see the error of his ways for everyone's sake!! \n\n[Press to close]", myStyle);
			start6 = false;
			start8 = true;
		}
		if (start8 == true && close == true) {
			GUI.TextField (rect, "Head Peasant: \n\nI see.... \n\n Sorry we got in your way... \n\n[Press to close]", myStyle);
			start7 = false;
			start9 = true;
		}
		if (start9 == true && close == false) {
			GUI.TextField (rect, "Head Peasant: \n\nThe king has been a big jerk lately... \n\nMaybe Humpty is the hero we have needed for so long! \n\n[Press to close]", myStyle);
			start8 = false;
			start10 = true;
		}
		if (start10 == true && close == true) {
			if (!highScore) {
				GUI.TextField (rect, "Score in seconds: " + timerText + "\n\nSorry! You didn't beat the high score! \n\n[Press to close]", myStyle);
				start9 = false;
				start11 = true;
			} else {
				GUI.TextField (rect, "Score in seconds: " + timerText + "\n\nGreat Job! You have the fastest time! \n\n[Press to close]", myStyle);
				start9 = false;
				start11 = true;
			}
		}
		if (start11 == true && close == false) {
			GUI.TextField (rect, "Humpty has renewed his confidence in his goal... \n\nTime to move on to the Outer Kingdom! \n\nWatch out for all King's knights! \n\n[Press to Load Level 2]", myStyle);
			start10 = false;
		}
	}
}
