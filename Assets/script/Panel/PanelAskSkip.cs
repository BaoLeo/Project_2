using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelAskSkip : MonoBehaviour {

	// Use this for initialization
//	dfLabel lb_tipLeft,lb_skipins;


		public Transform panel;
		public GameObject panelNotEnough;
		public GameObject panelDisplayTip;
		public GameObject panelBuyCoin;
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void showMe(){
				initView ();

				if (GameData.getInstance ().coin >= 60) {

						panelNotEnough.SetActive (false);
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 40, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnShowCompleted", "oncompletetarget", this.gameObject));

						//disable some UI;

				} else {
						
						panelNotEnough.SetActive (true);

						panelNotEnough.GetComponent<PanelNotEnough> ().showMe ();
					
						gameObject.SetActive (false);

				}
				GameData.getInstance ().lockGame (true);
	}
		public void OnClick(GameObject g)
	{

		// Add event handler code here
		switch (g.name) {
				case "btnCancel":
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale",true,"islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
						break;
				case "btnYes":
						GameData.getInstance ().coin -= 60;
						PlayerPrefs.SetInt ("coin", GameData.getInstance ().coin);
						ATween.MoveTo (panel.gameObject, ATween.Hash ("ignoretimescale", true, "islocal", true, "y", 460, "time", 1f, "easeType", "easeOutExpo", "oncomplete", "OnHideCompleted", "oncompletetarget", this.gameObject));
						GameData.getInstance ().main.txtCoin.text = GameData.getInstance ().coin.ToString ();
						GameData.getInstance ().main.nextLevel ();
						break;

		}
		
	}
		void initView(){
				panel.transform.Find ("skiptitle").GetComponent<Text> ().text = Localization.Instance.GetString ("askSkipTitle");
				panel.transform.Find ("skiptip").GetComponent<Text> ().text = Localization.Instance.GetString ("askSkipHit");
				panel.transform.Find ("btnYes").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnYes");
				panel.transform.Find ("btnCancel").GetComponentInChildren<Text> ().text = Localization.Instance.GetString ("btnCancel");
		}


		void OnHideCompleted(){
				gameObject.SetActive (false);
				GameData.getInstance ().lockGame (false);
		}
}
