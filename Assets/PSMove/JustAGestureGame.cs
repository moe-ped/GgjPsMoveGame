using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JustAGestureGame : MonoBehaviour {

	private static Dictionary<EventType, Color> GesturesToColorsMap = new Dictionary<EventType, Color>(){
		{EventType.Left, Color.red},
		{EventType.Right, Color.blue},
		{EventType.Up, Color.green}
	};

	void Start () 
	{
		GestureManager.Instance.OnGesture += OnGestureHandler;
	}

	void OnGestureHandler(PSMoveEvent ev)
	{
		if(ev.EventType == EventType.PsMoveButtonPressed){
			StartCoroutine(Blink (ev.Controller.Controller, ev.Controller.sphereLight.color));
			
			GestureManager.Instance.StopControllerRumble(ev.ControllerId);
			NotificationManager.Instance.ShowMessage("BOOM!");
		}
		else{
			NotificationManager.Instance.ShowMessage(ev.ControllerId + " : " + ev.EventType.ToString());
			GestureManager.Instance.SetControllerLEDColor(ev.ControllerId, GesturesToColorsMap[ev.EventType]);
			GestureManager.Instance.SetControllerRumble(ev.ControllerId, 0.15f, 50);
		}
	}

	IEnumerator Blink(UniMoveController controller, Color color){
		GestureManager.Instance.SetControllerLEDColor(controller.ControllerId, Color.white);
		yield return new WaitForSeconds(0.05f);
		GestureManager.Instance.SetControllerLEDColor(controller.ControllerId, color);
		yield return new WaitForSeconds(0.05f);
		GestureManager.Instance.SetControllerLEDColor(controller.ControllerId, Color.white);
		yield return new WaitForSeconds(0.05f);
		GestureManager.Instance.SetControllerLEDColor(controller.ControllerId, color);
		yield return new WaitForSeconds(0.05f);
		GestureManager.Instance.SetControllerLEDColor(controller.ControllerId, Color.white);
	}
}
