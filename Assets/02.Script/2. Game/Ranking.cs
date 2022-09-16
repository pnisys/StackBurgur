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
    public int[] bestScore = new int[5];
    public string[] bestName = new string[5];


    public string[] rankName;
    public float[] rankScore;

    public TextMeshProUGUI[] rankNameText;
    public TextMeshProUGUI[] rankScoreText;

    public GameManager gamemanager;
    public GameObject avatarManager;

    IEnumerator Start()
    {
        avatarManager = GameObject.Find("SoundManager");
        yield return new WaitUntil(() => avatarManager != null);

        ScoreSet();
    }
    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("다지움");

            for (int i = 0; i < 7; i++)
            {
                PlayerPrefs.SetString(i.ToString() + "BestName", rankNameText[i].text);
                PlayerPrefs.SetInt(i + "BestScore", int.Parse(rankScoreText[i].text));
            }
        }
    }

    public void ScoreSet()
    {
        int currentScore = gamemanager.score;
        string currentName = avatarManager.GetComponent<RankAvatarManager>()._saveName;

        PlayerPrefs.SetString("CurrentPlayerName", currentName);
        PlayerPrefs.SetInt("CurrentPlayerScore", currentScore);

        int tmpScore = 0;
        string tmpName = "";

        for (int i = 0; i < 7; i++)
        {
            //저장된 최고점수와 이름을 가져오기
            bestScore[i] = PlayerPrefs.GetInt(i + "BestScore");
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
                PlayerPrefs.SetInt(i + "BestScore", currentScore);
                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

                //다음 반복을 위한 준비
                currentScore = tmpScore;
                currentName = tmpName;

            }
        }
        //랭킹에 맞춰 점수, 이름 저장
        for (int i = 0; i < 7; i++)
        {
            PlayerPrefs.SetInt(i + "BestScore", bestScore[i]);
            PlayerPrefs.SetString(i.ToString() + "BestName", bestName[i]);
        }

        //SeeScore();
        avatarManager.GetComponent<RankAvatarManager>().ShowRankBoard();
    }


    public void SeeScore()
    {
        rankNameCurrent.text = PlayerPrefs.GetString("CurrentPlayerName");
        rankScoreCurrent.text = PlayerPrefs.GetInt("CurrentPlayerScore").ToString();

        for (int i = 0; i < 7; i++)
        {
            rankScore[i] = PlayerPrefs.GetInt(i + "BestScore");
            rankScoreText[i].text = rankScore[i].ToString();
            rankName[i] = PlayerPrefs.GetString(i.ToString() + "BestName");
            rankNameText[i].text = rankName[i];

            //if (rankScoreCurrent.text == rankScoreText[i].text)
            //{
            //    Color Rank = new Color(233, 116, 0, 255);
            //    rankNameText[i].color = Rank;
            //    rankScoreText[i].color = Rank;
            //}
        }
    }
}


