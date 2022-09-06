using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.Video;
using HighlightPlus;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;

public class PeopleAnimator : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public GameObject[] table;
    public GameObject people;
    GameObject door;
    GameObject clerkcollider;


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

    public GameObject selecthambugurcard;
    public GameObject selectsourcecard;

    public HandGrabInteractor grabstatus;

    public GameObject[] OneLevelBurgerCard;
    public GameObject[] TwoLevelBurgerCard;
    public GameObject[] ThreeLevelBurgerCard;
    public GameObject[] FourLevelBurgerCard;
    public GameObject[] sourceCard;

    int ran = 0;
    int ran1 = 0;



    bool audioing = false;

    private void Start()
    {
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
        agent.destination = clerkcollider.transform.position;
        mantray = gameObject.transform.GetChild(3);

        //게임매니저에, 출현한 사람 담기
        gamemanager.uppeople.Add(gameObject);
        gamemanager.peoplenumbur++;
        //1. 손님 등판
        StartCoroutine(ClerkStateCheck());
        //2.숫자 랜덤하게 섞기
        RandomNumberSelect();

        //스테이지,난이도 판독 함수
        StageLevelRandom();
    }

    void StageLevelRandom()
    {
        int randomstage = 0;
        if (gamemanager.stage == 1)
        {
            gamemanager.level = 1;
        }
        else if (gamemanager.stage == 2)
        {
            randomstage = UnityEngine.Random.Range(1, 3);
            gamemanager.level = randomstage;
        }
        else if (gamemanager.stage == 3)
        {
            randomstage = UnityEngine.Random.Range(2, 4);
            gamemanager.level = randomstage;
        }
        else if (gamemanager.stage == 4)
        {
            randomstage = UnityEngine.Random.Range(1, 4);
            gamemanager.level = randomstage;
        }
        else if (gamemanager.stage == 5)
        {
            randomstage = UnityEngine.Random.Range(3, 5);
            gamemanager.level = randomstage;
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
        //손님이 주문하는 상태 켜기
        gamemanager.isThinking = true;
        //손님이 주문하는 애니메이션 켜기
        animator.SetBool(hashIdle, false);
        animator.SetBool(hashTalk, true);

        //난이도에 따라 햄버거 카드에 맞는 햄버거와 소스를 카드를 보여주기
        LevelBurgurSetting();
        //제한시간 캔버스 켜기
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(true);
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
                gamemanager.orderlimitTime = 1;
                gamemanager.isThinking = false;
            }
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
        //주문 상태 On
        gamemanager.isOrder = true;

        while (gamemanager.isOrder == true)
        {
            yield return null;
            //제한 시간 켜주기
            gamemanager.limitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "제한 시간 : " + Mathf.Round(gamemanager.limitTime).ToString() + "초";

            //시간 끝나면
            if (gamemanager.limitTime < 0)
            {
                //agent 꺼줬던거 켜죽
                agent.isStopped = false;
                //제한 시간 끄기
                animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                animator.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                //이때 TrayControl의 적층한 것과 정답과의 비교 함수를 시작할 것임
                print("지금 여기는 타는거야?");
                OnLimitTimeComplete();
                yield return new WaitForSeconds(1f);
                //초기화 시켜주기
                gamemanager.limitTime = 1f;
                gamemanager.isOrder = false;
                //검사 후 성공이면
                if (gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true)
                {

                    mantray.gameObject.SetActive(true);
                    //테이블 10개 중 순차적으로 앉고, 만약 이전 손님이 테이블에 앉았으면
                    //다음 테이블로 넘어가기
                    for (int i = 0; i < 10; i++)
                    {
                        if (gamemanager.istable[i] == false)
                        {
                            //성공의 애니메이션
                            animator.SetBool(hashSuccess, true);
                            //테이블로 가게 하기
                            agent.destination = table[i].transform.position;
                            //근처 일정 범위안으로 들어가면
                            yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);
                            //agent가 멈추기
                            agent.isStopped = true;
                            agent.enabled = false;
                            //사람은 테이블 자식으로 들어간다.
                            transform.parent = table[i].transform;
                            //각 텡이블 포지션은 미리 설정해놓았던 곳으로 간다.
                            transform.localPosition = gamemanager.tableposition[i];
                            transform.localRotation = gamemanager.tablerotation[i];
                            //먹는 애니메이션 실행
                            animator.SetBool(hasheat, true);
                            gameObject.transform.GetChild(3).localPosition = new Vector3(0, 0.879f, 0.579f);
                            //들어간 테이블은 닫게 하기
                            gamemanager.istable[i] = true;
                            //반복 끝내기
                            break;
                        }
                    }
                    yield return new WaitForSeconds(2f);
                    //이제 다른 사람 한명을 켜야됨
                    gamemanager.iscompletesuccess = false;
                    gamemanager.islittlesuccess = false;
                    //테이블 숫자 한명 늘려주고
                    gamemanager.tablepeoplenumbur++;
                    //테이블숫자가 5명이 되었으면
                    if (gamemanager.tablepeoplenumbur == 5)
                    {
                        //스테이지 업 함수 호출
                        StageUp();
                        yield break;
                    }
                    else
                    {
                        gamemanager.people[gamemanager.tablepeoplenumbur].SetActive(true);
                    }

                }
                //검사 후 실패면
                else if (gamemanager.isfail)
                {
                    //실패 애니메이션 설정
                    animator.SetBool(hashfail, true);
                    yield return new WaitForSeconds(5f);
                    //문으로 간다.
                    agent.destination = door.transform.position;

                    //문 일정 범위 안으로 들어오면
                    yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);

                    agent.isStopped = true;
                    agent.enabled = false;
                    transform.position = new Vector3(100, 100, 100);
                    animator.SetBool(hashfail, false);
                    yield return new WaitForSeconds(2f);
                    //이제 다른 사람 한명을 켜야됨
                    gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
                    gamemanager.isfail = false;
                    Destroy(gameObject);
                }
            }
        }
    }

    void StageUp()
    {
        //스테이지 업!
        gamemanager.stage++;
        gamemanager.people[gamemanager.tablepeoplenumbur].SetActive(true);
        //기존 테이블에 있던 손님들 다시 People에 넣고 초기화시키고
        foreach (var item in gamemanager.uppeople)
        {
            item.SetActive(false);
            item.transform.parent = people.transform;
            item.transform.localPosition = new Vector3(0, 0, 0);
            item.transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
        gamemanager.tablepeoplenumbur = 0;
        //스테이지
    }



    //난이도에 맞는 카드를 Setactiove 하고, selecthambugurcard에 넣는 함수
    public void LevelBurgurSetting()
    {
        //레벨이 햄버거 난이도, 0번이 선택된 버거
        //튜토리얼 버거
        if (gamemanager.level == 1)
        {
            OneLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = OneLevelBurgerCard[hambugernumber[0]];
            gamemanager.selecthambugurcard = OneLevelBurgerCard[hambugernumber[0]];
        }
        else if (gamemanager.level == 2)
        {
            TwoLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = TwoLevelBurgerCard[hambugernumber[0]];
            gamemanager.selecthambugurcard = TwoLevelBurgerCard[hambugernumber[0]];
        }
        else if (gamemanager.level == 3)
        {
            ThreeLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = ThreeLevelBurgerCard[hambugernumber[0]];
            gamemanager.selecthambugurcard = ThreeLevelBurgerCard[hambugernumber[0]];
        }
        else if (gamemanager.level == 4)
        {
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
