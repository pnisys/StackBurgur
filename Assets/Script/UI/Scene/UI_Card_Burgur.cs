using Oculus.Voice.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Card_Burgur : UI_Scene
{
    Define.Levels currentLevel;

    private string burgurName;
    public string BurgurName
    {
        get
        {
            return burgurName;
        }
        set
        {
            burgurName = value;
            currentLevel = (Define.Levels)Managers.Card.BurgurLevelDict[burgurName];
        }
    }

    enum Texts
    {
        Text_BurgurName
    }

    enum BurgurImages
    {
        Image_Burgur,
    }

    enum GameObjects
    {
        BurgurNameParent
    }


    GameObject burgurNameParent;
    Image image_Burgur;

    private void Start()
    {
        InitBurgurCard();
    }

    public void InitBurgurCard()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(BurgurImages));
        Bind<GameObject>(typeof(GameObjects));

        TextMeshProUGUI burgurNameText = GetText((int)Texts.Text_BurgurName);
        burgurNameParent = GetGameObject((int)GameObjects.BurgurNameParent);
        image_Burgur = GetImage((int)BurgurImages.Image_Burgur);
        burgurNameText.text = BurgurName;
        image_Burgur.sprite = Managers.Card.BurgurImageSpriteDict[BurgurName];
        BurgurCardSetting(currentLevel);
    }

    private void BurgurCardSetting(Define.Levels currentLevel)
    {
        int value = currentLevel == Define.Levels.Tutorial ? 3 :
               currentLevel == Define.Levels.Level1 ? 4 :
               currentLevel == Define.Levels.Level2 ? 5 :
               currentLevel == Define.Levels.Level3 ? 6 :
               currentLevel == Define.Levels.Level4 ? 7 : -1;

        for (int i = 0; i < value; i++)
        {
            GameObject go = new GameObject { name = $"{i + 1}F" };
            go.transform.SetParent(burgurNameParent.transform);
            go.transform.Rotate(0f, 180f, 0f);
            float yPos = -36.9f + i * 4.3f; // 층 간의 간격은 4.3f로 가정
            go.transform.localPosition = new Vector3(6.1f, yPos, 0f);
            go.transform.localScale = new Vector3(0.44315f, 0.04390998f, 0.44315f);
            Image image = Util.GetOrAddComponet<Image>(go);
            string materialName = Managers.Card.BurgursInfoDict[BurgurName][i];

            // 조건 추가
            if (i == 0 && materialName.EndsWith("빵"))
            {
                materialName += "아래";
            }
            else if (i == value - 1 && materialName.EndsWith("빵"))
            {
                materialName += "위";
            }

            Sprite result;
            if (Managers.Card.BurgurMaterialSpriteDict.TryGetValue(materialName, out result))
            {
                image.sprite = result;
            }
            else
            {
                Debug.LogError($"'{materialName}'에 대한 스프라이트를 찾을 수 없습니다.");
            }
        }
    }
}
