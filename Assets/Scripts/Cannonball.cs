using UnityEngine;
using System.Collections;

public class Cannonball : MonoBehaviour {

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Enemy") {
			Destroy (gameObject);
			StartCoroutine(DragonDied(other.gameObject));
		}
	}

	IEnumerator DragonDied(GameObject dragon) {
		dragon.GetComponent<Animator>().Play("Died");
		yield return new WaitForSeconds(3f);
		Destroy (dragon);
	}
}
