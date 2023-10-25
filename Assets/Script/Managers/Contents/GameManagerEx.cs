using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Define;

[Serializable]
public class GameData
{
    public int Stage = 0;
    public int MaxBurgurLevel = 4;

    public string CurrentBurgur;
    public string CurrentSource;
    public string[] BurgurMaterials;
}


public class GameManagerEx
{
    GameData _gameData = new GameData();

    public int CurrentStage { get { return _gameData.Stage; } set { _gameData.Stage = value; } }
    public int MaxStage { get { return _gameData.MaxBurgurLevel; } }

    public string CurrentBurgur { get { return _gameData.CurrentBurgur; } set { _gameData.CurrentBurgur = value; } }
    public string CurrentSource { get { return _gameData.CurrentSource; } set { _gameData.CurrentSource = value; } }
    public string[] Burgur_Material { get { return _gameData.BurgurMaterials; } set { _gameData.BurgurMaterials = value; } }

    public Queue<string> PlayerAnswerMaterials = new Queue<string>();
    public string PlayerAnswerSource;


    public Dictionary<string, Sprite> BurgurImageSpriteDict = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> BurgurMaterialSpriteDict = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> SourceImageDict { get; private set; } = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> SourceTextNameDict { get; private set; } = new Dictionary<string, Sprite>();

    public void Init()
    {
        InitBurgurSprites();
        InitBurgurMaterialSprites();

        InitSourceSprites();
    }

    private void InitBurgurSprites()
    {
        for (int i = 0; i <= _gameData.MaxBurgurLevel; i++)
        {
            foreach (var item in GetLevelBurgur(i))
            {
                string path = i == 0 ? Define.burgurSpriteFolderPath_Tutorial : i == 1 ? Define.burgurSpriteFolderPath_Level1 :
                    i == 2 ? Define.burgurSpriteFolderPath_Level2 : i == 3 ? Define.burgurSpriteFolderPath_Level3 :
                    i == 4 ? Define.burgurSpriteFolderPath_Level4 : string.Empty;

                BurgurImageSpriteDict.Add(item, Managers.Resource.Load<Sprite>($"{path}{item}"));
            }
        }
    }

    private void InitBurgurMaterialSprites()
    {
        string path = Define.materialSpriteFolderPath;

        foreach (var item in Managers.Data.BurgursMaterialFileDict)
        {
            BurgurMaterialSpriteDict.Add(item.Key, Managers.Resource.Load<Sprite>($"{path}{item.Value}"));
        }
    }

    private void InitSourceSprites()
    {
        string path = Define.sourceSpriteFolderPath;

        foreach (var item in Managers.Data.SourceImageFileDict)
        {
            SourceImageDict.Add(item.Key, Managers.Resource.Load<Sprite>($"{path}{item.Value}"));
        }

        foreach (var item in Managers.Data.SourceTextFileDict)
        {
            SourceTextNameDict.Add(item.Key, Managers.Resource.Load<Sprite>($"{path}{item.Value}"));
        }
    }

    public string[] GetLevelBurgur(int level)
    {
        List<string> burgursList = new List<string>();
        Dictionary<string, BurgurData> dict = Managers.Data.BurgursInfoDict;

        foreach (var item in dict)
        {
            if (item.Value.level == level)
            {
                burgursList.Add(item.Value.name);
            }
        }

        string[] burgurs = null;
        burgurs = burgursList.ToArray<string>();

        return burgurs;
    }


    public void SetBurgurAndSource()
    {
        CurrentBurgur = null;
        CurrentBurgur = GetRandomBurgurName();

        Burgur_Material = null;
        BurgurData data = Managers.Data.BurgursInfoDict[CurrentBurgur];
        Burgur_Material = data.ingredients;

        CurrentSource = null;
        CurrentSource = GetRandomSourceName();
    }

    public string GetRandomBurgurName()
    {
        string burgur;
        string[] burgurs = GetLevelBurgur(CurrentStage);
        int randomvalue = UnityEngine.Random.Range(0, burgurs.Length);
        burgur = burgurs[randomvalue];

        return burgur;
    }

    public string GetRandomSourceName()
    {
        string[] sources = new string[4]
            { "바베큐소스","칠리소스","마요네즈소스","머스타드소스"};

        int randomValue = UnityEngine.Random.Range(0, sources.Length);
        return sources[randomValue];
    }

    public void Clear()
    {
        BurgurImageSpriteDict.Clear();
        BurgurMaterialSpriteDict.Clear();
        SourceImageDict.Clear();
        SourceTextNameDict.Clear();
    }
}
