using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_NameSelect : UI_Popup
{
    enum Buttons
    {
        BackButton,
        EnterButton,
        BackspaceButton,
        q,
        w,
        e,
        r,
        t,
        y,
        u,
        i,
        o,
        p,
        a,
        s,
        d,
        f,
        g,
        h,
        j,
        k,
        l,
        z,
        x,
        c,
        v,
        b,
        n,
        m,
    }

    enum InputFields
    {
        NameInputField
    }


    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TMP_InputField>(typeof(InputFields));

        Button backButton = GetButton((int)Buttons.BackButton);
        Button EnterButton = GetButton((int)Buttons.EnterButton);
        Button BackSpaceButton = GetButton((int)Buttons.BackspaceButton);
        TMP_InputField nameInputField = Get<TMP_InputField>((int)InputFields.NameInputField);

        backButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Util.FindChild<UI_Button>(Managers.UI.Root).gameObject.SetActive(true);
            Managers.UI.ClosePopupUI();
        });

        backButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        EnterButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.UI.ShowPopupUI<UI_AvatarSelect>(null, new Vector3(0, 0, 2));
        });

        EnterButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        BackSpaceButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            
        });

        BackSpaceButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);

        // q부터 m까지의 버튼에 대해 이벤트를 추가
        for (Buttons btn = Buttons.q; btn <= Buttons.m; btn++)
        {
            Button currentButton = GetButton((int)btn);

            currentButton.gameObject.AddUIEvnet((PointerEventData) =>
            {
                Managers.Sound.Play("Click_Button");
            }, Define.UIEvent.Highlight);
        }
    }
}
