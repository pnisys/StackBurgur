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
        GameObject gameStartButton = GetButton((int)Buttons.GameStartButton).gameObject;
        GameObject settingButton = GetButton((int)Buttons.SettingButton).gameObject;
        tutorialButton.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        tutorialButton.AddUIEvnet((PointerEventData) =>
        {
            Managers.Scene.LoadScene(Define.Scene.Tutorial);
        }, Define.UIEvent.Click);

        gameStartButton.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        gameStartButton.AddUIEvnet((PointerEventData) =>
        {
            Managers.UI.ShowPopupUI<UI_AvatarSelect>(null, new Vector3(0, 0, 2));
            gameObject.SetActive(false);
        }, Define.UIEvent.Click);

        settingButton.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        settingButton.AddUIEvnet((PointerEventData) =>
        {
            Managers.UI.ShowPopupUI<UI_Setting>(null, new Vector3(0, 0, 2));
            gameObject.SetActive(false);
        }, Define.UIEvent.Click);

    }
}
