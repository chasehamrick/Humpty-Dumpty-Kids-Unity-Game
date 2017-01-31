using UnityEngine;
using System.Collections;

public class selfDestruct : MonoBehaviour {

	public GameObject player;


	void Awake() {
	    Physics.IgnoreLayerCollision(8,9, true);
	}

	void Start() {
		StartCoroutine (Destruct ());
	}

	IEnumerator Destruct () {
		yield return new WaitForSeconds(10);
		Destroy (gameObject);
	
	}
}
