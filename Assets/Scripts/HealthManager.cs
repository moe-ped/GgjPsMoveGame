using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {

	public static HealthManager Instance;

	[SerializeField]
	int Lives = 5;

	// Use this for initialization
	void Awake () {
		CreateInstance();
	}

	void CreateInstance() {
		if(Instance == null) 
			Instance = this;
	}

	public void LoseLive() {
		UpdateLiveUI();
		Lives--;
	}

	void UpdateLiveUI() {
		GameObject[] lives = GameObject.FindGameObjectsWithTag("Live");
		if(lives.Length > 0) {
			Destroy(lives[lives.Length-1]);
		}
	}
}
