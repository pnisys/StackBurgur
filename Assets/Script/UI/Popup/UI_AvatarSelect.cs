using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_AvatarSelect : UI_Popup
{
    enum Buttons
    {
        ConfirmButton,
        BackButton,
        LeftSkinButton,
        RightSkinButton,
        LeftHairButton,
        RightHairButton,
        LeftEyeButton,
        RightEyeButton,
        LeftAccButton,
        RightAccButton,
    }

    enum Texts
    {
        SkinText,
        HairText,
        EyeText,
        AccessoryText
    }

    enum Images
    {
        SkinImage,
        HairImage,
        EyeImage,
        AccImage
    }

    Sprite[] skins;
    Sprite[] hairs;
    Sprite[] eyes;
    Sprite[] accs;
    

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));

        Button backButton = GetButton((int)Buttons.BackButton);
        Button confirmButton = GetButton((int)Buttons.ConfirmButton);
        Button leftSkinButton = GetButton((int)Buttons.LeftSkinButton);
        Button rightSkinButton = GetButton((int)Buttons.RightSkinButton);
        Button leftHairButton = GetButton((int)Buttons.LeftHairButton);
        Button rightHairButton = GetButton((int)Buttons.RightHairButton);
        Button leftEyeButton = GetButton((int)Buttons.LeftEyeButton);
        Button rightEyeButton = GetButton((int)Buttons.RightEyeButton);
        Button leftAccButton = GetButton((int)Buttons.LeftAccButton);
        Button rightAccButton = GetButton((int)Buttons.RightAccButton);

        skins = Managers.Resource.LoadAll<Sprite>("Art/Image/Avatar/skin");
        hairs = Managers.Resource.LoadAll<Sprite>("Art/Image/Avatar/hair");
        accs = Managers.Resource.LoadAll<Sprite>("Art/Image/Avatar/acc");
        eyes = Managers.Resource.LoadAll<Sprite>("Art/Image/Avatar/eye");

        Image skinImage = GetImage((int)Images.SkinImage);
        Image hairImage = GetImage((int)Images.HairImage);
        Image eyeImage = GetImage((int)Images.EyeImage);
        Image accImage = GetImage((int)Images.AccImage);

        skinImage.sprite = skins[0];
        hairImage.sprite = hairs[0];
        eyeImage.sprite = eyes[0];
        accImage.sprite = accs[0];

        TextMeshProUGUI skinText = GetText((int)Texts.SkinText);
        TextMeshProUGUI hairText = GetText((int)Texts.HairText);
        TextMeshProUGUI eyeText = GetText((int)Texts.EyeText);
        TextMeshProUGUI accText = GetText((int)Texts.AccessoryText);

        backButton.gameObject.AddUIEvnet((PointerEventData) =>
            {
                Util.FindChild<UI_Button>(Managers.UI.Root).gameObject.SetActive(true);
                Managers.UI.ClosePopupUI();
            }, Define.UIEvent.Click);

        backButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        confirmButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        confirmButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.UI.ShowPopupUI<UI_NameSelect>(null, new Vector3(0, 0, 2));
        }, Define.UIEvent.Click);


        leftSkinButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
        }, Define.UIEvent.Click);

        leftSkinButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        rightSkinButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
        }, Define.UIEvent.Click);

        rightSkinButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        leftHairButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
        }, Define.UIEvent.Click);

        leftHairButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        rightHairButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
        }, Define.UIEvent.Click);

        rightHairButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        leftEyeButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
        }, Define.UIEvent.Click);

        leftEyeButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        rightEyeButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
        }, Define.UIEvent.Click);

        rightEyeButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        leftAccButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
        }, Define.UIEvent.Click);

        leftAccButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        rightAccButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
        }, Define.UIEvent.Click);

        rightAccButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);







    }
}
