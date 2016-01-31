using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public enum GamePhase {Focus = 0, Cast =1, WaitForFire=2, Fire=3};

public class GamePhaseManager : MonoBehaviour {

	public static GamePhaseManager Instance;

	public GamePhase CurrentPhase;

	List<ControllerId> playersThatShot = new List<ControllerId>();

	public List<GameObject> Overlays;


	// Use this for initialization
	void Awake () {
		Instance = this;

		StartPhase (GamePhase.Focus);
	}
	
	public void StartPhase(GamePhase phase){
		foreach (var overlay in Overlays) {
			overlay.gameObject.SetActive (false);
		}

		this.CurrentPhase = phase;

		switch (phase) {
		case GamePhase.Focus:
			StartFocusFase ();
			Overlays [0].SetActive (true);
			break;
		case GamePhase.Cast:
			// handle in cannon
			Overlays [1].SetActive (true);
			break;
		case GamePhase.WaitForFire:
			// handle in cannon
			Overlays [2].SetActive (true);
			break;
		case GamePhase.Fire:
			// handle in cannonball
			Overlays [3].SetActive (true);
			break;
		}
	}

	void StartFocusFase(){
		GetGestureProvider ().OnGesture += FocusPhaseGestureHandler;
		StartCoroutine (WaitForAllToFocus ());
	}


	void FocusPhaseGestureHandler(PSMoveEvent ev){
		if(!playersThatShot.Contains(ev.ControllerId)){
			playersThatShot.Add(ev.ControllerId);
			GestureManager.Instance.SetControllerRumble (ev.ControllerId, 0.8f, 0.1f);
		}
	}

	IEnumerator WaitForAllToFocus() 
	{
		Time.timeScale = 0.3f;

		while (playersThatShot.Count < 3) {
			yield return new WaitForEndOfFrame ();
		}

		GetGestureProvider ().OnGesture -= FocusPhaseGestureHandler;
		playersThatShot.Clear ();
		StartPhase (GamePhase.Cast);

		Time.timeScale = 1f;
	}

	private IGestureProvider GetGestureProvider(){
		if(Application.platform == RuntimePlatform.OSXEditor){
			return FindObjectOfType<GestureManager> ();
		} else{
			return FindObjectOfType<KeyboardGestureProvider> ();
		}
	}

}
