using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

	public void GoToInstructionsScene() {
		SceneManager.LoadScene("Instructions");
	}

	public void StartGame() {
		SceneManager.LoadScene("Main");
	}
}
