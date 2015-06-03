using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

	public Animator contentPanle;
	public Animator gearImage;
	public Text levelText;

	public Animator quitDialog;

	private float TotalDistance = 0.0f;
	private int TotalFoxCount = 0;

	private int level = 0;

	DomobAD domAD;

	// Use this for initialization
	void Start () {
		domAD = GameObject.Find("DomobAD").GetComponent<DomobAD>();

		if (domAD) {
			domAD.ShowBanner ();
		}

		RectTransform transform = contentPanle.gameObject.transform as RectTransform;        
		Vector2 position = transform.anchoredPosition;
		position.y -= transform.rect.height;
		transform.anchoredPosition = position;

		TotalDistance = PlayerPrefs.GetFloat ("TotalDistance",0.0f);
		TotalFoxCount = PlayerPrefs.GetInt ("TotalFoxCount",0);

		level = (int)(TotalDistance / 1000);
		levelText.text = "Level:" + level;
	}

	public void LoadGame(){
		if (domAD != null) {
			domAD.HideBanner ();
		}
		Application.LoadLevel ("Game");
	}

	public void ToggleContentPanle(){
		contentPanle.enabled = true;
		contentPanle.SetBool ("isHidden",!contentPanle.GetBool("isHidden"));

		gearImage.enabled = true;
		gearImage.SetBool ("isHidden",!gearImage.GetBool("isHidden"));
	}

	public void OnYesButtonClicked(){
		Application.Quit ();
	}

	public void OnNoButtonClicked(){
		quitDialog.SetBool ("isHidden",true);
	}


	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)){
			quitDialog.enabled = true;
			quitDialog.SetBool ("isHidden",!quitDialog.GetBool("isHidden"));
		}
	}
}
