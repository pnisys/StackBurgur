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

    enum MaterialNames
    {
        �ܹ��Ż�,
        �Թ���,
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

    enum GameObjects
    {
        BurgurNameParent
    }

    GameObject burgurNameParent;
    Dictionary<string, Stack<string>> burgurdic = new Dictionary<string, Stack<string>>()
    {
        { TutorialBurgurNames.��������.ToString(),new Stack<string>(new[]{MaterialNames.�ܹ��Ż�.ToString(),MaterialNames.�Ұ��.ToString(),MaterialNames.�ܹ��Ż�.ToString() }) }
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
