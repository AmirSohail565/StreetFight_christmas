using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using GameAnalyticsSDK;

public class UICharSelection : UISceneLoader {

	private UICharSelectionPortrait[] portraits;
	public GameObject[] CharsUI;
	public GameObject[] CharsModels;
	public int[] CharsUnlockPrices;

	public Button SelectBtn;
	public Button UnlockBtnCoin;
	public Button UnlockBtnCash;
	public Transform CharsParent;

	public Button LeftBtn;
	public Button RightBtn;

	public int currentCharIndex;
	public GameObject purchaseFailPanel;

	public Slider health, power, speed, jump;

	void OnEnable()
	{
		//PlayerPrefs.SetInt("TotalMoney", 999000);

		//InputManager.onInputEvent += OnInputEvent;
		purchaseFailPanel.SetActive(false);
		CheckCharactersUnlockability();

		//GA Event
		//MyAnalytics.instance.ScreenEvent("MainMenu");
	}

	public void CheckCharactersUnlockability() 
	{
		int i = 0;
		foreach (Transform child in CharsParent) 
		{
			//Hiding it on purpose
			child.gameObject.SetActive(false);

			if (PlayerPrefs.GetInt("Hero" + i, 0) == 1)
			{
				child.GetComponent<UICharSelectionPortrait>().Unlocked = true;
			}
			else 
			{
				child.GetComponent<UICharSelectionPortrait>().Unlocked = false;
			}

			child.gameObject.SetActive(true);

			//Force enable first hero/character by default
			if (i == 0) 
			{
				child.GetComponent<UICharSelectionPortrait>().Unlocked = true;
				child.GetChild(2).gameObject.SetActive(false);
			}

			i++;
		}
	}

	public void purchaseChar() 
	{
		int index = currentCharIndex;

		if (CharsUnlockPrices[index] <= PlayerPrefs.GetInt("TotalMoney", 1000))
		{
			PlayerPrefs.SetInt("Hero" + index, 1);
			CheckCharactersUnlockability();
			selectChar(index);

			//Deduct money
			PlayerPrefs.SetInt("TotalMoney", PlayerPrefs.GetInt("TotalMoney",1000) - CharsUnlockPrices[index]);

			//GA Event
			//MyAnalytics.instance.BuyEvent(GlobalGameSettings.currentLevelId.ToString());

			FindObjectOfType<UIManager>().UpdateEconomy();
		}

		else
		{
			purchaseFailPanel.SetActive(true);
		}
		
	}

	void OnDisable() {
		//InputManager.onInputEvent += OnInputEvent;
	}

	void Start(){
		
		//find all character portraits
		portraits = GetComponentsInChildren<UICharSelectionPortrait>();

		//select last character by default
		//GetComponentInChildren<UICharSelectionPortrait>().OnClick(PlayerPrefs.GetInt("CurrentPlayer", 0));
		selectChar(PlayerPrefs.GetInt("CurrentPlayer", 0));
	}

	/*void OnInputEvent(string action, BUTTONSTATE buttonState){

		//move left
		if(action == "Left" && buttonState == BUTTONSTATE.PRESS) OnLeftButtonDown();

		//move right
		if(action == "Right" && buttonState == BUTTONSTATE.PRESS) OnRightButtonDown();

	}*/

	//select portrait on the left
	/*void OnLeftButtonDown(){
		int selectedPortrait = getSelectedPortrait();
		portraits[selectedPortrait].Selected = false; //disable the current selection
		if(selectedPortrait-1 >= 0) portraits[selectedPortrait-1].OnClick(); //select previous portrait
	}

	//select portrait on the right
	void OnRightButtonDown(){
		int selectedPortrait = getSelectedPortrait();
		portraits[selectedPortrait].Selected = false; //disable the current selection
		if(selectedPortrait+1 < portraits.Length) portraits[selectedPortrait+1].OnClick(); //select next portrait
	}*/

	//returns the index of the current selected portrait
	int getSelectedPortrait(){
		for(int i = 0; i < portraits.Length; i++) {
			if(portraits[i].Selected) return i;
		}
		return 0;
	}

	//select a player
	public void SelectPlayer(GameObject playerPrefab){
		GlobalGameSettings.Player1Prefab = playerPrefab;
	}

	public void navigateCharRight() 
	{
		int num = currentCharIndex;
		if (num >= CharsUI.Length - 1)
		{
			num = CharsUI.Length - 1;
		}
		else 
		{
			num++;
		}
		selectChar(num);
	}

	public void navigateCharLeft()
	{
		int num = currentCharIndex;
		if (num <= 0)
		{
			num = 0;
		}
		else 
		{
			num--;
		}
		selectChar(num);
	}

	public void selectChar(int index) 
	{

		if (PlayerPrefs.GetInt("firstTime") == 0)
		{
			//Play sound
			GlobalAudioPlayer.audioPlayer.playSFX("ButtonStart");
		}
		else
        {
			PlayerPrefs.SetInt("firstTime", 0);
		}
		currentCharIndex = index;
		//Validate unlocked status
		if (CharsUI[index].GetComponent<UICharSelectionPortrait>().Unlocked)
		{
			SelectBtn.gameObject.SetActive(true);
			UnlockBtnCash.gameObject.SetActive(false);
			UnlockBtnCoin.gameObject.SetActive(false);
		}
		else 
		{
			SelectBtn.gameObject.SetActive(false);
			UnlockBtnCash.gameObject.SetActive(true);
			UnlockBtnCoin.gameObject.SetActive(false);

			UnlockBtnCash.transform.GetChild(3).GetComponent<Text>().text = CharsUnlockPrices[index].ToString();
		}

		for (int i = 0; i < CharsUI.Length; i++)
		{
			if (i == index)
			{
				CharsUI[i].transform.GetChild(0).gameObject.SetActive(true);
				CharsUI[i].GetComponent<UICharSelectionPortrait>().UpdateStats();
				CharsModels[i].SetActive(true);

				//Select last selected character
				CharsUI[i].GetComponent<UICharSelectionPortrait>().OnClick(i);
			}
			else
			{
				CharsUI[i].transform.GetChild(0).gameObject.SetActive(false);
				CharsModels[i].SetActive(false);
			}
		}

		if (currentCharIndex <= 0)
		{
			LeftBtn.interactable = false;
		}
		else 
		{
			LeftBtn.interactable = true;
		}

		if (currentCharIndex >= CharsUI.Length - 1)
		{
			RightBtn.interactable = false;
		}
		else
		{
			RightBtn.interactable = true;
		}
	}

	public void QuitGame() 
	{
		Application.Quit();
	}

    public void UpdateStats(float Health,float Power,float Speed,float Jump)
    {
		health.value = Health;
		power.value = Power;
		speed.value = Speed;
		jump.value = Jump;
    }
}
