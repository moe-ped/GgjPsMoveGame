using UnityEngine;
using System.Collections;
using System;


public class KeyboardGestureProvider : MonoBehaviour,IGestureProvider {

	[Serializable]
	public struct Controller {
		public KeyCode[] Gestures;
	}

	public Action<PSMoveEvent> OnGesture {get; set;}

	// not actual controllers ...
	[SerializeField]
	private Controller[] Controllers;

	void Update () {
		EvaluateGestures ();
	}

	public void EvaluateGestures ()
	{
		for (int c = 0; c < Controllers.Length; c++) {
			for (int g = 0; g < Controllers[c].Gestures.Length; g++) {
				if (Input.GetKeyUp (Controllers[c].Gestures [g])) {
					OnGesture( new PSMoveEvent() { EventType = (EventType) g, ControllerId = (ControllerId) c+1});
				}
			}
		}
	}
}
