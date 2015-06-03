using UnityEngine;
using System.Collections;

public class TreeFlowerController : MonoBehaviour {
	
	private float maxX = 13;
	private float minX = -13;

	private float minTime = 6;
	private float maxTime = 36;

	private bool run = true;

	// Use this for initialization
	void Start () {

	}

	void StartMovement(){
		run = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}


	void Update(){
		if (!GameController.GamePaused) {
			if (transform.position.x <= minX) {
				transform.position = new Vector3 (maxX, transform.position.y, transform.position.z);
				run = false;
				Invoke ("StartMovement", Random.Range (minTime, maxTime));
			}
			if (run) {
				transform.Translate (-Vector2.right * 2 * 3.0f * Time.deltaTime);
			}
		}
	}

}
