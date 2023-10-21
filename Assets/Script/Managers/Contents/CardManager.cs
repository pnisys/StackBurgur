using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager
{
    #region Enums
    enum MaterialNames
    {
        햄버거빵위,
        햄버거빵아래,
        먹물빵위,
        먹물빵아래,
        샌드위치빵,
        베이컨,
        불고기,
        치즈,
        치킨,
        버섯,
        양파,
        새우,
        토마토,
        양상추
    }
    
    #endregion

    #region burgurDict

    public Dictionary<string, int> BurgurLevelDict = new Dictionary<string, int>()
{
    // TutorialBurgurNames
    { nameof(Define.TutorialBurgurNames.데리버거), 0 },

    // Level1BurgurNames
    { nameof(Define.Level1BurgurNames.새우버거), 1 },
    { nameof(Define.Level1BurgurNames.토토버거), 1 },
    { nameof(Define.Level1BurgurNames.토마토패티버거), 1 },
    { nameof(Define.Level1BurgurNames.더블치즈버거), 1 },
    { nameof(Define.Level1BurgurNames.베이컨버거), 1 },
    { nameof(Define.Level1BurgurNames.머쉬룸버거), 1 },
    { nameof(Define.Level1BurgurNames.촉촉한버거), 1 },
    { nameof(Define.Level1BurgurNames.불고기버거), 1 },

    // Level2BurgurNames
    { nameof(Define.Level2BurgurNames.치즈불고기버거), 2 },
    { nameof(Define.Level2BurgurNames.샌드위치햄버거), 2 },
    { nameof(Define.Level2BurgurNames.치킨버거), 2 },
    { nameof(Define.Level2BurgurNames.트리플고기버거), 2 },
    { nameof(Define.Level2BurgurNames.먹물버거), 2 },
    { nameof(Define.Level2BurgurNames.양파버거), 2 },
    { nameof(Define.Level2BurgurNames.육즙가득버거), 2 },
    { nameof(Define.Level2BurgurNames.버섯많이버거), 2 },

    // Level3BurgurNames
    { nameof(Define.Level3BurgurNames.살찌는버거), 3 },
    { nameof(Define.Level3BurgurNames.치즈토마토버거), 3 },
    { nameof(Define.Level3BurgurNames.샌드위치치즈버거), 3 },
    { nameof(Define.Level3BurgurNames.다이어트버거), 3 },
    { nameof(Define.Level3BurgurNames.고칼로리버거), 3 },
    { nameof(Define.Level3BurgurNames.더블새우버거), 3 },
    { nameof(Define.Level3BurgurNames.직화버섯불고기버거), 3 },
    { nameof(Define.Level3BurgurNames.치즈새우버거), 3 },

    // Level4BurgurNames
    { nameof(Define.Level4BurgurNames.기네스버거), 4 },
    { nameof(Define.Level4BurgurNames.수분가득버거), 4 },
    { nameof(Define.Level4BurgurNames.통큰버거), 4 },
    { nameof(Define.Level4BurgurNames.비주얼버거), 4 },
    { nameof(Define.Level4BurgurNames.폭탄칼로리버거), 4 },
    { nameof(Define.Level4BurgurNames.일편단심버거), 4 },
    { nameof(Define.Level4BurgurNames.기력보충버거), 4 },
    { nameof(Define.Level4BurgurNames.빵많이버거), 4 },
};

    private Dictionary<string, string> enumToString_BurgurCardNameDict = new Dictionary<string, string>()
{
    { nameof(MaterialNames.햄버거빵위), "HamburgurBreadUp" },
    { nameof(MaterialNames.햄버거빵아래), "HamburgurBreadDown" },
    { nameof(MaterialNames.먹물빵위), "BlackburgurBreadUp" },
    { nameof(MaterialNames.먹물빵아래), "BlackburgurBreadDown" },
    { nameof(MaterialNames.샌드위치빵), "SandwichBread" },
    { nameof(MaterialNames.베이컨), "Bacon" },
    { nameof(MaterialNames.불고기), "Bulmeat" },
    { nameof(MaterialNames.치즈), "Cheeze" },
    { nameof(MaterialNames.치킨), "Chicken" },
    { nameof(MaterialNames.버섯), "Mushroom" },
    { nameof(MaterialNames.양파), "Onion" },
    { nameof(MaterialNames.새우), "Shrimp" },
    { nameof(MaterialNames.토마토), "Tomato" },
    { nameof(MaterialNames.양상추), "Lettuce" }
};

    public Dictionary<string, Sprite> BurgurMaterialSpriteDict { get; private set; } = new Dictionary<string, Sprite>();
    public Dictionary<string, Sprite> BurgurImageSpriteDict { get; private set; } = new Dictionary<string, Sprite>();
    public Dictionary<string, string[]> BurgursInfoDict { get; private set; } = new Dictionary<string, string[]>()
{
    {
        nameof(Define.TutorialBurgurNames.데리버거), new string[] { "햄버거빵", "불고기", "햄버거빵" }
    },
    {
        nameof(Define.Level1BurgurNames.새우버거), new string[] { "먹물빵", "양상추", "새우", "먹물빵" }
    },
     {
          nameof(Define.Level1BurgurNames.토토버거), new string[] { "샌드위치빵", "토마토", "토마토","샌드위치빵" }
    },
    {
          nameof(Define.Level1BurgurNames.토마토패티버거), new string[] { "샌드위치빵", "불고기", "토마토", "불고기" }
    },
    {
           nameof(Define.Level1BurgurNames.더블치즈버거), new string[] { "햄버거빵", "치즈", "치즈", "햄버거빵" }
    },
    {
          nameof(Define.Level1BurgurNames.베이컨버거), new string[] { "먹물빵", "불고기", "베이컨", "먹물빵" }
    }, {
          nameof(Define.Level1BurgurNames.머쉬룸버거), new string[] { "햄버거빵", "불고기", "버섯","먹물빵"  }
    },
    {
          nameof(Define.Level1BurgurNames.촉촉한버거), new string[] { "햄버거빵", "불고기", "양파", "햄버거빵" }
    }, {
          nameof(Define.Level1BurgurNames.불고기버거), new string[] { "샌드위치빵", "양상추", "불고기", "햄버거빵" }
    },
    {
          nameof(Define.Level2BurgurNames.치즈불고기버거), new string[] { "햄버거빵", "불고기", "치즈", "양상추","햄버거빵" }
    },
     {
         nameof(Define.Level2BurgurNames.샌드위치햄버거), new string[] { "샌드위치빵", "불고기", "치킨", "치즈", "샌드위치빵" }
    },
    {
          nameof(Define.Level2BurgurNames.치킨버거), new string[] { "샌드위치빵", "치킨", "치즈", "토마토", "샌드위치빵" }
    },
        {
           nameof(Define.Level2BurgurNames.트리플고기버거), new string[] { "먹물빵", "치킨", "불고기", "베이컨", "샌드위치빵" }
    },
          {
      nameof(Define.Level2BurgurNames.먹물버거), new string[] { "먹물빵", "새우", "불고기", "양상추", "먹물빵" }
    },
              {
           nameof(Define.Level2BurgurNames.양파버거), new string[] { "양상추", "양파", "새우", "양파", "먹물빵" }
    },
                 {
           nameof(Define.Level2BurgurNames.육즙가득버거), new string[] { "불고기", "양파", "양상추", "토마토", "불고기" }
    },           {
         nameof(Define.Level2BurgurNames.버섯많이버거), new string[] { "샌드위치빵", "버섯", "불고기", "버섯", "햄버거빵" }
    },
                   {
           nameof(Define.Level3BurgurNames.살찌는버거), new string[] { "샌드위치빵", "치즈", "치킨", "양상추", "불고기","햄버거빵" }
    },
         {
   nameof(Define.Level3BurgurNames.치즈토마토버거), new string[] { "햄버거빵", "양상추", "토마토", "치즈", "불고기","햄버거빵" }
},
{
  nameof(Define.Level3BurgurNames.샌드위치치즈버거), new string[] { "햄버거빵", "불고기", "치즈", "토마토", "양상추", "샌드위치빵" }
},
{
  nameof(Define.Level3BurgurNames.다이어트버거), new string[] { "양상추", "샌드위치빵", "치즈", "토마토", "치즈", "양상추" }
},
{
  nameof(Define.Level3BurgurNames.고칼로리버거), new string[] { "먹물빵", "치즈", "베이컨", "새우", "불고기", "먹물빵" }
},
{
  nameof(Define.Level3BurgurNames.더블새우버거), new string[] { "샌드위치빵", "새우", "베이컨", "양상추", "새우", "햄버거빵" }
},
{
    nameof(Define.Level3BurgurNames.직화버섯불고기버거), new string[] { "먹물빵", "버섯", "불고기", "치즈", "양파", "햄버거빵" }
},
{
   nameof(Define.Level3BurgurNames.치즈새우버거), new string[] { "샌드위치빵", "치즈", "양파", "베이컨", "새우", "햄버거빵" }
},
{
  nameof(Define.Level4BurgurNames.기네스버거), new string[] { "먹물빵", "불고기", "치즈", "치즈", "불고기", "양파", "먹물빵" }
},
{
   nameof(Define.Level4BurgurNames.수분가득버거), new string[] { "햄버거빵", "베이컨", "양파", "양상추", "새우", "버섯", "햄버거빵" }
},
{
   nameof(Define.Level4BurgurNames.통큰버거), new string[] { "불고기", "양상추", "양파", "베이컨", "새우", "버섯", "불고기", "양상추" }
},
{
  nameof(Define.Level4BurgurNames.비주얼버거), new string[] { "햄버거빵", "양상추", "치즈", "베이컨", "불고기", "양파", "샌드위치빵" }
},
{
   nameof(Define.Level4BurgurNames.폭탄칼로리버거), new string[] { "먹물빵", "양상추", "치즈", "불고기", "치즈", "불고기", "먹물빵" }
},
{
   nameof(Define.Level4BurgurNames.일편단심버거), new string[] { "양상추", "베이컨", "양파", "새우", "양파", "불고기", "양상추" }
},
{
  nameof(Define.Level4BurgurNames.기력보충버거), new string[] { "샌드위치빵", "버섯", "베이컨", "양파", "새우", "버섯", "샌드위치빵" }
},
{
 nameof(Define.Level4BurgurNames.빵많이버거), new string[] { "먹물빵", "치킨", "베이컨", "양파", "샌드위치빵", "불고기", "햄버거빵" }
},

};
    #endregion

    #region SourceDit

    private Dictionary<string, string> enumToString_SourceCardTextNameDict = new Dictionary<string, string>()
{
    { nameof(Define.SourceNames.바베큐소스), "sauce_txt_1" },
    { nameof(Define.SourceNames.칠리소스), "sauce_txt_2" },
    { nameof(Define.SourceNames.머스타드소스), "sauce_txt_3" },
    { nameof(Define.SourceNames.마요네즈소스),"sauce_txt_4" },
};
    private Dictionary<string, string> enumToString_SourceCardImageDict = new Dictionary<string, string>()
{
    { nameof(Define.SourceNames.바베큐소스), "sauce_type_1" },
    { nameof(Define.SourceNames.칠리소스), "sauce_type_2" },
    { nameof(Define.SourceNames.머스타드소스), "sauce_type_3" },
    { nameof(Define.SourceNames.마요네즈소스),"sauce_type_4" },
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
