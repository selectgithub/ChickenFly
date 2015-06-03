using UnityEngine;
using System.Collections;

public class TapFoxTutorial : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(GameController.IsTapFoxTutorialFinished){
			Destroy(gameObject);
		}
	}
}
