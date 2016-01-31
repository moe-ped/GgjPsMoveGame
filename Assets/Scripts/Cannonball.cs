using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Cannonball : MonoBehaviour {

	[SerializeField]
	private GameObject[] ParticleSystems;
	[SerializeField]
	private Transform[] SpawnPositions = new Transform[3];
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
			Kill ();

			try {
				foreach(var controller in GestureManager.Instance.Controllers){
					GestureManager.Instance.SetControllerRumble(controller.Controller.ControllerId, 1.0f, 0.4f);
				}
			}
			catch (Exception ex) {
				Debug.LogWarning (ex + " -> ignored");
			}
		}
	}

	void Update(){
		if (this.transform.position.x >= 10 || this.transform.position.y > 7) {
			Kill ();
			GamePhaseManager.Instance.StartPhase (GamePhase.Focus);
		}

	}


	void Kill(){
		DestroyParticleSystems ();
		Destroy (gameObject);
	}


	void SpawnParticles () {
		try{
			int i = 0;
			foreach (var element in _elements)
			{
				Transform particleSystem = ((GameObject)Instantiate (ParticleSystems[(int)element], SpawnPositions[i].position, Quaternion.identity)).transform;
				particleSystem.SetParent (SpawnPositions[i]);
				SpawnedParticleSystems.Add (particleSystem);
				i++;
			}
		} 
		catch(Exception e){
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
