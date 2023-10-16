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

    enum GameObjects
    {
        BurgurNameParent
    }

    GameObject burgurNameParent;

    private Dictionary<string, string> spriteMappings = new Dictionary<string, string>()
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
            string materialName = item.ToString(); // MaterialNames ��� �̸��� ����
            materialSprites.Add(materialName, Managers.Resource.Load<Sprite>($"{spriteFolderPath}{spriteMappings[materialName]}"));
        }

        Setting(currentLevel);
    }

    private Dictionary<string, string[]> burgurDict = new Dictionary<string, string[]>()
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


    //��������, �ܹ��Ż�(1f), �Ұ��(2f), �ܹ��Ż�(3f)
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
            float yPos = -36.9f + i * 4.3f; // �� ���� ������ 4.3f�� ����
            go.transform.localPosition = new Vector3(6.1f, yPos, 0f);
            go.transform.localScale = new Vector3(0.44315f, 0.04390998f, 0.44315f);
            Image image = Util.GetOrAddComponet<Image>(go);
            string materialName = burgurDict["��������"][i]; // ���� ������ ��� �̸��� ������

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
            if (materialSprites.TryGetValue(materialName, out result))
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
