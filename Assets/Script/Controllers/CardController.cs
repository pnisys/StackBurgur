using Oculus.Voice.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardController : UI_Scene
{
    //currentLevel과 버거이름만 넘겨주면, BurgurManager가 들고있다가 받아오면됨
    //물론 Level과 버거이름은 여기서 정하는 건 아님
    //대신 갖고 있는 산하에 오브젝트들은 여기서 만들어줘야 함
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
            if (string.IsNullOrEmpty(value))
            {
                burgurName = value;
                currentLevel = (Define.Levels)Managers.Burgur.BurgurLevelDict[burgurName];
            }
        }
    }

    enum Texts
    {
        Text_BurgurName
    }

    enum Images
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
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        TextMeshProUGUI burgurNameText = GetText((int)Texts.Text_BurgurName);
        burgurNameParent = GetGameObject((int)GameObjects.BurgurNameParent);
        image_Burgur = GetImage((int)Images.Image_Burgur);
        burgurNameText.text = BurgurName;
        image_Burgur.sprite = Managers.Burgur.BurgurImageSpriteDict[BurgurName];
        Setting(currentLevel);
    }

    private void Setting(Define.Levels currentLevel)
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
