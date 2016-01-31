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
			StartCoroutine (DragonDied(gameObject));
		}
	}

	private bool ThatHurt(Element[] elements) {
		List<Element> elementList = new List<Element> (elements);
		foreach (var ele in Elements) {
			if (!elementList.Contains(ele)) {
				return false;
			}
		}
		return true;
	}
		
	IEnumerator DragonDied(GameObject dragon) {
		dragon.GetComponent<Animator>().Play("Died");
		GameScoreManager.Instance.AddSlainDragon();
		yield return new WaitForSeconds(3f);
		Destroy (dragon);
	}
}
