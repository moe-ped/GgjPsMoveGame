using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cannon : MonoBehaviour {

	public static Cannon Instance { get; private set; }
	public const float TimeToShootTogether = 1.5f;

	[SerializeField]
	private GameObject CannonballPrefab;
	[SerializeField]
	private Animator Animator;

	[SerializeField]
	private AudioClip fireSpellSound;

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
		Instance = this;
		if(Application.platform == RuntimePlatform.OSXEditor){
			GestureProvider = FindObjectOfType<GestureManager> ();
		} else{
			GestureProvider = FindObjectOfType<KeyboardGestureProvider> ();
		}

		GestureProvider.OnGesture += OnGestureHandler;
		Reset ();
	}


	private bool AllHaveGesture(){
		return CurrentGestures [0] != EventType.None && CurrentGestures [1] != EventType.None && CurrentGestures [2] != EventType.None;
	}

	public void OnGestureHandler (PSMoveEvent ev) {

		//if (GamePhaseManager.Instance.CurrentPhase == GamePhase.WaitForFire) 
		{
			if (ev.EventType == EventType.PsMoveButtonPressed && AllHaveGesture()) {
				var hasGesture = CurrentGestures [((int)ev.ControllerId) - 1] != EventType.None;

				if (hasGesture) {
					if (playersThatShot.Find (x => x == ev.ControllerId) != null) {
						playersThatShot.Add (ev.ControllerId);
						GestureManager.Instance.SetControllerRumble (ev.ControllerId, 0.8f, 0.1f);
					}

					if (!isInWaitingToShootPhase) {
						EnterWaitingToShootPhase ();
					}
				}
				return;
			}
		}

		if (GamePhaseManager.Instance.CurrentPhase == GamePhase.Cast) {
			if (ev.EventType == EventType.Left || ev.EventType == EventType.Right || ev.EventType == EventType.Up) {
				CurrentGestures [((int)ev.ControllerId) - 1] = ev.EventType;

				//NotificationManager.Instance.ShowMessage(ev.ControllerId + " : " + ev.EventType.ToString());

				GestureManager.Instance.SetControllerLEDColor (ev.ControllerId, GesturesToColorsMap [ev.EventType]);

				return;
			}
		}
	}

	private void EnterWaitingToShootPhase()
	{
		try {
			foreach (var controller in GestureManager.Instance.Controllers) {
				GestureManager.Instance.SetControllerRumble (controller.Controller.ControllerId, 0.65f, 50);
			}
		}
		catch(System.Exception ex) {
			Debug.LogWarning (ex);
		}

		isInWaitingToShootPhase = true;
		//TODO play sound, SHOOT MODE!!
		//NotificationManager.Instance.ShowMessage("Charge engaged!");
		StartCoroutine(WaitingToShootPhase());

	}

	IEnumerator WaitingToShootPhase(){
		GamePhaseManager.Instance.StartPhase (GamePhase.WaitForFire);
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

		try {
			foreach(var controller in GestureManager.Instance.Controllers){
				StartCoroutine(Blink (controller.Controller, controller.sphereLight.color));
				GestureManager.Instance.StopControllerRumble(controller.Controller.ControllerId);
			}
		}
		catch (System.Exception ex) {
			Debug.LogWarning (ex);
		}
		

		Reset ();
		isInWaitingToShootPhase = false;
	}

	private void FailedShoot(){
		//TODO play sound, Ahwww
		//NotificationManager.Instance.ShowMessage("Failed shot!");
		GamePhaseManager.Instance.StartPhase (GamePhase.Focus);
	}

	private void Shoot () {
		//NotificationManager.Instance.ShowMessage("Nice shot!");
		GamePhaseManager.Instance.StartPhase (GamePhase.Fire);

		
		GameObject cannonball = (GameObject) Instantiate (CannonballPrefab, transform.position, transform.rotation);
		cannonball.GetComponent<Cannonball> ().Elements = GesturesToElements(CurrentGestures);

		//add fire sound
		AudioSource.PlayClipAtPoint(fireSpellSound, cannonball.transform.position);

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
		{EventType.Up, Color.yellow}
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
