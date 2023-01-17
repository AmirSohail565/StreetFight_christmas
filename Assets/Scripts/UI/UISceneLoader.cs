using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISceneLoader : MonoBehaviour {

	private bool loadSceneInProgress;
	private string sceneTitle;
	//load a new scene
	public void LoadScene(string sceneName){
		sceneTitle = sceneName;
		if(!loadSceneInProgress) StartCoroutine(LoadSceneCoroutine(sceneName));
	}

	IEnumerator LoadSceneCoroutine(string sceneName){
		loadSceneInProgress = true;

		//Fade In Loading panel
		UIFader fader = GameObject.FindObjectOfType<UIFader>();
		//Debug.Log("NAME1 = " + fader.name);

		if (fader != null)
		{
			fader.Fade(UIFader.FADE.FadeIn, 0.4f, 0.4f);
		}

		Invoke("LoadnewScene",1.6f);
		yield return new WaitForSeconds(0);
	}

	public void LoadnewScene() 
	{
		//Load new scene
		SceneManager.LoadScene(sceneTitle);
		loadSceneInProgress = false;
	}
}
