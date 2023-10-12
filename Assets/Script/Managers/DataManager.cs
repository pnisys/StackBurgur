using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager
{
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
}
