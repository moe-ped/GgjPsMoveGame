using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthManager : MonoBehaviour {

	public static HealthManager Instance;

	[SerializeField]
	public GameObject[] Lives = new GameObject[5];

	int livesCount;

	// Use this for initialization
	void Awake () {
		CreateInstance();
		livesCount = Lives.Length;
	}

	void CreateInstance() {
		if(Instance == null) 
			Instance = this;
	}

	public void LoseLive() {
		UpdateLiveUI();

	}

	void UpdateLiveUI() {
		Destroy(Lives[livesCount-1]);
		livesCount--;

		if(livesCount == 0)
			GameOverController.Instance.GameOver();
	}
		
}
