using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class FakeGestureEvaluator : IGestureEvaluator {

	// TODO: connect this to a Move controller instead
	[SerializeField]
	private KeyCode[] Gestures = new KeyCode[3];

	#region IGestureEvaluator implementation

	public int EvaluateGesture ()
	{
		// TODO: actually evaluate gestures
		for (int i = 0; i < Gestures.Length; i++) {
			if (Input.GetKeyUp (Gestures [i])) {
				return i;
			}
		}
		return -1;
	}

	#endregion
}
