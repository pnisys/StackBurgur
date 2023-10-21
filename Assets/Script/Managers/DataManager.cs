using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class JsonFile
{
    public string name;
    public int level;
    public string[] ingredients;
}

[Serializable]
public class FileData
{
    public List<JsonFile> burgurList = new List<JsonFile>();
}

public class DataManager
{
    public Dictionary<string, string[]> BurgursInfoDict { get; private set; } = new Dictionary<string, string[]>();
    
    FileData data;

    public void Init()
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>("Data/TestData");
        data = JsonUtility.FromJson<FileData>(textAsset.text);

        // 데이터가 제대로 로드되었는지 확인
        foreach (var burgur in data.burgurList)
        {
            BurgursInfoDict.Add(burgur.name, burgur.ingredients);
        }

        foreach (KeyValuePair<string, string[]> keyValuePair in BurgursInfoDict)
        {
            string ingredients = string.Join(", ", keyValuePair.Value); // 배열의 모든 원소를 쉼표로 구분한 문자열로 변환
            Debug.Log($"Burger Name: {keyValuePair.Key}, Ingredients: {ingredients}");
        }
    }


    public Dictionary<string, AvatarStat> StatDict = new Dictionary<string, AvatarStat>();

    // 마지막으로 추가된 아바타의 ID를 저장하는 필드
    public string LastAddedAvatarId { get; private set; }
    public void AddAvatarStat(string id)
    {
        if (string.IsNullOrEmpty(id))
            return;

        if (!StatDict.ContainsKey(id))
        {
            AvatarStat avatarStat = new AvatarStat(id);
            StatDict.Add(id, avatarStat);

            // 마지막으로 추가된 아바타의 ID 업데이트
            LastAddedAvatarId = id;
        }
    }

    public AvatarStat GetAvatarStat(string id = null)
    {
        if (string.IsNullOrEmpty(id) && LastAddedAvatarId != null)
            id = LastAddedAvatarId; // ID가 제공되지 않은 경우 마지막으로 추가된 ID 사용

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
}
