using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public static float CurrentDistance = 0.0f;
	public static int CurrentFoxCount = 0;

	public static float BestDistance = 0.0f;
	public static int BestFoxCount = 0;

	public static float TotalDistance = 0.0f;
	public static int TotalFoxCount = 0;

	public static int Level = 0;

	public static bool GamePaused = false;
	public static bool IsTapFlyTutorialFinished = false;
	public static bool IsTapFoxTutorialFinished = false;

	public Animator restartButton;
	public Animator homeButton;
	public Animator gameoverPanel;

	public Text distanceText;
	public Text foxCountText;
	public Text levelText;

	public Toggle[] chickenIcons;
	public Toggle[] foxIcons;

	private int passedawayFriends = 0;
	private int escapeFox = 0;

	private bool gameOvered = false;

	public delegate void GameDelegate();
	public event GameDelegate GameOverEvent;

	PlayerController playerController;
	int touchCount = 0;

	DomobAD domAD;
	
	void Awake(){
		TotalDistance = PlayerPrefs.GetFloat ("TotalDistance",0.0f);
		TotalFoxCount = PlayerPrefs.GetInt ("TotalFoxCount",0);
		Level = (int)(TotalDistance / 1000);

		playerController = GameObject.Find("ChickenPlayer").GetComponent<PlayerController>();
		playerController.OnTouchEvent += OnLeftScreenTouched;
		playerController.OnTapFoxEvent += OnFoxTaped;

		domAD = GameObject.Find("DomobAD").GetComponent<DomobAD>();
	}
	

	// Use this for initialization
	void Start () {
		BestDistance = PlayerPrefs.GetFloat ("BestDistance",0.0f);
		BestFoxCount = PlayerPrefs.GetInt ("BestFoxCount",0);

		GamePaused = false;
		gameOvered = false;
		passedawayFriends = 0;
	}

	void OnLeftScreenTouched(){
		touchCount ++;
		if(touchCount >= 6){
			playerController.OnTouchEvent -= OnLeftScreenTouched;
			IsTapFlyTutorialFinished = true;
		}
	}
	void OnFoxTaped(){
		playerController.OnTapFoxEvent -= OnFoxTaped;
		IsTapFoxTutorialFinished = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnFriendPassedAway(){
		passedawayFriends ++;
		switch(passedawayFriends){
		case 1:
			chickenIcons[2].isOn = false;
			break;
		case 2:
			chickenIcons[1].isOn = false;
			break;
		case 3:
			chickenIcons[0].isOn = false;
			OnGameOver();
			break;
		}

		Debug.Log ("Friends:" + passedawayFriends);
	}

	void OnFoxEscape(){
		escapeFox ++;
		switch(escapeFox){
		case 1:
			foxIcons[0].isOn = true;
			break;
		case 2:
			foxIcons[1].isOn = true;
			break;
		case 3:
			foxIcons[2].isOn = true;
			OnGameOver();
			break;
		}
	}

	void OnGameOver(){
		GamePaused = true;
		gameOvered = true;

		PlayerPrefs.SetFloat ("TotalDistance",TotalDistance + CurrentDistance);
		PlayerPrefs.SetInt ("TotalFoxCount",TotalFoxCount + CurrentFoxCount);

		Level = (int)((TotalDistance + CurrentDistance) / 1000);

		levelText.text = "Level:" + Level;

		if (CurrentDistance > BestDistance) {
			BestDistance = CurrentDistance;
			PlayerPrefs.SetFloat ("BestDistance", BestDistance);
			distanceText.text = "" + BestDistance + "m Best!";
		} else {
			distanceText.text = "" + CurrentDistance + "m VS " + BestDistance + "m";
		}

		if (CurrentFoxCount > BestFoxCount) {
			BestFoxCount = CurrentFoxCount;
			PlayerPrefs.SetInt ("BestFoxCount", BestFoxCount);
			foxCountText.text = "" + BestFoxCount + " Best!";
		} else {
			foxCountText.text = "" + CurrentFoxCount + " VS " + BestFoxCount;
		}

		gameoverPanel.enabled = true;
		gameoverPanel.SetBool ("isHidden",false);

		if(GameOverEvent != null){
			GameOverEvent();
		}

		if (domAD) {
			//domAD.ShowInterstitial ();
		}

	}

	public void OnPauseButtonClicked(){
		if(gameOvered){return;}

		GamePaused = !GamePaused;
		restartButton.enabled = true;
		homeButton.enabled = true;
		restartButton.SetBool ("isHidden",!restartButton.GetBool("isHidden"));
		homeButton.SetBool ("isHidden",!homeButton.GetBool("isHidden"));
	}

	public void OnRestartButtonClicked(){

		CurrentDistance = 0.0f;
		CurrentFoxCount = 0;
		GamePaused = false;
		IsTapFlyTutorialFinished = false;
		IsTapFoxTutorialFinished = false;

		Application.LoadLevel ("Game");
	}

	public void OnHomeButtonClicked(){
		Application.LoadLevel ("Menu");
	}

}
