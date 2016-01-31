using UnityEngine;
using System.Collections;

public class Cannon : MonoBehaviour {

	[SerializeField]
	private GameObject CannonballPrefab;


	private IGestureProvider GestureProvider;

	private EventType[] Gestures = new EventType[3];
	private bool AllGesturesMade {
		get {
			for (int i = 0; i < Gestures.Length; i++) {
				if (Gestures [i] == EventType.None) {
					return false;
				}
			}
			return true;
		}
	}

	// Test
	void Start () {
		//GestureProvider = FindObjectOfType<KeyboardGestureProvider> ();
		GestureProvider = FindObjectOfType<GestureManager> ();

		GestureProvider.OnGesture += AddGesture;
		ResetGestures ();
	}

	public void AddGesture (PSMoveEvent ev) {
		if(ev.EventType == EventType.Left || ev.EventType == EventType.Right || ev.EventType == EventType.Up){
			Gestures [(int) ev.ControllerId] = ev.EventType;
			if (AllGesturesMade) {
				Shoot ();
				ResetGestures ();
			}
		}
	}

	private void Shoot () {
		GameObject cannonball = (GameObject) Instantiate (CannonballPrefab, transform.position, transform.rotation);
		cannonball.GetComponent<Cannonball> ().Elements = GesturesToElements(Gestures);
		Rigidbody2D rigidbody2D = cannonball.GetComponent<Rigidbody2D> ();
		rigidbody2D.AddForce (transform.right*15, ForceMode2D.Impulse);
	}

	private void ResetGestures() {
		for (int i = 0; i < Gestures.Length; i++) {
			Gestures [i] = EventType.None;
		}
	}

	private Element[] GesturesToElements(EventType[] gestures) {
		Element[] elements = new Element[gestures.Length];
		for (int i=0; i<gestures.Length; i++) {
			elements [i] = (Element)(int)gestures [i];
		}
		return elements;
	}
}
