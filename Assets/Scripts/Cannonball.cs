using UnityEngine;
using System.Collections;

public class Cannonball : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}
