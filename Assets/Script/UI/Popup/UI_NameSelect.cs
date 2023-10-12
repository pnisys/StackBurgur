using System;
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
        ShiftButton,
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
        TMP_InputField nameInputField = Get<TMP_InputField>((int)InputFields.NameInputField);

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
        });

        EnterButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            string id = nameInputField.text;
            Managers.Data.AddAvatarStat(id);
            Managers.UI.ShowPopupUI<UI_AvatarSelect>(null, new Vector3(0, 0, 2));
        });
    }
}
