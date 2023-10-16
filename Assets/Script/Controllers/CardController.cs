using Oculus.Voice.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardController : UI_Scene
{
    //currentLevel�� �����̸��� �Ѱ��ָ�, BurgurManager�� ����ִٰ� �޾ƿ����
    //���� Level�� �����̸��� ���⼭ ���ϴ� �� �ƴ�
    //��� ���� �ִ� ���Ͽ� ������Ʈ���� ���⼭ �������� ��
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
            float yPos = -36.9f + i * 4.3f; // �� ���� ������ 4.3f�� ����
            go.transform.localPosition = new Vector3(6.1f, yPos, 0f);
            go.transform.localScale = new Vector3(0.44315f, 0.04390998f, 0.44315f);
            Image image = Util.GetOrAddComponet<Image>(go);
            string materialName = Managers.Burgur.BurgursInfoDict[BurgurName][i];

            // ���� �߰�
            if (i == 0 && materialName.EndsWith("��"))
            {
                materialName += "�Ʒ�";
            }
            else if (i == value - 1 && materialName.EndsWith("��"))
            {
                materialName += "��";
            }

            Sprite result;
            if (Managers.Burgur.BurgurMaterialSpriteDict.TryGetValue(materialName, out result))
            {
                image.sprite = result;
            }
            else
            {
                Debug.LogError($"'{materialName}'�� ���� ��������Ʈ�� ã�� �� �����ϴ�.");
            }
        }
    }
}
