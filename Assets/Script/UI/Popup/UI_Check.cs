using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Check : UI_Popup
{
    enum Texts
    {
        ScoreText
    }
    enum Panels
    {
        CorrectPanel,
        MyAnserPanel
    }
    void Start()
    {
        Bind<Image>(typeof(Panels));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Image correct = GetImage((int)Panels.CorrectPanel);
        Image myanser = GetImage((int)Panels.MyAnserPanel);
        TextMeshProUGUI scoreText = GetText((int)Texts.ScoreText);

        Managers.Game.ShowCorrect(correct.transform);
        Managers.Game.ShowMyAnswer(myanser.transform);
        scoreText.text = $"{Managers.Game.Score}";
    }
}
