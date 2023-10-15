using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CardController : UI_Scene
{
    enum Texts
    {
        Text_BurgurName
    }

    enum Images
    {
        Image_Burgur,
        Image_BurgurName
    }

    enum GameObjects
    {
        
    }

    private void Start()
    {
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<GameObject>(typeof(GameObjects));

    }


}
