using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour
{// Use this for initialization
	public GameObject skull;
	SpriteRenderer sp;
	public GameObject bread;
	void Start()
	{
		sp = skull.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{

	}




	float myalpha = 1;//skull alpha,if not drop to 0,you pick the skull,you lose the game
	
	void OnFingerMove(FingerMotionEvent e)
	{
		if (e.Selection == skull)
		{//is touching on skull target
			if (e.Phase == FingerMotionPhase.Started)
			{//is start moving

			}
			else if (e.Phase == FingerMotionPhase.Updated)
			{//is moving
				myalpha = sp.color.a - .004f;//keep decrease the skull alpha
				sp.color = new Color(sp.color.r, sp.color.g, sp.color.b, myalpha);//update color
				if (myalpha <= 0)
				{
					sp.enabled = false;//keep alpha to zero if 
				}
			}
			else
			{


			}
		}
	}

	
	void OnTap(TapGesture gesture)
	{
		if (GameData.getInstance().isLock)//if locked,not response to player input
			return;
		if (gesture != null && gesture.Selection == bread)
		{//when choose bread
			DestroyObject(bread);//pick up bread
			if (sp.color.a > .01f)
			{//if skull have not faded out.
				GameData.getInstance().main.gameFailed();   //you lose the game
				GameManager.getInstance().playSfx("puke");//play a dead sound effect
			}
			else
			{
				GameData.getInstance().main.gameWin();  //you win the game
			}
		}
	}
}
