using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScene : BaseScene
{
    void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Tutorial;
        Managers.Sound.Play("Bgm_Game", Define.Sound.Bgm);

        switch (ModeType)
        {
            case Define.Mode.Test:
                break;
            case Define.Mode.Game:
                break;
        }

        GameObject player = Managers.Tutorial.Spawn(Define.WorldObject.Player, "Player");
        GameObject playerSpawnPosition = GameObject.Find("PlayerSpawnPosition");
        player.transform.position = playerSpawnPosition.transform.position;
        player.transform.rotation = playerSpawnPosition.transform.rotation;


        int randomCustomerValue = UnityEngine.Random.Range(0, Managers.Tutorial.Customers.Length);
        GameObject customer = Managers.Tutorial.Spawn(Define.WorldObject.Customer, Managers.Tutorial.Customers[randomCustomerValue]);
        customer.transform.position = GameObject.Find("CustomerSpawnPosition").transform.position;
    }

    public override void Clear()
    {
    }
}
