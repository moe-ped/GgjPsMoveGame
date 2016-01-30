using UnityEngine;
using System.Collections;

public class DragonAttack : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Destroy (gameObject);
		}
	}
}
