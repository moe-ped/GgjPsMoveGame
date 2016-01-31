using UnityEngine;
using System.Collections;
using System;

public class MoveController : MonoBehaviour {

	public UniMoveController Controller;
	public Transform Sphere;

	Renderer sphereRenderer;
	public Light sphereLight;

	public Action OnRight;
	public Action OnUp;

	public void Awake () {
		Transform[] trs = GetComponentsInChildren<Transform>();
		foreach (Transform tr in trs) {
			if(tr.name == "Sphere") {
				sphereRenderer = tr.GetComponent<Renderer>();
				Sphere = tr;
			}
		}
		sphereLight = GetComponentInChildren<Light>();
	}

	public void Update(){
		//Sphere.
	

	}


	public void SetLED(Color color)
	{
		sphereLight.enabled = color != Color.black;
		sphereRenderer.material.color = color == Color.black ? Color.white * 0.2f : color;
		sphereLight.color = color;
		sphereLight.intensity =  color.grayscale * 4;
	}


}
