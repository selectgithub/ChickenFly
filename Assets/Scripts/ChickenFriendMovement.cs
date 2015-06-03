using UnityEngine;
using System.Collections;

public class ChickenFriendMovement : MonoBehaviour {

	//private float maxX = 13.0f;
	private float minX = -13.0f;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameController.GamePaused) {
			transform.Translate (-Vector2.right * 2 * 3.0f * Time.deltaTime);
		}
	}

	void OnBecameInvisible() {
		Destroy (gameObject);
	}

}
