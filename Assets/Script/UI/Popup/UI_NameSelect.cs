using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_NameSelect : UI_Popup
{
    enum Buttons
    {
        BackButton,
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        Button backButton = GetButton((int)Buttons.BackButton);
        backButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.UI.ClosePopupUI();
        });

        backButton.gameObject.AddUIEvnet((PointerEventData) =>
        {
            Managers.Sound.Play("Click_Button");
        }, Define.UIEvent.Highlight);
    }

}
