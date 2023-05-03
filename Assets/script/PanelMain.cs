using UnityEngine;
using System.Collections;
using UnityEngine.UI;
////using DG.Tweening;
using UnityEngine.SceneManagement;
public class PanelMain : MonoBehaviour
{

    // game UI elements
    public Text btnStart, btnMore, btnReview;
    public GameObject  titleEN;
 
    public Image mask;
    // Use this for initialization
    void Start()
    {
        GameManager.getInstance().init();

        fadeOut();

        Localization.Instance.SetLanguage(GameData.getInstance().GetSystemLaguage());
			
        initView();
    }


	void initView()
	{
		GameObject.Find("btnStart").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnStart");
		GameObject.Find("btnMore").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnMore");
		GameObject.Find("btnReview").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnReview");
	}

	/// <summary>
	/// process toggle button(music and sound effect buttons)
	/// </summary>
	/// <param name="toggle">Toggle.</param>

	public void Onclick(GameObject g)
	{
		switch (g.name)
		{
			case "btnStart":
				fadeIn("LevelMenu");
				break;
			case "btnMore":
				fadeIn("Credit");
				break;
			case "btnReview":
				Application.OpenURL("https://github.com/BaoLeo/Project_2");
				break;
		}
	}

	void fadeOut()
	{
		mask.gameObject.SetActive(true);
		mask.color = Color.black;

		ATween.ValueTo(mask.gameObject, ATween.Hash("from", 1, "to", 0, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeOutOver", "oncompletetarget", this.gameObject));

	}

	void fadeIn(string sceneName)
	{
		if (mask.IsActive())
			return;
		mask.gameObject.SetActive(true);
		mask.color = new Color(0, 0, 0, 0);

		ATween.ValueTo(mask.gameObject, ATween.Hash("from", 0, "to", 1, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeInOver", "oncompleteparams", sceneName, "oncompletetarget", this.gameObject));

	}


	void fadeInOver(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}

	void fadeOutOver()
	{
		mask.gameObject.SetActive(false);
	}

	/// <summary>
	/// tween update event
	/// </summary>
	/// <param name="value">Value.</param>
	void OnUpdateTween(float value)

	{

		mask.color = new Color(0, 0, 0, value);
	}
}

