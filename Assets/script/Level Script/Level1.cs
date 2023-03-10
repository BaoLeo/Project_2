using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    public GameObject pig;
    public Image mask;
    void Start ()
    {
        
    }
     void Update ()
     {
        
     }
    
    void tapPig(TapGesture gesture)
    {
       
        if (gesture != null && gesture.Selection == pig )
        {
            Destroy(pig);
            
        }    
    }
    
}
