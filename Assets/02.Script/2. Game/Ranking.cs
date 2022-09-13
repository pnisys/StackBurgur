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
    //            //저장된 최고점수와 이름을 가져오기
    //            bestScore1[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //            bestName1[i] = PlayerPrefs.GetString(i + "BestName");

    //            //현재 점수가 랭킹에 오를 수 있을 때
    //            while (bestScore1[i] < currentScore)
    //            {
    //                //자리 바꾸기
    //                tmpScore = bestScore1[i];
    //                tmpName = bestName1[i];
    //                bestScore1[i] = currentScore;
    //                bestName1[i] = currentName;

    //                //랭킹에 저장
    //                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
    //                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

    //                //다음 반복을 위한 준비
    //                currentScore = tmpScore;
    //                currentName = tmpName;

    //            }
    //        }
    //        //랭킹에 맞춰 점수, 이름 저장
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
    //            //저장된 최고점수와 이름을 가져오기
    //            bestScore2[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //            bestName2[i] = PlayerPrefs.GetString(i + "BestName");

    //            //현재 점수가 랭킹에 오를 수 있을 때
    //            while (bestScore2[i] < currentScore)
    //            {
    //                //자리 바꾸기
    //                tmpScore = bestScore2[i];
    //                tmpName = bestName2[i];
    //                bestScore2[i] = currentScore;
    //                bestName2[i] = currentName;

    //                //랭킹에 저장
    //                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
    //                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

    //                //다음 반복을 위한 준비
    //                currentScore = tmpScore;
    //                currentName = tmpName;

    //            }
    //        }
    //        //랭킹에 맞춰 점수, 이름 저장
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
    //            //저장된 최고점수와 이름을 가져오기
    //            bestScore3[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //            bestName3[i] = PlayerPrefs.GetString(i + "BestName");

    //            //현재 점수가 랭킹에 오를 수 있을 때
    //            while (bestScore3[i] < currentScore)
    //            {
    //                //자리 바꾸기
    //                tmpScore = bestScore3[i];
    //                tmpName = bestName3[i];
    //                bestScore3[i] = currentScore;
    //                bestName3[i] = currentName;

    //                //랭킹에 저장
    //                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
    //                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

    //                //다음 반복을 위한 준비
    //                currentScore = tmpScore;
    //                currentName = tmpName;

    //            }
    //        }
    //        //랭킹에 맞춰 점수, 이름 저장
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
    //            //저장된 최고점수와 이름을 가져오기
    //            bestScore4[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //            bestName4[i] = PlayerPrefs.GetString(i + "BestName");

    //            //현재 점수가 랭킹에 오를 수 있을 때
    //            while (bestScore4[i] < currentScore)
    //            {
    //                //자리 바꾸기
    //                tmpScore = bestScore4[i];
    //                tmpName = bestName4[i];
    //                bestScore4[i] = currentScore;
    //                bestName4[i] = currentName;

    //                //랭킹에 저장
    //                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
    //                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

    //                //다음 반복을 위한 준비
    //                currentScore = tmpScore;
    //                currentName = tmpName;

    //            }
    //        }
    //        //랭킹에 맞춰 점수, 이름 저장
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
    //            //저장된 최고점수와 이름을 가져오기
    //            bestScore5[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //            bestName5[i] = PlayerPrefs.GetString(i + "BestName");

    //            //현재 점수가 랭킹에 오를 수 있을 때
    //            while (bestScore5[i] < currentScore)
    //            {
    //                //자리 바꾸기
    //                tmpScore = bestScore5[i];
    //                tmpName = bestName5[i];
    //                bestScore5[i] = currentScore;
    //                bestName5[i] = currentName;

    //                //랭킹에 저장
    //                PlayerPrefs.SetFloat(i + "BestScore", currentScore);
    //                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

    //                //다음 반복을 위한 준비
    //                currentScore = tmpScore;
    //                currentName = tmpName;

    //            }
    //        }
    //        //랭킹에 맞춰 점수, 이름 저장
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

    //    //플레이어 이름, 점수 텍스트를 현재 '나'의 이름과 점수에 표시
    //    currentStagename[(int)stage - 1].text = PlayerPrefs.GetString("CurrentPlayerName");
    //    currentStagescore[(int)stage - 1].text = string.Format("{0}점", PlayerPrefs.GetFloat("CurrentPlayerScore"));

    //    //랭킹에 맞춰 불러온 점수와 이름을 표시하는 부분
    //    for (int i = 0; i < 5; i++)
    //    {
    //        rankScore[i] = PlayerPrefs.GetFloat(i + "BestScore");
    //        RankScoreText[i].text = string.Format("{0}점", RankScore[i]);
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
