using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GestureManager : MonoBehaviour, IGestureProvider{
		
	public static GestureManager Instance;

	public Action<PSMoveEvent> OnGesture { get; set; }

	public List<MoveController> Controllers{ get{

			if(Application.platform != RuntimePlatform.OSXEditor) return null;
			return _controllers;
		}
	}


	List<MoveController> _controllers;

	public void Awake(){
		Instance = this;
	}

	public void AddController(MoveController controller){
		if(Application.platform != RuntimePlatform.OSXEditor) return;
		
		Controllers.Add(controller);

		controller.Controller.ControllerId = (ControllerId) Controllers.Count;
		controller.Controller.OnEvent = OnControllerGestureHandler;
	}

	public void RemoveController(MoveController controller){
		if(Application.platform != RuntimePlatform.OSXEditor) return;
		
		Controllers.Remove(controller);
	}

	public void SetControllerLEDColor (ControllerId controllerId, Color color, float resetColorAfterTime = -1)
	{
		if(Application.platform != RuntimePlatform.OSXEditor) return;

		GetControllerById(controllerId).SetLED(color);
		GetControllerById(controllerId).Controller.SetLED(color);

		if(resetColorAfterTime != -1){
			StartCoroutine(ResetColorAfterShortTime(controllerId));
		}
	}

	public void StopControllerRumble(ControllerId controllerId){
		if(Application.platform != RuntimePlatform.OSXEditor) return;
		
		GetControllerById(controllerId).Controller.StopConstantRumbleIfActive();
	}

	public void SetControllerRumble (ControllerId controllerId, float rumbleIntensity, float rumbleDuration = 0.7f)
	{
		if(Application.platform != RuntimePlatform.OSXEditor) return;
		
		if(rumbleDuration != -1)
		{
			GetControllerById(controllerId).Controller.LongerRumble(rumbleIntensity, rumbleDuration);
		} 
		else{
			GetControllerById(controllerId).Controller.SetRumble(rumbleIntensity);
		}
	}

	private IEnumerator ResetColorAfterShortTime(ControllerId id){
		yield return new WaitForSeconds(0.7f);
		GetControllerById(id).SetLED(Color.white);
		GetControllerById(id).Controller.SetLED(Color.white);
	}

	private void OnControllerGestureHandler(ControllerId id, EventType gesture){
		if(OnGesture != null){
			OnGesture(new PSMoveEvent() { EventType = gesture, ControllerId = id, Controller = GetControllerById(id)});
		}
	}

	private MoveController GetControllerById(ControllerId id){
		return this.Controllers.Find(x => x.Controller.ControllerId == id);
	}


}

public enum ControllerId {Zero = 0, One = 1, Two = 2};
public enum EventType {Left, Right, Up, PsMoveButtonPressed, None = -1};

public class PSMoveEvent
{
	public MoveController Controller;
	public ControllerId ControllerId;
	public EventType EventType;
}

//public interface IGestureManager  {
//	Action<PSMoveEvent> OnGesture {get; set;}
//	void SetControllerLEDColor(ControllerId controllerId, Color color, float resetColorAfterTime = -1);
//	/* rumble 0-1 */
//	void SetControllerRumble(ControllerId controllerId, float rumbleIntensity, float rumbleDuration = -1);
//	void StopControllerRumble(ControllerId controllerId);
//