using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
//using Assets.Source.DataControl.Profile;



public class AdmobManager : MonoBehaviour
{
    public static AdmobManager instance;
    public string interstitialID, bannerID, rewardedVideoID;
    InterstitialAd interstitialAd;
    BannerView bannerView;
    RewardedAd rewardedAd;
    public bool giveUnityAdsPriority = false;
    static int adCount = 1;


    bool isCounting;

    [HideInInspector]
    public int counter;

    public int nextAdWait;




    void Awake()
    {

        PlayerPrefs.SetInt("ContinueTimer", 0);
        counter = 0;
        isCounting = false;
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }


    IEnumerator StartCounter()
    {
        counter = PlayerPrefs.GetInt("Count");
        while (counter < nextAdWait)
        {
            counter++;
            PlayerPrefs.SetInt("Count", counter);
            Debug.Log("Counter: " + counter);
            yield return new WaitForSeconds(1);
        }
        isCounting = false;
        counter = 0;
        PlayerPrefs.SetInt("Count", 0);

    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Count", 0);
    }


    public void RequestBanner()
    {

#if UNITY_ANDROID
        string adUnitId = bannerID;
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);



        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        AdRequest request = new AdRequest.Builder().Build();

        this.bannerView.LoadAd(request);
    }
    void Start()
    {
        MobileAds.Initialize(initStatus =>
        {
            initInterstitialAds();
            RequestBanner();
        initRewardedVideoAd();
        });
    }

    private void Update()
    {

    }
    void initInterstitialAds()
    {
        this.interstitialAd = new InterstitialAd(interstitialID);
        this.interstitialAd.LoadAd(new AdRequest.Builder().Build());

        this.interstitialAd.OnAdFailedToLoad += InterstitialAd_OnAdFailedToLoad;
        this.interstitialAd.OnAdLoaded += InterstitialAd_OnAdLoaded;
        this.interstitialAd.OnAdClosed += HandleOnAdClosed;
    }

    public void showInterstitialAd()
    {
        if (!giveUnityAdsPriority)
        {
            if (this.interstitialAd.IsLoaded())
            {
                this.interstitialAd.Show();
            }
            else
            {
                this.interstitialAd.LoadAd(new AdRequest.Builder().Build());
                //            UnityAdsManager.instance.ShowStandardAd();
            }
        }
        else
        {
            //      UnityAdsManager.instance.ShowStandardAd();
        }
    }

    private void InterstitialAd_OnAdLoaded(object sender, System.EventArgs e)
    {
        Debug.Log("JDS: InterstitialAd_OnAdLoaded");
    }

    private void InterstitialAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
    {
        Debug.Log("JDS: InterstitialAd_OnAdFailedToLoad");
    }

    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        Debug.Log("JDS: HandleOnAdClosed");
        this.interstitialAd.LoadAd(new AdRequest.Builder().Build());
    }



    void initRewardedVideoAd()
    {
        this.rewardedAd = new RewardedAd(rewardedVideoID);

        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);

    }

    void loadRewardedAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }

    public bool isRewardedVideoAvailable()
    {
        return rewardedAd.IsLoaded();
    }

    public void showRewardedAd()
    {
        Debug.Log("Button pressed for rewarded ad");
        if (rewardedAd != null)
        {

            if (rewardedAd.IsLoaded() && isCounting == false)
            {
                rewardedAd.Show();
                isCounting = true;
                StartCoroutine(StartCounter());

            }
            else
            {
                loadRewardedAd();
                // UnityAdsManager.instance.ShowRewardedAd();
            }
        }
        else
        {

            initRewardedVideoAd();
        }

    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("JDS: HandleRewardedAdFailedToLoad");
        loadRewardedAd();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("JDS: HandleUserEarnedReward");
        GameObject.FindObjectOfType<UIManager>().Reward();

        //GlobalValue.StoreDiamond += 100;
        //SoundManager.PlaySfx(SoundManager.Instance.soundRewarded);
        loadRewardedAd();
    }
}
