using UnityEngine;
using System.Collections;

public class LookatCursor : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		UpdateRotation ();
	}

	private void UpdateRotation() {
		Vector3 worldPosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		worldPosition.z = transform.position.z;
		transform.right = worldPosition - transform.position;
	}
}
