using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    public GameObject pig;
     void Start () {
        
     }
     void Update ()
     {
        
     }
    
    void tapPig(TapGesture gesture)
    {
        if (GameData.getInstance().isLock)
        {
            return;
        }
        if (gesture != null && gesture.Selection == pig )
        {
            Destroy(pig);
            
        }    
    }    
}
