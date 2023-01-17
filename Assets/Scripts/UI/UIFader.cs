using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFader : MonoBehaviour {

	public Image img;
	public Text percentage;
	public Image loadingBar;
	public bool loading;
	public enum FADE { FadeIn, FadeOut }

    public void Fade(FADE fadeDir, float fadeDuration, float StartDelay){
		if(img != null){

			if (fadeDir == FADE.FadeIn	)
			{
				StartCoroutine(FadeIn(0));
			}

            /*if (fadeDir == FADE.FadeOut)
            {
                StartCoroutine(FadeOut(3f));
            }*/
        }
	}

	IEnumerator FadeIn (float StartDelay) 
	{

		yield return new WaitForSeconds(StartDelay);
		GetComponent<Animator>().SetTrigger("move");
		loading = true;
		StartCoroutine(FadeOut(3f));
	}

	IEnumerator FadeOut (float StartDelay)
	{
		yield return new WaitForSeconds(StartDelay);
		GetComponent<Animator>().SetTrigger("move");
		loading = false;
	}

	public void Update() 
	{
		if (loading) 
		{
			float val = loadingBar.fillAmount * 100;
			percentage.text = ((int)val).ToString() + "%"; 
		}
	}

	IEnumerator FadeCoroutine(float From, float To, float Duration, float StartDelay, bool DisableOnFinish){
		yield return new WaitForSeconds(StartDelay);
		
		float t=0;
		Color col = img.color;
		img.enabled = true;
		img.color = new Color(col.r, col.g, col.b, From);

		while(t<1){
			float alpha = Mathf.Lerp (From, To, t);
			img.color = new Color(col.r, col.g, col.b, alpha);
			t += Time.deltaTime/Duration;
			yield return 0;
		}

		img.color = new Color(col.r, col.g, col.b, To);
		img.enabled = !DisableOnFinish;
	}
}
