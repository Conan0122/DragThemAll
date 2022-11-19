/*          Responsible for managing ads in game,
            Includes interstitial and rewarded ads.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] Button rewardedShowAdsBtn;
    [SerializeField] string androidGameId;
    [SerializeField] string IOSGameId;
    [SerializeField] string androidInterstitialPlacementId;
    [SerializeField] string IOSInterstitialPlacementId;
    [SerializeField] string androidRewardedPlacementId;
    [SerializeField] string IOSRewardedPlacementId;
    string gameId;
    string placementId, rewardedPlacementId;
    [SerializeField] bool testMode = true;

    void Awake()
    {
        InitializeAds();
        // Disable the button untill the ads is ready to show
        // And change alpha of button bit lower
        rewardedShowAdsBtn.interactable = false;
        rewardedShowAdsBtn.GetComponent<Image>().color = new Color32(255,255,255,120);
    }

    void InitializeAds()
    {
        // #if UNITY_IOS
        //     placementId = IOSGameId;
        // #elif UNITY_ANDROID
        //         placementId = androidGameId;
        // #endif

        gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSGameId : androidGameId;
        placementId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSInterstitialPlacementId : androidInterstitialPlacementId;
        rewardedPlacementId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSRewardedPlacementId : androidRewardedPlacementId;
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void LoadInterstitialAds()
    {
        Debug.Log($"Interstitial ads loading...");
        Advertisement.Load(placementId, this);
    }

    public void LoadRewardedAds()
    {
        Debug.Log($"Rewarded ads loading...");
        Advertisement.Load(rewardedPlacementId, this);
    }

    public void ShowInterstitialAds(int randRangeToShowAds)
    {
        int rand = Random.Range(0, 100);

        rand = 24;                          // DEBUG purpose; --> Remove this when done with debugging.
        if (rand <= randRangeToShowAds)
        {
            Debug.Log($"Interstitial Ads showing");
            Advertisement.Show(placementId, this);
        }
    }

    public void ShowRewardedAds()
    {
        Debug.Log($"Rewarded Ads showing");
        // Disable the button
        rewardedShowAdsBtn.interactable = false;
        // And change alpha of button bit lower
        rewardedShowAdsBtn.GetComponent<Image>().color = new Color32(255,255,255,120);
        // Then show the ads
        Advertisement.Show(rewardedPlacementId, this);
    }

    // -----------------

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        LoadInterstitialAds();
        LoadRewardedAds();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Ads already loaded");
        if (rewardedPlacementId.Equals(placementId))
        {
            // Set button to true
            // And change color of button to normal
            rewardedShowAdsBtn.interactable = true;
            rewardedShowAdsBtn.GetComponent<Image>().color = new Color32(255,255,255,255);
        }
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"Ads started");
        
        Debug.Log($"timescale status {Time.timeScale}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"Ads clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ads completed:- {showCompletionState}");
        if (placementId.Equals(rewardedPlacementId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log($"Player rewarded");      //  Rewards like defenders or coins.
            Time.timeScale = 1;
            // Load another load
            Advertisement.Load(rewardedPlacementId, this);
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {placementId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {placementId}: {error.ToString()} - {message}");
    }


}
