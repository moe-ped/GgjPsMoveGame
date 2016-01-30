using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public struct BodyPartCollection {
	[HideInInspector]
	public string Name;
	public Sprite Head;
	public Sprite Body;
	public Sprite LeftWing;
	public Sprite RightWing;
	public bool UseSecondaryWings;

	public BodyPartCollection (string name) {
		Name = name;
		Head = null;
		Body = null;
		LeftWing = null;
		RightWing = null;
		UseSecondaryWings = false;
	}
}

public enum BodyPartType { 
	Head, 
	Body, 
	Wings 
}
public enum Element {
	Fire,
	Water,
	Plant
}

public class BodypartSpriteSelector : MonoBehaviour {

	[SerializeField]
	private Transform Head;
	[SerializeField]
	private Transform Body;
	[SerializeField]
	private Transform LeftWing;
	[SerializeField]
	private Transform RightWing;
	[SerializeField]
	private Transform SecondLeftWing;
	[SerializeField]
	private Transform SecondRightWing;
	[SerializeField]
	public BodyPartCollection[] BodyPartCollections = new global::BodyPartCollection[3] {
		new global::BodyPartCollection("Fire"),
		new global::BodyPartCollection("Water"),
		new global::BodyPartCollection("Earth")
	};

	public void Init() {
	}

	public void SetBodyPart(BodyPartType bodyPartType, Element element) {
		switch (bodyPartType) {
		case BodyPartType.Head:
			Head.gameObject.GetComponent<SpriteRenderer> ().sprite = BodyPartCollections [(int)element].Head;
			break;
		case BodyPartType.Body:
			Body.gameObject.GetComponent<SpriteRenderer> ().sprite = BodyPartCollections [(int)element].Body;
			LeftWing.gameObject.SetActive(BodyPartCollections[(int)element].UseSecondaryWings == false);
			RightWing.gameObject.SetActive(BodyPartCollections[(int)element].UseSecondaryWings == false);
			SecondLeftWing.gameObject.SetActive(BodyPartCollections[(int)element].UseSecondaryWings);
			SecondRightWing.gameObject.SetActive(BodyPartCollections[(int)element].UseSecondaryWings);
			break;
		case BodyPartType.Wings:
			SecondLeftWing.gameObject.GetComponent<SpriteRenderer> ().sprite = BodyPartCollections [(int)element].LeftWing;
			SecondRightWing.gameObject.GetComponent<SpriteRenderer> ().sprite = BodyPartCollections [(int)element].RightWing;

			LeftWing.gameObject.GetComponent<SpriteRenderer> ().sprite = BodyPartCollections [(int)element].LeftWing;
			RightWing.gameObject.GetComponent<SpriteRenderer> ().sprite = BodyPartCollections [(int)element].RightWing;

			break;
		}
	}
}
