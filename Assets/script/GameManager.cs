using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
//using MadLevelManager;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}





	public static GameManager instance;
	public static GameManager getInstance()
	{
		if (instance == null)
		{
			instance = new GameManager();
			instance.init();
		}
		return instance;
	}

	public void submitGameCenter()
	{
		if (!isAuthored)
		{
			//Debug.Log("authenticating...");
			//initGameCenter();
		}
		else
		{
			Debug.Log("submitting score...");
			//			int totalScore = getAllScore();
			int tbestScore = GameData.getInstance().bestScore;
			ReportScore(Const.LEADER_BOARD_ID, tbestScore);

		}

	}


	public void init()
	{
		GameData.getInstance().resetData();
		//								PlayerPrefs.DeleteAll ();
		//get data
		

		
		int allScore = 0;
		for (int i = 1; i <= GameData.totalLevel; i++)
		{
			int tScore = PlayerPrefs.GetInt("levelScore_" + i.ToString(), 0);
			allScore += tScore;
		}

		GameData.getInstance().levelPassed = PlayerPrefs.GetInt("levelPassed", 0);
		Debug.Log("current passed level = " + GameData.getInstance().levelPassed);
		for (int i = 0; i <= GameData.getInstance().levelPassed; i++)
		{
		}


		for (int i = 0; i <= GameData.totalLevel; i++)
		{

			//save star state to gameobject
			int tStar = PlayerPrefs.GetInt("levelStar_" + i.ToString(), 0);
			GameData.getInstance().lvStar.Add(tStar);
		}


		GameData.getInstance().bestScore = allScore;
		GameData.getInstance().isSoundOn = (int)PlayerPrefs.GetInt("sound", 0);
		GameData.getInstance().isSfxOn = (int)PlayerPrefs.GetInt("sfx", 0);
		Debug.Log("soundstate:" + GameData.getInstance().isSoundOn + "sfxstate:" + GameData.getInstance().isSfxOn);
		initGameCenter();

		/*initStore();*/

	}
	public bool noToggleSound = false;
	public void setToggleState()
	{
		noToggleSound = true;
	}


	//=================================GameCenter======================================
	public void initGameCenter()
	{
		Social.localUser.Authenticate(HandleAuthenticated);
	}


	private bool isAuthored = false;
	private void HandleAuthenticated(bool success)
	{
		if (success)
		{
			Social.localUser.LoadFriends(HandleFriendsLoaded);
			Social.LoadAchievements(HandleAchievementsLoaded);
			Social.LoadAchievementDescriptions(HandleAchievementDescriptionsLoaded);


			isAuthored = true;
			submitGameCenter();

		}



	}

	private void HandleFriendsLoaded(bool success)
	{
		foreach (IUserProfile friend in Social.localUser.friends)
		{
		}
	}

	private void HandleAchievementsLoaded(IAchievement[] achievements)
	{
		foreach (IAchievement achievement in achievements)
		{
		}
	}

	private void HandleAchievementDescriptionsLoaded(IAchievementDescription[] achievementDescriptions)
	{
		foreach (IAchievementDescription achievementDescription in achievementDescriptions)
		{
		}
	}


	public void ReportProgress(string achievementId, double progress)
	{
		if (Social.localUser.authenticated)
		{
			Social.ReportProgress(achievementId, progress, HandleProgressReported);
		}
	}

	private void HandleProgressReported(bool success)
	{
	}

	public void ShowAchievements()
	{
		if (Social.localUser.authenticated)
		{
			Social.ShowAchievementsUI();
		}
	}

	// leaderboard

	public void ReportScore(string leaderboardId, long score)
	{
#if UNITY_IOS
		Debug.Log("submitting score to GC...");
				if (Social.localUser.authenticated) {
						Social.ReportScore(score, leaderboardId, HandleScoreReported);
				}
#endif
	}

	public void HandleScoreReported(bool success)
	{
		//        Debug.Log("*** HandleScoreReported: success = " + success);
	}

	public void ShowLeaderboard()
	{
		Debug.Log("showLeaderboard");
		if (Social.localUser.authenticated)
		{
			Social.ShowLeaderboardUI();
		}
	}

	//=============================================GameCenter=========================

	public void buyFullVersion()
	{
		//		UnityPluginForWindowsPhone.Class1.BuyFullVersion(Const.wp8ID);
	}



	


	public void hideBanner(bool isHidden)
	{

	}

	public void showBanner()
	{
		if (GameData.isAds)
		{

		}
	}


	public void CacheInterestial()
	{
				
	}

	bool isfirst = true;
	


	







	void makeReward()
	{
		//add 10 coins;
		GameData.getInstance().coin += 1000;
		PlayerPrefs.SetInt("coin", GameData.getInstance().coin);

		GameObject topBar = GameObject.Find("PanelTopBar");
		if (topBar != null)
		{
			topBar.SendMessage("refreshView");
		}
		GameObject txtcoinNum = GameObject.Find("txtCoinNum");
		if (txtcoinNum != null)
		{
		}
		PlayerPrefs.Save();


	}


	//=============================================GameCenter=========================


	//in app
	//		public const string NON_CONSUMABLE0 = "com.xxx.unlockall";//only use this for this version
	public const string CONSUMABLE0 = "td15612.coin1";
	public const string CONSUMABLE1 = "td15612.coin2";
	public const string CONSUMABLE2 = "td15612.coin3";
	public const string CONSUMABLE3 = "td15612.coin4";

	/*public static Purchaser purchaser;
	void initStore()
	{

		GameObject music = GameObject.Find("music");
		if (music != null)
		{
			purchaser = music.GetComponent<Purchaser>();
		}
	}
*/

	//only for google store if have one.Otherwise just ignore.

	
	/// <summary>
	/// Buy item
	/// </summary>
	/// <param name="index">Index.</param>
	/*public void buy(int index)
	{
		if (test)
		{
			purchansedCallback("pack" + index);
		}
		else
		{
			switch (index)
			{
				case 0:
					purchaser.BuyConsumable("pack0");
					break;
				case 1:
					purchaser.BuyConsumable("pack1");
					break;
				case 2:
					purchaser.BuyConsumable("pack2");
					break;
				case 3:
					purchaser.BuyConsumable("pack3");
					break;



			}
		}
	}*/

	/*public void restore()
	{
		purchaser.RestorePurchases();
	}*/

	/// <summary>
	/// This will be called when a purchase completes.
	/// </summary>
	public void purchansedCallback(string id)
	{

		bool buyenough = false;
		switch (id)
		{
			case "pack0":
				buyenough = true;
				GameData.getInstance().coin += 300;
				break;
			case "pack1":
				buyenough = true;
				GameData.getInstance().coin += 600;
				break;
			case "pack2":
				buyenough = true;
				GameData.getInstance().coin += 1000;
				break;
			case "pack3":
				buyenough = true;
				GameData.getInstance().coin += 1500;
				break;
		}

		PlayerPrefs.SetInt("coin", GameData.getInstance().coin);
		GameObject txtCoin = GameObject.Find("txtCoin");
		if (txtCoin != null)
		{
			txtCoin.GetComponent<Text>().text = GameData.getInstance().coin.ToString();
		}



	}



	





}
