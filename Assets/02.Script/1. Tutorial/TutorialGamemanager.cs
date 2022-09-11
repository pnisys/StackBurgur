using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TutorialGamemanager : MonoBehaviour
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
    public Animator animator;

    public int score = 0;
    public int lifescore = 5;

    //생각 풍선 보여주는 시간
    public float orderlimitTime = 15;
    //주문 시간 30초
    public float limitTime = 30;

    private void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = SoundManager.instance.bgmSound;
    }
}
