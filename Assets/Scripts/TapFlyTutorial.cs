using UnityEngine;
using System.Collections;

public class TapFlyTutorial : MonoBehaviour {

	PlayerController playerController;
	int touchCount = 0;

	void Awake(){
		playerController = GameObject.Find("ChickenPlayer").GetComponent<PlayerController>();
		playerController.OnTouchEvent += OnLeftScreenTouched;
	}

	// Use this for initialization
	void Start () {
		RectTransform transform = gameObject.transform as RectTransform;

		//Debug.Log ("ScreenWidth:" + Screen.width);

		//Vector2 position = transform.anchoredPosition;
		//position.x = Screen.width / 4;
		//transform.anchoredPosition = position;

		//Vector2 size = transform.sizeDelta;
		//size.x = Screen.width / 2;
		//transform.sizeDelta = size;

	}

	void OnLeftScreenTouched(){
		touchCount ++;
		if(touchCount >= 6){
			playerController.OnTouchEvent -= OnLeftScreenTouched;
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
