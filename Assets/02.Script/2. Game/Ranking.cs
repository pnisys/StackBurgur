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
            Debug.Log("������");

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
            //����� �ְ������� �̸��� ��������
            bestScore[i] = PlayerPrefs.GetInt(i + "BestScore");
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
                PlayerPrefs.SetInt(i + "BestScore", currentScore);
                PlayerPrefs.SetString(i.ToString() + "BestName", currentName);

                //���� �ݺ��� ���� �غ�
                currentScore = tmpScore;
                currentName = tmpName;

            }
        }
        //��ŷ�� ���� ����, �̸� ����
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


