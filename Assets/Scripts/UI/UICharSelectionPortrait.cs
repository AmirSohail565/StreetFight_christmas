using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICharSelectionPortrait : MonoBehaviour {

	public bool Selected;
	public bool Unlocked;
	public UICharSelection characterSelectionScrn;
	public float Health, Power, Speed, Jump;


    private void OnEnable()
    {
		if (Unlocked)
		{
			transform.GetChild(2).gameObject.SetActive(false);
		}
		else 
		{
			transform.GetChild(2).gameObject.SetActive(true);
		}
	}

    private void Awake()
    {
		characterSelectionScrn = GameObject.FindObjectOfType<UICharSelection>();
	}

	[Header("The Player Character Prefab")]
	public GameObject PlayerPrefab;

	[Header("HUD Portrait")]
	public Sprite HUDPortrait;

    public void OnClick(int index){
		Selected = true;
		if (Unlocked) 
		{ 
			PlayerPrefs.SetInt("CurrentPlayer", index);
		}
		//set selected player prefab
		if (characterSelectionScrn) characterSelectionScrn.SelectPlayer(PlayerPrefab);
		UpdateStats();
	}

	public void UpdateStats() 
	{
		characterSelectionScrn.UpdateStats(Health, Power, Speed, Jump);
	}
}