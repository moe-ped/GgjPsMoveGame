using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScoreManager : MonoBehaviour {

	public static GameScoreManager Instance;

	public Text ScoreLabel;
	public Text LiveLabel;

	public int Score;

	public int Lives = 5;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	public void AddSlainDragon(){
		Score++;

		ScoreLabel.text = Score + " dragons slain!";
	}

	public void RemoveLive(){
		Lives--;
		
		LiveLabel.text = Lives + " left!";

		if(Lives== 0){

			Application.LoadLevel("YouLostScene");
		}
	}
}
