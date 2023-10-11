using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyScene : BaseScene
{
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Lobby;
        Managers.UI.ShowSceneUI<UI_Button>(null, new Vector3(0, 0, 2));

        Managers.Sound.Play("Bgm_Lobby", Define.Sound.Bgm);
    }
    public override void Clear()
    {
        Debug.Log("LobbyScene Clear!");
    }
}
