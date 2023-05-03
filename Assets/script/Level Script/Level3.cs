using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class Level3 :MonoBehaviour {

		// Use this for initialization
		public GameObject diamond,fragile,stone,wallhole,diamondfake,destroywall;
		void Start () {


		}


		int n = 0;
		void OnTap( TapGesture gesture )
		{
				if (GameData.getInstance ().isLock)
						return;
				if (gesture != null && gesture.Selection == destroywall) {
						n++;
						if (n == 12) { 
								wallhole.SetActive(true);
								diamond.SetActive(true);
								Destroy(destroywall);
								GameManager.getInstance().playSfx("break");
						}
				}



				if (gesture != null && gesture.Selection == diamondfake) {
//						DestroyObject (bread);
						diamondfake.SetActive (false);
						stone.SetActive(true);
						GameData.getInstance ().main.gameFailed ();
				} 
				if (gesture != null && gesture.Selection == diamond) { 
						DestroyObject (diamond);
						GameData.getInstance ().main.gameWin ();
				}
		}

}
