using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject DragonPrefab;
	[SerializeField]
	private float SpawnCooldown = 1;
	[SerializeField]
	private float DragonSpeed = 0.2f;
	public float[] LanesY = new float[3];

	private List<Transform> Dragons = new List<Transform> ();

	// Use this for initialization
	void Start () {
		// Test
		StartCoroutine(Co_SpawnDragons());
	}
	
	// Update is called once per frame
	void Update () {
		//DestroyDragons ();
	}

	private void SpawnDragon () {
		float laneY = LanesY[Random.Range(0, 3)];
		Vector3 position = transform.position + Vector3.up * laneY;
		GameObject dragon = (GameObject)Instantiate (DragonPrefab, position, Quaternion.identity);
		BodypartSpriteSelector bodypartSpriteSelector = dragon.GetComponent<BodypartSpriteSelector> ();
		MoveAtConstantSpeed moveAtConstantSpeed = dragon.GetComponent<MoveAtConstantSpeed> ();
		DragonAttack dragonAttack = dragon.GetComponent<DragonAttack> ();
		Element[] elements = new Element[3];
		for (int i = 0; i < elements.Length; i++) {
			elements [i] = (Element)Random.Range (0, 3);
		}
		dragonAttack.Elements = elements;
		moveAtConstantSpeed.velocity = Vector2.left * DragonSpeed;
		//Dragons.Add (bodypartSpriteSelector.transform);
		bodypartSpriteSelector.SetBodyPart(BodyPartType.Head, elements[0]);
		bodypartSpriteSelector.SetBodyPart(BodyPartType.Body, elements[1]);
		bodypartSpriteSelector.SetBodyPart(BodyPartType.Wings, elements[2]);
		bodypartSpriteSelector.SetRoarSound(Random.Range(0,4));
		bodypartSpriteSelector.BeginRoaring();
	}

	private void DestroyDragons() {
		Stack<Transform> dragonsToDestroy = new Stack<Transform> ();
		foreach (var dragon in Dragons) {
			if (dragon.position.x < -10) {
				dragonsToDestroy.Push (dragon);
			}
		}
		while (dragonsToDestroy.Count > 0) {
			Transform dragon = dragonsToDestroy.Pop ();
			Dragons.Remove (dragon);
			Destroy (dragon.gameObject);
		}
	}

	private IEnumerator Co_SpawnDragons () {
		while (true) {
			SpawnDragon ();
			yield return new WaitForSeconds (SpawnCooldown);
		}
	}



	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		foreach (var laneY in LanesY) {
			Vector3 position = transform.position + Vector3.up * laneY;
			Gizmos.DrawLine (position, position + Vector3.left * 10);
		}
	}
}
