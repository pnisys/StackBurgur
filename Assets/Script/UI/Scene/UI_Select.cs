using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public interface I_UI_Select_Strategy
{
    void Decision(UI_Select uI_Select);
}

public class Tutorial_UI_Select_Strategy : I_UI_Select_Strategy
{
    public void Decision(UI_Select uI_Select)
    {
        Managers.EventBus.Trigger("GameSceneLoad");
    }
}

public class Game_UI_Select_Strategy : I_UI_Select_Strategy
{
    public void Decision(UI_Select uI_Select)
    {
        Managers.UI.CloseSceneUI("UI_Card_Burgur");
        Managers.UI.CloseSceneUI("UI_Card_Source");
        Managers.UI.CloseSceneUI("UI_TimeLimit");

        Managers.UI.ShowPopupUI<UI_Check>();
        Managers.EventBus.Trigger("ChangeCustomerStateToJudge");
        Managers.Object.Despawn(uI_Select.gameObject);
    }
}

public class UI_Select : UI_Scene
{
    I_UI_Select_Strategy strategy;

    enum GameObjects
    {
        MeterialPanel,
        SourcePanel
    }

    enum Buttons
    {
        DecisionButton
    }
    void Start()
    {
        if (Managers.Scene.CurrentSceneType == Define.SceneType.Tutorial)
            strategy = new Tutorial_UI_Select_Strategy();
        else if (Managers.Scene.CurrentSceneType == Define.SceneType.Game)
            strategy = new Game_UI_Select_Strategy();

        Init();
    }

    Sprite[] sprites = null;
    //이미지 이름 목록을 저장할 배열이나 리스트

    public override void Init()
    {
        Managers.EventBus.Subscribe("GameSceneLoad", Managers.EventBus.GameSceneLoad);
        Managers.EventBus.Subscribe("ChangeCustomerStateToJudge", Managers.EventBus.ChangeCustomerStateToJudge);

        Bind<GameObject>(typeof(GameObjects));
        Bind<UnityEngine.UI.Button>(typeof(Buttons));
        GameObject MaterialPanel = GetGameObject((int)GameObjects.MeterialPanel);
        UnityEngine.UI.Button DecisionButton = GetButton((int)Buttons.DecisionButton);

        DecisionButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Game.CheckAnswer();
            strategy.Decision(this);
        }
        );

        #region 스프라이트 설정
        foreach (Transform item in MaterialPanel.transform)
        {
            Managers.Resource.Destory(item.gameObject);
        }

        GameObject SourcePanel = GetGameObject((int)GameObjects.SourcePanel);

        foreach (Transform item in SourcePanel.transform)
        {
            Managers.Resource.Destory(item.gameObject);
        }

        sprites = Managers.Resource.LoadAll<Sprite>("Art/Image/BurgurMaterialsSprite");

        string[] array = (string[])Managers.Data.BurgursMaterialFileDict.ConvertDictToArray(Define.ConvertDict.Value);
        Dictionary<string, string> dict = Managers.Data.BurgursMaterialFileDict.ReverseDict();

        // 배열을 리스트로 변환
        List<string> arrayList = new List<string>(array);

        // 필요한 요소를 제거
        arrayList.Remove("HamburgurBreadUp");
        arrayList.Remove("BlackburgurBreadUp");

        // 리스트를 다시 배열로 변환
        array = arrayList.ToArray();

        for (int i = 0; i < array.Length; i++)
        {
            GameObject item = Managers.Resource.Instantite("UI/Scene/UI_Select_Item");
            item.transform.SetParent(MaterialPanel.transform);

            Image imageComponent = Util.FindChild<Image>(item);
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == array[i])
                {
                    string name = sprite.name;
                    imageComponent.sprite = sprite;

                    string dictName = dict[name];
                    if (dictName.EndsWith("위"))
                        dictName = dictName.Substring(0, dictName.Length - 1);
                    else if (dictName.EndsWith("아래"))
                        dictName = dictName.Substring(0, dictName.Length - 2);

                    item.AddUIEvnet((PointerEventData) =>
                    {
                        Managers.Game.PlayerAnswerMaterials.Enqueue(dictName);
                        Queue<string> strings = Managers.Game.PlayerAnswerMaterials;
                    });
                    break;
                }
            }
        }

        string[] sourceArray = (string[])Managers.Data.SourceImageFileDict.ConvertDictToArray(Define.ConvertDict.Key);

        for (int i = 0; i < sourceArray.Length; i++)
        {
            GameObject item = Managers.Resource.Instantite("UI/Scene/UI_Select_Item");
            item.transform.SetParent(SourcePanel.transform);
            if (Util.FindChild<TextMeshProUGUI>(item, out var textChild))
            {
                string name = sourceArray[i];
                textChild.text = name;
                item.AddUIEvnet((PointerEventData) =>
                {
                    Managers.Game.PlayerAnswerSource = null;
                    Managers.Game.PlayerAnswerSource = name;
                    Debug.Log($"CurrentSource : {Managers.Game.PlayerAnswerSource}");
                });
            }
            else
            {
                Debug.LogError("해당 textChild가 존재하지 않습니다");
            }
        }
        #endregion
    }
}
