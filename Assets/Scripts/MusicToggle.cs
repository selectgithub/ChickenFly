using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicToggle : MonoBehaviour {

	Toggle toggle;

	void Awake(){
		toggle = GetComponent<Toggle>();
		int v = PlayerPrefs.GetInt ("Music", 1);
		if (v == 0) {
			toggle.isOn = true;
		} else {
			toggle.isOn = false;
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMusicToggleChanged(){
		if(toggle.isOn){PlayerPrefs.SetInt ("Music",0);}else{PlayerPrefs.SetInt ("Music",1);};
	}
}
