using UnityEngine;
using System.Collections;

public class GestureHandler : MonoBehaviour {

	// One for each player
	public IGestureEvaluator[] GestureEvaluators = new FakeGestureEvaluator[3];

	void Start () {
		
	}

	void Update () {
		foreach (var evaluator in GestureEvaluators) {
			DoStuff (evaluator.EvaluateGesture ());
		}
	}

	private void DoStuff (int gesture) {
		if (gesture == -1) {
			return;
		}
		Debug.Log (gesture);
	}
}
