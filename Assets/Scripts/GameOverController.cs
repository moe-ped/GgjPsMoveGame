using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {
	public static GameOverController Instance;

	[SerializeField]
	GameObject GameOverPanel;

	void Awake () {
		CreateInstance();
	}

	void CreateInstance() {
		if(Instance == null) 
			Instance = this;
	}
	
	public void GameOver(){
		Time.timeScale = 0;
		GameOverPanel.SetActive(true);
	}

	public void RestarGame() {
		SceneManager.LoadScene("Main");
		Time.timeScale = 1f;
	}

	public void QuitGame() {
		Application.Quit();
	}
}
