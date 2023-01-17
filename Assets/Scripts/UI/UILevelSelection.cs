using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UILevelSelection : MonoBehaviour
{
	public Button[] levels;

	private void OnEnable() 
	{
		//Check levels unlocked
		int i = 0;
		foreach (Button level in levels) 
		{
			if (i <= PlayerPrefs.GetInt("LevelsCompleted", 0))
			{
				level.interactable = true;
				level.GetComponent<UILevelItem>().enabled = true;
			}
            else
            {
                level.interactable = false;
				level.GetComponent<UILevelItem>().enabled = false;
			}
			i++;
		}

		//GA Event
		//MyAnalytics.instance.ScreenEvent("LevelSelection");
	}
}

[System.Serializable]
public class LevelData {
	public string levelTitle = "";
	public Sprite levelSprite;
	public string sceneToLoad = "";
	public int levelId = 0;
}