using UnityEngine;
using System.Collections;

public class destructionController : MonoBehaviour {

	public GameObject Wall;
	public bool touchingWall = false;
	float x = 16.932F;
	float y = 0.373F;
	float z = 1.62F;
	Rigidbody rb;
	public AudioClip crumble;
	public AudioClip smash;

	void Update () {
		bool held = Input.GetKey(KeyCode.Space);

		if (touchingWall) {
			if (held) {
				Instantiate (Wall, new Vector3(gameObject.transform.position.x + x, 
					gameObject.transform.position.y + y, 
					gameObject.transform.position.z + z), 
					gameObject.transform.rotation);
				AudioSource.PlayClipAtPoint (crumble, gameObject.transform.position);
				AudioSource.PlayClipAtPoint (smash, gameObject.transform.position);
			    Destroy (gameObject);
		    }
	    }
    }

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Player") {
			touchingWall = true;
		}
	}
}
