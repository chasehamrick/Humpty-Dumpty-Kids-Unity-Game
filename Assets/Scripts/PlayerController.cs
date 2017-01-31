using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	bool text;
	bool text2;
	bool text3;
	bool text4;
	Rect rect;
	public GameObject dialogue;
	bool keyAlternate = false;
	public float speed;
	public float acceleration;
	public float force;
	Vector3 dir;
	Vector3 oldRot;
	float maxSpeed = 18000F;
	private Rigidbody rb;


	void Start()
	{
		keyAlternate = false;
		rect = new Rect(750F, 300F, 425, 150);
		rb = GetComponent<Rigidbody> ();
		oldRot = transform.rotation.eulerAngles;
	}

	void Update()
	{
		if (speed > 0) {
			speed *= 0.965F;
		}
	
		if (Input.GetKeyDown (KeyCode.LeftArrow) /*&& keyAlternate == false*/) {
			/*
			gameObject.transform.position += transform.forward * speed * Time.deltaTime;

			speed += acceleration;

			if (speed > maxSpeed) {
				speed = maxSpeed;
			}
			*/
			GetComponent<Rigidbody> ().AddForce(transform.forward * speed);
			speed += acceleration * 3;

			if (speed > maxSpeed) {
				speed = maxSpeed;
			}

			keyAlternate = true;

		} else if (Input.GetKeyDown (KeyCode.RightArrow) /*&& keyAlternate == true*/) {
			/*
			gameObject.transform.position += transform.forward * speed * Time.deltaTime;

			speed += acceleration;

			if (speed > maxSpeed) {
				speed = maxSpeed;
			}
			*/
			GetComponent<Rigidbody> ().AddForce (transform.forward * speed);
			speed += acceleration *3;

			if (speed > maxSpeed) {
				speed = maxSpeed;
			}

			keyAlternate = false;
		}
	}

	void FixedUpdate()
	{
		if (!transform.rotation.Equals(oldRot))
		{
			transform.rotation = Quaternion.Euler (oldRot.x, oldRot.y, oldRot.z);
		}
		/*
		if (gameObject.transform.position.z != 3.08)
		{
			if (gameObject.transform.position.z > 3.08) {
				while (gameObject.transform.position.z != 3.08) {
					gameObject.transform.position -= transform.right * speed * Time.deltaTime;
				}
			} else {
				while (gameObject.transform.position.z != 3.08) {
					gameObject.transform.position += transform.right * speed * Time.deltaTime;
				}
			}
		}
		*/
	}

	void OnCollisionEnter(Collision col) {
		if (col.collider.tag == "Enemy") {
			dir = col.contacts [0].point - transform.position;
			dir = -dir.normalized;
			GetComponent<Rigidbody>().AddForce(dir*force);
		}
	}
}