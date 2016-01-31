using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

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
		notification.transform.localScale = Vector3.one;

		var extraY = messages.Count != 0 ? messages.Last().transform.position.y - 5 : 0;
		notification.transform.localPosition = new Vector3(0,extraY);
	}

	public void Update(){

		for(int i = messages.Count -1; i >= 0; i--)
		{
			var msg = messages[i];
			var pos = msg.transform.position;
			pos.y += 0.1f;
			msg.transform.position = pos;

			if(pos.y >= 1000){
				GameObject.Destroy(msg.gameObject);
				messages.RemoveAt(i);
			}
		}


	}
}
