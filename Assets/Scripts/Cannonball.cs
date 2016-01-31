using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cannonball : MonoBehaviour {

	[SerializeField]
	private GameObject[] ParticleSystems;
	private List<Transform> SpawnedParticleSystems = new List<Transform>();

	private Element[] _elements;
	public Element[] Elements {
		get {
			return _elements;
		}
		set {
			_elements = value;
			SpawnParticles ();
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if (other.gameObject.tag == "Enemy")
		{
			var dragon = other.gameObject.GetComponent<DragonEnemy> ();
			dragon.HitWithElements (Elements);
			DestroyParticleSystems ();
			Destroy (gameObject);
		}
	}

	void SpawnParticles () {
		try{
		foreach (var element in _elements)
		{
			Transform particleSystem = ((GameObject)Instantiate (ParticleSystems[(int)element], transform.position, Quaternion.identity)).transform;
			particleSystem.SetParent (transform);
			SpawnedParticleSystems.Add (particleSystem);
		}
		} catch(UnityException e){
			Debug.Log(e);
		}
	}

	private void DestroyParticleSystems() {
		foreach (var particleSystem in SpawnedParticleSystems) {
			particleSystem.transform.parent = null;
			// Just put it somewhere REALLY far away, before I do very bad things to it
			particleSystem.position += Vector3.down * 1000;
			Destroy (particleSystem.gameObject, 5);
		}
	}
}
