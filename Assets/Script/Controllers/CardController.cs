using Oculus.Voice.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardController : UI_Scene
{
    Define.Cards currentCard = Define.Cards.Source;
    Define.Levels currentLevel = Define.Levels.None;

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
            currentLevel = (Define.Levels)Managers.Burgur.BurgurLevelDict[burgurName];
        }
    }

    Define.SourceNames currentSource = Define.SourceNames.마요네즈소스;
    public Define.SourceNames CurrentSource
    {
        get
        {
            return currentSource;
        }
        set
        {
            currentSource = value;
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

    enum SourceImages
    {
        Image_TitleName,
        Image_Source,
    }

    enum GameObjects
    {
        BurgurNameParent
    }

    GameObject burgurNameParent;
    Image image_Burgur;
    Image image_sourceImage;
    Image image_sourceName;


    private void Start()
    {
        switch (currentCard)
        {
            case Define.Cards.Burgur:
                InitBurgurCard();
                break;
            case Define.Cards.Source:
                InitSourceCard();
                break;
        }
    }
    private void InitBurgurCard()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(BurgurImages));
        Bind<GameObject>(typeof(GameObjects));

        TextMeshProUGUI burgurNameText = GetText((int)Texts.Text_BurgurName);
        burgurNameParent = GetGameObject((int)GameObjects.BurgurNameParent);
        image_Burgur = GetImage((int)BurgurImages.Image_Burgur);
        burgurNameText.text = BurgurName;
        image_Burgur.sprite = Managers.Burgur.BurgurImageSpriteDict[BurgurName];
        BurgurCardSetting(currentLevel);
    }

    private void InitSourceCard()
    {
        Bind<Image>(typeof(SourceImages));
        image_sourceImage = GetImage((int)SourceImages.Image_Source);
        image_sourceName = GetImage((int)SourceImages.Image_TitleName);

        image_sourceImage.sprite = Managers.Burgur.SourceImageDict[currentSource.ToString()];
        image_sourceName.sprite = Managers.Burgur.SourceTextNameDict[currentSource.ToString()];
    }

    private void BurgurCardSetting(Define.Levels currentLevel)
    {

        int value = currentLevel == Define.Levels.Tutorial ? 3 :
            currentLevel == Define.Levels.Level1 ? 4 : currentLevel == Define.Levels.Level2 ? 5 :
            currentLevel == Define.Levels.Level3 ? 6 : 7;

        for (int i = 0; i < value; i++)
        {
            GameObject go = new GameObject { name = $"{i + 1}F" };
            go.transform.SetParent(burgurNameParent.transform);
            float yPos = -36.9f + i * 4.3f; // 층 간의 간격은 4.3f로 가정
            go.transform.localPosition = new Vector3(6.1f, yPos, 0f);
            go.transform.localScale = new Vector3(0.44315f, 0.04390998f, 0.44315f);
            Image image = Util.GetOrAddComponet<Image>(go);
            string materialName = Managers.Burgur.BurgursInfoDict[BurgurName][i];

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
            if (Managers.Burgur.BurgurMaterialSpriteDict.TryGetValue(materialName, out result))
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
