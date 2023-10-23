using Oculus.Voice.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UI_Card_Source : UI_Scene
{
    enum SourceImages
    {
        Image_TitleName,
        Image_Source,
    }
    
    Image image_sourceImage;
    Image image_sourceName;
    private void Start()
    {
        InitSourceCard();
    }
    private void InitSourceCard()
    {
        Bind<Image>(typeof(SourceImages));
        image_sourceImage = GetImage((int)SourceImages.Image_Source);
        image_sourceName = GetImage((int)SourceImages.Image_TitleName);

        image_sourceImage.sprite = Managers.Game.SourceImageDict[Managers.Game.CurrentSource];
        image_sourceName.sprite = Managers.Game.SourceTextNameDict[Managers.Game.CurrentSource];
    }
}
