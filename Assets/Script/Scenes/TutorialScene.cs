using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScene : BaseScene
{
    // Start is called before the first frame update
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

        for (int i = 0; i < 2; i++)
        {
            Managers.Resource.Instantite("Customer");
        }
    }

    public override void Clear()
    {
    }
}
