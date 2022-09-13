using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ranking : MonoBehaviour
{
    public float[] bestScore1 = new float[5];
    public string[] bestName1 = new string[5];

    public float[] bestScore2 = new float[5];
    public string[] bestName2 = new string[5];

    public float[] bestScore3 = new float[5];
    public string[] bestName3 = new string[5];

    public float[] bestScore4 = new float[5];
    public string[] bestName4 = new string[5];

    public float[] bestScore5 = new float[5];
    public string[] bestName5 = new string[5];

    public TextMeshProUGUI[] stage1name;
    public TextMeshProUGUI[] stage1score;

    public TextMeshProUGUI[] stage2name;
    public TextMeshProUGUI[] stage2score;

    public TextMeshProUGUI[] stage3name;
    public TextMeshProUGUI[] stage3score;

    public TextMeshProUGUI[] stage4name;
    public TextMeshProUGUI[] stage4score;

    public TextMeshProUGUI[] stage5name;
    public TextMeshProUGUI[] stage5score;

    public TextMeshProUGUI[] currentStagename;
    public TextMeshProUGUI[] currentStagescore;

    public GameManager gamemanager;


    //public void ScoreSet(float currentScore, string currentName, float stage)
    //{
    //    if (stage == 1)
    //    {
    //        PlayerPrefs.SetString("CurrentPlayerName", currentName);
    //        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

    //        float tmpScore = 0f;
    //        string tmpName = "";
    //        for (int i = 0; i < 5; i++)
    //        {
    //            //����� �ְ������� �̸��� ��������
    //            bestScore1[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //            bestName1[i] = PlayerPrefs.GetString(i + "BestName");

    //            //���� ������ ��ŷ�� ���� �� ���� ��
    //            while (bestScore1[i] < currentScore)
    //            {
    //                //�ڸ� �ٲٱ�
    //                tmpScore = bestScore1[i];
    //                tmpName = bestName1[i];
    //                bestScore1[i] = currentScore;
    //                bestName1[i] = currentName;

    //                //��ŷ�� ����
    //                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
    //                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

    //                //���� �ݺ��� ���� �غ�
    //                currentScore = tmpScore;
    //                currentName = tmpName;

    //            }
    //        }
    //        //��ŷ�� ���� ����, �̸� ����
    //        for (int i = 0; i < 5; i++)
    //        {
    //            PlayerPrefs.SetFloat(i + "BestScore", bestScore1[i]);
    //            PlayerPrefs.SetString(i.ToString() + "BestName", bestName1[i]);
    //        }
    //    }
    //    else if (stage == 2)
    //    {
    //        PlayerPrefs.SetString("CurrentPlayerName", currentName);
    //        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

    //        float tmpScore = 0f;
    //        string tmpName = "";
    //        for (int i = 0; i < 5; i++)
    //        {
    //            //����� �ְ������� �̸��� ��������
    //            bestScore2[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //            bestName2[i] = PlayerPrefs.GetString(i + "BestName");

    //            //���� ������ ��ŷ�� ���� �� ���� ��
    //            while (bestScore2[i] < currentScore)
    //            {
    //                //�ڸ� �ٲٱ�
    //                tmpScore = bestScore2[i];
    //                tmpName = bestName2[i];
    //                bestScore2[i] = currentScore;
    //                bestName2[i] = currentName;

    //                //��ŷ�� ����
    //                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
    //                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

    //                //���� �ݺ��� ���� �غ�
    //                currentScore = tmpScore;
    //                currentName = tmpName;

    //            }
    //        }
    //        //��ŷ�� ���� ����, �̸� ����
    //        for (int i = 0; i < 5; i++)
    //        {
    //            PlayerPrefs.SetFloat(i + "BestScore", bestScore2[i]);
    //            PlayerPrefs.SetString(i.ToString() + "BestName", bestName2[i]);
    //        }
    //    }
    //    else if (stage == 3)
    //    {
    //        PlayerPrefs.SetString("CurrentPlayerName", currentName);
    //        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

    //        float tmpScore = 0f;
    //        string tmpName = "";
    //        for (int i = 0; i < 5; i++)
    //        {
    //            //����� �ְ������� �̸��� ��������
    //            bestScore3[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //            bestName3[i] = PlayerPrefs.GetString(i + "BestName");

    //            //���� ������ ��ŷ�� ���� �� ���� ��
    //            while (bestScore3[i] < currentScore)
    //            {
    //                //�ڸ� �ٲٱ�
    //                tmpScore = bestScore3[i];
    //                tmpName = bestName3[i];
    //                bestScore3[i] = currentScore;
    //                bestName3[i] = currentName;

    //                //��ŷ�� ����
    //                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
    //                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

    //                //���� �ݺ��� ���� �غ�
    //                currentScore = tmpScore;
    //                currentName = tmpName;

    //            }
    //        }
    //        //��ŷ�� ���� ����, �̸� ����
    //        for (int i = 0; i < 5; i++)
    //        {
    //            PlayerPrefs.SetFloat(i + "BestScore", bestScore3[i]);
    //            PlayerPrefs.SetString(i.ToString() + "BestName", bestName3[i]);
    //        }
    //    }
    //    else if (stage == 4)
    //    {
    //        PlayerPrefs.SetString("CurrentPlayerName", currentName);
    //        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

    //        float tmpScore = 0f;
    //        string tmpName = "";
    //        for (int i = 0; i < 5; i++)
    //        {
    //            //����� �ְ������� �̸��� ��������
    //            bestScore4[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //            bestName4[i] = PlayerPrefs.GetString(i + "BestName");

    //            //���� ������ ��ŷ�� ���� �� ���� ��
    //            while (bestScore4[i] < currentScore)
    //            {
    //                //�ڸ� �ٲٱ�
    //                tmpScore = bestScore4[i];
    //                tmpName = bestName4[i];
    //                bestScore4[i] = currentScore;
    //                bestName4[i] = currentName;

    //                //��ŷ�� ����
    //                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
    //                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

    //                //���� �ݺ��� ���� �غ�
    //                currentScore = tmpScore;
    //                currentName = tmpName;

    //            }
    //        }
    //        //��ŷ�� ���� ����, �̸� ����
    //        for (int i = 0; i < 5; i++)
    //        {
    //            PlayerPrefs.SetFloat(i + "BestScore", bestScore4[i]);
    //            PlayerPrefs.SetString(i.ToString() + "BestName", bestName4[i]);
    //        }
    //    }
    //    else if (stage == 5)
    //    {
    //        PlayerPrefs.SetString("CurrentPlayerName", currentName);
    //        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

    //        float tmpScore = 0f;
    //        string tmpName = "";
    //        for (int i = 0; i < 5; i++)
    //        {
    //            //����� �ְ������� �̸��� ��������
    //            bestScore5[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //            bestName5[i] = PlayerPrefs.GetString(i + "BestName");

    //            //���� ������ ��ŷ�� ���� �� ���� ��
    //            while (bestScore5[i] < currentScore)
    //            {
    //                //�ڸ� �ٲٱ�
    //                tmpScore = bestScore5[i];
    //                tmpName = bestName5[i];
    //                bestScore5[i] = currentScore;
    //                bestName5[i] = currentName;

    //                //��ŷ�� ����
    //                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
    //                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

    //                //���� �ݺ��� ���� �غ�
    //                currentScore = tmpScore;
    //                currentName = tmpName;

    //            }
    //        }
    //        //��ŷ�� ���� ����, �̸� ����
    //        for (int i = 0; i < 5; i++)
    //        {
    //            PlayerPrefs.SetFloat(i + "BestScore", bestScore5[i]);
    //            PlayerPrefs.SetString(i.ToString() + "BestName", bestName5[i]);
    //        }
    //    }
     

    //    //SeeRank(stage);
    //}

    //public void SeeRank(float stage)
    //{

    //    //�÷��̾� �̸�, ���� �ؽ�Ʈ�� ���� '��'�� �̸��� ������ ǥ��
    //    currentStagename[(int)stage - 1].text = PlayerPrefs.GetString("CurrentPlayerName");
    //    currentStagescore[(int)stage - 1].text = string.Format("{0}��", PlayerPrefs.GetFloat("CurrentPlayerScore"));

    //    //��ŷ�� ���� �ҷ��� ������ �̸��� ǥ���ϴ� �κ�
    //    for (int i = 0; i < 5; i++)
    //    {
    //        rankScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //        RankScoreText[i].text = string.Format("{0}��", RankScore[i]);
    //        RankNameCurrent[i].text = PlayerPrefs.GetString(i.ToString() + "BestName");
    //        RankName[i].text = string.Format(rankName[i]);

    //        if (RankScoreCurrent.text == RankScoreText[i].text)
    //        {
    //            Color rank = new Color(255, 255, 0);
    //            RankText[i].color = Rank;
    //            RankNameText[i].color = Rank;
    //            RankScoreText[i].color = Rank;
    //        }
    //    }
    //}
}
