/*          Responsible for managing ads in game,
            Includes interstitial and rewarded ads.
            Ad1 = Coin rewards
            Ad2 = Defender rewards
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

using TMPro;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [Header("Game Id")]
    [SerializeField] string androidGameId;
    [SerializeField] string IOSGameId;
    [Header("Interstitial Ads")]
    [SerializeField] string androidInterstitialAdUnitId;
    [SerializeField] string IOSInterstitialAdUnitId;
    [Header("Buttons")]
    [SerializeField] Button[] coinsRewardedAdsBtns;
    [SerializeField] Button[] defenderRewardedAdsBtns;
    [Header("Rewarded Ads")]
    [SerializeField] string androidRewardedAdUnitId;
    [SerializeField] string androidRewardedAdUnitId2;
    [SerializeField] string IOSRewardedAdUnitId;
    [SerializeField] string IOSRewardedAdUnitId2;

    string gameId;
    string adUnitId, rewardedAdUnitId, rewardedAdUnitId2;
    [SerializeField] bool testMode = true;

    void Awake()
    {
        // Initialize ads before showing it
        InitializeAds();
        // Disable the Rewarded Ads button untill the ads are ready to show
        // And change alpha of button bit lower
        if (coinsRewardedAdsBtns != null)
        {
            foreach (var item in coinsRewardedAdsBtns)
            {
                item.interactable = false;
                item.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
            }
        }

        if (defenderRewardedAdsBtns != null)
        {
            foreach (var item in defenderRewardedAdsBtns)
            {
                item.interactable = false;
                item.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
            }
        }
        // Load Ads
        LoadInterstitialAds();
        LoadRewardedAds();
    }

    void InitializeAds()
    {
        // #if UNITY_IOS
        //     placementId = IOSGameId;
        // #elif UNITY_ANDROID
        //         placementId = androidGameId;
        // #endif
        gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSGameId : androidGameId;
        adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSInterstitialAdUnitId : androidInterstitialAdUnitId;
        rewardedAdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSRewardedAdUnitId : androidRewardedAdUnitId;
        rewardedAdUnitId2 = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSRewardedAdUnitId2 : androidRewardedAdUnitId2;
        Advertisement.Initialize(gameId, testMode, this);
    }

    public void LoadInterstitialAds()
    {
        // Load Interstitial ads
        Debug.Log($"Load Interstitial ads...");
        Advertisement.Load(adUnitId, this);
    }

    public void LoadRewardedAds()
    {
        // Load Rewarded ads for Coins
        Debug.Log($"Load Coins Rewarded ads1..." + rewardedAdUnitId);
        Advertisement.Load(rewardedAdUnitId, this);

        // Load Rewarded ads for Defender
        Debug.Log($"Load Defender Rewarded ads2..." + rewardedAdUnitId2);
        Advertisement.Load(rewardedAdUnitId2, this);
    }

    public void ShowInterstitialAds(int percentageToShowAds)
    {
        // Generate random number
        // and check if its under the range to show ads
        int rand = Random.Range(0, 100);
        // rand = 24;                          // DEBUG purpose; --> Remove this when done with debugging.

        if (rand <= percentageToShowAds)
        {
            Debug.Log($"Show Interstitial Ads");
            Advertisement.Show(adUnitId, this);
        }
    }

    public void ShowCoinRewardedAds()
    {
        // Disable the button
        // And change alpha of button bit lower
        foreach (var item in coinsRewardedAdsBtns)
        {
            item.interactable = false;
            item.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
        }
        // Then show the ads
        Debug.Log($"Show Coin Rewarded Ads");
        Advertisement.Show(rewardedAdUnitId, this);
    }

    public void ShowDefenderRewardedAds()
    {
        // Disable the button
        // And change alpha of button bit lower
        foreach (var item in defenderRewardedAdsBtns)
        {
            item.interactable = false;
            item.GetComponent<Image>().color = new Color32(255, 255, 255, 120);
        }
        // Then show the ads
        Debug.Log($"Show Defender Rewarded Ads");
        Advertisement.Show(rewardedAdUnitId2, this);
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
        // Button1 --> Ad 1
        Debug.Log($"Coins rewarded ads loaded " + rewardedAdUnitId);
        if (rewardedAdUnitId.Equals(placementId))
        {
            // Set button to true
            // And change color of button to normal
            foreach (var item in coinsRewardedAdsBtns)
            {
                item.interactable = true;
                item.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }

        // Button2 --> Ad 2
        Debug.Log($"Defender rewarded ads loaded " + rewardedAdUnitId2);
        if (rewardedAdUnitId2.Equals(placementId))
        {
            // Set button to true
            // And change color of button to normal
            foreach (var item in defenderRewardedAdsBtns)
            {
                item.interactable = true;
                item.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }
        }
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"Ads started to show");
        Debug.Log($"TEST MODE IS TURNED "+ testMode);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"Ads clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ads completed:- {showCompletionState}");

        // For coins reward
        if (placementId.Equals(rewardedAdUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log($"Player rewarded with Coins");      //  Rewards coins.
            Time.timeScale = 1;
            Debug.Log($"timescale status {Time.timeScale}");
            // Load another load
            Advertisement.Load(rewardedAdUnitId, this);
        }

        // For defender reward
        if (placementId.Equals(rewardedAdUnitId2) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log($"Player rewarded with Defender");      //  Rewards defenders.
            Time.timeScale = 1;
            Debug.Log($"timescale status {Time.timeScale}");
            // Load another load
            Advertisement.Load(rewardedAdUnitId2, this);
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
