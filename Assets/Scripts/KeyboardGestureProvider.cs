using UnityEngine;
using System.Collections;
using System;

public class KeyboardGestureProvider : MonoBehaviour, IGestureProvider {

	[Serializable]
	public struct Controller {
		public KeyCode[] Gestures;
	}

	#region IGestureProvider implementation
	public Action<EventType, int> EventGestureMade {
		get {
			return _eventGestureMade;
		}
		set {
			_eventGestureMade = value;
		}
	}
	#endregion

	private Action<EventType, int> _eventGestureMade;

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
					EventGestureMade((EventType)g, c);
				}
			}
		}
	}
}
