using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UI_Button : UI_Scene
{
    enum Buttons
    {
        TutorialStartButton,
        GameStartButton,
        SettingButton
    }
    enum Images
    {

    }

    enum Texts
    {
        TutorialStartText,
        GameStartText,
        SettingText
    }

    enum GameObjects
    {
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.TutorialStartButton).gameObject.AddUIEvnet((PointerEventData) => Debug.Log("Å¬¸¯"));
    }
}
