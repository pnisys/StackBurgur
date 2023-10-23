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
        // 데이터가 제대로 로드되었는지 확인
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
    public string 햄버거빵위;
    public string 햄버거빵아래;
    public string 먹물빵위;
    public string 먹물빵아래;
    public string 샌드위치빵;
    public string 베이컨;
    public string 불고기;
    public string 치즈;
    public string 치킨;
    public string 버섯;
    public string 양파;
    public string 새우;
    public string 토마토;
    public string 양상추;
}

[Serializable]
public class BurgurMaterialLoader : ILoader<string, string>
{
    public List<BurgurMaterialData> burgurfileDict = new List<BurgurMaterialData>();

    public Dictionary<string, string> MakeDict()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        // 데이터가 제대로 로드되었는지 확인
        foreach (var file in burgurfileDict)
        {
            // 클래스의 모든 프로퍼티를 가져옴
            var fields = file.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                // 프로퍼티의 이름과 값 가져옴
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
    public string 바베큐소스;
    public string 칠리소스;
    public string 머스타드소스;
    public string 마요네즈소스;
}

[Serializable]
public class SourceImageFileData
{
    public string 바베큐소스;
    public string 칠리소스;
    public string 머스타드소스;
    public string 마요네즈소스;
}



[Serializable]
public class SourceTextFileLoader : ILoader<string, string>
{
    public List<SourceTextFileData> sourceCardTextNameDict = new List<SourceTextFileData>();

    public Dictionary<string, string> MakeDict()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();

        // 데이터가 제대로 로드되었는지 확인
        foreach (var file in sourceCardTextNameDict)
        {
            // 클래스의 모든 프로퍼티를 가져옴
            var fields = file.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                // 프로퍼티의 이름과 값 가져옴
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

        // 데이터가 제대로 로드되었는지 확인
        foreach (var file in sourceCardImageDict)
        {
            // 클래스의 모든 프로퍼티를 가져옴
            var fields = file.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                // 프로퍼티의 이름과 값 가져옴
                string key = field.Name;
                string value = (string)field.GetValue(file);
                dict.Add(key, value);
            }
        }

        return dict;
    }
}
#endregion
