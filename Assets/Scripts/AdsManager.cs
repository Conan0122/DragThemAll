/*          Responsible for managing ads in game,
            Includes interstitial and rewarded ads.
            Ad1 = Coin rewards
            Ad2 = Defender rewards

            Process = Check user Device --> Check game id --> Load Ads --> Show ads if loaded succesfully

            // Add this script in scenes where ads needs to be displayed
*/

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Networking;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    ShortMsgPopUpManager shortMsgPopUpManager;

    [Header("Game Id")]
    [SerializeField] string androidGameId;
    [SerializeField] string IOSGameId;
    [Header("Interstitial Ads IDs")]
    [SerializeField] string androidInterstitialAdUnitId;
    [SerializeField] string IOSInterstitialAdUnitId;
    [Header("Rewarded Ads IDs")]
    [SerializeField] string androidRewardedAdUnitId;
    [SerializeField] string androidRewardedAdUnitId2;
    [SerializeField] string IOSRewardedAdUnitId;
    [SerializeField] string IOSRewardedAdUnitId2;
    [Header("Buttons for Rewarded ads")]
    [SerializeField] Button[] coinsRewardedAdsBtns;
    [SerializeField] Button[] defenderRewardedAdsBtns;

    string gameId;
    string adUnitId, coinRewardAdUnitId, defenderRewardAdUnitId;
    const string WEB_REQUEST_URL = "http://clients3.google.com/generate_204";
    const string INTERNET_CONNECTION_ERROR = "Check internet connection and try again";
    [SerializeField] bool testMode = true;      // Turn it false for production

    void Awake()
    {
        shortMsgPopUpManager = FindObjectOfType<ShortMsgPopUpManager>();

        // Check for internet connection before initializing ads
        StartCoroutine(CheckInternetConnection((connection) =>
        {
            if (connection)
            {
                // Initialize Ads before showing it
                InitializeAds();

                // Load Ads
                LoadInterstitialAds();
                LoadRewardedAds();
            }
            else
            {
                Debug.Log($"Check internet connection:- Can't initialize or load ads");
            }
        }));
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
        coinRewardAdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSRewardedAdUnitId : androidRewardedAdUnitId;  // For Coins
        defenderRewardAdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? IOSRewardedAdUnitId2 : androidRewardedAdUnitId2;// For Defenders
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
        Debug.Log($"Load Coins Rewarded ads..." + coinRewardAdUnitId);
        Advertisement.Load(coinRewardAdUnitId, this);

        // Load Rewarded ads for Defender
        Debug.Log($"Load Defender Rewarded ads..." + defenderRewardAdUnitId);
        Advertisement.Load(defenderRewardAdUnitId, this);
    }

    public void ShowInterstitialAds(int percentageToShowAds)
    {
        // Generate random number
        // and check if its under the range to show ads
        int rand = UnityEngine.Random.Range(0, 100);
        // int rand = Random.Range(0, 100);
        // rand = 24;                          // DEBUG purpose; --> Remove this when done with debugging.

        if (rand <= percentageToShowAds)
        {
            Debug.Log($"Show Interstitial Ads");
            Advertisement.Show(adUnitId, this);
        }
    }

    public void ShowCoinRewardedAds()
    {
        StartCoroutine(CheckInternetConnection((isConnected) =>
        {
            foreach (var btns in coinsRewardedAdsBtns)
            {
                if (isConnected)
                {
                    // Show ads
                    Debug.Log($"Show ads now------------");
                    Advertisement.Show(coinRewardAdUnitId, this);
                }
                else
                {
                    // Show pop up asking to turn on internet connection
                    // or just wait for sometime to load ads.
                    // Show message pop up.
                    Debug.Log($"Show message pop up---------------");                    
                    shortMsgPopUpManager.ShowPopUpMessage(INTERNET_CONNECTION_ERROR);
                }
            }
        }));
    }

    public void ShowDefenderRewardedAds()
    {
        StartCoroutine(CheckInternetConnection((isConnected) =>
        {
            foreach (var btns in defenderRewardedAdsBtns)
            {
                if (isConnected)
                {
                    Debug.Log($"Show Defender Rewarded Ads");
                    Advertisement.Show(defenderRewardAdUnitId, this);
                }
                else
                {
                    Debug.Log($"Show message pop up---------------");
                    shortMsgPopUpManager.ShowPopUpMessage(INTERNET_CONNECTION_ERROR);
                }
            }
        }));
    }

    // ------------------------------------------------------

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
        // This gets called only when
        // ads gets loaded successfully.

        // Button1 --> Ad 1
        Debug.Log($"Coins rewarded ads loaded " + coinRewardAdUnitId);

        // Button2 --> Ad 2
        Debug.Log($"Defender rewarded ads loaded " + defenderRewardAdUnitId);
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"{placementId} Ads started to show");
        Debug.Log($"TEST MODE IS TURNED " + testMode);
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"{placementId} Ads clicked");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        // Reward player based on the ads watched
        Debug.Log($"{placementId} Ads completed:- {showCompletionState}");

        // For coins reward
        if (placementId.Equals(coinRewardAdUnitId) &&
            showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log($"Player rewarded with Coins");      //  Rewards coins.
            Time.timeScale = 1;
            Debug.Log($"timescale status {Time.timeScale}");
            // Load another ad
            // LoadRewardedAds();   //  Use this instead of below line.
            Advertisement.Load(coinRewardAdUnitId, this);
        }

        // For defender reward
        if (placementId.Equals(defenderRewardAdUnitId) &&
            showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log($"Player rewarded with Defender");      //  Rewards defenders.
            Time.timeScale = 1;
            Debug.Log($"timescale status {Time.timeScale}");
            // Load another load
            // LoadRewardedAds();   //  Use this instead of below line.
            Advertisement.Load(defenderRewardAdUnitId, this);
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

    // --------------------------------------------------

    // Check if user is connected to internet or not
    // Call this before initializing,loading and showing ads.
    IEnumerator CheckInternetConnection(Action<bool> connectionStatus)
    {
        // To check internet connection 
        // use http://clients3.google.com/generate_204" which is a non-existent file or resource 
        // that is a specific endpoint that is designed to return a 204 status code and no content
        UnityWebRequest request = UnityWebRequest.Head(WEB_REQUEST_URL);
        request.timeout = 5;    // Timeout after 5 secs

        yield return request.SendWebRequest();

        // "responseCode" specifically check for the expected response code
        // from the "http://clients3.google.com/generate_204" endpoint
        if (request.result == UnityWebRequest.Result.Success && request.responseCode == 204)
        {
            Debug.Log($"User has proper internet connection");
            connectionStatus?.Invoke(true);
        }
        else
        {
            Debug.Log($"User doesn't have internet connection");

            // Wait for 1 second before showing the pop-up message
            yield return new WaitForSeconds(1f);

            connectionStatus?.Invoke(false);
        }
    }

}
