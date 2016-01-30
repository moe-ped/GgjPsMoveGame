using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JustAGestureGame : MonoBehaviour {

	private static Dictionary<GestureType, Color> GesturesToColorsMap = new Dictionary<GestureType, Color>(){
		{GestureType.Left, Color.green},
		{GestureType.Right, Color.blue},
		{GestureType.Up, Color.red}
	};

	void Start () 
	{
		GestureManager.Instance.OnGesture += OnGestureHandler;
	}

	void OnGestureHandler(GestureEvent ev)
	{
		NotificationManager.Instance.ShowMessage(ev.ControllerId + " : " + ev.GestureType.ToString());
		GestureManager.Instance.SetControllerLEDColor(ev.ControllerId, GesturesToColorsMap[ev.GestureType]);
		GestureManager.Instance.SetControllerRumble(ev.ControllerId, 0.3f);
	}
}
