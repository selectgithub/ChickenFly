using UnityEngine;
using System.Collections;

public class DomobAD : MonoBehaviour {

	static string PUBLISHER_ID = "56OJyM74uN0GCAzsYk";
	static string BANNER_PLACEMENT_ID = "16TLweelApHuYNUdnpKS0ERz";
	static string INSERT_PLACEMENT_ID = "16TLweelApHuYNUdnc6roB8k";

	static string DOMOB_INTERSTITIAL = "Interstitial";
	static string BANNER = "banner";
	string labelString = "Click The Button";

#if UNITY_IOS
#else
	private AndroidJavaClass jc;
	private AndroidJavaObject jo;
#endif
	
	void Awake(){
		
	}
	
	// Use this for initialization
	void Start () {
#if UNITY_IOS
#else
		if(Application.platform == RuntimePlatform.Android){
			jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
		}
#endif
		InitInterstitial ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void ShowBanner(){
#if UNITY_IOS
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			DMUnityManager.DomobAddBannerADView (Screen.width / 4 - 160, Screen.height / 2 - 50, 320, 50, PUBLISHER_ID, BANNER_PLACEMENT_ID, "DomobAD", BANNER);
		}
#else
		if (Application.platform == RuntimePlatform.Android) {
			jo.Call ("showBanner");
		}
#endif
	}
	
	public void HideBanner(){
#if UNITY_IOS
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			DMUnityManager.DomobRemoveBannerADView(BANNER);
		}
#else
		if(Application.platform == RuntimePlatform.Android){
			jo.Call("hideBanner");
		}
#endif
	}
	
	public void InitInterstitial(){
#if UNITY_IOS
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			DMUnityManager.DomobInterstitialsAdInitialize(PUBLISHER_ID,INSERT_PLACEMENT_ID,"DomobAD",DOMOB_INTERSTITIAL);
		}
#else
		if(Application.platform == RuntimePlatform.Android){
			jo.Call("initInterstitial");
		}
#endif
	}
	
	public void ShowInterstitial(){
#if UNITY_IOS
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			DMUnityManager.DomobPresentInterstitialsAd(DOMOB_INTERSTITIAL);
		}
#else
		if(Application.platform == RuntimePlatform.Android){
			jo.Call("showInterstitial");
		}
#endif
	}

	// delegate method
	public void DMAdViewSuccessToLoadAd() {
		labelString = "DMAdViewSuccessToLoadAd";
	}
	
	public void DMWillPresentModalViewFromAd() {
		labelString = "DMWillPresentModalViewFromAd";
		
	}
	
	public void DMAdViewFailToLoadAd(string info) {
		string[] infoStrings = info.Split(',');
		labelString = "DMOfferWallDidFailLoadWithError. ErrorCode:" + infoStrings[0] + ", ErrorContent:" + infoStrings[1];
	}
	
	public void DMDidDismissModalViewFromAd() {
		labelString = "DMDidDismissModalViewFromAd";
	}
	
	public void DMApplicationWillEnterBackgroundFromAd() {
		labelString = "DMApplicationWillEnterBackgroundFromAd";
	}
	
	public void DMInterstitialSuccessToLoadAd() {
		
		labelString = "DMInterstitialSuccessToLoadAd";
	}
	
	public void DMInterstitialFailToLoadAd(string info) {
		string[] infoStrings = info.Split(',');
		labelString = "DMInterstitialFailToLoadAd. ErrorCode:" + infoStrings[0] + ", ErrorContent:" + infoStrings[1];
	}
	
	public void DMInterstitialWillPresentScreen() {
		
		labelString = "DMInterstitialWillPresentScreen";
	}
	
	public void DMInterstitialDidDismissScreen() {
		
		labelString = "DMInterstitialDidDismissScreen";
	}
	public void DMInterstitialWillPresentModalView() {
		
		labelString = "DMInterstitialWillPresentModalView";
	}
	public void DMInterstitialDidDismissModalView() {
		
		labelString = "DMInterstitialDidDismissModalView";
	}
	
	public void DMInterstitialApplicationWillEnterBackground() {
		labelString = "DMInterstitialApplicationWillEnterBackground";
	}
	
}
