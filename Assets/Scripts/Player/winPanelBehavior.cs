using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winPanelBehavior : MonoBehaviour
{
    public Text cointext;
    public Text moneytext;

    private int earnedCoins;
    private int earnedMoney;

    public bool claimedReward;

    public Button claimBtn1;
    public Button claimBtn2;

    // Start is called before the first frame update
    void Start()
    {
        claimedReward = false;
    }

    public void OnEnable()
    {
        if (!claimedReward) 
        {
            claimBtn1.interactable = true;
            claimBtn2.interactable = true;

            claimedReward = true;
            earnedCoins = randCoin();
            earnedMoney = randMoney();

            broadcastEarnings();
        }

        //GA Event
        //MyAnalytics.instance.ScreenEvent("WinScreen");
    }

    public int randCoin() 
    {
        int c = 0;
        c = Random.Range(30, 40);
        return c;
    }

    public int randMoney()
    {
        int m = 0;
        m = Random.Range(1000, 1201);
        return m;
    }

    public void broadcastEarnings() 
    {
        cointext.text = earnedCoins.ToString();
        moneytext.text = earnedMoney.ToString();
    }

    public void claimReward() 
    {
        claimedReward = false;

        claimBtn1.interactable = false;
        claimBtn2.interactable = false;

        PlayerPrefs.SetInt("TotalCoin", PlayerPrefs.GetInt("TotalCoin",50) + earnedCoins);
        PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney", 1000) + earnedMoney);
    }

    public void playWinSound() 
    {
        //Mute gameplay music
        Destroy(GameObject.Find("Music"));
        //adsplugin.instance.Showinterstial();
        AdmobManager.instance.showInterstitialAd();
        //Play sound
        GlobalAudioPlayer.audioPlayer.playMusic("LevelComplete");
    }
}