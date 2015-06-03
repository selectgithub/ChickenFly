using UnityEngine;
using System.Collections;

public class GameOverPanel : MonoBehaviour {

	DomobAD domAD;

	// Use this for initialization
	void Start () {
		domAD = GameObject.Find("DomobAD").GetComponent<DomobAD>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnPanleAppeared(){
		Invoke ("DelayAD",1.0f);
	}

	void DelayAD(){
		if (domAD) {
			domAD.ShowInterstitial ();
		}
	}
}
