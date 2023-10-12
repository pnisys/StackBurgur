using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        return Resources.Load<T>(path);
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        return Resources.LoadAll<T>(path);
    }

    public GameObject Instantite(string path, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.LogError($"Failed Load to Prefab : {path}");
            return null;
        }

        Vector3 finalPosition = prefab.transform.position + position;
        Quaternion finalRotation = rotation == Quaternion.identity ? prefab.transform.rotation : rotation;

        GameObject go = Object.Instantiate(prefab, finalPosition, finalRotation, parent);
        int index = go.name.IndexOf("(Clone)");
        if (index > 0)
            go.name = go.name.Substring(0, index);

        return go;
    }

    public void Destory(GameObject go)
    {
        if (go == null)
            return;

        Object.Destroy(go);
    }
}
