using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[Serializable]
public class GameData
{
    public int Chapter;
    public int Stage;

    public int Coin;
    public int Dia;


    public int ShortRangeWeaponID;
    public int MiddleRangeWeaponID;
    public int LongRangeWeaponID;

    public int SelectedChapter;
    public int SelectedStage;

    public bool BGMOn = true;
    public bool EffectSoundOn = true;

    public int LastStoryID = -1;

    public Dictionary<string, string[]> currentBurgurDic = new Dictionary<string, string[]>();
    public Stack<string> burgurs = new Stack<string>();

   
}


public class GameManagerEx
{
    GameData _gameData = new GameData();

    public Dictionary<string, string[]> CurrentBurgurDic
    {
        get { return _gameData.currentBurgurDic; }
        set { _gameData.currentBurgurDic = value; }
    }

    public void SetBurgur(string item)
    {
        _gameData.burgurs.Push(item);
        foreach (var burgur in _gameData.burgurs)
        {
            Debug.Log(burgur);
        }
    }
}
