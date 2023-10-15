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

    enum TutorialBurgurName
    {

    }

    enum Level1BurgurName
    {

    }
    enum Level2BurgurName
    {

    }
    enum Level3BurgurName
    {

    }
    enum Level4BurgurName
    {

    }

    enum GameObjects
    {
        BurgurNameParent
    }

    GameObject burgurNameParent;
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
        int value = currentLevel == Levels.Tutorial ? 4 :
            currentLevel == Levels.Level1 ? 5 : currentLevel == Levels.Level2 ? 6 :
            currentLevel == Levels.Level3 ? 7 : 8;

        for (int i = 0; i < value; i++)
        {
            GameObject go = new GameObject { name = $"{i + 1}F" };
            go.transform.SetParent(burgurNameParent.transform);
            go.transform.localScale = Vector3.one;

            GameObject go2 = new GameObject { name = $"{i + 1}BurgurImage" };
            Util.GetOrAddComponet<Image>(go2);
            go2.transform.SetParent(go.transform);
        }

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
