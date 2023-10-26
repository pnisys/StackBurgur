using Oculus.Voice.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Card_Burgur : UI_Scene
{
    enum Texts
    {
        Text_BurgurName
    }

    enum BurgurImages
    {
        Image_Burgur,
    }

    enum GameObjects
    {
        BurgurNameParent
    }

    GameObject burgurNameParent;
    Image image_Burgur;

    private void Start()
    {
        InitBurgurCard();
    }

    public void InitBurgurCard()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(BurgurImages));
        Bind<GameObject>(typeof(GameObjects));

        TextMeshProUGUI burgurNameText = GetText((int)Texts.Text_BurgurName);
        burgurNameParent = GetGameObject((int)GameObjects.BurgurNameParent);
        image_Burgur = GetImage((int)BurgurImages.Image_Burgur);

        //Todo
        burgurNameText.text = Managers.Game.CurrentBurgur;
        image_Burgur.sprite = Managers.Game.BurgurImageSpriteDict[Managers.Game.CurrentBurgur];

        BurgurCardSetting();
    }

    private void BurgurCardSetting()
    {
        int floor = Managers.Game.CurrentStage + 3;

        for (int i = 0; i < floor; i++)
        {
            GameObject go = new GameObject { name = $"{i + 1}F" };
            go.transform.SetParent(burgurNameParent.transform);
            go.transform.Rotate(0f, 180f, 0f);
            float yPos = -36.9f + i * 4.3f; // Ãþ °£ÀÇ °£°ÝÀº 4.3f·Î °¡Á¤
            go.transform.localPosition = new Vector3(6.1f, yPos, 0f);
            go.transform.localScale = new Vector3(0.44315f, 0.04390998f, 0.44315f);
            Image image = Util.GetOrAddComponet<Image>(go);
            //Todo
            string materialName = Managers.Game.Burgur_Material[i];
            // Á¶°Ç Ãß°¡
            if (i == 0 && materialName.EndsWith("»§") && materialName != "»÷µåÀ§Ä¡»§")
            {
                materialName += "¾Æ·¡";
            }
            else if (i == floor - 1 && materialName.EndsWith("»§") && materialName != "»÷µåÀ§Ä¡»§")
            {
                materialName += "À§";
            }

            image.sprite = Managers.Game.BurgurMaterialSpriteDict[materialName];
        }
    }
}
