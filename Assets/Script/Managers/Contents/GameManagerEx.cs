using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

[Serializable]
public class GameData
{
    public int Stage = 0;
    public string Burgur;

    public bool BGMOn = true;
    public bool EffectSoundOn = true;

    public string[] BurgurMaterials;
}


public class GameManagerEx
{
    GameData _gameData = new GameData();

    public int CurrentStage { get { return _gameData.Stage; } set { _gameData.Stage = value; } }
    public string CurrentBurgur { get { return _gameData.Burgur; } set { _gameData.Burgur = value; } }
    public string[] CurrentBurgur_Material { get { return _gameData.BurgurMaterials; } set { _gameData.BurgurMaterials = value; } }

    public void SetBurgur()
    {
        CurrentBurgur = GetRandomBurgurName();
        BurgurData data = Managers.Data.BurgursInfoDict[CurrentBurgur];
        CurrentBurgur_Material = data.ingredients;
    }

    public string GetRandomBurgurName()
    {
        string burgur;
        Array values;

        switch (CurrentStage)
        {
            case 0:
                burgur = Define.TutorialBurgurNames.데리버거.ToString();
                break;
            case 1:
                values = Enum.GetValues(typeof(Define.Level1BurgurNames));
                burgur = ((Define.Level1BurgurNames)values.GetValue(UnityEngine.Random.Range(0, values.Length))).ToString();
                break;
            case 2:
                values = Enum.GetValues(typeof(Define.Level2BurgurNames));
                burgur = ((Define.Level2BurgurNames)values.GetValue(UnityEngine.Random.Range(0, values.Length))).ToString();
                break;
            case 3:
                values = Enum.GetValues(typeof(Define.Level3BurgurNames));
                burgur = ((Define.Level3BurgurNames)values.GetValue(UnityEngine.Random.Range(0, values.Length))).ToString();
                break;
            case 4:
                values = Enum.GetValues(typeof(Define.Level4BurgurNames));
                burgur = ((Define.Level4BurgurNames)values.GetValue(UnityEngine.Random.Range(0, values.Length))).ToString();
                break;
            default:
                burgur = "Unknown";
                break;
        }
        return burgur;
    }
}
