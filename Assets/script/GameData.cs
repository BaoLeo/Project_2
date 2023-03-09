using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameData : MonoBehaviour
{
    public int levelPassed = 0;
    public int cLevel = 0;
    public static string lastwindow = "";
    public static int totalLevel = 10;
    public static GameData instance;
    public static GameData getInstance()
    {
        if (instance == null)
        {
            instance = new GameData();
        }
        return instance;
    }
    name = 
    public void lockGame(bool _lock)
    {
        islock = _lock;
       
    }
    public bool isLock
    {
        get
        {
            return isLock;
        }
    }
}
