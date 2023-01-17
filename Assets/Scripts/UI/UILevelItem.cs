using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UILevelItem : UIButtonEvents {

	public LevelData levelData;

	override public void OnSelect(BaseEventData eventData){
		if(Time.time - menuOpenTime > .1f) GlobalAudioPlayer.PlaySFX(SFXOnButtonSelect);
	}
		
	override public void OnSubmit(BaseEventData eventData){
		GlobalAudioPlayer.PlaySFX(SFXOnButtonPress);
		GlobalGameSettings.currentLevelId = levelData.levelId;
		LoadScene(levelData.sceneToLoad);
		//FindObjectOf  Type<UIManager>().UI_fader.Fade(UIFader.FADE.FadeIn, .5f, .3f);
	}

	override public void OnPointerDown(PointerEventData eventData){
		OnSubmit(new BaseEventData(EventSystem.current));
	}
}
