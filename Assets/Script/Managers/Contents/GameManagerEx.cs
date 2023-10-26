using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Define;

[Serializable]
public class GameData
{
    public int Stage = 0;
    public int MaxStage = 4;
    public int Score = 0;

    public string CurrentBurgur;
    public string CurrentSource;
    public string[] BurgurMaterials;
}


public class GameManagerEx
{
    GameData _gameData = new GameData();

    public int CurrentStage { get { return _gameData.Stage; } set { _gameData.Stage = value; } }
    public int MaxStage { get { return _gameData.MaxStage; } }
    public int Score { get { return _gameData.Score; } set { _gameData.Score = value; } }

    public string CurrentBurgur { get { return _gameData.CurrentBurgur; } set { _gameData.CurrentBurgur = value; } }
    public string CurrentSource { get { return _gameData.CurrentSource; } set { _gameData.CurrentSource = value; } }
    public string[] Burgur_Material { get { return _gameData.BurgurMaterials; } set { _gameData.BurgurMaterials = value; } }

    public Queue<string> PlayerAnswerMaterials = new Queue<string>();
    public string PlayerAnswerSource;

    public GameObject CurrentCustomer;

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
            { "�ٺ�ť�ҽ�","ĥ���ҽ�","�������ҽ�","�ӽ�Ÿ��ҽ�"};

        int randomValue = UnityEngine.Random.Range(0, sources.Length);
        return sources[randomValue];
    }

    public void CheckAnswer()
    {
        Queue<string> queue = Managers.Game.PlayerAnswerMaterials;
        string[] array = queue.ToArray();
        int count = Managers.Game.Burgur_Material.Length;

        if (array.Length != 0)
        {
            for (int i = 0; i < count; i++)
            {
                if (array[i] != null)
                {
                    if (array[i] == Managers.Game.Burgur_Material[i]) { Debug.Log("����"); Score += 10; }
                    else
                    {
                        Debug.Log($"���� : {Managers.Game.Burgur_Material[i]} \n �׷��� ���� �� {array[i]}�̹Ƿ� ����");
                        Score -= 5;
                    }
                }
                else
                {
                    Debug.Log($"���� :  {Managers.Game.Burgur_Material[i]} \n �׷��� ���� ����");
                    Score -= 5;
                }
            }

            if (Managers.Game.CurrentSource.Equals(Managers.Game.PlayerAnswerSource))
            {
                Debug.Log("�ҽ��� ����");
                Score += 10;
            }
            else
            {
                if (Managers.Game.PlayerAnswerSource == string.Empty) { Debug.Log($"�ҽ� ���� : {Managers.Game.CurrentSource} �׷��� �ҽ� ��� ���� ����"); Score -= 5; }

                else { Debug.Log($"�ҽ� ���� : {Managers.Game.CurrentSource} �׷��� ���� ��� : {Managers.Game.PlayerAnswerSource} "); Score = 5; }
            }
        }
        else
        {
            Debug.Log("�ƹ��͵� �Է����� ����");
            Score -= 10 * count;
        }
    }

    public void ShowCorrect(Transform parent)
    {
        int floor = Managers.Game.CurrentStage + 3;

        for (int i = 0; i < floor; i++)
        {
            GameObject go = new GameObject { name = $"{i + 1}F" };
            go.transform.SetParent(parent);
            float yPos = -51f + i * 34f; // �� ���� ������ 4.3f�� ����
            go.transform.localPosition = new Vector3(1.8f, yPos, 0f);
            go.transform.localScale = new Vector3(0.44315f, 0.04390998f, 0.44315f);
            Image image = Util.GetOrAddComponet<Image>(go);
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(270.6f, 789.8613f);

            //Todo
            string materialName = Managers.Game.Burgur_Material[i];
            // ���� �߰�
            if (i == 0 && materialName.EndsWith("��") && materialName != "������ġ��")
            {
                materialName += "�Ʒ�";
            }
            else if (i == floor - 1 && materialName.EndsWith("��") && materialName != "������ġ��")
            {
                materialName += "��";
            }

            image.sprite = Managers.Data.BurgurMaterialSpriteDict[materialName];
        }
    }

    public void ShowMyAnswer(Transform parent)
    {
        int floor = PlayerAnswerMaterials.Count;
        string[] strings = PlayerAnswerMaterials.ToArray();
        for (int i = 0; i < floor; i++)
        {
            GameObject go = new GameObject { name = $"{i + 1}F" };
            go.transform.SetParent(parent);
            float yPos = -51f + i * 34f; // �� ���� ������ 4.3f�� ����
            go.transform.localPosition = new Vector3(1.8f, yPos, 0f);
            go.transform.localScale = new Vector3(0.44315f, 0.04390998f, 0.44315f);
            Image image = Util.GetOrAddComponet<Image>(go);
            RectTransform rect = go.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(270.6f, 789.8613f);

            //Todo
            string materialName = strings[i];
            // ���� �߰�
            if (i == 0 && materialName.EndsWith("��") && materialName != "������ġ��")
            {
                materialName += "�Ʒ�";
            }
            else if (i == floor - 1 && materialName.EndsWith("��") && materialName != "������ġ��")
            {
                materialName += "��";
            }

            image.sprite = Managers.Data.BurgurMaterialSpriteDict[materialName];
        }
    }

    public void Clear()
    {
        CurrentBurgur = string.Empty;
        CurrentSource = string.Empty;
        Burgur_Material = null;
        PlayerAnswerMaterials.Clear();
        PlayerAnswerSource = string.Empty;
    }
}
