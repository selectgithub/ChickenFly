using UnityEngine;
using System.Collections;

public class DreamlandMovement : MonoBehaviour {

	public float maxX;
	public float minX;

	private Transform anotherSameSpriteTransform;
	private float intervalBetweenSameSprite = 24.0f;

	void Awake(){
		//find all GameObjects that the tag same with this GameObject self.
		GameObject[] gos = GameObject.FindGameObjectsWithTag (gameObject.tag);

		foreach(GameObject go in gos){
			if(!go.name.Equals(gameObject.name)){
				//find out the GameObject with different name with this GameObject self.
				anotherSameSpriteTransform = go.transform;
			}
		}
	}

	void OnBecameVisible() {
		if (gameObject.tag.Equals ("Floor")) {
			if(!GameController.IsTapFlyTutorialFinished){return;}
			GameController.CurrentDistance += 24.0f;
		}
	}

	void Update(){
		if(transform.position.x <= minX){
			transform.position = new Vector3(anotherSameSpriteTransform.position.x + intervalBetweenSameSprite,transform.position.y,transform.position.z);
		}

		if (!GameController.GamePaused) {
			if (gameObject.tag.Equals ("Background")) {
				transform.Translate (-Vector2.right * 2 * 0.8f * Time.deltaTime);
			} else if (gameObject.tag.Equals ("MiddleLayer")) {
				transform.Translate (-Vector2.right * 2 * 2.0f * Time.deltaTime);
			} else if (gameObject.tag.Equals ("Floor")) {
				transform.Translate (-Vector2.right * 2 * 3.0f * Time.deltaTime);
			} else {
				transform.Translate (-Vector2.right * 2 * 0.25f * Time.deltaTime);
			}
		}
	}
}
