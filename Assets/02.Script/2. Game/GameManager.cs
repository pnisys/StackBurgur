using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    //1. 점원이 매대 앞에 서면
    public bool isClerk = false;
    //2. 점원이 생각하는 중
    public bool isThinking = false;
    //3. 주문 들어옴
    public bool isOrder = false;
    //4. 선택한 버거 순간
    public bool isSelect = false;

    public bool iscompletesuccess = false;
    public bool islittlesuccess = false;
    public bool isfail = false;

    public bool iscompletesuccess2 = false;
    public bool islittlesuccess2 = false;
    public bool isfail2 = false;

    public bool isTray = false;

    public GameObject[] people;
    public List<GameObject> uppeople = new List<GameObject>();
    public int tablepeoplenumbur = 0;
    public int peoplenumbur = 0;

    //public Animator animator;

    public int score = 0;
    public int lifescore = 5;

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

    public GameObject selecthambugurcard;
    public GameObject selecthambugurcard2;
    public GameObject selectsourcecard;
    public GameObject selectsourcecard2;


    public bool[] istable = new bool[10] { true, true, true, true, false, false, false, false, false, false };
    public Vector3[] tableposition = new Vector3[10] { new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0.051f, 0.32f, -0.075f), new Vector3(0, 0.278f, 0) };
    public Quaternion[] tablerotation = new Quaternion[10] { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 29.878f, 0), Quaternion.Euler(0, 58.383f, 0) };
}
