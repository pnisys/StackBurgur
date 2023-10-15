using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGameManagerEx
{
    public Transform CounterPosition { get; private set; }
    GameObject _player;
    HashSet<GameObject> _customer = new HashSet<GameObject>();
    public GameObject[] Customers { get; private set; }

    public Action<int> OnSpawnEvent;

    public void Init()
    {
        CounterPosition = GameObject.Find("CounterPosition").transform;
        Customers = Managers.Resource.LoadAll<GameObject>("Prefabs/Customers");
        Debug.Log(Customers.Length);
    }

    public GameObject GetPlayer() { return _player; }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantite(path, default, default, parent);

        switch (type)
        {
            case Define.WorldObject.Customer:
                _customer.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }

        return go;
    }

    public GameObject Spawn(Define.WorldObject type, GameObject gameObject, Vector3 position = default, Quaternion rotiation = default, Transform parent = null)
    {
        GameObject go = Managers.Resource.Instantite(gameObject, default, default, parent);

        switch (type)
        {
            case Define.WorldObject.Customer:
                _customer.Add(go);
                if (OnSpawnEvent != null)
                    OnSpawnEvent.Invoke(1);
                break;
            case Define.WorldObject.Player:
                _player = go;
                break;
        }

        return go;
    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType;
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch (type)
        {
            case Define.WorldObject.Customer:
                {
                    if (_customer.Contains(go))
                    {
                        _customer.Remove(go);
                        if (OnSpawnEvent != null)
                            OnSpawnEvent.Invoke(-1);
                    }
                }
                break;
            case Define.WorldObject.Player:
                {
                    if (_player == go)
                        _player = null;
                }
                break;
        }

        Managers.Resource.Destory(go);
    }
}
