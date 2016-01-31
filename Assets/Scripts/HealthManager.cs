using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HealthManager : MonoBehaviour {

	public static HealthManager Instance;

	[SerializeField]
	public GameObject[] Lives = new GameObject[5];

	[SerializeField]
	WHCameraRandomShaker shaker;

	[SerializeField]
	AudioClip[] ouchSounds;

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
		shaker.doShake();
		AudioSource.PlayClipAtPoint(ouchSounds[Random.Range(0,ouchSounds.Length)], transform.position, 1f);
	}

	void UpdateLiveUI() {
		Destroy(Lives[livesCount-1]);
		livesCount--;

		if(livesCount == 0)
			GameOverController.Instance.GameOver();
	}
		
}
