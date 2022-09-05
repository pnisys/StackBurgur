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

    public bool isTray = false;

    public GameObject[] people;
    public int peoplenumbur = 0;

    public Animator animator;

    public int score = 0;
    public int lifescore = 5;

    //생각 풍선 보여주는 시간
    public float orderlimitTime = 20;
    //주문 시간 30초
    public float limitTime = 5;

    //level이 햄버거 난이도, 튜토리얼 난이도부터 4난이도까지 있음
    public int level = 1;
    //stage가 단계, 1단계~5단계까지 있음 -> 튜토리얼 단계는 없음
    public int stage = 1;

    public GameObject selecthambugurcard;
    public GameObject selectsourcecard;

    public bool[] istable = new bool[10] { true, true, true, true, false, false, false, false, false, false };
    public Vector3[] tableposition = new Vector3[10] { new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0.051f, 0.32f, -0.075f), new Vector3(0, 0.278f, 0) };
    public Quaternion[] tablerotation = new Quaternion[10] { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 29.878f, 0), Quaternion.Euler(0, 58.383f, 0) };
}
