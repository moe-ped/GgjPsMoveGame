using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// And die and stuff ... -> consider renaming
public class DragonEnemy : MonoBehaviour {

	public Element[] Elements;

	// Use this for initialization
	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Destroy (gameObject);

			HealthManager.Instance.LoseLive();
		}
	}

	public void HitWithElements (Element[] elements) {
		if (ThatHurt (elements)) {
			DragonSpawner.Instance.OnDragonKilled ();
			StartCoroutine (DragonDied (gameObject));
		} else {
			GamePhaseManager.Instance.StartPhase (GamePhase.Focus);
		}
			
	}

	private bool ThatHurt(Element[] elements) {
		List<Element> elementList = new List<Element> (elements);
		int rightElements = 0;
		foreach (var ele in Elements) {
			if (elementList.Contains(ele)) {
				elementList.Remove (ele);
				rightElements++;
			}
		}
		return rightElements == 3;
	}
		
	IEnumerator DragonDied(GameObject dragon) {
		dragon.GetComponent<Animator>().Play("Died");
		GameScoreManager.Instance.AddSlainDragon();
		yield return new WaitForSeconds(0.6f);
		Destroy (dragon);
		GamePhaseManager.Instance.StartPhase (GamePhase.Focus);
	}
}
