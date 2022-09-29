using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //1. 점원이 매대 앞에 서면
    public bool isClerk = false;
    //2. 점원이 생각하는 중
    public bool isThinking = false;
    //3. 주문 들어옴
    public bool isOrder = false;
    //4. 4라운드, 5라운드 주문
    public bool isOrder2 = false;

    //4. 선택한 버거 순간
    public bool isSelect = false;

    public bool iscompletesuccess = false;
    public bool islittlesuccess = false;
    public bool isfail = false;

    public bool iscompletesuccess2 = false;
    public bool islittlesuccess2 = false;
    public bool isfail2 = false;

    public bool isTray = false;

    public bool isbutton = false;
    public GameObject[] people;

    //현재 진행중인 사람
    public GameObject currentpeople;
    public TextMeshProUGUI GuideUiText;
    public TextMeshProUGUI GuideUiText2;
    public TextMeshProUGUI GuideUiText3;
    public GameObject[] lifescoreimage;

    public string phase1selectedsource;
    public string phase2selectedsource;

    public List<GameObject> uppeople = new List<GameObject>();
    public int tablepeoplenumbur = 0;
    public int peoplenumbur = 0;

    public delegate void ButtonLifeDie();
    public static event ButtonLifeDie OnButtonLifeDie;

    public int score = 0;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            GuideUiText3.text = score.ToString();
        }
    }
    public int lifescore = 5;
    public int LifeScore
    {
        get
        {
            return lifescore;
        }
        set
        {
            lifescore = value;
            lifescoreimage[lifescore].SetActive(false);
        }
    }

    //생각 풍선 보여주는 시간
    [HideInInspector]
    public float orderlimitTime = 1;
    //주문 시간 30초
    [HideInInspector]
    public float limitTime = 1;

    //level이 햄버거 난이도, 1난이도~ 4난이도까지 있음
    public int level = 1;

    //stage가 단계, 1단계~5단계까지 있음
    public int stage = 1;
    public int Stage
    {
        get
        {
            return stage;
        }
        set
        {
            stage = value;
            GuideUiText.text = "스테이지  " + stage.ToString();
        }
    }
    private void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = SoundManager.instance.bgmSound;
        GuideUiText = gameObject.transform.parent.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        GuideUiText2 = gameObject.transform.parent.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        GuideUiText3 = gameObject.transform.parent.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>();

        GuideUiText.text = "스테이지 : " + stage.ToString();
        GuideUiText3.text = score.ToString();
    }

    public GameObject selecthambugurcard;
    public GameObject selecthambugurcard2;
    public GameObject selectsourcecard;
    public GameObject selectsourcecard2;

    public void Lobby()
    {
        if (isbutton == false)
        {
            isbutton = true;
            OnButtonLifeDie();
        }
        else
        {
            return;
        }
    }

    public bool[] istable = new bool[10] { false, false, false, false, false, false, false, false, false, false };
    public Vector3[] tableposition = new Vector3[10] { new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0.051f, 0.32f, -0.075f), new Vector3(0, 0.278f, 0) };
    public Quaternion[] tablerotation = new Quaternion[10] { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 29.878f, 0), Quaternion.Euler(0, 58.383f, 0) };
}
