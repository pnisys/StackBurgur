using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Oculus.Interaction.HandGrab;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PeopleAnimator : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public GameObject[] table;
    public GameObject people;

    AudioSource audiosource;
    public AudioClip[] audioclip;
    GameObject door;
    GameObject clerkcollider;
    GameObject mood;
    public Ranking ranking;

    public List<int> sourcenumber = new List<int>();
    public List<int> hambugernumber = new List<int>();

    readonly int hashIdle = Animator.StringToHash("Idle");
    readonly int hashTalk = Animator.StringToHash("Talk");
    readonly int hashSuccess = Animator.StringToHash("Success");
    readonly int hashfail = Animator.StringToHash("Fail");
    readonly int hasheat = Animator.StringToHash("Eat");


    public GameManager gamemanager;
    public Transform mantray;


    public delegate void LimitTimeComplete();
    public static event LimitTimeComplete OnLimitTimeComplete;

    public delegate void MeatHighlight();
    public static event MeatHighlight OnMeatHighlight;

    public delegate void StageChange();
    public static event StageChange OnStageChange;

    public delegate void LifeDie();
    public static event LifeDie OnLifeDie;

    public delegate void GameClear();
    public static event GameClear OnClear;

    public GameObject selecthambugurcard;
    public GameObject selecthambugurcard2;
    public GameObject selectsourcecard;
    public GameObject selectsourcecard2;


    public HandGrabInteractor grabstatus;
    public TrayControl traycontrol;

    public GameObject[] OneLevelBurgerCard;
    public GameObject[] TwoLevelBurgerCard;
    public GameObject[] ThreeLevelBurgerCard;
    public GameObject[] FourLevelBurgerCard;
    public GameObject[] sourceCard;

    int ran = 0;
    int ran1 = 0;
    bool testsuccess = true;
    public bool phase1source = false;
    public bool phase2source = false;

    void Start()
    {
        for (int i = 0; i < OneLevelBurgerCard.Length; i++)
        {
            OneLevelBurgerCard[i] = transform.GetChild(2).GetChild(0).GetChild(1).GetChild(i).gameObject;
        }
        for (int i = 0; i < TwoLevelBurgerCard.Length; i++)
        {
            TwoLevelBurgerCard[i] = transform.GetChild(2).GetChild(0).GetChild(2).GetChild(i).gameObject;
        }
        for (int i = 0; i < ThreeLevelBurgerCard.Length; i++)
        {
            ThreeLevelBurgerCard[i] = transform.GetChild(2).GetChild(0).GetChild(3).GetChild(i).gameObject;
        }
        for (int i = 0; i < FourLevelBurgerCard.Length; i++)
        {
            FourLevelBurgerCard[i] = transform.GetChild(2).GetChild(0).GetChild(4).GetChild(i).gameObject;
        }
        for (int i = 0; i < 4; i++)
        {
            sourceCard[i] = transform.GetChild(2).GetChild(1).GetChild(i).gameObject;
        }
        mood = transform.GetChild(4).gameObject;
        audiosource = GetComponent<AudioSource>();
        audiosource.volume = SoundManager.instance.anothersound;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        table[0] = GameObject.FindGameObjectWithTag("1TABLE");
        table[1] = GameObject.FindGameObjectWithTag("2TABLE");
        table[2] = GameObject.FindGameObjectWithTag("3TABLE");
        table[3] = GameObject.FindGameObjectWithTag("4TABLE");
        table[4] = GameObject.FindGameObjectWithTag("5TABLE");
        table[5] = GameObject.FindGameObjectWithTag("6TABLE");
        table[6] = GameObject.FindGameObjectWithTag("7TABLE");
        table[7] = GameObject.FindGameObjectWithTag("8TABLE");
        table[8] = GameObject.FindGameObjectWithTag("9TABLE");
        table[9] = GameObject.FindGameObjectWithTag("10TABLE");
        door = GameObject.FindGameObjectWithTag("DOOR");
        clerkcollider = GameObject.FindGameObjectWithTag("CLERKCOLLIDER");
        audiosource.PlayOneShot(audioclip[3]);
        audiosource.clip = audioclip[4];
        audiosource.Play();
        mantray = gameObject.transform.GetChild(3);
        gamemanager.currentpeople = gameObject;
        phase1source = true;
        //게임매니저에, 출현한 사람 담기
        gamemanager.uppeople.Add(gameObject);
        gamemanager.peoplenumbur++;
        gamemanager.phase1selectedsource = null;
        gamemanager.phase2selectedsource = null;

        //1. 손님 등판
        StartCoroutine(ClerkStateCheck());
        //2.숫자 랜덤하게 섞기
        RandomNumberSelect();

        StageLevelRandom();
        agent.destination = clerkcollider.transform.position;
    }

    //스테이지,난이도 판독 함수
    void StageLevelRandom()
    {
        int randomstage = 0;
        if (gamemanager.stage == 1)
        {
            gamemanager.level = 1;
            gamemanager.limitTime = 90f;
            gamemanager.orderlimitTime = 15f;
        }
        else if (gamemanager.stage == 2)
        {
            randomstage = UnityEngine.Random.Range(1, 3);
            gamemanager.level = randomstage;
            gamemanager.limitTime = 90f;
            gamemanager.orderlimitTime = 15f;

        }
        else if (gamemanager.stage == 3)
        {
            randomstage = UnityEngine.Random.Range(2, 4);
            gamemanager.level = randomstage;
            gamemanager.limitTime = 90f;
            gamemanager.orderlimitTime = 15f;

        }
        else if (gamemanager.stage == 4)
        {
            randomstage = UnityEngine.Random.Range(1, 4);
            gamemanager.level = randomstage;
            gamemanager.limitTime = 75f;
            gamemanager.orderlimitTime = 30f;

        }
        else if (gamemanager.stage == 5)
        {
            randomstage = UnityEngine.Random.Range(3, 5);
            gamemanager.level = randomstage;
            gamemanager.limitTime = 60f;
            gamemanager.orderlimitTime = 30f;
        }
    }


    //1. 점원이 매장까지 걸어오다가 멈춤
    IEnumerator ClerkStateCheck()
    {
        while (gamemanager.isClerk == false)
        {
            yield return new WaitForSeconds(0.3f);
            if (gamemanager.isClerk == true)
            {
                print("멈춘다");
                audiosource.Stop();
                //손님 Navagent 꺼주기
                agent.isStopped = true;
                //손님 서 있는 애니메이션 틀기
                animator.SetBool(hashIdle, true);
                StartCoroutine(ThinkBallon());
            }
        }
    }

    //2. 2초 대기하다가 주문함
    IEnumerator ThinkBallon()
    {
        yield return new WaitForSeconds(1f);
        audiosource.PlayOneShot(audioclip[5]);
        //손님이 주문하는 상태 켜기
        gamemanager.isThinking = true;
        //손님이 주문하는 애니메이션 켜기
        animator.SetBool(hashIdle, false);
        animator.SetBool(hashTalk, true);

        //난이도에 따라 햄버거 카드에 맞는 햄버거와 소스를 카드를 보여주기
        LevelBurgurSetting();
        //제한시간 캔버스 켜기
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        audiosource.clip = audioclip[0];
        audiosource.Play();
        //주문하고 있으면 계속 반복 켜기
        while (gamemanager.isThinking == true)
        {
            yield return null;
            //주문하는 시간 15초 생성
            gamemanager.orderlimitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "제한 시간 : " + Mathf.Round(gamemanager.orderlimitTime).ToString() + "초";

            //15초 지나면 다음단계로 넘어감
            if (gamemanager.orderlimitTime < 0)
            {
                //초기화
                //gamemanager.orderlimitTime = 20;
                audiosource.Stop();
                gamemanager.isThinking = false;
            }
        }
        //스테이지가 4일 경우에만
        if (gamemanager.stage >= 4)
        {
            selecthambugurcard2.SetActive(false);
            selectsourcecard2.SetActive(false);
        }
        //카드 Setactive(false); 시키기
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        selecthambugurcard.SetActive(false);
        selectsourcecard.SetActive(false);
        animator.SetBool(hashTalk, false);
        StartCoroutine(Order());
    }

    //3. 주문 받음
    IEnumerator Order()
    {
        audiosource.PlayOneShot(audioclip[6]);
        //주문 상태 On
        gamemanager.isOrder = true;
        animator.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        audiosource.Play();
        while (gamemanager.isOrder == true)
        {
            yield return null;
            //제한 시간 켜주기

            gamemanager.limitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "제한 시간 : " + Mathf.Round(gamemanager.limitTime).ToString() + "초";

            //시간 끝나면
            if (gamemanager.limitTime < 0)
            {
                //스테이지가 4이상이면
                if (gamemanager.stage >= 4)
                {
                    audiosource.PlayOneShot(audioclip[6]);
                    phase1source = false;
                    phase2source = true;
                    StartCoroutine(Stage45());
                    yield break;
                }
                //스테이지가 4이하이면
                else
                {
                    audiosource.Stop();
                    //agent 꺼줬던거 켜죽
                    agent.isStopped = false;
                    //제한 시간 끄기
                    animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    animator.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    //이때 TrayControl의 적층한 것과 정답과의 비교 함수를 시작할 것임
                    OnLimitTimeComplete();
                    yield return new WaitForSeconds(1f);
                    //초기화 시켜주기
                    gamemanager.isOrder = false;
                    //검사 후 성공이면
                    if (gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true)
                    {
                        StartCoroutine(SucessTable());
                    }
                    //검사 후 실패면
                    else if (gamemanager.isfail)
                    {
                        StartCoroutine(FailTable());
                    }
                }
            }
        }
    }

    //스테이지 4,5 따로 처리
    IEnumerator Stage45()
    {
        audiosource.Play();
        print("이거 왜 안탐?");
        foreach (var item in traycontrol.stackcreateburgur.ToArray())
        {
            item.transform.parent = traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(15).transform;
            item.transform.localPosition = new Vector3(0, item.transform.localPosition.y + 0.27f, 0);
            traycontrol.stackcreateburgur.Pop();
        }
        //소스가 남아 있다면?
        if (traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(13).childCount == 1)
        {
            GameObject aa = traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(13).GetChild(0).gameObject;
            aa.transform.parent = traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(16).transform;
            aa.transform.localPosition = new Vector3(0, 0, 0);
        }

        //4단계나 5단계는 한번 더 돌아야 함
        if (gamemanager.stage == 4)
        {
            gamemanager.limitTime = 75f;
        }
        else if (gamemanager.stage == 5)
        {
            gamemanager.limitTime = 60f;
        }
        while (true)
        {
            yield return null;
            gamemanager.limitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "제한 시간 : " + Mathf.Round(gamemanager.limitTime).ToString() + "초";
            if (gamemanager.limitTime < 0)
            {
                audiosource.Stop();
                phase2source = false;

                //agent 꺼줬던거 켜죽
                agent.isStopped = false;
                //제한 시간 끄기
                animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);

                //이때 TrayControl의 적층한 것과 정답과의 비교 함수를 시작할 것임
                OnLimitTimeComplete();
                yield return new WaitForSeconds(1f);
                //초기화 시켜주기
                gamemanager.isOrder = false;
                //검사 후 성공이면
                if (((gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true) && (gamemanager.iscompletesuccess2 == true || gamemanager.islittlesuccess2 == true))/* || testsuccess == true*/)
                {
                    StartCoroutine(SucessTable());
                }
                //둘 중 하나라도 실패하면
                else if (gamemanager.isfail == true || gamemanager.isfail2 == true)
                {
                    StartCoroutine(FailTable());
                }
                yield break;
            }
        }
    }

    //실패시, 테이블, 캐릭터 애니메이션 처리
    IEnumerator FailTable()
    {
        audiosource.PlayOneShot(audioclip[1]);
        gamemanager.LifeScore--;
        //실패 애니메이션 설정
        animator.SetBool(hashfail, true);
        mood.SetActive(true);
        mood.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        //점수 다 깎였을 떄
        if (gamemanager.lifescore == 0)
        {
            OnLifeDie();
            StartCoroutine(Died());
        }
        yield return new WaitForSeconds(5f);
        mood.SetActive(false);
        mood.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        //문으로 간다.
        agent.destination = door.transform.position;
        audiosource.clip = audioclip[4];
        audiosource.Play();

        //문 일정 범위 안으로 들어오면
        yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);
        audiosource.Stop();
        agent.isStopped = true;
        agent.enabled = false;
        transform.position = new Vector3(100, 100, 100);
        animator.SetBool(hashfail, false);
        yield return new WaitForSeconds(2f);
        //이제 다른 사람 한명을 켜야됨
        GameManagerInit();
        gamemanager.people[gamemanager.peoplenumbur].SetActive(true);

    }
    //성공시, 테이블, 캐릭터 애니메이션 처리
    IEnumerator SucessTable()
    {
        mood.SetActive(true);
        mood.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        audiosource.PlayOneShot(audioclip[2]);
        //테이블 숫자 한명 늘려주고
        gamemanager.tablepeoplenumbur++;
        mantray.gameObject.SetActive(true);
        //테이블 10개 중 순차적으로 앉고, 만약 이전 손님이 테이블에 앉았으면
        //다음 테이블로 넘어가기
        for (int i = 0; i < 10; i++)
        {
            if (gamemanager.istable[i] == false)
            {
                animator.SetBool(hashSuccess, true);
                //yield return new WaitForSeconds(3f);
                //성공의 애니메이션
                //테이블로 가게 하기
                agent.destination = table[i].transform.position;
                audiosource.clip = audioclip[4];
                audiosource.Play();
                //근처 일정 범위안으로 들어가면
                yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);
                mood.SetActive(false);
                mood.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                audiosource.Stop();
                //agent가 멈추기
                agent.isStopped = true;
                agent.enabled = false;
                //사람은 테이블 자식으로 들어간다.
                transform.parent = table[i].transform;
                //각 테이블 포지션은 미리 설정해놓았던 곳으로 간다.
                transform.localPosition = gamemanager.tableposition[i];
                transform.localRotation = gamemanager.tablerotation[i];
                //먹는 애니메이션 실행
                animator.SetBool(hasheat, true);
                gameObject.transform.GetChild(3).localPosition = new Vector3(0, 0.879f, 0.579f);
                //들어간 테이블은 닫게 하기
                gamemanager.istable[i] = true;
                GameManagerInit();
                //테이블숫자가 5명이 되었으면
                if (gamemanager.tablepeoplenumbur == 5)
                {
                    if (gamemanager.stage <= 4)
                    {
                        //스테이지 업 함수 호출
                        StageUp();
                        gamemanager.isClerk = false;
                    }
                    //게임 모두 클리어
                    else
                    {
                        OnClear();
                        StartCoroutine(StageComplete5());
                        gamemanager.isClerk = false;
                    }
                    yield break;
                }
                gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
                //반복 끝내기
                break;
            }
        }
        yield return new WaitForSeconds(2f);
        gamemanager.iscompletesuccess = false;
        gamemanager.iscompletesuccess2 = false;
        gamemanager.islittlesuccess = false;
        gamemanager.islittlesuccess2 = false;

    }

    //죽을때
    IEnumerator Died()
    {
        //ranking.ScoreSet(gamemanager.score, SoundManager.instance.username,gamemanager.stage);
        audiosource.PlayOneShot(audioclip[8]);
        gamemanager.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }

    //모든 5스테이지 완료
    IEnumerator StageComplete5()
    {
        GameManagerInit();
        gamemanager.GetComponent<AudioSource>().Stop();
        audiosource.PlayOneShot(audioclip[9]);
        //gamemanager.GuideUiText.text = "게임 모두 클리어";
        //gamemanager.GuideUiText.fontSize = 0.05f;
        //gamemanager.GuideUiText2.text = null;
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }

    //스테이지 업했을 때 함수
    void StageUp()
    {
        //스테이지 업!
        gamemanager.Stage++;
        OnStageChange();
        gamemanager.tablepeoplenumbur = 0;
        //gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
        //기존 테이블에 있던 손님들 다시 People에 넣고 초기화시키고
        foreach (var item in gamemanager.uppeople)
        {
            item.SetActive(false);
            item.transform.parent = people.transform;
            item.transform.localPosition = new Vector3(0, 0, 0);
            item.transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
        audiosource.PlayOneShot(audioclip[7]);
        if (gamemanager.istable[9] == true)
        {
            for (int i = 0; i < 10; i++)
            {
                gamemanager.istable[i] = false;
            }
        }
        GameManagerInit();
    }

    public void GameManagerInit()
    {
        gamemanager.iscompletesuccess = false;
        gamemanager.iscompletesuccess2 = false;
        gamemanager.islittlesuccess = false;
        gamemanager.islittlesuccess2 = false;
        gamemanager.isfail = false;
        gamemanager.isfail2 = false;
    }
    //난이도에 맞는 카드를 Setactiove 하고, selecthambugurcard에 넣는 함수
    public void LevelBurgurSetting()
    {
        if (gamemanager.level == 1)
        {
            //스테이지가 4라면
            if (gamemanager.stage >= 4)
            {
                OneLevelBurgerCard[hambugernumber[1]].SetActive(true);
                selecthambugurcard2 = OneLevelBurgerCard[hambugernumber[1]];
                gamemanager.selecthambugurcard2 = OneLevelBurgerCard[hambugernumber[1]];
                sourceCard[sourcenumber[1]].SetActive(true);
                selectsourcecard2 = sourceCard[sourcenumber[1]];
                gamemanager.selectsourcecard2 = sourceCard[sourcenumber[1]];

                OneLevelBurgerCard[hambugernumber[0]].transform.localPosition = new Vector3(0, 52.4f, 0);
                OneLevelBurgerCard[hambugernumber[0]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                OneLevelBurgerCard[hambugernumber[1]].transform.localPosition = new Vector3(0, -67.89999f, 0);
                OneLevelBurgerCard[hambugernumber[1]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                sourceCard[sourcenumber[0]].transform.localPosition = new Vector3(48, 52.4f, 0);
                sourceCard[sourcenumber[0]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                sourceCard[sourcenumber[1]].transform.localPosition = new Vector3(48, -67.89999f, 0);
                sourceCard[sourcenumber[1]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);
            }
            OneLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = OneLevelBurgerCard[hambugernumber[0]];
            gamemanager.selecthambugurcard = OneLevelBurgerCard[hambugernumber[0]];
        }
        else if (gamemanager.level == 2)
        {
            //스테이지가 4라면
            if (gamemanager.stage >= 4)
            {
                TwoLevelBurgerCard[hambugernumber[1]].SetActive(true);
                selecthambugurcard2 = TwoLevelBurgerCard[hambugernumber[1]];
                gamemanager.selecthambugurcard2 = TwoLevelBurgerCard[hambugernumber[1]];
                sourceCard[sourcenumber[1]].SetActive(true);
                selectsourcecard2 = sourceCard[sourcenumber[1]];
                gamemanager.selectsourcecard2 = sourceCard[sourcenumber[1]];

                TwoLevelBurgerCard[hambugernumber[0]].transform.localPosition = new Vector3(0, 52.4f, 0);
                TwoLevelBurgerCard[hambugernumber[0]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                TwoLevelBurgerCard[hambugernumber[1]].transform.localPosition = new Vector3(0, -67.89999f, 0);
                TwoLevelBurgerCard[hambugernumber[1]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                sourceCard[sourcenumber[0]].transform.localPosition = new Vector3(48, 52.4f, 0);
                sourceCard[sourcenumber[0]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                sourceCard[sourcenumber[1]].transform.localPosition = new Vector3(48, -67.89999f, 0);
                sourceCard[sourcenumber[1]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);
            }
            TwoLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = TwoLevelBurgerCard[hambugernumber[0]];
            gamemanager.selecthambugurcard = TwoLevelBurgerCard[hambugernumber[0]];
        }
        else if (gamemanager.level == 3)
        {
            //스테이지가 4라면
            if (gamemanager.stage >= 4)
            {
                ThreeLevelBurgerCard[hambugernumber[1]].SetActive(true);
                selecthambugurcard2 = ThreeLevelBurgerCard[hambugernumber[1]];
                gamemanager.selecthambugurcard2 = ThreeLevelBurgerCard[hambugernumber[1]];
                sourceCard[sourcenumber[1]].SetActive(true);
                selectsourcecard2 = sourceCard[sourcenumber[1]];
                gamemanager.selectsourcecard2 = sourceCard[sourcenumber[1]];

                ThreeLevelBurgerCard[hambugernumber[0]].transform.localPosition = new Vector3(0, 52.4f, 0);
                ThreeLevelBurgerCard[hambugernumber[0]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                ThreeLevelBurgerCard[hambugernumber[1]].transform.localPosition = new Vector3(0, -67.89999f, 0);
                ThreeLevelBurgerCard[hambugernumber[1]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                sourceCard[sourcenumber[0]].transform.localPosition = new Vector3(48, 52.4f, 0);
                sourceCard[sourcenumber[0]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                sourceCard[sourcenumber[1]].transform.localPosition = new Vector3(48, -67.89999f, 0);
                sourceCard[sourcenumber[1]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);
            }
            ThreeLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = ThreeLevelBurgerCard[hambugernumber[0]];
            gamemanager.selecthambugurcard = ThreeLevelBurgerCard[hambugernumber[0]];
        }
        else if (gamemanager.level == 4)
        {
            //스테이지가 4라면
            if (gamemanager.stage >= 4)
            {
                FourLevelBurgerCard[hambugernumber[1]].SetActive(true);
                selecthambugurcard2 = FourLevelBurgerCard[hambugernumber[1]];
                gamemanager.selecthambugurcard2 = FourLevelBurgerCard[hambugernumber[1]];
                sourceCard[sourcenumber[1]].SetActive(true);
                selectsourcecard2 = sourceCard[sourcenumber[1]];
                gamemanager.selectsourcecard2 = sourceCard[sourcenumber[1]];

                FourLevelBurgerCard[hambugernumber[0]].transform.localPosition = new Vector3(0, 52.4f, 0);
                FourLevelBurgerCard[hambugernumber[0]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                FourLevelBurgerCard[hambugernumber[1]].transform.localPosition = new Vector3(0, -67.89999f, 0);
                FourLevelBurgerCard[hambugernumber[1]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                sourceCard[sourcenumber[0]].transform.localPosition = new Vector3(48, 52.4f, 0);
                sourceCard[sourcenumber[0]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);

                sourceCard[sourcenumber[1]].transform.localPosition = new Vector3(48, -67.89999f, 0);
                sourceCard[sourcenumber[1]].transform.localScale = new Vector3(1.686302f, 1.302871f, 1.686302f);
            }
            FourLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = FourLevelBurgerCard[hambugernumber[0]];
            gamemanager.selecthambugurcard = FourLevelBurgerCard[hambugernumber[0]];
        }

        sourceCard[sourcenumber[0]].SetActive(true);
        selectsourcecard = sourceCard[sourcenumber[0]];
        gamemanager.selectsourcecard = sourceCard[sourcenumber[0]];
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
