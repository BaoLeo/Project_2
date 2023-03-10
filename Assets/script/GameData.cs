using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameData 
{
    public int levelPassed = 0;
    public int cLevel = 0;
    public static string lastwindow = "";
    public static int totalLevel = 10;
    public static GameData instance;
    public MainScript main;
    public bool islock = false;
    void Start()
    {

    }
    void Update()
    {

    }
   /* public List<int>lvStar = new List<int>(260);*/
    public static GameData getInstance()
    {
        if (instance == null)
        {
            instance = new GameData();
        }
        return instance;
    }
    public void resetData()
    {
        islock = false;
    }

    public void lockGame(bool _lock, bool stopTime = true)
    {
        islock = _lock;
        if (stopTime)
        {
            Time.timeScale = islock ? 0 : 1;
        }
    }
    
}
