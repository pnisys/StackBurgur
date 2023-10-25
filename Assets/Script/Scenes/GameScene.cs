using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public Transform CounterPosition { get; private set; }

    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.SceneType.Game;
        Managers.UI.ShowSceneUI<UI_Button>(null, new Vector3(0, 0, 2));
        Managers.Sound.Play("Bgm_Game", Define.Sound.Bgm);

        Managers.Object.Init();

        GameObject player = Managers.Object.Spawn(Define.WorldObject.Player, "Player");

        switch (ModeType)
        {
            case Define.Mode.Test:
                GameObject inputOVR = GameObject.Find("InputOVR");
                inputOVR.SetActive(false);
                break;
            case Define.Mode.Game:
                break;
        }

        Transform playerSpawnPosition = GameObject.Find("PlayerSpawnPosition").transform;
        player.transform.position = playerSpawnPosition.position;
        player.transform.rotation = playerSpawnPosition.rotation;

        int randomCustomerValue = UnityEngine.Random.Range(0, Managers.Object.Customers.Length);
        GameObject customer = Managers.Object.Spawn(Define.WorldObject.Customer, Managers.Object.Customers[randomCustomerValue]);
        customer.transform.position = GameObject.Find("CustomerSpawnPosition").transform.position;
    }

    public override void Clear()
    {
    }
}
