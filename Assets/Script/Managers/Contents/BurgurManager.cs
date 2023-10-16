using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgurManager
{
    //버거 이름과 currentLevel을 넘겨주면
    //여기서 각각 이미지 파일과 기타 정보들을 CardController로 넘겨주면 됨
    //그럼 파일들은 얘가 다 갖고 있어야 함

    enum MaterialNames
    {
        햄버거빵위,
        햄버거빵아래,
        먹물빵위,
        먹물빵아래,
        샌드위치빵위,
        샌드위치빵아래,
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

    enum TutorialBurgurNames
    {
        데리버거,
    }

    enum Level1BurgurNames
    {
        새우버거,
        토토버거,
        토마토패티버거,
        더블치즈버거,
        베이컨버거,
        머쉬룸버거,
        촉촉한버거,
        불고기버거
    }
    enum Level2BurgurNames
    {
        치즈불고기버거,
        샌드위치햄버거,
        치킨버거,
        트리플고기버거,
        먹물버거,
        양파버거,
        육즙가득버거,
        버섯많이버거
    }
    enum Level3BurgurNames
    {
        살찌는버거,
        치즈토마토버거,
        샌드위치치즈버거,
        다이어트버거,
        고칼로리버거,
        더블새우버거,
        직화버섯불고기버거,
        치즈새우버거
    }
    enum Level4BurgurNames
    {
        기네스버거,
        수분가득버거,
        통큰버거,
        비주얼버거,
        폭탄칼로리버거,
        일편단심버거,
        기력보충버거,
        빵많이버거
    }

    private const string materialSpriteFolderPath = "Art/Image/BurgurMaterialsSprite/";
    private const string burgurSpriteFolderPath_Tutorial = "Art/Image/BurgurImageSprite/";
    private const string burgurSpriteFolderPath_Level1 = "Art/Image/BurgurImageSprite/1stage/";
    private const string burgurSpriteFolderPath_Level2 = "Art/Image/BurgurImageSprite/2stage/";
    private const string burgurSpriteFolderPath_Level3 = "Art/Image/BurgurImageSprite/3stage/";
    private const string burgurSpriteFolderPath_Level4 = "Art/Image/BurgurImageSprite/4stage/";

    public Dictionary<string, int> BurgurLevelDict = new Dictionary<string, int>()
{
    // TutorialBurgurNames
    { nameof(TutorialBurgurNames.데리버거), 0 },

    // Level1BurgurNames
    { nameof(Level1BurgurNames.새우버거), 1 },
    { nameof(Level1BurgurNames.토토버거), 1 },
    { nameof(Level1BurgurNames.토마토패티버거), 1 },
    { nameof(Level1BurgurNames.더블치즈버거), 1 },
    { nameof(Level1BurgurNames.베이컨버거), 1 },
    { nameof(Level1BurgurNames.머쉬룸버거), 1 },
    { nameof(Level1BurgurNames.촉촉한버거), 1 },
    { nameof(Level1BurgurNames.불고기버거), 1 },

    // Level2BurgurNames
    { nameof(Level2BurgurNames.치즈불고기버거), 2 },
    { nameof(Level2BurgurNames.샌드위치햄버거), 2 },
    { nameof(Level2BurgurNames.치킨버거), 2 },
    { nameof(Level2BurgurNames.트리플고기버거), 2 },
    { nameof(Level2BurgurNames.먹물버거), 2 },
    { nameof(Level2BurgurNames.양파버거), 2 },
    { nameof(Level2BurgurNames.육즙가득버거), 2 },
    { nameof(Level2BurgurNames.버섯많이버거), 2 },

    // Level3BurgurNames
    { nameof(Level3BurgurNames.살찌는버거), 3 },
    { nameof(Level3BurgurNames.치즈토마토버거), 3 },
    { nameof(Level3BurgurNames.샌드위치치즈버거), 3 },
    { nameof(Level3BurgurNames.다이어트버거), 3 },
    { nameof(Level3BurgurNames.고칼로리버거), 3 },
    { nameof(Level3BurgurNames.더블새우버거), 3 },
    { nameof(Level3BurgurNames.직화버섯불고기버거), 3 },
    { nameof(Level3BurgurNames.치즈새우버거), 3 },

    // Level4BurgurNames
    { nameof(Level4BurgurNames.기네스버거), 4 },
    { nameof(Level4BurgurNames.수분가득버거), 4 },
    { nameof(Level4BurgurNames.통큰버거), 4 },
    { nameof(Level4BurgurNames.비주얼버거), 4 },
    { nameof(Level4BurgurNames.폭탄칼로리버거), 4 },
    { nameof(Level4BurgurNames.일편단심버거), 4 },
    { nameof(Level4BurgurNames.기력보충버거), 4 },
    { nameof(Level4BurgurNames.빵많이버거), 4 },
};

    private Dictionary<string, string> enumToString_BurgurCardNameDict = new Dictionary<string, string>()
{
    { nameof(MaterialNames.햄버거빵위), "HamburgurBreadUp" },
    { nameof(MaterialNames.햄버거빵아래), "HamburgurBreadDown" },
    { nameof(MaterialNames.먹물빵위), "BlackburgurBreadUp" },
    { nameof(MaterialNames.먹물빵아래), "BlackburgurBreadDown" },
    { nameof(MaterialNames.샌드위치빵위), "HamburgurBreadUp" },
    { nameof(MaterialNames.샌드위치빵아래), "HamburgurBreadDown" },
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
        "데리버거", new string[] { "햄버거빵", "불고기", "햄버거빵" }
    },
    {
        "새우버거", new string[] { "먹물빵", "양상추", "새우", "먹물빵" }
    },
     {
        "토토버거", new string[] { "샌드위치빵", "토마토", "토마토","샌드위치빵" }
    },
    {
        "토마토패티버거", new string[] { "샌드위치빵", "불고기", "토마토", "불고기" }
    }, {
        "더블치즈버거", new string[] { "햄버거빵", "치즈", "치즈", "햄버거빵" }
    },
    {
        "베이컨버거", new string[] { "먹물빵", "불고기", "베이컨", "먹물빵" }
    }, {
        "머쉬룸버거", new string[] { "햄버거빵", "불고기", "버섯","먹물빵"  }
    },
    {
        "촉촉한버거", new string[] { "햄버거빵", "불고기", "양파", "햄버거빵" }
    }, {
        "불고기버거", new string[] { "샌드위치빵", "양상추", "불고기", "햄버거빵" }
    },
    {
        "치즈불고기버거", new string[] { "햄버거빵", "불고기", "치즈", "양상추","햄버거빵" }
    },
     {
        "샌드위치햄버거", new string[] { "샌드위치빵", "불고기", "치킨", "치즈", "샌드위치빵" }
    },
    {
        "치킨버거", new string[] { "샌드위치빵", "치킨", "치즈", "토마토", "샌드위치빵" }
    },
        {
        "트리플고기버거", new string[] { "먹물빵", "치킨", "불고기", "베이컨", "샌드위치빵" }
    },
          {
        "먹물버거", new string[] { "먹물빵", "새우", "불고기", "양상추", "먹물빵" }
    },
              {
        "양파버거", new string[] { "양상추", "양파", "새우", "양파", "먹물빵" }
    },
                 {
        "육즙가득버거", new string[] { "불고기", "양파", "양상추", "토마토", "불고기" }
    },           {
        "버섯많이버거", new string[] { "샌드위치빵", "버섯", "불고기", "버섯", "햄버거빵" }
    },
                   {
        "살찌는버거", new string[] { "샌드위치빵", "치즈", "치킨", "양상추", "불고기","햄버거빵" }
    },
         {
    "치즈토마토버거", new string[] { "햄버거빵", "양상추", "토마토", "치즈", "불고기","햄버거빵" }
},
{
    "샌드위치치즈버거", new string[] { "햄버거빵", "불고기", "치즈", "토마토", "양상추", "샌드위치빵" }
},
{
    "다이어트버거", new string[] { "양상추", "샌드위치빵", "치즈", "토마토", "치즈", "양상추" }
},
{
    "고칼로리버거", new string[] { "먹물빵", "치즈", "베이컨", "새우", "불고기", "먹물빵" }
},
{
    "더블새우버거", new string[] { "샌드위치빵", "새우", "베이컨", "양상추", "새우", "햄버거빵" }
},
{
    "직화버섯불고기버거", new string[] { "먹물빵", "버섯", "불고기", "치즈", "양파", "햄버거빵" }
},
{
    "치즈새우버거", new string[] { "샌드위치빵", "치즈", "양파", "베이컨", "새우", "햄버거빵" }
},
{
    "기네스버거", new string[] { "먹물빵", "불고기", "치즈", "치즈", "불고기", "양파", "먹물빵" }
},
{
    "수분가득버거", new string[] { "햄버거빵", "베이컨", "양파", "양상추", "새우", "버섯", "햄버거빵" }
},
{
    "통큰버거", new string[] { "불고기", "양상추", "양파", "베이컨", "새우", "버섯", "불고기", "양상추" }
},
{
    "비주얼버거", new string[] { "햄버거빵", "양상추", "치즈", "베이컨", "불고기", "양파", "샌드위치빵" }
},
{
    "폭탄칼로리버거", new string[] { "먹물빵", "양상추", "치즈", "불고기", "치즈", "불고기", "먹물빵" }
},
{
    "일편단심버거", new string[] { "양상추", "베이컨", "양파", "새우", "양파", "불고기", "양상추" }
},
{
    "기력보충버거", new string[] { "샌드위치빵", "버섯", "베이컨", "양파", "새우", "버섯", "샌드위치빵" }
},
{
    "빵많이버거", new string[] { "먹물빵", "치킨", "베이컨", "양파", "샌드위치빵", "불고기", "햄버거빵" }
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
