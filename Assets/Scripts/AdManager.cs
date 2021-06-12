using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Advertisements;



public class AdManager : MonoBehaviour, IUnityAdsListener
{
#if !UNITY_IOS
    private static readonly string storeID = "4163141";
#else
    private static readonly string storeID = "4163140";
#endif
    private static readonly string videoID = "video";
    private static readonly string rewardedID = "rewardedVideo";
    private static readonly string bannerID = "Banner";

    private Action adSuccess;//Métodos que llamar al ver un anuncio recompensado
    private Action adSkipped;
    private Action adFailed;

#if UNITY_EDITOR
    private static bool testMode = true;

#else
    private static bool testMode = false;
#endif

    public static AdManager instance;

    //Clase estática que se encarga de la publicidad del juego
    //Genera la instancia si no existe, si existe, la destruye
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

    //Muestra un vídeo de publicidad
    public static void ShowStandardAd()
    {
        if (Advertisement.IsReady(videoID))
        {
            Advertisement.Show(videoID);
        }
    }

    public static void ShowRewardedAd(Action success, Action skipped, Action failed)
    {
        instance.adSuccess = success;
        instance.adSkipped = skipped;
        instance.adFailed = failed;

        if (Advertisement.IsReady(rewardedID))
        {
            Advertisement.Show(rewardedID);
        }
    }
    public void OnUnityAdsDidError(string message) { }
    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        if(placementId == rewardedID)//llamar a los callbacks al terminar un anuncio recompensado
        {
            switch (showResult)
            {
                case ShowResult.Finished:
                    adSuccess();
                    break;
                case ShowResult.Failed:
                    adFailed();
                    break;
                case ShowResult.Skipped:
                    adSkipped();
                    break;
            }
        }
    }
    public void OnUnityAdsDidStart(string placementId) { }
    public void OnUnityAdsReady(string placementId) { }
}
