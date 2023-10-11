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
        LeftFaceButton,
        RightFaceButton,
        LeftHairButton,
        RightHairButton,
        LeftEyeButton,
        RightEyeButton,
        LeftAccButton,
        RightAccButton,
    }

    enum Texts
    {
        FaceText,
        HairText,
        EyeText,
        AccessoryText
    }

    enum Images
    {
        FaceImage,
        HairImage,
        EyeImage,
        AccImage
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
        Bind<Image>(typeof(Images));

        Button backButton = GetButton((int)Buttons.BackButton);
        Button confirmButton = GetButton((int)Buttons.ConfirmButton);

        Image faceImage = GetImage((int)Images.FaceImage);

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

        Sprite acc_1 = Managers.Resource.Load<Sprite>("Art/Image/Avatar/acc/acc_1");
        faceImage.sprite = acc_1;


    }
}
