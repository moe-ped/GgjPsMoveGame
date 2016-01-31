using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	[SerializeField]
	private GameObject CannonballPrefab;
	[SerializeField]
	private KeyboardGestureProvider GestureProvider;
	[SerializeField]
	private Animator Animator;

	private GestureType[] Gestures = new GestureType[3];
	private bool AllGesturesMade {
		get {
			for (int i = 0; i < Gestures.Length; i++) {
				if (Gestures [i] == GestureType.None) {
					return false;
				}
			}
			return true;
		}
	}

	// Test
	void Start () {
		GestureProvider = FindObjectOfType<KeyboardGestureProvider> ();
		GestureProvider.EventGestureMade += AddGesture;
		ResetGestures ();
	}

	public void AddGesture (GestureType type, int controllerId) {
		Gestures [controllerId] = type;
		if (AllGesturesMade) {
			Shoot ();
			ResetGestures ();
		}
	}

	private void Shoot () {
		GameObject cannonball = (GameObject) Instantiate (CannonballPrefab, transform.position, transform.rotation);
		cannonball.GetComponent<Cannonball> ().Elements = GesturesToElements(Gestures);
		Rigidbody2D rigidbody2D = cannonball.GetComponent<Rigidbody2D> ();
		rigidbody2D.AddForce (transform.right*15, ForceMode2D.Impulse);
		Animator.Play ("Attack");
	}

	private void ResetGestures() {
		for (int i = 0; i < Gestures.Length; i++) {
			Gestures [i] = GestureType.None;
		}
	}

	private Element[] GesturesToElements(GestureType[] gestures) {
		Element[] elements = new Element[gestures.Length];
		for (int i=0; i<gestures.Length; i++) {
			elements [i] = (Element)(int)gestures [i];
		}
		return elements;
	}
}
