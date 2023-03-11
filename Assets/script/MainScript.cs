using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour
{
    public int timeCount = 0;
    public Text txtLevel;
    public Image mask;
    int clevel=0;
    // Start is called before the first frame update
    void Start()
    {
        initData();
        initGameView();
        fadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    GameObject gameContainer;
    void initData()
    {
        
        Time.timeScale = 1;
        gameContainer = GameObject.Find("gameContainer");
    }
    
    void initGameView()
    {

        GameObject tFloor = Resources.Load("floor" + 1) as GameObject;
        GameObject tWall = Resources.Load("wall" + 1) as GameObject;
        tFloor = Instantiate(tFloor, new Vector3(0, -2.5f, 0), Quaternion.identity) as GameObject;
        tWall = Instantiate(tWall, new Vector3(0, .5f, 0), Quaternion.identity) as GameObject;
        if (Camera.main.transform.position.y != 1)
        {
            return;
        }
        tFloor.transform.position = new Vector3(0, -1.5f, 0);
        tWall.transform.position = new Vector3(0, 1.5f, 0);
    }
    void fadeOut()
    {
        mask.gameObject.SetActive(true);
        mask.color = Color.black;
        ATween.ValueTo(mask.gameObject, ATween.Hash("ignoretimescale", true, "from", 1, "to", 0, "time", 1, "onupdate", "OnUpdateTween", "onupdatetarget", this.gameObject, "oncomplete", "fadeOutOver", "oncompletetarget", this.gameObject));

    }
   
    public void OnClick(GameObject g)
    {
        switch (g.name)
        {
          
            case "btnLevel":
                fadeIn("LevelMenu");
                break;
            case "btnTitle":
                fadeIn("MainMenu");
                break;
            case "btnRetry":
                fadeIn(SceneManager.GetActiveScene().name);
                break;
            case "btnNext":
                fadeIn("level" + (clevel + 1));
                break;
        }
    }
    public void nextLevel()
    {
        fadeIn("level" + (clevel + 1));
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
}
