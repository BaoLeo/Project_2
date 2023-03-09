using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
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
        if(instance==null)
        {
            instance = new GameManager();
            instance.init();
        }
        return instance;
    }    
    public void init()
    {
        GameData.getInstance().resetData();
    }    
}
