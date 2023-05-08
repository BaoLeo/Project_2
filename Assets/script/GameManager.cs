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



	GameObject music;//sound control instance
	/// <summary>
	/// Plaies the music.
	/// </summary>
	/// <param name="str">String.</param>
	/// <param name="isforce">If set to <c>true</c> isforce.</param>
	public void playMusic(string str, bool isforce = false)
	{

		//do not play the same music againDebug.Log (musicName+"__"+str);
		if (!isforce)
		{
			if (bgMusic != null && musicName == str)
			{
				return;
			}
		}


		if (!music)
			return;


		AudioSource tmusic = null;

		AudioClip clip = (AudioClip)Resources.Load("sound\\" + str, typeof(AudioClip));//调用Resources方法加载AudioClip资源

		Debug.Log(clip);
		if (GameData.getInstance().isSoundOn == 0)
		{
			if (bgMusic)
				bgMusic.Stop();
			/*tmusic = music.GetComponent<musicScript>().PlayAudioClip(clip, true);*/
			if (str.Substring(0, 2) == "bg")
			{
				musicName = str;
				bgMusic = tmusic;

			}
		}

	}






	List<AudioSource> currentSFX = new List<AudioSource>();
	Dictionary<string, int> sfxdic = new Dictionary<string, int>();

	AudioSource cWalk = new AudioSource(); //sometime for continous sound like walk steps.
	/// <summary>
	/// Plaies the sfx.
	/// </summary>
	/// <returns>The sfx.</returns>
	/// <param name="str">String.</param>
	public AudioSource playSfx(string str)
	{
		AudioSource sfxSound = null;

		if (!music)
			return null;
		//				if (sfxdic.ContainsKey("walk") && sfxdic["walk"] == 1 && str == "walk") {
		//						
		//				}
		AudioClip clip = (AudioClip)Resources.Load("sound\\" + str, typeof(AudioClip));//调用Resources方法加载AudioClip资源
		if (GameData.getInstance().isSfxOn == 0)
		{
			/*sfxSound = music.GetComponent<musicScript>().PlayAudioClip(clip);*/
			if (sfxSound != null)
			{
				if (sfxdic.ContainsKey(str) == false || sfxdic[str] != 1)
				{
					currentSFX.Add(sfxSound);

					sfxdic[str] = 1;
					if (str == "walk")
					{
						cWalk = sfxSound;
					}
				}
			}
		}

		return sfxSound;


	}


	AudioSource bgMusic = new AudioSource();//record background music
	public string musicName = "";
	/// <summary>
	/// Stops the background music.
	/// </summary>
	public void stopBGMusic()
	{
		if (bgMusic)
		{
			bgMusic.Stop();
			musicName = "";
		}
	}
	/// <summary>
	/// Stops all sound effect.
	/// </summary>
	public void stopAllSFX()
	{
		foreach (AudioSource taudio in currentSFX)
		{
			if (taudio != null) taudio.Stop();
		}
		currentSFX.Clear();
		sfxdic.Clear();
	}

	/// <summary>
	/// detect a certain sound whether is playing
	/// </summary>
	/// <returns><c>true</c>, if playing sfx was ised, <c>false</c> otherwise.</returns>
	/// <param name="str">String.</param>
	public bool isPlayingSfx(string str)
	{
		bool isPlaying = false;
		if (sfxdic.ContainsKey(str) && sfxdic[str] == 1)
		{
			isPlaying = true;
		}
		return isPlaying;

	}

	/// <summary>
	/// Stops the music.
	/// </summary>
	/// <param name="musicName">Music name.</param>
	public void stopMusic(string musicName = "")
	{
		if (music)
		{
			AudioSource[] as1 = music.GetComponentsInChildren<AudioSource>();
			foreach (AudioSource tas in as1)
			{
				if (musicName == "")
				{
					tas.Stop();
					break;
				}
				else
				{
					if (tas && tas.clip)
					{
						string clipname = (tas.clip.name);
						if (clipname == musicName)
						{
							tas.Stop();


							musicName = "";
							if (sfxdic.ContainsKey(clipname))
							{
								sfxdic[clipname] = 0;
								if (clipname == "walk")
								{
									if (cWalk != null)
									{
										cWalk.Stop();
										cWalk = null;
									}
								}
							}
							break;
						}
					}
				}
			}
		}
	}

	/// <summary>
	/// switch the sound.
	/// </summary>
	public void toggleSound()
	{


		int soundState = GameData.getInstance().isSoundOn;




	}



	/// <summary>
	/// Submits the game center.
	/// </summary>
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
		

		music = GameObject.Find("music") as GameObject;
		int allScore = 0;
		for (int i = 1; i <= GameData.totalLevel; i++)
		{
			int tScore = PlayerPrefs.GetInt("levelScore_" + i.ToString(), 0);
			allScore += tScore;
			//						Debug.Log("=========================bestScore is:"+tScore);
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
	/// <summary>
	/// Sets the state of the toggle buttons.
	/// </summary>
	public void setToggleState()
	{
		//this section will trigger the click itself.So force not play the sound.(if notogglesound is true)
		noToggleSound = true;
		GameObject checkMusicG = GameObject.Find("toggleMusic");
		if (checkMusicG)
		{

			noToggleSound = false;

		}
	}


	//=================================GameCenter======================================
	public void initGameCenter()
	{
		Social.localUser.Authenticate(HandleAuthenticated);
	}


	private bool isAuthored = false;
	private void HandleAuthenticated(bool success)
	{
		//        Debug.Log("*** HandleAuthenticated: success = " + success);
		if (success)
		{
			Social.localUser.LoadFriends(HandleFriendsLoaded);
			Social.LoadAchievements(HandleAchievementsLoaded);
			Social.LoadAchievementDescriptions(HandleAchievementDescriptionsLoaded);


			isAuthored = true;
			//登录成功就提交分数
			submitGameCenter();

		}



	}

	private void HandleFriendsLoaded(bool success)
	{
		//        Debug.Log("*** HandleFriendsLoaded: success = " + success);
		foreach (IUserProfile friend in Social.localUser.friends)
		{
			//            Debug.Log("*   friend = " + friend.ToString());
		}
	}

	private void HandleAchievementsLoaded(IAchievement[] achievements)
	{
		//        Debug.Log("*** HandleAchievementsLoaded");
		foreach (IAchievement achievement in achievements)
		{
			//            Debug.Log("*   achievement = " + achievement.ToString());
		}
	}

	private void HandleAchievementDescriptionsLoaded(IAchievementDescription[] achievementDescriptions)
	{
		//        Debug.Log("*** HandleAchievementDescriptionsLoaded");
		foreach (IAchievementDescription achievementDescription in achievementDescriptions)
		{
			//            Debug.Log("*   achievementDescription = " + achievementDescription.ToString());
		}
	}

	// achievements

	public void ReportProgress(string achievementId, double progress)
	{
		if (Social.localUser.authenticated)
		{
			Social.ReportProgress(achievementId, progress, HandleProgressReported);
		}
	}

	private void HandleProgressReported(bool success)
	{
		//        Debug.Log("*** HandleProgressReported: success = " + success);
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



	//ads
	/*bool testmode = true;
	string androidGameId = "3346291";
	string iosGameId = "3346290";
	string gameId;
	string rewardPlacementId = "rewardedVideo";
	string videoPlacementId = "video";*/
	


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
