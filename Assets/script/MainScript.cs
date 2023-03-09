using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        initData();
        initGameView();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    GameObject gameContainer;
    void initData()
    {
        GameData.getInstance().main = this;
        GameData.getInstance().resetData();
        Time.timeScale = 1;
        gameContainer = GameObject.Find("gameContainer");
    }
    void initGameView()
    {
        
        GameObject tFloor = Resources.Load("floor" + 1) as GameObject;
        GameObject tWall = Resources.Load("wall" + 1) as GameObject;
        GameObject tcorner = Resources.Load("cornerbar") as GameObject;
        tFloor = Instantiate(tFloor, new Vector3(0, -2.5f, 0), Quaternion.identity) as GameObject;
        tWall = Instantiate(tWall, new Vector3(0, .5f, 0), Quaternion.identity) as GameObject;
        if (Camera.main.transform.position.y == 1)
        {
            tFloor.transform.position = new Vector3(0, -1.5f, 0);
            tWall.transform.position = new Vector3(0, 1.5f, 0);
        }
    }
}
