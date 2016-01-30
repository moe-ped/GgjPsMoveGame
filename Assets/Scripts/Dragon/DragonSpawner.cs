using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject DragonPrefab;
	[SerializeField]
	private float SpawnCooldown = 1;
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
		BodypartSpriteSelector bodypartSpriteSelector = ((GameObject)Instantiate (DragonPrefab, position, Quaternion.identity)).GetComponent<BodypartSpriteSelector> ();
		//Dragons.Add (bodypartSpriteSelector.transform);
		bodypartSpriteSelector.SetBodyPart(BodyPartType.Head, (Element)Random.Range(0, 3));
		bodypartSpriteSelector.SetBodyPart(BodyPartType.Body, (Element)Random.Range(0, 3));
		bodypartSpriteSelector.SetBodyPart(BodyPartType.Wings, (Element)Random.Range(0, 3));
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		foreach (var laneY in LanesY) {
			Vector3 position = transform.position + Vector3.up * laneY;
			Gizmos.DrawLine (position, position + Vector3.left * 10);
		}
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
}
