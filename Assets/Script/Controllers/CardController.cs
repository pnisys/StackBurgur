using Oculus.Voice.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardController : UI_Scene
{
    Levels currentLevel = Levels.Tutorial;
    enum Levels
    {
        Tutorial,
        Level1,
        Level2,
        Level3,
        Level4,
    }
    enum Texts
    {
        Text_BurgurName
    }

    enum Images
    {
        Image_Burgur,
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

    enum GameObjects
    {
        BurgurNameParent
    }

    GameObject burgurNameParent;

    private Dictionary<string, string> spriteMappings = new Dictionary<string, string>()
        {
           { MaterialNames.햄버거빵위.ToString(), "HamburgurBreadUp" },
            { MaterialNames.햄버거빵아래.ToString(), "HamburgurBreadDown" },
            { MaterialNames.먹물빵위.ToString(), "BlackburgurBreadUp" },
            { MaterialNames.먹물빵아래.ToString(), "BlackburgurBreadDown" },
            { MaterialNames.샌드위치빵위.ToString(), "HamburgurBreadUp" },
            { MaterialNames.샌드위치빵아래.ToString(), "HamburgurBreadDown" },
            { MaterialNames.베이컨.ToString(), "Bacon" },
            { MaterialNames.불고기.ToString(), "Bulmeat" },
            { MaterialNames.치즈.ToString(), "Cheeze" },
            { MaterialNames.치킨.ToString(), "Chicken" },
            { MaterialNames.버섯.ToString(), "Mushroom" },
            { MaterialNames.양파.ToString(), "Onion" },
            { MaterialNames.새우.ToString(), "Shrimp" },
            { MaterialNames.토마토.ToString(), "Tomato" },
            { MaterialNames.양상추.ToString(), "Lettuce" }
        };
    private Dictionary<string, Sprite> materialSprites = new Dictionary<string, Sprite>();
    private string spriteFolderPath = "Art/Image/BurgurMaterialsSprite/";

    private void Start()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        TextMeshProUGUI burgurNameText = GetText((int)Texts.Text_BurgurName);
        burgurNameParent = GetGameObject((int)GameObjects.BurgurNameParent);

        foreach (MaterialNames item in Enum.GetValues(typeof(MaterialNames)))
        {
            string materialName = item.ToString(); // MaterialNames 멤버 이름을 얻음
            materialSprites.Add(materialName, Managers.Resource.Load<Sprite>($"{spriteFolderPath}{spriteMappings[materialName]}"));
        }

        Setting(currentLevel);
    }

    private Dictionary<string, string[]> burgurDict = new Dictionary<string, string[]>()
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


    //데리버거, 햄버거빵(1f), 불고기(2f), 햄버거빵(3f)
    private void Setting(Levels currentLevel)
    {
        int value = currentLevel == Levels.Tutorial ? 3 :
            currentLevel == Levels.Level1 ? 4 : currentLevel == Levels.Level2 ? 5 :
            currentLevel == Levels.Level3 ? 6 : 7;

        
        switch (currentLevel)
        {
            
        }
        for (int i = 0; i < value; i++)
        {
            GameObject go = new GameObject { name = $"{i + 1}F" };
            go.transform.SetParent(burgurNameParent.transform);
            float yPos = -36.9f + i * 4.3f; // 층 간의 간격은 4.3f로 가정
            go.transform.localPosition = new Vector3(6.1f, yPos, 0f);
            go.transform.localScale = new Vector3(0.44315f, 0.04390998f, 0.44315f);
            Image image = Util.GetOrAddComponet<Image>(go);
            string materialName = burgurDict["데리버거"][i]; // 현재 버거의 재료 이름을 가져옴

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
            if (materialSprites.TryGetValue(materialName, out result))
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
