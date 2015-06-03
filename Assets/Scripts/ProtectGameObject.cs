using UnityEngine;
using System.Collections;

public class ProtectGameObject : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
