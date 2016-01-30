using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	[SerializeField]
	private GameObject CannonballPrefab;

	// Test
	/*void Update () {
		if (Input.GetKeyDown (KeyCode.S)) {
			Shoot ();
		}
	}*/

	public void Shoot () {
		GameObject cannonball = (GameObject) Instantiate (CannonballPrefab, transform.position, transform.rotation);
		Rigidbody2D rigidbody2D = cannonball.GetComponent<Rigidbody2D> ();
		rigidbody2D.AddForce (transform.right*15, ForceMode2D.Impulse);
	}
}
