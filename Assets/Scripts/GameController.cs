using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * @author Mihail Georgiev
 * */


public class GameController : MonoBehaviour {

	[SerializeField]
	Image P1_Up_img, P1_Down_img, P1_Left_img, P1_Right_img,
	P2_Up_img, P2_Down_img, P2_Left_img, P2_Right_img,
	P3_Up_img, P3_Down_img, P3_Left_img, P3_Right_img, contolLight_img;

	[SerializeField]
	Text sequenceToMatch_txt, inputSequence_txt, countdown_txt;

	[SerializeField]
	GameObject ballPrefab;

	bool beginInput = false;

	const int P1_U = 0, P1_D = 1 , P1_L= 2, P1_R= 3,
	P2_U = 4, P2_D = 5, P2_L = 6, P2_R = 7,
	P3_U = 8, P3_D = 9, P3_L = 10, P3_R = 11;

	ArrayList sequenceToMatch, seqeunceToMatch;


	void Start () {
		sequenceToMatch = new ArrayList();
		seqeunceToMatch = new ArrayList();
		createNewRandomSequenceToMatch();
		StartCoroutine("inputCountdown5sec");
	}

	void Update () {
		if(beginInput) {
			if(Input.GetKeyDown(KeyCode.W)) {
				P1_Up_img.color = Color.blue;
				seqeunceToMatch.Add(P1_U);
			}

			if(Input.GetKeyDown(KeyCode.A)){
				P1_Left_img.color = Color.blue;
				seqeunceToMatch.Add(P1_L);
			}

			if(Input.GetKeyDown(KeyCode.S)) {
				P1_Down_img.color = Color.blue;
				seqeunceToMatch.Add(P1_D);
			}

			if(Input.GetKeyDown(KeyCode.D)) {
				P1_Right_img.color = Color.blue;
				seqeunceToMatch.Add(P1_R);
			}

			if(Input.GetKeyDown(KeyCode.Z)){
				P2_Up_img.color = Color.blue;
				seqeunceToMatch.Add(P2_U);
			}
				
			if(Input.GetKeyDown(KeyCode.G)){
				P2_Left_img.color = Color.blue;
				seqeunceToMatch.Add(P2_L);
			}


			if(Input.GetKeyDown(KeyCode.H)){
				P2_Down_img.color = Color.blue;
				seqeunceToMatch.Add(P2_D);
			}

			if(Input.GetKeyDown(KeyCode.J)){
				P2_Right_img.color = Color.blue;
				seqeunceToMatch.Add(P2_R);
			}

			if(Input.GetKeyDown(KeyCode.UpArrow)){
				P3_Up_img.color = Color.blue;
				seqeunceToMatch.Add(P3_U);
			}

			if(Input.GetKeyDown(KeyCode.LeftArrow)){
				P3_Left_img.color = Color.blue;
				seqeunceToMatch.Add(P3_L);
			}

			if(Input.GetKeyDown(KeyCode.DownArrow)){
				P3_Down_img.color = Color.blue;
				seqeunceToMatch.Add(P3_D);
			}

			if(Input.GetKeyDown(KeyCode.RightArrow)){
				P3_Right_img.color = Color.blue;
				seqeunceToMatch.Add(P3_R);
			}

			if(seqeunceToMatch.Count == sequenceToMatch.Count) {
				if(checkSeq()){
					shootBall();
					contolLight_img.color = Color.green;
					StopCoroutine("inputCountdown5sec");
					ResetStuff();
					createNewRandomSequenceToMatch();
					StartCoroutine("inputCountdown5sec");
				} else {
					contolLight_img.color = Color.red;
					StopCoroutine("inputCountdown5sec");
					ResetStuff();
					StartCoroutine("inputCountdown5sec");
				}
			}
		}

		inputSequence_txt.text = getMappedTextFromSeq(seqeunceToMatch);
	}

	void createNewRandomSequenceToMatch(){
		sequenceToMatch.Clear();
		int capacity = Random.Range(1, 6);
		for(int i = 0; i < capacity; i++) {
			sequenceToMatch.Add(Random.Range(0, 12));
		}
			
		sequenceToMatch_txt.text = getMappedTextFromSeq(sequenceToMatch);
	}


	IEnumerator inputCountdown5sec() {
		countdown_txt.text = "5";
		yield return new WaitForSeconds(1f);
		countdown_txt.text = "4";
		yield return new WaitForSeconds(1f);
		countdown_txt.text = "3";
		yield return new WaitForSeconds(1f);
		countdown_txt.text = "2";
		yield return new WaitForSeconds(1f);
		countdown_txt.text = "1";
		yield return new WaitForSeconds(1f);
		countdown_txt.text = "0";
		ResetStuff ();
		beginInput = true;
		StartCoroutine("inputCountdown5sec");
	}

	void ResetStuff () {
		seqeunceToMatch.Clear();
		ResetColors();
	}

	bool checkSeq() {
		for(int i = 0; i < sequenceToMatch.Count; i++) {
				if(!sequenceToMatch[i].Equals(seqeunceToMatch[i]))
				return false;
		}

		return true;
	}

	void ResetColors() {
		P1_Up_img.color = Color.white;
		P1_Down_img.color = Color.white;
		P1_Left_img.color = Color.white;
		P1_Right_img.color = Color.white;

		P2_Up_img.color = Color.white;
		P2_Down_img.color = Color.white;
		P2_Left_img.color = Color.white;
		P2_Right_img.color = Color.white;


		P3_Up_img.color = Color.white;
		P3_Left_img.color = Color.white;
		P3_Down_img.color = Color.white;
		P3_Right_img.color = Color.white;
	}

	string getMappedTextFromSeq(ArrayList seq) {
		string t = ""; 

		foreach(var item in seq) {
			t += decodeDir((int)item);
		} 

		return t;
	}

	string decodeDir(int i) {
		switch (i) {
		case 0:
			return "W ";
			break;
		case 1:
			return "S ";
			break;
		case 2:
			return "A ";
			break;
		case 3:
			return "D ";
			break;
		case 4:
			return "Z ";
			break;
		case 5:
			return "H ";
			break;
		case 6:
			return "G ";
			break;
		case 7:
			return "J ";
			break;
		case 8:
			return "Up ";
			break;
		case 9:
			return "Down ";
			break;
		case 10:
			return "Left ";
			break;
		case 11:
			return "Right ";
			break;

		default: 
			return "";

		}
	}


	void shootBall() {
		GameObject ball = Instantiate(ballPrefab);
		ball.GetComponent<Rigidbody>().velocity = new Vector3 (10f,10f,0);
	}

}
