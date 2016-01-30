using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class NotificationManager : MonoBehaviour {

	public static NotificationManager Instance;
	
	public GameObject NotificationPrefab;


	List<GameObject> messages = new List<GameObject>();

	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	public void ShowMessage(string message)
	{
		var notification = GameObject.Instantiate(NotificationPrefab);

		var text = notification.GetComponent<Text>();
		text.text = message;

		messages.Add(notification);

		notification.transform.SetParent (GameObject.Find ("Canvas").transform);
	}

	public void Update(){

		for(int i = messages.Count -1; i >= 0; i--)
		{
			var msg = messages[i];
			var pos = msg.transform.position;
			pos.y -= 5;
			msg.transform.position = pos;

			if(pos.y <= -50){
				GameObject.Destroy(msg.gameObject);
				messages.RemoveAt(i);
			}
		}


	}
}
