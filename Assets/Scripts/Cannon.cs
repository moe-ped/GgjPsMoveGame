using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cannon : MonoBehaviour {

	public const float TimeToShootTogether = 2.0f;

	[SerializeField]
	private GameObject CannonballPrefab;
	[SerializeField]
	private Animator Animator;

	private IGestureProvider GestureProvider;

	public EventType[] CurrentGestures = new EventType[3];

	List<ControllerId> playersThatShot = new List<ControllerId>();

	private bool isInWaitingToShootPhase = false;

	private bool AllGesturesMade {
		get {
			for (int i = 0; i < CurrentGestures.Length; i++) {
				if (CurrentGestures [i] == EventType.None) {
					return false;
				}
			}
			return true;
		}
	}

	// Test
	void Start () {
		GestureProvider = FindObjectOfType<KeyboardGestureProvider> ();
		//GestureProvider = FindObjectOfType<GestureManager> ();

		GestureProvider.OnGesture += OnGestureHandler;
		Reset ();
	}


	public void OnGestureHandler (PSMoveEvent ev) {

		if(ev.EventType == EventType.PsMoveButtonPressed)
		{
			var hasGesture = CurrentGestures [((int) ev.ControllerId)] != EventType.None;

			if(hasGesture){
				if(playersThatShot.Find(x => x == ev.ControllerId) != null){
					playersThatShot.Add(ev.ControllerId);
				}
				if(!isInWaitingToShootPhase){
					EnterWaitingToShootPhase();
				}
			}
			return;
		}

		if(ev.EventType == EventType.Left || ev.EventType == EventType.Right || ev.EventType == EventType.Up){
			CurrentGestures [((int) ev.ControllerId)] = ev.EventType;

			NotificationManager.Instance.ShowMessage(ev.ControllerId + " : " + ev.EventType.ToString());

			GestureManager.Instance.SetControllerLEDColor(ev.ControllerId, GesturesToColorsMap[ev.EventType]);
			GestureManager.Instance.SetControllerRumble(ev.ControllerId, 0.15f, 50);

			return;
		}
	}

	private void EnterWaitingToShootPhase()
	{
		isInWaitingToShootPhase = true;
		//TODO play sound, SHOOT MODE!!
		NotificationManager.Instance.ShowMessage("ALL SHOOT!");
		StartCoroutine(WaitingToShootPhase());

	}

	IEnumerator WaitingToShootPhase(){
		float time = 0.0f;
		bool shot = false;

		while((time += Time.deltaTime) <= TimeToShootTogether)
		{
			if(playersThatShot.Count >= 3)
			{
				Shoot ();
				shot = true;
				break;
			} 
			yield return new WaitForEndOfFrame();
		}

		if(!shot)
			FailedShoot();

		foreach(var controller in GestureManager.Instance.Controllers){
			StartCoroutine(Blink (controller.Controller, controller.sphereLight.color));
			GestureManager.Instance.StopControllerRumble(controller.Controller.ControllerId);
		}
		

		Reset ();
		isInWaitingToShootPhase = false;
	}

	private void FailedShoot(){
		//TODO play sound, Ahwww
		NotificationManager.Instance.ShowMessage("Failed shot!");
	}

	private void Shoot () {
		NotificationManager.Instance.ShowMessage("Nice shot!");
		
		GameObject cannonball = (GameObject) Instantiate (CannonballPrefab, transform.position, transform.rotation);
		cannonball.GetComponent<Cannonball> ().Elements = GesturesToElements(CurrentGestures);
		Rigidbody2D rigidbody2D = cannonball.GetComponent<Rigidbody2D> ();
		rigidbody2D.AddForce (transform.right*15, ForceMode2D.Impulse);
		Animator.Play ("Attack");
	}

	private void Reset() {
		for (int i = 0; i < CurrentGestures.Length; i++) {
			CurrentGestures [i] = EventType.None;
		}
		playersThatShot.Clear();
	}

	private Element[] GesturesToElements(EventType[] gestures) {
		Element[] elements = new Element[gestures.Length];
		for (int i=0; i<gestures.Length; i++) {
			elements [i] = (Element)(int)gestures [i];
		}
		return elements;
	}



	private static Dictionary<EventType, Color> GesturesToColorsMap = new Dictionary<EventType, Color>(){
		{EventType.Left, Color.red},
		{EventType.Right, Color.blue},
		{EventType.Up, Color.green}
	};

	
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
