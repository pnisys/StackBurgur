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
    }

    public override void Clear()
    {
    }
}
