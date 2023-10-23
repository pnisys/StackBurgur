using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditateManager
{
    public void Notify()
    {
        Managers.Game.SetBurgurAndSource();
        UI_Card_Burgur burgurCard = Managers.UI.ShowSceneUI<UI_Card_Burgur>();

        burgurCard.transform.position = new Vector3(0.9900001f, -0.572f, 0.013f);
        burgurCard.transform.Rotate(new Vector3(0f, 180f, 0f));

        UI_Card_Source sourceCard = Managers.UI.ShowSceneUI<UI_Card_Source>();

        sourceCard.transform.position = new Vector3(0.397f, -0.572f, 0.013f);
        sourceCard.transform.Rotate(new Vector3(0f, 180f, 0f));

        //¼Ò½º

        UI_TimeLimit timeLimit = Managers.UI.ShowSceneUI<UI_TimeLimit>();
        timeLimit.transform.Rotate(new Vector3(0f, 180f, 0f));
        timeLimit.transform.position = new Vector3(-0.285f, -0.54f, 0);

        UI_Select select = Managers.UI.ShowSceneUI<UI_Select>();
    }
}
