using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PanelMain : MonoBehaviour
{
    public Text btnStart, btnMore, btnAbout;
    public Image mask;
    // Start is called before the first frame update
    void Start()
    {
        fadeOut();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Onclick(GameObject g)
    {
        switch (g.name)
        {
            case "btnStart":
                fadeIn("LevelMenu");
                break;
            case "btnMore":
                break;
            case "btnAbout":
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
    void OnUpdateTween(float value)

    {
        mask.color = new Color(0, 0, 0, value);
    }
}
