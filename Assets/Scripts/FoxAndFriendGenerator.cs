using UnityEngine;
using System.Collections;

public class FoxAndFriendGenerator : MonoBehaviour {
	
	public GameObject fox;
	public GameObject[] friends;
	
	
	private float minPositionXofChicken = 10.5f;
	private float maxPositionXofChicken = 11.5f;
	
	private float minPositionXofFox = 15.5f;
	private float maxPositionXofFox = 16.5f;

	PlayerController playerController;
	int touchCount = 0;

	int foxCountMax = 0;

	void Awake(){
		playerController = GameObject.Find("ChickenPlayer").GetComponent<PlayerController>();
		playerController.OnTouchEvent += OnLeftScreenTouched;
	}
	
	// Use this for initialization
	void Start () {
		if(GameController.Level < 2){
			foxCountMax = 1;
		}else if(GameController.Level >= 2 && GameController.Level < 13){
			foxCountMax = 2;
		}else if(GameController.Level >= 13 && GameController.Level < 20){
			foxCountMax = 3;
		}else if(GameController.Level >= 20 && GameController.Level < 40){
			foxCountMax = 4;
		}else if(GameController.Level >= 40){
			foxCountMax = 5;
		}
	}

	void OnLeftScreenTouched(){
		touchCount ++;
		if(touchCount >= 6){
			playerController.OnTouchEvent -= OnLeftScreenTouched;
			Invoke ("GenerateFriend",12.0f);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void GenerateFox(){
		if (!GameController.GamePaused) {
			int howMany = Random.Range (1, foxCountMax + 1);
			for (int i = 0; i < howMany; i++) {
				float x = Random.Range (minPositionXofFox, maxPositionXofFox);
				Instantiate (fox, new Vector3 (x, -2.2f, 0), Quaternion.identity);
			}
		}
		
		Invoke ("GenerateFriend", 1.0f);
		
	}
	
	void GenerateFriend(){
		if (!GameController.GamePaused) {
			float x = Random.Range (minPositionXofChicken, maxPositionXofChicken);
			int whichOne = Random.Range (0, 2);
			Instantiate (friends [whichOne], new Vector3 (x, -2.65f, 0), Quaternion.identity);
		}
		Invoke ("GenerateFox", 0.7f);
	}
	
	
}
