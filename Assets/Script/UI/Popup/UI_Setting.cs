using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Setting : UI_Popup
{
    enum Buttons
    {
        BackButton
    }
    enum Sliders
    {
        BgmSettingSlider,
        EffectSettingSlider
    }

    enum Texts
    {
        BgmSettingText,
        EffectSettingText,
    }

    enum GameObjects
    {
        SoundSettingObject
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
        Bind<Slider>(typeof(Sliders));

        Slider bgmSettingSlider = GetSlider((int)Sliders.BgmSettingSlider);
        Slider effectSettingSlider = GetSlider((int)Sliders.EffectSettingSlider);
        Button backButton = GetButton((int)Buttons.BackButton);
        backButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        backButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Util.FindChild<UI_Button>(Managers.UI.Root).gameObject.SetActive(true);
            Managers.UI.ClosePopupUI();
        }, Define.UIEvent.Click);

        bgmSettingSlider.gameObject.AddUIEvnet((PointerEventData) => Managers.Sound.Play("Click_Button"), Define.UIEvent.Highlight);
        effectSettingSlider.gameObject.AddUIEvnet((PointerEventData) => Managers.Sound.Play("Click_Button"), Define.UIEvent.Highlight);

        bgmSettingSlider.minValue = 0;
        bgmSettingSlider.maxValue = 1;
        effectSettingSlider.minValue = 0;
        effectSettingSlider.maxValue = 1;
        bgmSettingSlider.onValueChanged.AddListener(OnBgmSliderValueChanged);
        effectSettingSlider.onValueChanged.AddListener(OnEffectSliderValueChanged);
    }

    private void OnEffectSliderValueChanged(float value)
    {
        AudioSource audioSource;
        audioSource = Managers.Sound.GetAudioSource(Define.Sound.Effect);
        audioSource.volume = value;
    }

    private void OnBgmSliderValueChanged(float value)
    {
        AudioSource audioSource;
        audioSource = Managers.Sound.GetAudioSource(Define.Sound.Bgm);
        audioSource.volume = value;
    }
}
