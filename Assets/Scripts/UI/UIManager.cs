using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {
	public UIFader UI_fader;
	public GameObject Privacy_Policy;
	public UI_Screen[] UIMenus;
	public GameObject UIHeader;
	public int MaxLevels;

	public Text CoinsText;
	public Text MoneyText;

	public int totalCoins;
	public int totalMoney;

	public Image PunchIcon;

	public Sprite[] ReplacePunchImgs;

	public GameObject TutorialPanel;

	public GameObject[] TutorialScreens;
	public GameObject adPanel;

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		//Check if main menu is loaded
		Scene scen = SceneManager.GetActiveScene();
		if (scen.name == "00_MainMenu")
		{
			UIHeader.SetActive(true);
			//Broadcast economy
			UpdateEconomy();
			//

			if (PlayerPrefs.GetInt("privacy", 0) == 0)
			{
				Privacy_Policy.SetActive(true);

				//GA Event
				//MyAnalytics.instance.ScreenEvent("PrivacyScreen");
			}
		}
		else
		{
			UIHeader.SetActive(false);
		}
		if (scen.name == "01_Game") 
		{
			if (PlayerPrefs.GetInt("Tutorial", 0) == 0) 
			{
				Invoke("startTutorial",3);
			}

			//GA Event
			//MyAnalytics.instance.ScreenEvent("GameplayScreen");
		}
	}

	public void UpdateEconomy() 
	{
		totalCoins = PlayerPrefs.GetInt("TotalCoin", 50);
		totalMoney = PlayerPrefs.GetInt("TotalMoney", 1000);
		CoinsText.text = totalCoins.ToString();
		MoneyText.text = totalMoney.ToString();
	}

	void Awake()
	{
		DisableAllScreens();

		//don't destroy
		DontDestroyOnLoad(gameObject);
	}

	//shows a menu by name
	public void ShowMenu(string name, bool disableAllScreens){
		if(disableAllScreens) DisableAllScreens();

		foreach (UI_Screen UI in UIMenus){
			if (UI.UI_Name == name) {
				if (UI.UI_Gameobject != null) {
					UI.UI_Gameobject.SetActive (true);
					SetTouchScreenControls(UI);
					//Enable touch controls on mobile devices
					if (UI.UI_Name == "HUD") 
					{
						//automatically enable touch controls on IOS or android
						if (!Application.isEditor) 
						{ 
							UI.showTouchControls = true;
						}
						else
						{ 
							UI.showTouchControls = false;
						}
					}
					//
				} else {
					Debug.Log ("no menu found with name: " + name);
				}
			}
		}

        //EnableLoader
        if (UI_fader != null) UI_fader.gameObject.SetActive(true);
	}

	public void ShowMenu(string name){
		ShowMenu(name, true);
	}

	//close a menu by name
	public void CloseMenu(string name){
		foreach (UI_Screen UI in UIMenus){
			if (UI.UI_Name == name)	UI.UI_Gameobject.SetActive (false);
		}
	}
		
	//disable all the menus
	public void DisableAllScreens(){
		foreach (UI_Screen UI in UIMenus){ 
			if(UI.UI_Gameobject != null) 
				UI.UI_Gameobject.SetActive(false);
			else 
				Debug.Log("Null ref found in UI with name: " + UI.UI_Name);
		}
	}

	//show or hide touch screen controls
	void SetTouchScreenControls(UI_Screen UI){
		if(UI.UI_Name == "TouchScreenControls") return;
		InputManager inputManager = GameObject.FindObjectOfType<InputManager>();
		if(inputManager != null && inputManager.inputType == INPUTTYPE.TOUCHSCREEN){
			if(UI.showTouchControls){
				ShowMenu("TouchScreenControls", false);
			} else {
				CloseMenu("TouchScreenControls");
			}
		}
	}

	public void PlayNextStage() 
	{
		GlobalGameSettings.currentLevelId += 1;
	}

	public void openURL(string url) 
	{
        Application.OpenURL(url);
	}

	public void AcceptPrivacyPolicy() 
	{
		PlayerPrefs.SetInt("privacy",1);
		Privacy_Policy.SetActive(false);
	}

	public void playInsterstitialAd() 
	{
		AdmobManager.instance.showInterstitialAd();

		//adsplugin.instance.Showinterstial();
	}

	public void playRewardedAd()
	{
		//adsplugin.instance.ShowRewardedAd();
		AdmobManager.instance.showRewardedAd();
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnSceneLoaded;
	}
	
	public void Reward()
	{
		GlobalAudioPlayer.PlaySFX("Cash");
		PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney", 1000) + 500);
		adPanel.SetActive(false);
		UpdateEconomy();
	}

	public void startTutorial() 
	{
		Time.timeScale = 0;
		TutorialPanel.SetActive(true);
		TutorialScreens[0].SetActive(true);
	}

	public void nextTutorial(int index) 
	{
		foreach (GameObject screen in TutorialScreens) 
		{
			screen.SetActive(false);
		}
		TutorialScreens[index].SetActive(true);

		if (index >= TutorialScreens.Length) 
		{
			finishTutorial();
		}
	}

	public void finishTutorial()
	{
		Time.timeScale = 1;
		TutorialPanel.SetActive(false);
		foreach (GameObject screen in TutorialScreens)
		{
			screen.SetActive(false);
		}
		PlayerPrefs.SetInt("Tutorial", 1);
	}

	public void SendScreenAnalytics(string screenName) 
	{
		//GA Event
		//MyAnalytics.instance.ScreenEvent(screenName);
	}
}
	
[System.Serializable]
public class UI_Screen 
{
	public string UI_Name;
	public GameObject UI_Gameobject;
	public bool showTouchControls;
}
