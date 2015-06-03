using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundToggle : MonoBehaviour {

	Toggle toggle;

	void Awake(){
		toggle = GetComponent<Toggle>();
		int v = PlayerPrefs.GetInt ("Sound", 1);
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

	public void OnSoundToggleChanged(){
		if(toggle.isOn){PlayerPrefs.SetInt ("Sound",0);}else{PlayerPrefs.SetInt ("Sound",1);};
	}
}
