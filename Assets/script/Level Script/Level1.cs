using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    public GameObject pig;
    
    void Start()
    {

    }
    void Update()
    {

    }

    void tapPig(TapGesture gesture)
    {
        if (GameData.getInstance().isLock)//If the game locked,you can not control the game.
            return;
        if (gesture != null && gesture.Selection == pig)
        {
            Destroy(pig);
            GameData.getInstance().main.gameWin();
        }
    }

}
