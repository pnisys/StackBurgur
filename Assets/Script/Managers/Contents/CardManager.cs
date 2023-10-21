using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager
{
    #region Enums
    enum MaterialNames
    {
        �ܹ��Ż���,
        �ܹ��Ż��Ʒ�,
        �Թ�����,
        �Թ����Ʒ�,
        ������ġ��,
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
    
    #endregion

    #region burgurDict

    public Dictionary<string, int> BurgurLevelDict = new Dictionary<string, int>()
{
    // TutorialBurgurNames
    { nameof(Define.TutorialBurgurNames.��������), 0 },

    // Level1BurgurNames
    { nameof(Define.Level1BurgurNames.�������), 1 },
    { nameof(Define.Level1BurgurNames.�������), 1 },
    { nameof(Define.Level1BurgurNames.�丶����Ƽ����), 1 },
    { nameof(Define.Level1BurgurNames.����ġ�����), 1 },
    { nameof(Define.Level1BurgurNames.����������), 1 },
    { nameof(Define.Level1BurgurNames.�ӽ������), 1 },
    { nameof(Define.Level1BurgurNames.�����ѹ���), 1 },
    { nameof(Define.Level1BurgurNames.�Ұ�����), 1 },

    // Level2BurgurNames
    { nameof(Define.Level2BurgurNames.ġ��Ұ�����), 2 },
    { nameof(Define.Level2BurgurNames.������ġ�ܹ���), 2 },
    { nameof(Define.Level2BurgurNames.ġŲ����), 2 },
    { nameof(Define.Level2BurgurNames.Ʈ���ð�����), 2 },
    { nameof(Define.Level2BurgurNames.�Թ�����), 2 },
    { nameof(Define.Level2BurgurNames.���Ĺ���), 2 },
    { nameof(Define.Level2BurgurNames.���󰡵����), 2 },
    { nameof(Define.Level2BurgurNames.�������̹���), 2 },

    // Level3BurgurNames
    { nameof(Define.Level3BurgurNames.����¹���), 3 },
    { nameof(Define.Level3BurgurNames.ġ���丶�����), 3 },
    { nameof(Define.Level3BurgurNames.������ġġ�����), 3 },
    { nameof(Define.Level3BurgurNames.���̾�Ʈ����), 3 },
    { nameof(Define.Level3BurgurNames.��Į�θ�����), 3 },
    { nameof(Define.Level3BurgurNames.����������), 3 },
    { nameof(Define.Level3BurgurNames.��ȭ�����Ұ�����), 3 },
    { nameof(Define.Level3BurgurNames.ġ��������), 3 },

    // Level4BurgurNames
    { nameof(Define.Level4BurgurNames.��׽�����), 4 },
    { nameof(Define.Level4BurgurNames.���а������), 4 },
    { nameof(Define.Level4BurgurNames.��ū����), 4 },
    { nameof(Define.Level4BurgurNames.���־����), 4 },
    { nameof(Define.Level4BurgurNames.��źĮ�θ�����), 4 },
    { nameof(Define.Level4BurgurNames.����ܽɹ���), 4 },
    { nameof(Define.Level4BurgurNames.��º������), 4 },
    { nameof(Define.Level4BurgurNames.�����̹���), 4 },
};

    private Dictionary<string, string> enumToString_BurgurCardNameDict = new Dictionary<string, string>()
{
    { nameof(MaterialNames.�ܹ��Ż���), "HamburgurBreadUp" },
    { nameof(MaterialNames.�ܹ��Ż��Ʒ�), "HamburgurBreadDown" },
    { nameof(MaterialNames.�Թ�����), "BlackburgurBreadUp" },
    { nameof(MaterialNames.�Թ����Ʒ�), "BlackburgurBreadDown" },
    { nameof(MaterialNames.������ġ��), "SandwichBread" },
    { nameof(MaterialNames.������), "Bacon" },
    { nameof(MaterialNames.�Ұ��), "Bulmeat" },
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
        nameof(Define.TutorialBurgurNames.��������), new string[] { "�ܹ��Ż�", "�Ұ��", "�ܹ��Ż�" }
    },
    {
        nameof(Define.Level1BurgurNames.�������), new string[] { "�Թ���", "�����", "����", "�Թ���" }
    },
     {
          nameof(Define.Level1BurgurNames.�������), new string[] { "������ġ��", "�丶��", "�丶��","������ġ��" }
    },
    {
          nameof(Define.Level1BurgurNames.�丶����Ƽ����), new string[] { "������ġ��", "�Ұ��", "�丶��", "�Ұ��" }
    },
    {
           nameof(Define.Level1BurgurNames.����ġ�����), new string[] { "�ܹ��Ż�", "ġ��", "ġ��", "�ܹ��Ż�" }
    },
    {
          nameof(Define.Level1BurgurNames.����������), new string[] { "�Թ���", "�Ұ��", "������", "�Թ���" }
    }, {
          nameof(Define.Level1BurgurNames.�ӽ������), new string[] { "�ܹ��Ż�", "�Ұ��", "����","�Թ���"  }
    },
    {
          nameof(Define.Level1BurgurNames.�����ѹ���), new string[] { "�ܹ��Ż�", "�Ұ��", "����", "�ܹ��Ż�" }
    }, {
          nameof(Define.Level1BurgurNames.�Ұ�����), new string[] { "������ġ��", "�����", "�Ұ��", "�ܹ��Ż�" }
    },
    {
          nameof(Define.Level2BurgurNames.ġ��Ұ�����), new string[] { "�ܹ��Ż�", "�Ұ��", "ġ��", "�����","�ܹ��Ż�" }
    },
     {
         nameof(Define.Level2BurgurNames.������ġ�ܹ���), new string[] { "������ġ��", "�Ұ��", "ġŲ", "ġ��", "������ġ��" }
    },
    {
          nameof(Define.Level2BurgurNames.ġŲ����), new string[] { "������ġ��", "ġŲ", "ġ��", "�丶��", "������ġ��" }
    },
        {
           nameof(Define.Level2BurgurNames.Ʈ���ð�����), new string[] { "�Թ���", "ġŲ", "�Ұ��", "������", "������ġ��" }
    },
          {
      nameof(Define.Level2BurgurNames.�Թ�����), new string[] { "�Թ���", "����", "�Ұ��", "�����", "�Թ���" }
    },
              {
           nameof(Define.Level2BurgurNames.���Ĺ���), new string[] { "�����", "����", "����", "����", "�Թ���" }
    },
                 {
           nameof(Define.Level2BurgurNames.���󰡵����), new string[] { "�Ұ��", "����", "�����", "�丶��", "�Ұ��" }
    },           {
         nameof(Define.Level2BurgurNames.�������̹���), new string[] { "������ġ��", "����", "�Ұ��", "����", "�ܹ��Ż�" }
    },
                   {
           nameof(Define.Level3BurgurNames.����¹���), new string[] { "������ġ��", "ġ��", "ġŲ", "�����", "�Ұ��","�ܹ��Ż�" }
    },
         {
   nameof(Define.Level3BurgurNames.ġ���丶�����), new string[] { "�ܹ��Ż�", "�����", "�丶��", "ġ��", "�Ұ��","�ܹ��Ż�" }
},
{
  nameof(Define.Level3BurgurNames.������ġġ�����), new string[] { "�ܹ��Ż�", "�Ұ��", "ġ��", "�丶��", "�����", "������ġ��" }
},
{
  nameof(Define.Level3BurgurNames.���̾�Ʈ����), new string[] { "�����", "������ġ��", "ġ��", "�丶��", "ġ��", "�����" }
},
{
  nameof(Define.Level3BurgurNames.��Į�θ�����), new string[] { "�Թ���", "ġ��", "������", "����", "�Ұ��", "�Թ���" }
},
{
  nameof(Define.Level3BurgurNames.����������), new string[] { "������ġ��", "����", "������", "�����", "����", "�ܹ��Ż�" }
},
{
    nameof(Define.Level3BurgurNames.��ȭ�����Ұ�����), new string[] { "�Թ���", "����", "�Ұ��", "ġ��", "����", "�ܹ��Ż�" }
},
{
   nameof(Define.Level3BurgurNames.ġ��������), new string[] { "������ġ��", "ġ��", "����", "������", "����", "�ܹ��Ż�" }
},
{
  nameof(Define.Level4BurgurNames.��׽�����), new string[] { "�Թ���", "�Ұ��", "ġ��", "ġ��", "�Ұ��", "����", "�Թ���" }
},
{
   nameof(Define.Level4BurgurNames.���а������), new string[] { "�ܹ��Ż�", "������", "����", "�����", "����", "����", "�ܹ��Ż�" }
},
{
   nameof(Define.Level4BurgurNames.��ū����), new string[] { "�Ұ��", "�����", "����", "������", "����", "����", "�Ұ��", "�����" }
},
{
  nameof(Define.Level4BurgurNames.���־����), new string[] { "�ܹ��Ż�", "�����", "ġ��", "������", "�Ұ��", "����", "������ġ��" }
},
{
   nameof(Define.Level4BurgurNames.��źĮ�θ�����), new string[] { "�Թ���", "�����", "ġ��", "�Ұ��", "ġ��", "�Ұ��", "�Թ���" }
},
{
   nameof(Define.Level4BurgurNames.����ܽɹ���), new string[] { "�����", "������", "����", "����", "����", "�Ұ��", "�����" }
},
{
  nameof(Define.Level4BurgurNames.��º������), new string[] { "������ġ��", "����", "������", "����", "����", "����", "������ġ��" }
},
{
 nameof(Define.Level4BurgurNames.�����̹���), new string[] { "�Թ���", "ġŲ", "������", "����", "������ġ��", "�Ұ��", "�ܹ��Ż�" }
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
        InitMaterialSprites(typeof(MaterialNames), Define.materialSpriteFolderPath, enumToString_BurgurCardNameDict);
        InitBurgurSprites(typeof(Define.TutorialBurgurNames), Define.burgurSpriteFolderPath_Tutorial, BurgurImageSpriteDict);
        InitBurgurSprites(typeof(Define.Level1BurgurNames), Define.burgurSpriteFolderPath_Level1, BurgurImageSpriteDict);
        InitBurgurSprites(typeof(Define.Level2BurgurNames), Define.burgurSpriteFolderPath_Level2, BurgurImageSpriteDict);
        InitBurgurSprites(typeof(Define.Level3BurgurNames), Define.burgurSpriteFolderPath_Level3, BurgurImageSpriteDict);
        InitBurgurSprites(typeof(Define.Level4BurgurNames), Define.burgurSpriteFolderPath_Level4, BurgurImageSpriteDict);

        InitSourceSprites(typeof(Define.SourceNames), Define.sourceSpriteFolderPath, enumToString_SourceCardImageDict, SourceImageDict);
        InitSourceSprites(typeof(Define.SourceNames), Define.sourceSpriteFolderPath, enumToString_SourceCardTextNameDict, SourceTextNameDict);
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
