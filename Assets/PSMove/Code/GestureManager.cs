using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GestureManager : MonoBehaviour, IGestureManager{
		
	public static IGestureManager Instance;

	public Action<PSMoveEvent> OnGesture { get; set; }

	List<MoveController> controllers = new List<MoveController>();

	public void Awake(){
		Instance = this;
	}

	public void AddController(MoveController controller){
		controllers.Add(controller);

		controller.Controller.ControllerId = (ControllerId) controllers.Count;
		controller.Controller.OnEvent = OnControllerGestureHandler;
	}

	public void RemoveController(MoveController controller){
		controllers.Remove(controller);
	}

	public void SetControllerLEDColor (ControllerId controllerId, Color color, float resetColorAfterTime = -1)
	{
		GetControllerById(controllerId).SetLED(color);
		GetControllerById(controllerId).Controller.SetLED(color);

		if(resetColorAfterTime != -1){
			StartCoroutine(ResetColorAfterShortTime(controllerId));
		}
	}

	public void StopControllerRumble(ControllerId controllerId){
		GetControllerById(controllerId).Controller.StopConstantRumbleIfActive();
	}

	public void SetControllerRumble (ControllerId controllerId, float rumbleIntensity, float rumbleDuration = 0.7f)
	{
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
		return this.controllers.Find(x => x.Controller.ControllerId == id);
	}


}

public enum ControllerId {Zero = 0, One = 1, Two = 2};
public enum EventType {Left, Right, Up, PsMoveButtonPressed};

public class PSMoveEvent
{
	public MoveController Controller;
	public ControllerId ControllerId;
	public EventType EventType;
}

public interface IGestureManager  {
	Action<PSMoveEvent> OnGesture {get; set;}
	void SetControllerLEDColor(ControllerId controllerId, Color color, float resetColorAfterTime = -1);
	/* rumble 0-1 */
	void SetControllerRumble(ControllerId controllerId, float rumbleIntensity, float rumbleDuration = -1);
	void StopControllerRumble(ControllerId controllerId);
}