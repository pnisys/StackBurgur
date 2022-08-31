using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null) //instance가 null. 즉, 시스템상에 존재하고 있지 않을때
        {
            instance = this; //내자신을 instance로 넣어줍니다.
            DontDestroyOnLoad(gameObject); //OnLoad(씬이 로드 되었을때) 자신을 파괴하지 않고 유지
        }
        else
        {
            if (instance != this) //instance가 내가 아니라면 이미 instance가 하나 존재하고 있다는 의미
                Destroy(this.gameObject); //둘 이상 존재하면 안되는 객체이니 방금 AWake된 자신을 삭제
        }
    }

    #region 변수
    //1. 점원이 매대 앞에 서면
    public bool isClerk = false;
    //2. 점원이 생각하는 중
    public bool isThinking = false;
    //3. 주문 들어옴
    public bool isOrder = false;
    //4. 선택한 버거 순간
    public bool isSelect = false;

    public GameObject[] people;
    public Animator animator;

    public int score = 0;
    public int lifescore = 5;

    int ran = 0;
    int ran1 = 0;

    //생각 풍선 보여주는 시간
    public float orderlimitTime = 15;
    //주문 시간 30초
    public float limitTime = 30;

    public GameObject TutorialLevelBurgerCard;
    public GameObject[] OneLevelBurgerCard;
    public GameObject[] TwoLevelBurgerCard;
    public GameObject[] ThreeLevelBurgerCard;
    public GameObject[] FourLevelBurgerCard;
    public GameObject[] sourceCard;

    public GameObject selecthambugurcard;
    public GameObject selectsourcecard;

    public GameObject[] breadmaterial;
    public GameObject[] vegetablematerial;
    public GameObject[] meatmaterial;
    public GameObject[] addmaterial;
    public GameObject[] sourcematerial;


    public List<int> hambugernumber = new List<int>();
    public List<int> sourcenumber = new List<int>();

    //level이 햄버거 난이도, 튜토리얼 난이도부터 4난이도까지 있음
    public int level = 0;
    //stage가 단계, 1단계~5단계까지 있음 -> 튜토리얼 단계는 없음
    public int stage = 1;

    #endregion

    //난이도에 맞는 카드를 Setactiove 하고, selecthambugurcard에 넣는 함수
    public void LevelBurgurSetting()
    {
        //레벨이 햄버거 난이도, 0번이 선택된 버거
        //튜토리얼 버거
        if (level == 0)
        {
            TutorialLevelBurgerCard.SetActive(true);
            selecthambugurcard = TutorialLevelBurgerCard;
        }
        else if (level == 1)
        {
            OneLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = OneLevelBurgerCard[hambugernumber[0]];
        }
        else if (level == 2)
        {
            TwoLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = TwoLevelBurgerCard[hambugernumber[0]];

        }
        else if (level == 3)
        {
            ThreeLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = ThreeLevelBurgerCard[hambugernumber[0]];

        }
        else if (level == 4)
        {
            FourLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = FourLevelBurgerCard[hambugernumber[0]];
        }

        sourceCard[sourcenumber[0]].SetActive(true);
        selectsourcecard = sourceCard[sourcenumber[0]];
    }

    //8개중에 랜덤한 숫자 골라내는 함수
    public void RandomNumberSelect()
    {
        for (int i = 0; i < 8; i++)
        {
            hambugernumber.Add(i);
        }
        for (int i = 0; i < 8; i++)
        {
            int temp = hambugernumber[i];
            ran = UnityEngine.Random.Range(0, 7);
            hambugernumber[i] = hambugernumber[ran];
            hambugernumber[ran] = temp;
        }

        for (int i = 0; i < 4; i++)
        {
            sourcenumber.Add(i);
        }

        for (int i = 0; i < 4; i++)
        {
            int temp1 = sourcenumber[i];
            ran1 = UnityEngine.Random.Range(0, 3);
            sourcenumber[i] = sourcenumber[ran1];
            sourcenumber[ran1] = temp1;
        }
    }
}
