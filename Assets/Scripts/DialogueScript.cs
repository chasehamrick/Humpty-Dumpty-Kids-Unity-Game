using UnityEngine;
using System.Collections;

public class DialogueScript : MonoBehaviour {

	bool start;
	bool start2;
	bool start3;
	bool start4;
	bool start5;
	bool movePeasant;
	bool movement;
	bool close;
	Rect rect = new Rect(675F, 200F, 700, 400);
	GameObject talkingPeasant;
	GameObject player;
	public float minDist;
	public float moveSpeed;
	public Font myFont;
	public GameObject before;
	public GameObject during;

	// Use this for initialization
	void Start () {
		talkingPeasant = GameObject.FindGameObjectWithTag ("TalkingPeasant");
		player = GameObject.FindGameObjectWithTag ("Player");
		close = false;
	}

	void Update() {
		bool held = Input.GetKeyDown(KeyCode.Space);
		if (held) {
			start = false;
			close = !close;
		}
		if (movePeasant == true) {
			if (Vector3.Distance (talkingPeasant.transform.position, player.transform.position) > minDist) {
				talkingPeasant.transform.position += transform.right * moveSpeed * Time.deltaTime;
			} else if (Vector3.Distance (talkingPeasant.transform.position, player.transform.position) <= minDist) {
				start = true;
				talkingPeasant.GetComponent<PeasantController> ().enabled = false;
				movePeasant = false;
			}
		} else
		if (movement == true) {
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().enabled = true;
			talkingPeasant.GetComponent<PeasantController> ().enabled = true;
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			other.GetComponent<PlayerController> ().enabled = false;
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
		myStyle.focused.background = GUI.skin.textField.focused.background;
		myStyle.normal.textColor = Color.white;
		myStyle.active.textColor = Color.white;
		myStyle.hover.textColor = Color.white;
		Vector2 myVector = new Vector2 (0, 70);
		myStyle.contentOffset = myVector;
		if (start == true) {
			GUI.TextField (rect, "Head Peasant: \n\nStop you heathen!!!! We'll never let you destroy our village!!! \n\n[Press to close]", myStyle);
			start2 = true;
		}
		if (start2 && close == true) {
			GUI.TextField (rect, "Humpty: \n\nI only want to take down these walls!! \n\nDon't you see they aren't good for anyone?\n\n I have no problems with you!!! \n\n[Press to close]", myStyle);
			start3 = true;
		}
		if (start3 && close == false) {
			GUI.TextField (rect, "Head Peasant: \n\nDON'T LISTEN TO HIS LIES!!! \n\nStop humpty!!!!\n\n It is the order of the KING!!! \n\n[Press to close]", myStyle);
			start2 = false;
			start4 = true;
		}
		if (start4 && close == true) {
			GUI.TextField (rect, "Uh Oh! The peasants aren't listening! \n\nIt's for their own good! \n\nGet past the peasant and knock down the walls as fast as you can!!\n\n[Press to close]", myStyle);
			start3 = false;
			start5 = true;
		}
		if (start5 && close == false) {
			start4 = false;
			movement = true;
		}
	}
}
