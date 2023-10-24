using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public static class Util
{
    public static T GetOrAddComponet<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();

        if (component == null)
            component = go.AddComponent<T>();

        return component;
    }

    public static bool FindChild<T>(GameObject go, out T child, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        child = FindChild<T>(go, name, recursive);
        return child != null;
    }

    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform! == null)
            return null;

        return transform.gameObject;
    }
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null)
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }

        return null;
    }

    public static object ConvertDictToArray<U, V>(Dictionary<U, V> keyValuePairs, Define.ConvertDict version)
    {
        if (keyValuePairs == null)
        {
            Debug.LogError("매개변수 딕셔너리가 null입니다");
            return null;
        }
        if (version == Define.ConvertDict.Key)
        {
            return keyValuePairs.Keys.ToArray();
        }
        else
        {
            return keyValuePairs.Values.ToArray();
        }
    }

    public static Dictionary<V, U> ReverseDict<U, V>(Dictionary<U, V> dict)
    {
        Dictionary<V, U> reversedDict = new Dictionary<V, U>();

        foreach (KeyValuePair<U, V> entry in dict)
        {
            if (!reversedDict.ContainsKey(entry.Value)) // 값이 유일한지 확인
            {
                reversedDict.Add(entry.Value, entry.Key);
            }
            else
            {
                throw new ArgumentException("Values in the original dictionary must be unique.");
            }
        }

        return reversedDict;
    }
}
