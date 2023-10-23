using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

#region Avatar
[Serializable]
public class AvatarStat
{
    public AvatarStat(string id) { Id = id; }

    public int skinNumber = 0;
    public int eyesNumber = 0;
    public int hairsNumber = 0;
    public int accsNumber = 0;

    public string Id;
}

[Serializable]
public class AvatarStatData
{
    public List<AvatarStat> stats = new List<AvatarStat>();
}

#endregion

#region Burgur

[Serializable]
public class BurgurData
{
    public string name;
    public int level;
    public string[] ingredients;
}

[Serializable]
public class BurgurDataLoader : ILoader<string, BurgurData>
{
    public List<BurgurData> burgurList = new List<BurgurData>();

    public Dictionary<string, BurgurData> MakeDict()
    {
        Dictionary<string, BurgurData> dict = new Dictionary<string, BurgurData>();
        // �����Ͱ� ����� �ε�Ǿ����� Ȯ��
        foreach (var burgur in burgurList)
            dict.Add(burgur.name, burgur);

        return dict;
    }
}
#endregion

#region BurgurMaterial

[Serializable]
public class BurgurMaterialData
{
    public string �ܹ��Ż���;
    public string �ܹ��Ż��Ʒ�;
    public string �Թ�����;
    public string �Թ����Ʒ�;
    public string ������ġ��;
    public string ������;
    public string �Ұ��;
    public string ġ��;
    public string ġŲ;
    public string ����;
    public string ����;
    public string ����;
    public string �丶��;
    public string �����;
}

[Serializable]
public class BurgurMaterialLoader : ILoader<string, string>
{
    public List<BurgurMaterialData> burgurfileDict = new List<BurgurMaterialData>();

    public Dictionary<string, string> MakeDict()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        // �����Ͱ� ����� �ε�Ǿ����� Ȯ��
        foreach (var file in burgurfileDict)
        {
            // Ŭ������ ��� ������Ƽ�� ������
            var fields = file.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                // ������Ƽ�� �̸��� �� ������
                string key = field.Name;
                string value = (string)field.GetValue(file);
                dict.Add(key, value);
            }
        }

        return dict;
    }
}
#endregion

#region SourceMaterial

[Serializable]
public class SourceTextFileData
{
    public string �ٺ�ť�ҽ�;
    public string ĥ���ҽ�;
    public string �ӽ�Ÿ��ҽ�;
    public string �������ҽ�;
}

[Serializable]
public class SourceImageFileData
{
    public string �ٺ�ť�ҽ�;
    public string ĥ���ҽ�;
    public string �ӽ�Ÿ��ҽ�;
    public string �������ҽ�;
}



[Serializable]
public class SourceTextFileLoader : ILoader<string, string>
{
    public List<SourceTextFileData> sourceCardTextNameDict = new List<SourceTextFileData>();

    public Dictionary<string, string> MakeDict()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        // �����Ͱ� ����� �ε�Ǿ����� Ȯ��
        foreach (var file in sourceCardTextNameDict)
        {
            // Ŭ������ ��� ������Ƽ�� ������
            var fields = file.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                // ������Ƽ�� �̸��� �� ������
                string key = field.Name;
                string value = (string)field.GetValue(file);
                dict.Add(key, value);
            }
        }

        return dict;
    }
}

[Serializable]
public class SourceImageFileLoader : ILoader<string, string>
{
    public List<SourceImageFileData> sourceCardImageDict = new List<SourceImageFileData>();

    public Dictionary<string, string> MakeDict()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        // �����Ͱ� ����� �ε�Ǿ����� Ȯ��
        foreach (var file in sourceCardImageDict)
        {
            // Ŭ������ ��� ������Ƽ�� ������
            var fields = file.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                // ������Ƽ�� �̸��� �� ������
                string key = field.Name;
                string value = (string)field.GetValue(file);
                dict.Add(key, value);
            }
        }

        return dict;
    }
}
#endregion
