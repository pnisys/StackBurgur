using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;

        Transform playerSpawnPosition = GameObject.Find("PlayerSpawnPosition").transform;
        transform.position = playerSpawnPosition.position;
        transform.rotation = playerSpawnPosition.rotation;
    }
}
