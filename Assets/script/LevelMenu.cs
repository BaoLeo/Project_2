using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
public class LevelMenu : MonoBehaviour
{

	// Use this for initialization

	GameObject listItemg;
	GameObject mainContainer;
	List<GameObject> groups;
	void Start()
	{

		GameManager.getInstance().init();
		GameData.getInstance().resetData();
		Localization.Instance.SetLanguage(GameData.getInstance().GetSystemLaguage());
		initView();
		mainContainer = GameObject.Find("mainContainer");
		groups = new List<GameObject>();
		foreach (Transform group_ in mainContainer.transform)
		{
			groups.Add(group_.gameObject);
		}
	}

	// Update is called once per frame
	void Update()
	{


	}

	bool isMoving = false;
	public void move(float dis)
	{
		if (canmove)
		{
			foreach (Transform m in mainContainer.transform)
			{
				m.transform.Translate(dis, 0, 0);
			}
			isMoving = true;
		}
	}

	
	public void swipePage(float force)
	{


		if (Mathf.Abs(force) < 1f)
		{
			if (groups[page].transform.position.x < Screen.width / 4)
			{
				if (page >= 0 && page < pages)
				{
					GoRight();
				}
				else
				{
					returnPage();
				}

			}
			else if (groups[page].transform.position.x > Screen.width)
			{
				if (page <= pages && page > 0)
				{
					GoLeft();
				}
				else
				{
					returnPage();

				}
			}
			else
			{
				returnPage();
			}

		}
		else
		{
			if (groups[page].transform.position.x < Screen.width / 2)
			{
				if (page >= 0 && page < pages)
				{
					GoRight();
				}
				else
				{
					returnPage();
				}

			}
			else if (groups[page].transform.position.x > Screen.width / 2)
			{
				if (page <= pages && page > 0)
				{
					GoLeft();
				}
				else
				{
					returnPage();

				}
			}
			else
			{
				returnPage();
			}
		}

		StopCoroutine("swiped");
		StartCoroutine("swiped");

	}

	IEnumerator swiped()
	{
		yield return new WaitForEndOfFrame();
		isMoving = false;
	}

	public GameObject levelButton;
	public GameObject dot;

	int page = 0;//current page
	int pages = 1;//how many page
	public int perpage = 8;//icons per page
	List<GameObject> gContainer;//each icon group for per page
	List<GameObject> pageDots;//all page dots
	float gap = Screen.width / 8.5f;//the gap for each page
	public Image mask;//the fade in/out mask
	void initView()
	{

		pageDots = new List<GameObject>();


		pages = Mathf.FloorToInt(GameData.totalLevel / perpage);
		for (int i = 0; i <= pages; i++)
		{
			GameObject tdot = Instantiate(dot, dot.transform.parent) as GameObject;
			tdot.SetActive(true);
			pageDots.Add(tdot);
			tdot.name = "dot_" + i;

		}

		setpageDot();
		fadeOut();

		gContainer = new List<GameObject>();
		gContainer.Add(levelButton.transform.parent.gameObject);
		//		levelButton.GetComponent<RectTransform> ().localScale = Vector3.one;
		Transform container = levelButton.transform.parent;
		container.transform.localScale = Vector3.one;

		for (int i = perpage; i < GameData.totalLevel; i += perpage)
		{
			GameObject tgroup = Instantiate(levelButton.transform.parent.gameObject, levelButton.transform.parent.position, Quaternion.identity) as GameObject;
			tgroup.transform.Translate(gap * (i + 1), 0, 0);
			gContainer.Add(tgroup);

			tgroup.transform.parent = levelButton.transform.parent.gameObject.transform.parent;
		}


		for (int i = 0; i < GameData.totalLevel; i++)
		{
			GameObject tbtn = Instantiate(levelButton, Vector3.zero, Quaternion.identity) as GameObject;

			int tContainerNo = Mathf.FloorToInt(i / perpage);
			tbtn.transform.parent = gContainer[tContainerNo].transform;
			//			gContainer[tContainerNo].GetComponent<RectTransform> ().localScale = Vector3.one;
			tbtn.SetActive(true);

			tbtn.transform.localScale = new Vector3(2, 2, 1);
			tbtn.GetComponentInChildren<Text>().text = (i + 1).ToString();
			tbtn.transform.parent.localScale = Vector3.one;




			Text ttext = tbtn.GetComponentInChildren<Text>();


			if (GameData.getInstance().lvStar.Count > i + 1)
			{

				int starCount = GameData.getInstance().lvStar[i + 1];


				if (GameData.getInstance().lvStar.Count > i + 1)
				{
					for (int j = 1; j <= starCount; j++)
					{
						ttext.transform.parent.Find("star" + j).GetComponent<Image>().enabled = true;
					}
				}
			}


			if (i >= GameData.getInstance().levelPassed && i > 0)
			{

				ttext.enabled = false;




			}
			else
			{


				tbtn.name = "level" + (i + 1);
				tbtn.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => clickLevel(tbtn));
				ttext.gameObject.transform.parent.Find("lock").GetComponent<Image>().enabled = false;

			}

		}


		GameObject.Find("txtScores").GetComponent<Text>().text = Localization.Instance.GetString("levelScore") + GameData.getInstance().bestScore;
		GameObject.Find("confirm").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnContinue");


	}
	public void clickDot(GameObject tdot)
	{
		int tdotIndex = int.Parse(tdot.transform.parent.name.Substring(4, tdot.transform.parent.name.Length - 4));
		page = tdotIndex;
		canmove = false;


		ATween.MoveTo(gContainer[0].transform.parent.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "x", -gContainer[page].transform.localPosition.x, "time", .3f, "easeType", "easeOutExpo", "oncomplete", "dotclicked", "oncompletetarget", this.gameObject));


	}

	void dotclicked()
	{
		canmove = true;
		setpageDot();

	}


	public static bool islock = false;

	void clickLevel(GameObject tbtn)
	{
		if (!isMoving)
		{
			GameData.getInstance().cLevel = int.Parse(tbtn.GetComponentInChildren<Text>().text) - 1;
			fadeIn(tbtn.name);
		}


	}


	
	void setpageDot()
	{
		for (int i = 0; i < pageDots.Count; i++)
		{
			pageDots[i].GetComponent<Image>().color = new Color(1, 1, 1, .5f);
		}
		pageDots[page].GetComponent<Image>().color = new Color(1, 1, 1, 1);
	}


	
	public void continueLevel()
	{

		int tLastLevel = GameData.getInstance().levelPassed;

		if (tLastLevel < GameData.totalLevel)
		{
			GameData.getInstance().cLevel = tLastLevel;
		}
		else
		{
			GameData.getInstance().cLevel = GameData.totalLevel;
		}

		string tstr = "level" + GameData.getInstance().cLevel;
		fadeIn(tstr);
	}

	
	public void backMain()
	{
		fadeIn("MainMenu");
	}

	
	public void loadGameScene()
	{

		SceneManager.LoadScene("Game");
	}
	public void loadMainScene()
	{

		SceneManager.LoadScene("MainMenu");
	}


	bool canmove = true;//can not enter a level and can not move when moving
	public void GoRight()
	{
		if (!canmove)
			return;
		if (page < pages)
		{

			page++;
			canmove = false;


			ATween.MoveTo(gContainer[0].transform.parent.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "x", -gContainer[page].transform.localPosition.x, "time", .3f, "easeType", "easeOutExpo", "oncomplete", "dotclicked", "oncompletetarget", this.gameObject));


		}
	}
	public void GoLeft()
	{
		if (!canmove)
			return;
		if (page > 0)
		{

			page--;
			canmove = false;


			ATween.MoveTo(gContainer[0].transform.parent.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "x", -gContainer[page].transform.localPosition.x, "time", .3f, "easeType", "easeOutExpo", "oncomplete", "dotclicked", "oncompletetarget", this.gameObject));


		}
	}


	void fadeOut()
	{
		mask.gameObject.SetActive(true);
		mask.color = Color.black;

		ATween.ValueTo(mask.gameObject, ATween.Hash("ignoretimescale", true, "from", 1, "to", 0, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeOutOver", "oncompletetarget", this.gameObject));

	}

	void fadeIn(string sceneName)
	{
		if (mask.IsActive())
			return;
		mask.gameObject.SetActive(true);
		mask.color = new Color(0, 0, 0, 0);

		ATween.ValueTo(mask.gameObject, ATween.Hash("ignoretimescale", true, "from", 0, "to", 1, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeInOver", "oncompleteparams", sceneName, "oncompletetarget", this.gameObject));

	}
	void fadeInOver(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
	void fadeOutOver()
	{
		mask.gameObject.SetActive(false);
	}
	void OnUpdateTween(float value)

	{

		mask.color = new Color(0, 0, 0, value);
	}
	void returnPage()
	{
		canmove = false;
		ATween.MoveTo(gContainer[page].transform.parent.gameObject, ATween.Hash("ignoretimescale", true, "islocal", true, "x", -gContainer[page].transform.localPosition.x, "time", .3f, "easeType", "easeOutExpo", "oncomplete", "dotclicked", "oncompletetarget", this.gameObject));

	}

	//debug use
	public void debugtext(string str)
	{
		//				GameObject.Find ("txtScores").GetComponent<Text> ().text = str;
	}

}
