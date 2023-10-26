using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectManager
{
    GameObject _player;
    HashSet<GameObject> _customer = new HashSet<GameObject>();

    private GameObject[] _customers;
    public GameObject[] Customers
    {
        get
        {
            if (_customers == null)
            {
                GameObject[] customers = Managers.Resource.LoadAll<GameObject>("Prefabs/Customers");
                _customers = null;
                return _customers = customers;
            }
            else
                return _customers;
        }
        private set
        {
            _customers = value;
        }
    }

    public Dictionary<string, GameObject> ObjectDict { get; private set; } = new Dictionary<string, GameObject>();


    public Action<int> OnSpawnEvent;

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

    public void Clear()
    {
        _player = null;
        _customer.Clear();
        Customers = null;
    }
}
