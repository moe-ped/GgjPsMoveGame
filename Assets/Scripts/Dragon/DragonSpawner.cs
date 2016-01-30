using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonSpawner : MonoBehaviour {

	[SerializeField]
	private GameObject DragonPrefab;
	public float[] LanesY = new float[3];

	private List<Transform> Dragons = new List<Transform> ();

	// Use this for initialization
	void Start () {
		// Test
		SpawnDragon();
	}
	
	// Update is called once per frame
	void Update () {
		DestroyDragons ();
	}

	private void SpawnDragon () {
		float laneY = LanesY[Random.Range(0, 3)];
		Vector3 position = transform.position + Vector3.up * laneY;
		BodypartSpriteSelector bodypartSpriteSelector = ((GameObject)Instantiate (DragonPrefab, transform.position, Quaternion.identity)).GetComponent<BodypartSpriteSelector> ();
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
		//for 
	}
}
