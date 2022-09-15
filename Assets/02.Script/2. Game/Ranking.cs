using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ranking : MonoBehaviour
{

    public TextMeshProUGUI rankNameCurrent;
    public TextMeshProUGUI rankScoreCurrent;

    private void OnEnable()
    {
        PeopleAnimator.OnLifeDie += this.ScoreSet;
        PeopleAnimator.OnLifeDie += this.SeeScore;

    }
    private void OnDisable()
    {
        PeopleAnimator.OnLifeDie -= this.ScoreSet;
        PeopleAnimator.OnLifeDie -= this.SeeScore;
    }
    public float[] bestScore = new float[5];
    public string[] bestName = new string[5];


    public string[] rankName;
    public float[] rankScore;

    public TextMeshProUGUI[] rankNameText;
    public TextMeshProUGUI[] rankScoreText;

    public GameManager gamemanager;


    public void ScoreSet()
    {
        float currentScore = gamemanager.score;
        string currentName = SoundManager.instance.username;

        PlayerPrefs.SetString("CurrentPlayerName", currentName);
        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

        float tmpScore = 0f;
        string tmpName = "";

        for (int i = 0; i < 5; i++)
        {
            //저장된 최고점수와 이름을 가져오기
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            bestName[i] = PlayerPrefs.GetString(i + "BestName");

            //현재 점수가 랭킹에 오를 수 있을 때
            while (bestScore[i] < currentScore)
            {
                //자리 바꾸기
                tmpScore = bestScore[i];
                tmpName = bestName[i];
                bestScore[i] = currentScore;
                bestName[i] = currentName;

                //랭킹에 저장
                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

                //다음 반복을 위한 준비
                currentScore = tmpScore;
                currentName = tmpName;

            }
        }
        //랭킹에 맞춰 점수, 이름 저장
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetString(i.ToString() + "BestName", bestName[i]);
        }
    }

    public void SeeScore()
    {
        rankNameCurrent.text = PlayerPrefs.GetString("CurrentPlayerName");
        rankScoreCurrent.text = PlayerPrefs.GetFloat("CurrentPlayerScore").ToString();

        for (int i = 0; i < 5; i++)
        {
            rankScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            rankScoreText[i].text = rankScore[i].ToString();
            rankName[i] = PlayerPrefs.GetString(i.ToString() + "BestName");
            rankNameText[i].text = rankName[i];

            if (rankScoreCurrent.text == rankScoreText[i].text)
            {
                Color Rank = new Color(255, 255, 0);
                rankNameText[i].color = Rank;
                rankScoreText[i].color = Rank;
            }
        }
    }
}


