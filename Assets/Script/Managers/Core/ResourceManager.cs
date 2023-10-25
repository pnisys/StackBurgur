using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public Dictionary<string, GameObject> Resources { get; private set; } = new Dictionary<string, GameObject>();

    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }
        return UnityEngine.Resources.Load<T>(path);
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        return UnityEngine.Resources.LoadAll<T>(path);
    }

    public GameObject Instantite(string path, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
    {
        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if (original == null)
        {
            Debug.LogError($"Failed Load to Prefab : {path}");
            return null;
        }

        //풀링된 녀석이 있을까?
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        Vector3 finalPosition = original.transform.position + position;
        Quaternion finalRotation = rotation == Quaternion.identity ? original.transform.rotation : rotation;

        GameObject go = Object.Instantiate(original, finalPosition, finalRotation, parent);
        go.name = original.name;

        Resources.TryAdd(go.name, go);

        return go;
    }

    public GameObject Instantite(GameObject original, Vector3 position = default, Quaternion rotation = default, Transform parent = null)
    {
        if (original == null)
        {
            Debug.LogError($"Failed Load to Prefab");
            return null;
        }

        //풀링된 녀석이 있을까?
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;

        Vector3 finalPosition = original.transform.position + position;
        Quaternion finalRotation = rotation == Quaternion.identity ? original.transform.rotation : rotation;

        GameObject go = Object.Instantiate(original, finalPosition, finalRotation, parent);
        go.name = original.name;

        Resources.TryAdd(go.name, go);

        return go;
    }

    public void Destory(GameObject go)
    {
        if (go == null)
            return;

        Poolable poolable = go.GetComponent<Poolable>();
        if (poolable != null)
        {
            Managers.Pool.Push(poolable);
            return;
        }

        Resources.Remove(go.name);

        Object.Destroy(go);
    }

    public void Clear()
    {
        Resources.Clear();
    }
}
