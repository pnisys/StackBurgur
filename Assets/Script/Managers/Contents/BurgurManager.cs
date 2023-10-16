using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgurManager
{
    //���� �̸��� currentLevel�� �Ѱ��ָ�
    //���⼭ ���� �̹��� ���ϰ� ��Ÿ �������� CardController�� �Ѱ��ָ� ��
    //�׷� ���ϵ��� �갡 �� ���� �־�� ��

    enum MaterialNames
    {
        �ܹ��Ż���,
        �ܹ��Ż��Ʒ�,
        �Թ�����,
        �Թ����Ʒ�,
        ������ġ����,
        ������ġ���Ʒ�,
        ������,
        �Ұ��,
        ġ��,
        ġŲ,
        ����,
        ����,
        ����,
        �丶��,
        �����
    }

    enum TutorialBurgurNames
    {
        ��������,
    }

    enum Level1BurgurNames
    {
        �������,
        �������,
        �丶����Ƽ����,
        ����ġ�����,
        ����������,
        �ӽ������,
        �����ѹ���,
        �Ұ�����
    }
    enum Level2BurgurNames
    {
        ġ��Ұ�����,
        ������ġ�ܹ���,
        ġŲ����,
        Ʈ���ð�����,
        �Թ�����,
        ���Ĺ���,
        ���󰡵����,
        �������̹���
    }
    enum Level3BurgurNames
    {
        ����¹���,
        ġ���丶�����,
        ������ġġ�����,
        ���̾�Ʈ����,
        ��Į�θ�����,
        ����������,
        ��ȭ�����Ұ�����,
        ġ��������
    }
    enum Level4BurgurNames
    {
        ��׽�����,
        ���а������,
        ��ū����,
        ���־����,
        ��źĮ�θ�����,
        ����ܽɹ���,
        ��º������,
        �����̹���
    }

    private const string materialSpriteFolderPath = "Art/Image/BurgurMaterialsSprite/";
    private const string burgurSpriteFolderPath_Tutorial = "Art/Image/BurgurImageSprite/";
    private const string burgurSpriteFolderPath_Level1 = "Art/Image/BurgurImageSprite/1stage/";
    private const string burgurSpriteFolderPath_Level2 = "Art/Image/BurgurImageSprite/2stage/";
    private const string burgurSpriteFolderPath_Level3 = "Art/Image/BurgurImageSprite/3stage/";
    private const string burgurSpriteFolderPath_Level4 = "Art/Image/BurgurImageSprite/4stage/";


    private Dictionary<string, string> enumToString_BurgurCardNameDict = new Dictionary<string, string>()
        {
           { MaterialNames.�ܹ��Ż���.ToString(), "HamburgurBreadUp" },
            { MaterialNames.�ܹ��Ż��Ʒ�.ToString(), "HamburgurBreadDown" },
            { MaterialNames.�Թ�����.ToString(), "BlackburgurBreadUp" },
            { MaterialNames.�Թ����Ʒ�.ToString(), "BlackburgurBreadDown" },
            { MaterialNames.������ġ����.ToString(), "HamburgurBreadUp" },
            { MaterialNames.������ġ���Ʒ�.ToString(), "HamburgurBreadDown" },
            { MaterialNames.������.ToString(), "Bacon" },
            { MaterialNames.�Ұ��.ToString(), "Bulmeat" },
            { MaterialNames.ġ��.ToString(), "Cheeze" },
            { MaterialNames.ġŲ.ToString(), "Chicken" },
            { MaterialNames.����.ToString(), "Mushroom" },
            { MaterialNames.����.ToString(), "Onion" },
            { MaterialNames.����.ToString(), "Shrimp" },
            { MaterialNames.�丶��.ToString(), "Tomato" },
            { MaterialNames.�����.ToString(), "Lettuce" }
        };
    public Dictionary<string, Sprite> BurgurMaterialSpriteDict { get; private set; } = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> BurgurImageSpriteDict { get; private set; } = new Dictionary<string, Sprite>();
    public Dictionary<string, string[]> BurgursInfoDict { get; private set; } = new Dictionary<string, string[]>()
{
    {
        "��������", new string[] { "�ܹ��Ż�", "�Ұ��", "�ܹ��Ż�" }
    },
    {
        "�������", new string[] { "�Թ���", "�����", "����", "�Թ���" }
    },
     {
        "�������", new string[] { "������ġ��", "�丶��", "�丶��","������ġ��" }
    },
    {
        "�丶����Ƽ����", new string[] { "������ġ��", "�Ұ��", "�丶��", "�Ұ��" }
    }, {
        "����ġ�����", new string[] { "�ܹ��Ż�", "ġ��", "ġ��", "�ܹ��Ż�" }
    },
    {
        "����������", new string[] { "�Թ���", "�Ұ��", "������", "�Թ���" }
    }, {
        "�ӽ������", new string[] { "�ܹ��Ż�", "�Ұ��", "����","�Թ���"  }
    },
    {
        "�����ѹ���", new string[] { "�ܹ��Ż�", "�Ұ��", "����", "�ܹ��Ż�" }
    }, {
        "�Ұ�����", new string[] { "������ġ��", "�����", "�Ұ��", "�ܹ��Ż�" }
    },
    {
        "ġ��Ұ�����", new string[] { "�ܹ��Ż�", "�Ұ��", "ġ��", "�����","�ܹ��Ż�" }
    },
     {
        "������ġ�ܹ���", new string[] { "������ġ��", "�Ұ��", "ġŲ", "ġ��", "������ġ��" }
    },
    {
        "ġŲ����", new string[] { "������ġ��", "ġŲ", "ġ��", "�丶��", "������ġ��" }
    },
        {
        "Ʈ���ð�����", new string[] { "�Թ���", "ġŲ", "�Ұ��", "������", "������ġ��" }
    },
          {
        "�Թ�����", new string[] { "�Թ���", "����", "�Ұ��", "�����", "�Թ���" }
    },
              {
        "���Ĺ���", new string[] { "�����", "����", "����", "����", "�Թ���" }
    },
                 {
        "���󰡵����", new string[] { "�Ұ��", "����", "�����", "�丶��", "�Ұ��" }
    },           {
        "�������̹���", new string[] { "������ġ��", "����", "�Ұ��", "����", "�ܹ��Ż�" }
    },
                   {
        "����¹���", new string[] { "������ġ��", "ġ��", "ġŲ", "�����", "�Ұ��","�ܹ��Ż�" }
    },
         {
    "ġ���丶�����", new string[] { "�ܹ��Ż�", "�����", "�丶��", "ġ��", "�Ұ��","�ܹ��Ż�" }
},
{
    "������ġġ�����", new string[] { "�ܹ��Ż�", "�Ұ��", "ġ��", "�丶��", "�����", "������ġ��" }
},
{
    "���̾�Ʈ����", new string[] { "�����", "������ġ��", "ġ��", "�丶��", "ġ��", "�����" }
},
{
    "��Į�θ�����", new string[] { "�Թ���", "ġ��", "������", "����", "�Ұ��", "�Թ���" }
},
{
    "����������", new string[] { "������ġ��", "����", "������", "�����", "����", "�ܹ��Ż�" }
},
{
    "��ȭ�����Ұ�����", new string[] { "�Թ���", "����", "�Ұ��", "ġ��", "����", "�ܹ��Ż�" }
},
{
    "ġ��������", new string[] { "������ġ��", "ġ��", "����", "������", "����", "�ܹ��Ż�" }
},
{
    "��׽�����", new string[] { "�Թ���", "�Ұ��", "ġ��", "ġ��", "�Ұ��", "����", "�Թ���" }
},
{
    "���а������", new string[] { "�ܹ��Ż�", "������", "����", "�����", "����", "����", "�ܹ��Ż�" }
},
{
    "��ū����", new string[] { "�Ұ��", "�����", "����", "������", "����", "����", "�Ұ��", "�����" }
},
{
    "���־����", new string[] { "�ܹ��Ż�", "�����", "ġ��", "������", "�Ұ��", "����", "������ġ��" }
},
{
    "��źĮ�θ�����", new string[] { "�Թ���", "�����", "ġ��", "�Ұ��", "ġ��", "�Ұ��", "�Թ���" }
},
{
    "����ܽɹ���", new string[] { "�����", "������", "����", "����", "����", "�Ұ��", "�����" }
},
{
    "��º������", new string[] { "������ġ��", "����", "������", "����", "����", "����", "������ġ��" }
},
{
    "�����̹���", new string[] { "�Թ���", "ġŲ", "������", "����", "������ġ��", "�Ұ��", "�ܹ��Ż�" }
},

};

    public void Init()
    {
        InitMaterialSprites(typeof(MaterialNames), materialSpriteFolderPath, enumToString_BurgurCardNameDict);
        InitBurgurSprites(typeof(TutorialBurgurNames), burgurSpriteFolderPath_Tutorial);
        InitBurgurSprites(typeof(Level1BurgurNames), burgurSpriteFolderPath_Level1);
        InitBurgurSprites(typeof(Level2BurgurNames), burgurSpriteFolderPath_Level2);
        InitBurgurSprites(typeof(Level3BurgurNames), burgurSpriteFolderPath_Level3);
        InitBurgurSprites(typeof(Level4BurgurNames), burgurSpriteFolderPath_Level4);
    }

    private void InitMaterialSprites(Type enumType, string folderPath, Dictionary<string, string> nameDict)
    {
        foreach (Enum item in Enum.GetValues(enumType))
        {
            string materialName = item.ToString();
            BurgurMaterialSpriteDict.Add(materialName, Managers.Resource.Load<Sprite>($"{folderPath}{nameDict[materialName]}"));
        }
    }

    private void InitBurgurSprites(Type enumType, string folderPath)
    {
        foreach (Enum item in Enum.GetValues(enumType))
        {
            string burgurName = item.ToString();
            BurgurImageSpriteDict.Add(burgurName, Managers.Resource.Load<Sprite>($"{folderPath}{burgurName}"));
        }
    }
}
