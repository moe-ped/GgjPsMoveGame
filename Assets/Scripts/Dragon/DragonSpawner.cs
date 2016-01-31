using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct Level {
	public int KillsForNextLevel;
	public float SpawnCooldown;
	public float DragonSpeed;
	public float DragonSpeedRandomDeviation;
}

public class DragonSpawner : MonoBehaviour {

	public static DragonSpawner Instance { get; private set;}

	[SerializeField]
	private GameObject DragonPrefab;
	[SerializeField]
	private Level[] LevelBalancingValues;
	public int CurrentLevel = 0;
	private static int DragonsKilled = 0;

	private List<Transform> Dragons = new List<Transform> ();

	public Transform[] StartPositions;
	public Transform[] EndPositions;
	
	// Use this for initialization
	void Start () {
		Instance = this;
		StartCoroutine(Co_SpawnDragons());
	}
	
	// Update is called once per frame
	void Update () {
		//DestroyDragons ();


	}

	public void OnDragonKilled() {
		DragonsKilled++;
		if (DragonsKilled >= LevelBalancingValues [CurrentLevel].KillsForNextLevel) {
			CurrentLevel++;
		}
	}

	private void SpawnDragon () {
		int lane = Random.Range(0, 3);
		Vector3 startPosition = StartPositions[lane].position;
		GameObject dragon = (GameObject)Instantiate (DragonPrefab, startPosition, Quaternion.identity);
		BodypartSpriteSelector bodypartSpriteSelector = dragon.GetComponent<BodypartSpriteSelector> ();
		MoveAtConstantSpeed moveAtConstantSpeed = dragon.GetComponent<MoveAtConstantSpeed> ();
		DragonEnemy dragonAttack = dragon.GetComponent<DragonEnemy> ();
		Element[] elements = new Element[3];
		for (int i = 0; i < elements.Length; i++) {
			elements [i] = (Element)Random.Range (0, 3);
		}
		dragonAttack.Elements = elements;
		moveAtConstantSpeed.velocity = ( EndPositions[lane].position - startPosition).normalized * (LevelBalancingValues[CurrentLevel].DragonSpeed + Random.Range(-LevelBalancingValues[CurrentLevel].DragonSpeedRandomDeviation, LevelBalancingValues[CurrentLevel].DragonSpeedRandomDeviation));
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
			yield return new WaitForSeconds (LevelBalancingValues[CurrentLevel].SpawnCooldown);
		}
	}

	//void OnDrawGizmos() {
	//	Gizmos.color = Color.green;
	//	foreach (var laneY in lan) {
	//		Vector3 position = transform.position + Vector3.up * laneY;
	//		Gizmos.DrawLine (position, position + Vector3.left * 10);
	//	}
	//}
}
