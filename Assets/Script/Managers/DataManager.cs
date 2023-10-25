using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public interface ILoader<Key, Value>
{
    Dictionary<Key, Value> MakeDict();
}

public class DataManager
{
    public Dictionary<string, BurgurData> BurgursInfoDict { get; private set; } = new Dictionary<string, BurgurData>();
    public Dictionary<string, string> BurgursMaterialFileDict { get; private set; } = new Dictionary<string, string>();
    public Dictionary<string, string> SourceImageFileDict { get; private set; } = new Dictionary<string, string>();
    public Dictionary<string, string> SourceTextFileDict { get; private set; } = new Dictionary<string, string>();

    public void Init()
    {
        BurgursInfoDict = LoadJson<BurgurDataLoader, string, BurgurData>("BurgurData").MakeDict();
        BurgursMaterialFileDict = LoadJson<BurgurMaterialLoader, string, string>("ProjectFileData").MakeDict();
        SourceImageFileDict = LoadJson<SourceImageFileLoader, string, string>("ProjectFileData").MakeDict();
        SourceTextFileDict = LoadJson<SourceTextFileLoader, string, string>("ProjectFileData").MakeDict();
    }

    Loader LoadJson<Loader, key, value>(string path) where Loader : ILoader<key, value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"Data/{path}");
        var jsonLoader = JsonUtility.FromJson<Loader>(textAsset.text);
        return jsonLoader;
    }


    public Dictionary<string, AvatarStat> StatDict = new Dictionary<string, AvatarStat>();

    // ���������� �߰��� �ƹ�Ÿ�� ID�� �����ϴ� �ʵ�
    public string LastAddedAvatarId { get; private set; }
    public void AddAvatarStat(string id)
    {
        if (string.IsNullOrEmpty(id))
            return;

        if (!StatDict.ContainsKey(id))
        {
            AvatarStat avatarStat = new AvatarStat(id);
            StatDict.Add(id, avatarStat);

            // ���������� �߰��� �ƹ�Ÿ�� ID ������Ʈ
            LastAddedAvatarId = id;
        }
    }

    public AvatarStat GetAvatarStat(string id = null)
    {
        if (string.IsNullOrEmpty(id) && LastAddedAvatarId != null)
            id = LastAddedAvatarId; // ID�� �������� ���� ��� ���������� �߰��� ID ���

        if (!string.IsNullOrEmpty(id) && StatDict.ContainsKey(id))
        {
            return StatDict[id];
        }

        return null;
    }

    public void SaveToJson(AvatarStat avatarStat, string filename = "avatarStat.json")
    {
        string json = JsonUtility.ToJson(avatarStat);
        string path = Path.Combine(Application.persistentDataPath, filename);

        File.WriteAllText(path, json);

        Debug.Log("Saved to: " + path);
    }

    public AvatarStat LoadFromJson(string filename = "avatarStat.json")
    {
        string path = Path.Combine(Application.persistentDataPath, filename);

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            AvatarStat avatarStat = JsonUtility.FromJson<AvatarStat>(json);

            Debug.Log("Loaded from: " + path);
            return avatarStat;
        }
        else
        {
            Debug.LogError("File not found: " + path);
            return null;
        }
    }

    public void Clear()
    {
        BurgursInfoDict.Clear();
        BurgursMaterialFileDict.Clear();
        SourceImageFileDict.Clear();
        SourceTextFileDict.Clear();
    }
}
