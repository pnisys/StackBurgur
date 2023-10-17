using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgurManager
{
    #region Enums
    enum MaterialNames
    {
        �ܹ��Ż���,
        �ܹ��Ż��Ʒ�,
        �Թ�����,
        �Թ����Ʒ�,
        ������ġ����,
        ������ġ���Ʒ�,
        ������,
        �Ұ���,
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
        �Ұ������
    }
    enum Level2BurgurNames
    {
        ġ��Ұ������,
        ������ġ�ܹ���,
        ġŲ����,
        Ʈ���ð������,
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
        �����������,
        ��ȭ�����Ұ������,
        ġ��������
    }
    enum Level4BurgurNames
    {
        ��׽�����,
        ���а������,
        ��ū����,
        ���־����,
        ��źĮ�θ�����,
        �����ܽɹ���,
        ��º������,
        �����̹���
    }


    #endregion

    #region Path
    private const string materialSpriteFolderPath = "Art/Image/BurgurMaterialsSprite/";
    private const string burgurSpriteFolderPath_Tutorial = "Art/Image/BurgurImageSprite/";
    private const string burgurSpriteFolderPath_Level1 = "Art/Image/BurgurImageSprite/1stage/";
    private const string burgurSpriteFolderPath_Level2 = "Art/Image/BurgurImageSprite/2stage/";
    private const string burgurSpriteFolderPath_Level3 = "Art/Image/BurgurImageSprite/3stage/";
    private const string burgurSpriteFolderPath_Level4 = "Art/Image/BurgurImageSprite/4stage/";

    private const string sourceSpriteFolderPath = "Art/Image/SourceCardMaterialsSprite/";
    #endregion

    #region burgurDict

    public Dictionary<string, int> BurgurLevelDict = new Dictionary<string, int>()
{
    // TutorialBurgurNames
    { nameof(TutorialBurgurNames.��������), 0 },

    // Level1BurgurNames
    { nameof(Level1BurgurNames.�������), 1 },
    { nameof(Level1BurgurNames.�������), 1 },
    { nameof(Level1BurgurNames.�丶����Ƽ����), 1 },
    { nameof(Level1BurgurNames.����ġ�����), 1 },
    { nameof(Level1BurgurNames.����������), 1 },
    { nameof(Level1BurgurNames.�ӽ������), 1 },
    { nameof(Level1BurgurNames.�����ѹ���), 1 },
    { nameof(Level1BurgurNames.�Ұ������), 1 },

    // Level2BurgurNames
    { nameof(Level2BurgurNames.ġ��Ұ������), 2 },
    { nameof(Level2BurgurNames.������ġ�ܹ���), 2 },
    { nameof(Level2BurgurNames.ġŲ����), 2 },
    { nameof(Level2BurgurNames.Ʈ���ð������), 2 },
    { nameof(Level2BurgurNames.�Թ�����), 2 },
    { nameof(Level2BurgurNames.���Ĺ���), 2 },
    { nameof(Level2BurgurNames.���󰡵����), 2 },
    { nameof(Level2BurgurNames.�������̹���), 2 },

    // Level3BurgurNames
    { nameof(Level3BurgurNames.����¹���), 3 },
    { nameof(Level3BurgurNames.ġ���丶�����), 3 },
    { nameof(Level3BurgurNames.������ġġ�����), 3 },
    { nameof(Level3BurgurNames.���̾�Ʈ����), 3 },
    { nameof(Level3BurgurNames.��Į�θ�����), 3 },
    { nameof(Level3BurgurNames.�����������), 3 },
    { nameof(Level3BurgurNames.��ȭ�����Ұ������), 3 },
    { nameof(Level3BurgurNames.ġ��������), 3 },

    // Level4BurgurNames
    { nameof(Level4BurgurNames.��׽�����), 4 },
    { nameof(Level4BurgurNames.���а������), 4 },
    { nameof(Level4BurgurNames.��ū����), 4 },
    { nameof(Level4BurgurNames.���־����), 4 },
    { nameof(Level4BurgurNames.��źĮ�θ�����), 4 },
    { nameof(Level4BurgurNames.�����ܽɹ���), 4 },
    { nameof(Level4BurgurNames.��º������), 4 },
    { nameof(Level4BurgurNames.�����̹���), 4 },
};

    private Dictionary<string, string> enumToString_BurgurCardNameDict = new Dictionary<string, string>()
{
    { nameof(MaterialNames.�ܹ��Ż���), "HamburgurBreadUp" },
    { nameof(MaterialNames.�ܹ��Ż��Ʒ�), "HamburgurBreadDown" },
    { nameof(MaterialNames.�Թ�����), "BlackburgurBreadUp" },
    { nameof(MaterialNames.�Թ����Ʒ�), "BlackburgurBreadDown" },
    { nameof(MaterialNames.������ġ����), "HamburgurBreadUp" },
    { nameof(MaterialNames.������ġ���Ʒ�), "HamburgurBreadDown" },
    { nameof(MaterialNames.������), "Bacon" },
    { nameof(MaterialNames.�Ұ���), "Bulmeat" },
    { nameof(MaterialNames.ġ��), "Cheeze" },
    { nameof(MaterialNames.ġŲ), "Chicken" },
    { nameof(MaterialNames.����), "Mushroom" },
    { nameof(MaterialNames.����), "Onion" },
    { nameof(MaterialNames.����), "Shrimp" },
    { nameof(MaterialNames.�丶��), "Tomato" },
    { nameof(MaterialNames.�����), "Lettuce" }
};

    public Dictionary<string, Sprite> BurgurMaterialSpriteDict { get; private set; } = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> BurgurImageSpriteDict { get; private set; } = new Dictionary<string, Sprite>();
    public Dictionary<string, string[]> BurgursInfoDict { get; private set; } = new Dictionary<string, string[]>()


{
    {
        nameof(TutorialBurgurNames.��������), new string[] { "�ܹ��Ż�", "�Ұ���", "�ܹ��Ż�" }
    },
    {
        nameof(Level1BurgurNames.�������), new string[] { "�Թ���", "�����", "����", "�Թ���" }
    },
     {
          nameof(Level1BurgurNames.�������), new string[] { "������ġ��", "�丶��", "�丶��","������ġ��" }
    },
    {
          nameof(Level1BurgurNames.�丶����Ƽ����), new string[] { "������ġ��", "�Ұ���", "�丶��", "�Ұ���" }
    },
    {
           nameof(Level1BurgurNames.����ġ�����), new string[] { "�ܹ��Ż�", "ġ��", "ġ��", "�ܹ��Ż�" }
    },
    {
          nameof(Level1BurgurNames.����������), new string[] { "�Թ���", "�Ұ���", "������", "�Թ���" }
    }, {
          nameof(Level1BurgurNames.�ӽ������), new string[] { "�ܹ��Ż�", "�Ұ���", "����","�Թ���"  }
    },
    {
          nameof(Level1BurgurNames.�����ѹ���), new string[] { "�ܹ��Ż�", "�Ұ���", "����", "�ܹ��Ż�" }
    }, {
          nameof(Level1BurgurNames.�Ұ������), new string[] { "������ġ��", "�����", "�Ұ���", "�ܹ��Ż�" }
    },
    {
          nameof(Level2BurgurNames.ġ��Ұ������), new string[] { "�ܹ��Ż�", "�Ұ���", "ġ��", "�����","�ܹ��Ż�" }
    },
     {
         nameof(Level2BurgurNames.������ġ�ܹ���), new string[] { "������ġ��", "�Ұ���", "ġŲ", "ġ��", "������ġ��" }
    },
    {
          nameof(Level2BurgurNames.ġŲ����), new string[] { "������ġ��", "ġŲ", "ġ��", "�丶��", "������ġ��" }
    },
        {
           nameof(Level2BurgurNames.Ʈ���ð������), new string[] { "�Թ���", "ġŲ", "�Ұ���", "������", "������ġ��" }
    },
          {
      nameof(Level2BurgurNames.�Թ�����), new string[] { "�Թ���", "����", "�Ұ���", "�����", "�Թ���" }
    },
              {
           nameof(Level2BurgurNames.���Ĺ���), new string[] { "�����", "����", "����", "����", "�Թ���" }
    },
                 {
           nameof(Level2BurgurNames.���󰡵����), new string[] { "�Ұ���", "����", "�����", "�丶��", "�Ұ���" }
    },           {
         nameof(Level2BurgurNames.�������̹���), new string[] { "������ġ��", "����", "�Ұ���", "����", "�ܹ��Ż�" }
    },
                   {
           nameof(Level3BurgurNames.����¹���), new string[] { "������ġ��", "ġ��", "ġŲ", "�����", "�Ұ���","�ܹ��Ż�" }
    },
         {
   nameof(Level3BurgurNames.ġ���丶�����), new string[] { "�ܹ��Ż�", "�����", "�丶��", "ġ��", "�Ұ���","�ܹ��Ż�" }
},
{
  nameof(Level3BurgurNames.������ġġ�����), new string[] { "�ܹ��Ż�", "�Ұ���", "ġ��", "�丶��", "�����", "������ġ��" }
},
{
  nameof(Level3BurgurNames.���̾�Ʈ����), new string[] { "�����", "������ġ��", "ġ��", "�丶��", "ġ��", "�����" }
},
{
  nameof(Level3BurgurNames.��Į�θ�����), new string[] { "�Թ���", "ġ��", "������", "����", "�Ұ���", "�Թ���" }
},
{
  nameof(Level3BurgurNames.�����������), new string[] { "������ġ��", "����", "������", "�����", "����", "�ܹ��Ż�" }
},
{
    nameof(Level3BurgurNames.��ȭ�����Ұ������), new string[] { "�Թ���", "����", "�Ұ���", "ġ��", "����", "�ܹ��Ż�" }
},
{
   nameof(Level3BurgurNames.ġ��������), new string[] { "������ġ��", "ġ��", "����", "������", "����", "�ܹ��Ż�" }
},
{
  nameof(Level4BurgurNames.��׽�����), new string[] { "�Թ���", "�Ұ���", "ġ��", "ġ��", "�Ұ���", "����", "�Թ���" }
},
{
   nameof(Level4BurgurNames.���а������), new string[] { "�ܹ��Ż�", "������", "����", "�����", "����", "����", "�ܹ��Ż�" }
},
{
   nameof(Level4BurgurNames.��ū����), new string[] { "�Ұ���", "�����", "����", "������", "����", "����", "�Ұ���", "�����" }
},
{
  nameof(Level4BurgurNames.���־����), new string[] { "�ܹ��Ż�", "�����", "ġ��", "������", "�Ұ���", "����", "������ġ��" }
},
{
   nameof(Level4BurgurNames.��źĮ�θ�����), new string[] { "�Թ���", "�����", "ġ��", "�Ұ���", "ġ��", "�Ұ���", "�Թ���" }
},
{
   nameof(Level4BurgurNames.�����ܽɹ���), new string[] { "�����", "������", "����", "����", "����", "�Ұ���", "�����" }
},
{
  nameof(Level4BurgurNames.��º������), new string[] { "������ġ��", "����", "������", "����", "����", "����", "������ġ��" }
},
{
 nameof(Level4BurgurNames.�����̹���), new string[] { "�Թ���", "ġŲ", "������", "����", "������ġ��", "�Ұ���", "�ܹ��Ż�" }
},

};
    #endregion
    #region SourceDit

    private Dictionary<string, string> enumToString_SourceCardTextNameDict = new Dictionary<string, string>()
{
    { nameof(Define.SourceNames.�ٺ�ť�ҽ�), "sauce_txt_1" },
    { nameof(Define.SourceNames.ĥ���ҽ�), "sauce_txt_2" },
    { nameof(Define.SourceNames.�ӽ�Ÿ��ҽ�), "sauce_txt_3" },
    { nameof(Define.SourceNames.�������ҽ�),"sauce_txt_4" },
};
    private Dictionary<string, string> enumToString_SourceCardImageDict = new Dictionary<string, string>()
{
    { nameof(Define.SourceNames.�ٺ�ť�ҽ�), "sauce_type_1" },
    { nameof(Define.SourceNames.ĥ���ҽ�), "sauce_type_2" },
    { nameof(Define.SourceNames.�ӽ�Ÿ��ҽ�), "sauce_type_3" },
    { nameof(Define.SourceNames.�������ҽ�),"sauce_type_4" },
};

    public Dictionary<string, Sprite> SourceImageDict { get; private set; } = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> SourceTextNameDict { get; private set; } = new Dictionary<string, Sprite>();
    #endregion

    #region Init
    public void Init()
    {
        InitMaterialSprites(typeof(MaterialNames), materialSpriteFolderPath, enumToString_BurgurCardNameDict);
        InitBurgurSprites(typeof(TutorialBurgurNames), burgurSpriteFolderPath_Tutorial, BurgurImageSpriteDict);
        InitBurgurSprites(typeof(Level1BurgurNames), burgurSpriteFolderPath_Level1, BurgurImageSpriteDict);
        InitBurgurSprites(typeof(Level2BurgurNames), burgurSpriteFolderPath_Level2, BurgurImageSpriteDict);
        InitBurgurSprites(typeof(Level3BurgurNames), burgurSpriteFolderPath_Level3, BurgurImageSpriteDict);
        InitBurgurSprites(typeof(Level4BurgurNames), burgurSpriteFolderPath_Level4, BurgurImageSpriteDict);

        InitSourceSprites(typeof(Define.SourceNames), sourceSpriteFolderPath, enumToString_SourceCardImageDict, SourceImageDict);
        InitSourceSprites(typeof(Define.SourceNames), sourceSpriteFolderPath, enumToString_SourceCardTextNameDict, SourceTextNameDict);
    }

    private void InitMaterialSprites(Type enumType, string folderPath, Dictionary<string, string> nameDict)
    {
        foreach (Enum item in Enum.GetValues(enumType))
        {
            string materialName = item.ToString();
            BurgurMaterialSpriteDict.Add(materialName, Managers.Resource.Load<Sprite>($"{folderPath}{nameDict[materialName]}"));
        }
    }

    private void InitBurgurSprites(Type enumType, string folderPath, Dictionary<string, Sprite> dict)
    {
        foreach (Enum item in Enum.GetValues(enumType))
        {
            string burgurName = item.ToString();
            dict.Add(burgurName, Managers.Resource.Load<Sprite>($"{folderPath}{burgurName}"));
        }
    }

    private void InitSourceSprites(Type enumType, string folderPath, Dictionary<string, string> nameDict, Dictionary<string, Sprite> dict)
    {
        foreach (Enum item in Enum.GetValues(enumType))
        {
            string sourceFileName = nameDict[item.ToString()];
            dict.Add(item.ToString(), Managers.Resource.Load<Sprite>($"{folderPath}{sourceFileName}"));
        }
    }
    #endregion
}
