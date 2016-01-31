using UnityEngine;
using System.Collections;

public class ApprenticeAura : MonoBehaviour {
	
	[SerializeField]
	private Transform[] AuraEffects;
	[SerializeField]
	private int PlayerId;

	private IGestureProvider GestureProvider;


	void Start () {
		if(Application.platform == RuntimePlatform.OSXEditor){
			GestureProvider = FindObjectOfType<GestureManager> ();
		} else{
			GestureProvider = FindObjectOfType<KeyboardGestureProvider> ();
		}

		GestureProvider.OnGesture += OnAuraChanged;
	}

	// NOTE: not using ev
	private void OnAuraChanged (PSMoveEvent ev) {
		ExecuteDelayed (UpdateAura, 0.1f);
	}

	private void UpdateAura () {
		int activeAura = (int)Cannon.Instance.CurrentGestures [PlayerId];
		if (activeAura > 2 || activeAura < 0) {
			foreach (var aura in AuraEffects) {
				aura.gameObject.SetActive (false);
			}
			return;
		}
		if (!AuraEffects [activeAura].gameObject.activeSelf) {
			foreach (var aura in AuraEffects) {
				aura.gameObject.SetActive (false);
			}
			AuraEffects [activeAura].gameObject.SetActive (true);
		}
	}

	private void ExecuteDelayed (System.Action action, float delay) {
		StartCoroutine (Co_ExecuteDelayed (action, delay));
	}

	private IEnumerator Co_ExecuteDelayed (System.Action action, float delay) {
		yield return new WaitForSeconds (delay);
		action ();
	}
}
