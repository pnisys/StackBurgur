using System;
using System.Collections;
using System.Collections.Generic;
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
    void Start()
    {
        Init();
    }

    Sprite[] sprites = null;
    //�̹��� �̸� ����� ������ �迭�̳� ����Ʈ
    string[] burgurMaterialNames = new string[]
    {
    "HamburgurBreadDown", "BlackburgurBreadDown", "SandwichBread", "Shrimp",
    "Lettuce","Onion","Bacon","Chicken",
    "Tomato","Cheeze","Bulmeat","Mushroom"
    };

    string[] sourceNames = new string[]
    {
    "�ٺ�ť�ҽ�", "ĥ���ҽ�", "�������ҽ�", "�ӽ�Ÿ��ҽ�",
    };

    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        GameObject MaterialPanel = GetGameObject((int)GameObjects.MeterialPanel);

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

        for (int i = 0; i < 12; i++)
        {
            GameObject item = Managers.Resource.Instantite("UI/Scene/UI_Select_Item");
            item.transform.SetParent(MaterialPanel.transform);

            Image imageComponent = Util.FindChild<Image>(item);
            foreach (Sprite sprite in sprites)
            {
                if (sprite.name == burgurMaterialNames[i])
                {
                    string name = sprite.name;
                    imageComponent.sprite = sprite;
                    //item.AddUIEvnet((PointerEventData) => );
                    break;
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            GameObject item = Managers.Resource.Instantite("UI/Scene/UI_Select_Item");
            item.transform.SetParent(SourcePanel.transform);
            if (Util.FindChild<TextMeshProUGUI>(item, out var textChild))
            {
                textChild.text = sourceNames[i];
            }
            else
            {
                Debug.LogError("�ش� textChild�� �������� �ʽ��ϴ�");
            }
        }
    }
}
