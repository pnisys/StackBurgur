using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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

        AvatarStat lastAvatarStat = Managers.Data.GetAvatarStat();

        foreach (Buttons item in Enum.GetValues(typeof(Buttons)))
        {
            Button currentButton = GetButton((int)item);

            currentButton.gameObject.AddUIEvnet((PointerEventData) =>
            {
                Managers.Sound.Play("Click_Button");
            }, Define.UIEvent.Highlight);
        }

        backButton.gameObject.AddUIEvnet((PointerEventData) =>
            {
                Util.FindChild<UI_Button>(Managers.UI.Root).gameObject.SetActive(true);
                Managers.UI.ClosePopupUI();
            }, Define.UIEvent.Click);


        confirmButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Data.SaveToJson(lastAvatarStat);
            Managers.Scene.LoadScene(Define.SceneType.Game);
        }, Define.UIEvent.Click);


        #region 왼쪽, 오른쪽 버튼 이벤트
        leftSkinButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            // lastAvatarStat.skinNumber가 0보다 클 때만 로직을 실행
            if (lastAvatarStat.skinNumber > 0)
            {
                // skinNumber 감소
                lastAvatarStat.skinNumber--;

                // sprite 업데이트
                skinImage.sprite = skins[lastAvatarStat.skinNumber];
                skinText.text = $"피부/얼굴형{lastAvatarStat.skinNumber + 1}";
            }
        }, Define.UIEvent.Click);


        rightSkinButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            // lastAvatarStat.skinNumber가 0보다 클 때만 로직을 실행
            if (lastAvatarStat.skinNumber < skins.Length - 1)
            {
                // skinNumber 감소
                lastAvatarStat.skinNumber++;

                // sprite 업데이트
                skinImage.sprite = skins[lastAvatarStat.skinNumber];
                skinText.text = $"피부/얼굴형{lastAvatarStat.skinNumber + 1}";
            }
        }, Define.UIEvent.Click);

        leftHairButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            // lastAvatarStat.skinNumber가 0보다 클 때만 로직을 실행
            if (lastAvatarStat.hairsNumber > 0)
            {
                // skinNumber 감소
                lastAvatarStat.hairsNumber--;

                // sprite 업데이트
                hairImage.sprite = hairs[lastAvatarStat.hairsNumber];
                hairText.text = $"머리{lastAvatarStat.hairsNumber + 1}";
            }
        }, Define.UIEvent.Click);


        rightHairButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            // lastAvatarStat.skinNumber가 0보다 클 때만 로직을 실행
            if (lastAvatarStat.hairsNumber < hairs.Length - 1)
            {
                // skinNumber 감소
                lastAvatarStat.hairsNumber++;

                // sprite 업데이트
                hairImage.sprite = hairs[lastAvatarStat.hairsNumber];
                hairText.text = $"머리{lastAvatarStat.hairsNumber + 1}";
            }
        }, Define.UIEvent.Click);

        leftEyeButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            // lastAvatarStat.skinNumber가 0보다 클 때만 로직을 실행
            if (lastAvatarStat.eyesNumber > 0)
            {
                // skinNumber 감소
                lastAvatarStat.eyesNumber--;

                // sprite 업데이트
                eyeImage.sprite = eyes[lastAvatarStat.eyesNumber];
                eyeText.text = $"표정{lastAvatarStat.eyesNumber + 1}";
            }
        }, Define.UIEvent.Click);


        rightEyeButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            // lastAvatarStat.skinNumber가 0보다 클 때만 로직을 실행
            if (lastAvatarStat.eyesNumber < eyes.Length - 1)
            {
                // skinNumber 감소
                lastAvatarStat.eyesNumber++;

                // sprite 업데이트
                eyeImage.sprite = eyes[lastAvatarStat.eyesNumber];
                eyeText.text = $"표정{lastAvatarStat.eyesNumber + 1}";
            }
        }, Define.UIEvent.Click);


        leftAccButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            // lastAvatarStat.skinNumber가 0보다 클 때만 로직을 실행
            if (lastAvatarStat.accsNumber > 0)
            {
                // skinNumber 감소
                lastAvatarStat.accsNumber--;

                // sprite 업데이트
                accImage.sprite = accs[lastAvatarStat.accsNumber];
                accText.text = $"악세사리{lastAvatarStat.accsNumber + 1}";
            }
        }, Define.UIEvent.Click);


        rightAccButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            // lastAvatarStat.skinNumber가 0보다 클 때만 로직을 실행
            if (lastAvatarStat.accsNumber < accs.Length - 1)
            {
                // skinNumber 감소
                lastAvatarStat.accsNumber++;

                // sprite 업데이트
                accImage.sprite = accs[lastAvatarStat.accsNumber];
                accText.text = $"악세사리{lastAvatarStat.accsNumber + 1}";
            }
        }, Define.UIEvent.Click);

        #endregion
    }
}
