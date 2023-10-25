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

        SceneType = Define.SceneType.Tutorial;
        Managers.Sound.Play("Bgm_Game", Define.Sound.Bgm);

        Managers.Object.Spawn(Define.WorldObject.Player, "Player");

        switch (ModeType)
        {
            case Define.Mode.Test:
                GameObject inputOVR = GameObject.Find("InputOVR");
                inputOVR.SetActive(false);
                break;
            case Define.Mode.Game:
                break;
        }

        int randomCustomerValue = UnityEngine.Random.Range(0, Managers.Object.Customers.Length);
        Managers.Object.Spawn(Define.WorldObject.Customer, Managers.Object.Customers[randomCustomerValue]);
    }

    public override void Clear()
    {
    }
}
