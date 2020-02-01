using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;
	
    private BannerView bannerView;
    private InterstitialAd interstitial;
	
    void Awake()
    {
        if(!instance)
        {
            instance = this;
		}
	}

    void Start()
    {
        #if UNITY_ANDROID
			string appId = "ca-app-pub-1224980819146957/4033141753";
		#elif UNITY_IPHONE
			string appId = "todo";
		#else
			string appId = "unexpected_platform";
		#endif
        MobileAds.Initialize(appId);
    }
	
    public void startAd()
    {
        RequestInterstitial();
	}
	
	private void RequestInterstitial()
	{
		#if UNITY_ANDROID
			string interstitialID = "ca-app-pub-1224980819146957/4033141753";
		#elif UNITY_IPHONE
			string interstitialID = "todo";
		#else
			string interstitialID = "unexpected_platform";
		#endif
		
        if(interstitial != null)
        {
            interstitial.Destroy();
        }
        interstitial = new InterstitialAd(interstitialID);
        interstitial.OnAdLoaded += HandleOnAdLoaded;

        interstitial.LoadAd(CreateNewRequest());
	}

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        if(interstitial.IsLoaded())
            interstitial.Show();
    }
    private AdRequest CreateNewRequest()
    {
        return new AdRequest.Builder().Build();
    }
	
	/*private void testRequest()
		{
		#if UNITY_ANDROID
		string adUnitId = "ca-app-pub-1224980819146957/4033141753";
		#elif UNITY_IPHONE
		//string adUnitId = "ca-app-pub-3940256099942544/2934735716";
		#else
		string adUnitId = "unexpected_platform";
		#endif
		
		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
		//InterstitialAd ad = new InterstitialAd(adUnitId);
		// Create an empty ad request.
		//AdRequest request = new AdRequest.Builder().Build();
		AdRequest request = new AdRequest.Builder().Build();
		// Load the banner with the request.
		bannerView.LoadAd(request);
	}  */
}
