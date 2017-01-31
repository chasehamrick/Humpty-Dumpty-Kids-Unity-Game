using UnityEngine;
using System.Collections;

public class PeasantController : MonoBehaviour {

	Transform player;
	public float moveSpeed;
	public float rotationSpeed;
	public float minDist;
	public float force1;
	public float force2;
	public int numberPushed;
	Vector3 dir;
	Vector3 oldPos;
	Vector3 oldRot;
	public bool playerCollide;
	public AudioClip grunt;
	public AudioClip scream;

	void Start () {
		numberPushed = 0;
		player = GameObject.FindWithTag ("Player").transform;
		oldPos = transform.position;
		oldRot = transform.rotation.eulerAngles;
		playerCollide = false;

	}

	void Update () {
		bool held = Input.GetKey(KeyCode.Space);


		if (!held) {
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(player.position - transform.position), 
				rotationSpeed * Time.deltaTime);

			transform.LookAt (player.transform);

			if (Vector3.Distance (transform.position, player.transform.position) <= minDist) {
				transform.position += transform.forward * moveSpeed * Time.deltaTime;
			}
		}

		if (playerCollide) {
			if (held) {
				if (numberPushed < 2) {
					GetComponent<Rigidbody> ().AddForce (dir * force1);
					AudioSource.PlayClipAtPoint (grunt, gameObject.transform.position);
					numberPushed++;
					playerCollide = false;
				} else {
					GetComponent<Rigidbody> ().AddForce (dir * force2);
					AudioSource.PlayClipAtPoint (scream, gameObject.transform.position);
				}
			}
		}

	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Player") {
			dir = col.contacts [0].point - transform.position;
			dir = -dir.normalized;
			playerCollide = true;
		}
	}
}
