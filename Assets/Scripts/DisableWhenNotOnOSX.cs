using UnityEngine;
using System.Collections;

public class DisableWhenNotOnOSX : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		this.gameObject.SetActive(Application.platform == RuntimePlatform.OSXEditor);
	}
}
