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
            //����� �ְ������� �̸��� ��������
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
            bestName[i] = PlayerPrefs.GetString(i + "BestName");

            //���� ������ ��ŷ�� ���� �� ���� ��
            while (bestScore[i] < currentScore)
            {
                //�ڸ� �ٲٱ�
                tmpScore = bestScore[i];
                tmpName = bestName[i];
                bestScore[i] = currentScore;
                bestName[i] = currentName;

                //��ŷ�� ����
                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

                //���� �ݺ��� ���� �غ�
                currentScore = tmpScore;
                currentName = tmpName;

            }
        }
        //��ŷ�� ���� ����, �̸� ����
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


