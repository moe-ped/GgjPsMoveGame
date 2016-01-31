using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScoreManager : MonoBehaviour {

	public static GameScoreManager Instance;

	public Text ScoreLabel;

	public int Score;

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	public void AddSlainDragon(){
		Score++;

		ScoreLabel.text = Score + " dragons slain!";
	}
}
