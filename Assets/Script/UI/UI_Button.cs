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

        GameObject tutorialButton = GetButton((int)Buttons.TutorialStartButton).gameObject;
        GameObject GameStartButton = GetButton((int)Buttons.GameStartButton).gameObject;
        GameObject SettingButton = GetButton((int)Buttons.SettingButton).gameObject;
        tutorialButton.AddUIEvnet((PointerEventData) => Managers.Scene.LoadScene(Define.Scene.Tutorial));
        tutorialButton.AddUIEvnet((PointerEventData) => Managers.Sound.Play("Click_Button"), Define.UIEvent.Highlight);
        GameStartButton.AddUIEvnet((PointerEventData) => Managers.Scene.LoadScene(Define.Scene.Game));
        GameStartButton.AddUIEvnet((PointerEventData) => Managers.Sound.Play("Click_Button"), Define.UIEvent.Highlight);
        SettingButton.AddUIEvnet((PointerEventData) => Managers.Sound.Play("Click_Button"), Define.UIEvent.Highlight);
    }
}
