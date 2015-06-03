using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public AudioClip runMusic;
	public AudioClip flyMusic;
	public AudioClip gameOverMusic;

	AudioSource source;
	GameController controller;
	PlayerController playerController;

	private bool gameOvered = false;

	void Awake(){
		source = GetComponent<AudioSource>();
		controller = GameObject.FindObjectOfType<GameController> ();
		controller.GameOverEvent += OnGameOver;

		playerController = GameObject.Find("ChickenPlayer").GetComponent<PlayerController>();
		playerController.RunEvent += OnRunTigger;
		playerController.FlyEvent += OnFlyTiggler;

		int v = PlayerPrefs.GetInt ("Music", 1);
		if (v == 0) {
			source.mute = true;
		} else {
			source.mute = false;
		}
	}

	// Use this for initialization
	void Start () {
		source.clip = runMusic;
		source.Play ();
	}

	void OnGameOver(){
		source.clip = gameOverMusic;
		source.Play ();
		gameOvered = true;
	}

	void OnRunTigger(){
		if(gameOvered){return;}
		source.clip = runMusic;
		source.Play ();
		playerController.RunEvent -= OnRunTigger;
		playerController.FlyEvent += OnFlyTiggler;
	}
	void OnFlyTiggler(){
		if(gameOvered){return;}
		source.clip = flyMusic;
		source.Play ();
		playerController.RunEvent += OnRunTigger;
		playerController.FlyEvent -= OnFlyTiggler;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
