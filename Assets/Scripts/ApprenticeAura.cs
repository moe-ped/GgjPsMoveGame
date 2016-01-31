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

		GestureProvider.OnGesture += UpdateAura;
	}

	// NOTE: not using ev
	void UpdateAura (PSMoveEvent ev) {
		int activeAura = (int)Cannon.Instance.CurrentGestures [PlayerId];
		if (activeAura > 2) {
			return;
		}
		foreach (var aura in AuraEffects) {
			aura.gameObject.SetActive (false);
		}
		AuraEffects [activeAura].gameObject.SetActive (true);
	}
}
