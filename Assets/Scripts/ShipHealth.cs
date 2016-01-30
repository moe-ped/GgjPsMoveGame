using UnityEngine;
using System.Collections;

public class ShipHealth : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other) {
		Debug.Log ("ouch");
		if (other.gameObject.tag == "Enemy") {
			Destroy (other.gameObject);
		}
	}
}
