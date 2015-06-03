using UnityEngine;
using System.Collections;

public class FoxRun : MonoBehaviour {
	
	public float maxX;
	public float minX;

	public GameObject foxTutorial;

	public AudioClip eat;
	public AudioClip die;

	private AudioSource source;

	private GameObject cloneTutorial;
	
	private Animator animator;
	
	private GameObject catchedChicken;
	
	private GameObject player;
	
	private GameObject gameController;

	private bool isAlive = true;

	private float randomSpeed = 0.0f;
	
	
	void Awake(){
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		gameController = GameObject.Find ("GameController");

		source = GetComponent<AudioSource>();
	}
	
	// Use this for initialization
	void Start () {
		//randomSpeed = Random.Range (3.5f,5.0f);
		randomSpeed = Random.Range (3.3f,(3.3f + GameController.Level / 6.18f));
		if(!GameController.IsTapFoxTutorialFinished){
			cloneTutorial = Instantiate (foxTutorial,transform.position,foxTutorial.transform.rotation) as GameObject;
		}
	}
	
	void OnBecameInvisible() {
		if (isAlive && gameController) {
			gameController.SendMessage ("OnFoxEscape", SendMessageOptions.DontRequireReceiver);
		}
		Destroy (gameObject);
	}
	
	void OnMouseDown() {
		if (player) {
			player.SendMessage ("OnPointedOutFox", gameObject.transform, SendMessageOptions.DontRequireReceiver);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag.Equals ("Player")) {
			animator.SetTrigger ("EncounterChicken");
			catchedChicken = other.gameObject;
			source.clip = eat;
			source.Play();
		}else if (other.gameObject.tag.Equals ("Friend")) {
			animator.SetTrigger ("EncounterChicken");
			catchedChicken = other.gameObject;
			source.clip = eat;
			source.Play();
		}else if(other.gameObject.tag.Equals("Egg")){
			isAlive = false;
			animator.SetTrigger("EncounterEgg"); //this animation added FallDown() Event.
			Destroy(other.gameObject);
			GameController.CurrentFoxCount ++;
			source.clip = die;
			source.Play();
		}
	}
	
	void DestoryChicken(){
		if(!catchedChicken){return;}

		if (catchedChicken.tag.Equals ("Player")) {
			gameController.SendMessage("OnGameOver",SendMessageOptions.DontRequireReceiver);
			
		}else if (catchedChicken.tag.Equals ("Friend")) {
			gameController.SendMessage("OnFriendPassedAway",SendMessageOptions.DontRequireReceiver);
			
		}
		Destroy (catchedChicken);
	}
	
	
	void Update(){

		if(!GameController.GamePaused){
			transform.Translate (-Vector2.right * (2 * 3.0f + randomSpeed) *  Time.deltaTime);
		}

		if(cloneTutorial){
			cloneTutorial.transform.position = gameObject.transform.position;
		}

		if(Input.GetKeyUp(KeyCode.RightArrow)){
			OnMouseDown();
		}
		
	}
	
	void FallDown(){//this is called by animation event.

		Destroy (gameObject);
	}
}
