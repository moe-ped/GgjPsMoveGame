using UnityEngine;
using System.Collections;

public class MoveAtConstantSpeed : MonoBehaviour {

	public Vector3 velocity;

	void Update () {
		Move ();
	}

	private void Move () {
		transform.Translate (velocity * Time.deltaTime);
	}
}
