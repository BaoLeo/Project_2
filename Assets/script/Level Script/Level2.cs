using UnityEngine;
using System.Collections;
//using DG.Tweening;
using System.Collections.Generic;

public class Level2 : MonoBehaviour
{

	// Use this for initialization;
	Vector2 steelStartPos;
	void Start()
	{
		steelStartPos = steelstick.transform.position;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public bool touched = false;
	public GameObject trapoff, trapon, Diamond, steelstick, steelfixed;

	public GameObject[] dragObjects;//you assign all object which can be drag into this object





	
	void OnDrag(DragGesture gesture)
	{

		if (GameData.getInstance().isLock)//if game locked,ignore any inputs
			return;
		if (gesture.Phase == ContinuousGesturePhase.Started)//start drag
		{

		}
		else if (gesture.Phase == ContinuousGesturePhase.Updated)//is draging
		{
			//keep all objects can be drag by moving
			foreach (GameObject dragObject in dragObjects)
			{
				if (gesture.Selection == dragObject)
				{
					//this make all dragable objects moving while draging
					dragObject.transform.position = Util.GetWorldPos(gesture.Position, dragObject);

				}
			}

		}
		else
		{
			foreach (GameObject dragObject in dragObjects)
			{
				if (gesture.Selection == dragObject)
				{//stop drag

					//deal with the things when stop draging
					if (dragObject == steelstick)
					{
						float tdis = Vector2.Distance(trapoff.transform.position, steelstick.transform.position);
						if (tdis < .4f && !touched)
						{
							DestroyImmediate(dragObject);
							steelfixed.SetActive(true);
						}
						else
						{
							steelstick.transform.position = steelStartPos;
						}
					}
				}
			}

		}

	}


	void OnTap(TapGesture gesture)
	{
		if (GameData.getInstance().isLock)
			return;
		if (gesture != null && gesture.Selection == Diamond)
		{
			if (touched)
				return;
			touched = true;

			if (steelfixed.activeSelf == false)
			{



				//play a destroy anim
				GameObject tsmoke = GameObject.Find("explode");
				tsmoke.transform.position = Diamond.transform.position + new Vector3(0, .3f, 0);
				tsmoke.GetComponent<Animator>().SetTrigger("play");
				Destroy(tsmoke, .6f);

				Diamond.SetActive(false);

				GameObject.Find("floor").SetActive(false);
				GameData.getInstance().main.gameFailed();
				trapoff.SetActive(false);
				trapon.SetActive(true);
				GameManager.getInstance().playSfx("hitmetal");
			}
			else
			{
				Destroy(Diamond);

				GameData.getInstance().main.gameWin();

			}
		}
	}


}
