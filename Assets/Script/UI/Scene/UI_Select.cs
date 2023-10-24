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

public class UI_Select : UI_Scene
{
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
        Init();
    }

    Sprite[] sprites = null;
    //�̹��� �̸� ����� ������ �迭�̳� ����Ʈ

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Bind<UnityEngine.UI.Button>(typeof(Buttons));
        GameObject MaterialPanel = GetGameObject((int)GameObjects.MeterialPanel);
        UnityEngine.UI.Button DecisionButton = GetButton((int)Buttons.DecisionButton);

        DecisionButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Debug.Log("����");
        }
        );

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

        // �迭�� ����Ʈ�� ��ȯ
        List<string> arrayList = new List<string>(array);

        // �ʿ��� ��Ҹ� ����
        arrayList.Remove("HamburgurBreadUp");
        arrayList.Remove("BlackburgurBreadUp");

        // ����Ʈ�� �ٽ� �迭�� ��ȯ
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
                    item.AddUIEvnet((PointerEventData) =>
                    {
                        Managers.Game.CurrentBurgurMaterials.Enqueue(name);
                        Queue<string> strings = Managers.Game.CurrentBurgurMaterials;
                        foreach (string str in strings)
                        {
                            Debug.Log(str);
                        }
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
                    Managers.Game.CurrentSource = null;
                    Managers.Game.CurrentSource = name;
                    Debug.Log($"CurrentSource : {Managers.Game.CurrentSource}");
                });
            }
            else
            {
                Debug.LogError("�ش� textChild�� �������� �ʽ��ϴ�");
            }
        }
    }
}
