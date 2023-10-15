using Oculus.Voice.Demo;
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
        햄버거빵,
        먹물빵,
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

    enum GameObjects
    {
        BurgurNameParent
    }

    GameObject burgurNameParent;
    Dictionary<string, Stack<string>> burgurdic = new Dictionary<string, Stack<string>>()
    {
        { TutorialBurgurNames.데리버거.ToString(),new Stack<string>(new[]{MaterialNames.햄버거빵.ToString(),MaterialNames.불고기.ToString(),MaterialNames.햄버거빵.ToString() }) }
    };
    Stack<string> burgurStack = new Stack<string>();
    Dictionary<string,Sprite> sprite = new Dictionary<string,Sprite>();

    private void Start()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

        TextMeshProUGUI burgurNameText = GetText((int)Texts.Text_BurgurName);
        burgurNameParent = GetGameObject((int)GameObjects.BurgurNameParent);

        Setting(currentLevel);
    }

    private void Setting(Levels currentLevel)
    {
        int value = currentLevel == Levels.Tutorial ? 3 :
            currentLevel == Levels.Level1 ? 4 : currentLevel == Levels.Level2 ? 5 :
            currentLevel == Levels.Level3 ? 6 : 7;

        for (int i = 0; i < value; i++)
        {
            GameObject go = new GameObject { name = $"{i + 1}F" };
            go.transform.SetParent(burgurNameParent.transform);
            go.transform.localScale = Vector3.one;

            GameObject go2 = new GameObject { name = $"{i + 1}BurgurImage" };
            Util.GetOrAddComponet<Image>(go2);
            go2.transform.SetParent(go.transform);

            switch (currentLevel)
            {
                case Levels.Tutorial:
                    break;
                case Levels.Level1:
                    break;
                case Levels.Level2:
                    break;
                case Levels.Level3:
                    break;
                case Levels.Level4:
                    break;
            }
        }

    }
}
