using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
#if UNITY_ANDROID
    private static readonly string storeID = "3980365";
#elif UNITY_IOS
    private static readonly string storeID = "3980364";
#endif

    private static readonly string videoID = "video";
    private static readonly string rewardedID = "rewardedVideo";
    private static readonly string bannerID = "Banner";

#if UNITY_EDITOR
    private static bool testMode = true;

#else
    private static bool testMode = false;
#endif
    public static AdManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Advertisement.AddListener(this);
            Advertisement.Initialize(storeID, testMode);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void ShowStandardAd()
    {
        if (Advertisement.IsReady(videoID))
        {
            Advertisement.Show(videoID);
        }
    }
    public void OnUnityAdsDidError(string message) { }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {

    }
    public void OnUnityAdsDidStart(string placementId) { }
    public void OnUnityAdsReady(string placementId) { }
}
