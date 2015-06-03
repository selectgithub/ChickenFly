using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("Load",1.2f);
	}

	void Load(){
		Application.LoadLevel ("Menu");
	}

	// Update is called once per frame
	void Update () {
	
	}
}
