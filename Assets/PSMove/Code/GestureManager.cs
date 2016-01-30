using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GestureManager : MonoBehaviour, IGestureManager{
		
	public static IGestureManager Instance;

	public Action<GestureEvent> OnGesture { get; set; }

	List<MoveController> controllers = new List<MoveController>();

	public void Awake(){
		Instance = this;
	}

	public void AddController(MoveController controller){
		controllers.Add(controller);

		controller.Controller.ControllerId = (ControllerId) controllers.Count;
		controller.Controller.OnGesture = OnControllerGestureHandler;
	}

	public void RemoveController(MoveController controller){
		controllers.Remove(controller);
	}

	public void SetControllerLEDColor (ControllerId controllerId, Color color)
	{
		GetControllerById(controllerId).SetLED(color);
		GetControllerById(controllerId).Controller.SetLED(color);

		StartCoroutine(ResetColorAfterShortTime(controllerId));
	}

	public void SetControllerRumble (ControllerId controllerId, float rumbleIntensity, float rumbleDuration = 0.7f)
	{
		if(rumbleDuration != -1){
			StartCoroutine(DoRumble(controllerId, rumbleIntensity, rumbleDuration));
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

	private void OnControllerGestureHandler(ControllerId id, GestureType gesture){
		if(OnGesture != null){
			OnGesture(new GestureEvent() { GestureType = gesture, ControllerId = id});
		}
	}

	private MoveController GetControllerById(ControllerId id){
		return this.controllers.Find(x => x.Controller.ControllerId == id);
	}

	private IEnumerator DoRumble(ControllerId controllerId, float intensity, float duration){
		float totalTime = 0.0f;
		while(totalTime  < duration)
		{
			GetControllerById(controllerId).Controller.SetRumble(intensity);
			yield return new WaitForEndOfFrame();
			totalTime += Time.deltaTime;
		}
	}
}

public enum ControllerId {Zero = 0, One = 1, Two = 2};
public enum GestureType {Left, Right, Up, None = -1};

public class GestureEvent
{
	public ControllerId ControllerId;
	public GestureType GestureType;
}

public interface IGestureManager  {
	Action<GestureEvent> OnGesture {get; set;}
	void SetControllerLEDColor(ControllerId controllerId, Color color);
	/* rumble 0-1 */
	void SetControllerRumble(ControllerId controllerId, float rumbleIntensity, float rumbleDuration = -1);
}