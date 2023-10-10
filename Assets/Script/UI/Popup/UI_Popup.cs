using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup : UI_Base
{
    public virtual void Init()
    {
        Managers.UIManager.SetCanvas(gameObject, true);
    }

    public virtual void ClosePopuiUI()
    {
        Managers.UIManager.ClosePopupUI(this);
    }
}
