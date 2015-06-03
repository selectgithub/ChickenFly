using UnityEngine;
using System.Runtime.InteropServices;

public class DMUnityManager
{
	[DllImport ("__Internal")]
	//init Banner with origin.x ,origin.y, size.width, size.height, publisherid, placementid, delegateObject, key string(desided by yourself for recognize the banner view) 
	public static extern void DomobAddBannerADView (float x,float y,float w,float h,string publisherid,string placementid,string delegateObject,string key);
	
	[DllImport ("__Internal")]
	//init autoRefresh Banner with origin.x ,origin.y, size.width, size.height, publisherid, placementid, delegateObject, key string (desided by yourself for recognize the banner view)
	public static extern void DomobAddBannerADViewWithAutoRefresh (float x,float y,float w,float h,string publisherid,string placementid,string delegateObject, string key,bool autoRefresh);

	[DllImport ("__Internal")]
	//remove banner view from parentView 
	public static extern void DomobRemoveBannerADView (string key);
	
	[DllImport ("__Internal")]
	//init interstitial view with publisherid,placementid,delegateObject,key string(desided by yourself for recognize the interstitial view)
	public static extern void DomobInterstitialsAdInitialize (string publisherid,string placementid,string delegateObject, string key);
	
	[DllImport ("__Internal")]
	//present the interstitial view 
	public static extern void DomobPresentInterstitialsAd (string key);
	
	
	[DllImport ("__Internal")]
	//remove the interstitial view
	public static extern void DomobRemoveInterstitialsAd (string key);
}