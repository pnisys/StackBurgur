using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeditateManager
{
    public void Notify()
    {
        //1. Stage를 가져와서, Stage에 맞는 버거들 중 랜덤으로 하나를 얻는다
        //2. 그 버거가 셋팅되고, 정보를 카드에 넘긴다.
        Managers.Game.SetBurgur();
        UI_Card_Burgur burgurCard = Managers.UI.ShowSceneUI<UI_Card_Burgur>();

        burgurCard.transform.position = new Vector3(0.9900001f, -0.572f, 0.013f);
        burgurCard.transform.Rotate(new Vector3(0f, 180f, 0f));

        UI_Card_Source sourceCard = Managers.UI.ShowSceneUI<UI_Card_Source>();

        sourceCard.transform.position = new Vector3(0.397f, -0.572f, 0.013f);
        sourceCard.transform.Rotate(new Vector3(0f, 180f, 0f));
        Array sourceNames = Enum.GetValues(typeof(Define.SourceNames));
        int randomValue = UnityEngine.Random.Range(0, sourceNames.Length);
        sourceCard.CurrentSource = (Define.SourceNames)sourceNames.GetValue(randomValue);

        UI_TimeLimit timeLimit = Managers.UI.ShowSceneUI<UI_TimeLimit>();
        timeLimit.transform.Rotate(new Vector3(0f, 180f, 0f));
        timeLimit.transform.position = new Vector3(-0.285f, -0.54f, 0);

        UI_Select select = Managers.UI.ShowSceneUI<UI_Select>();
    }
}
