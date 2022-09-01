using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class TutorialPeopleAnimator : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public GameObject[] table;

    GameObject door;
    GameObject clerkcollider;

    bool[] istable = new bool[4] { true, true, true, false };
    Vector3[] tableposition = new Vector3[4] { new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0.57f), new Vector3(0.02999886f, 0.35f, 0.02999872f), new Vector3(0, 0.22f, 0) };
    Quaternion[] tablerotation = new Quaternion[4] { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 34.479f, 0), Quaternion.Euler(0, 0, 0) };


    readonly int hashIdle = Animator.StringToHash("Idle");
    readonly int hashTalk = Animator.StringToHash("Talk");
    readonly int hashSuccess = Animator.StringToHash("Success");
    readonly int hashfail = Animator.StringToHash("Fail");
    readonly int hasheat = Animator.StringToHash("Eat");


    public TutorialGamemanager tutorialgamemanager;

    public delegate void LimitTimeComplete();
    public static event LimitTimeComplete OnLimitTimeComplete;

    public delegate void MeatHighlight();
    public static event MeatHighlight OnMeatHighlight;

    public GameObject TutorialLevelBurgerCard;
    public GameObject[] sourceCard;

    public GameObject selecthambugurcard;
    //public GameObject selectsourcecard;
    public TextMeshProUGUI guidetext;

    //public List<int> sourcenumber = new List<int>();


    public AudioClip[] audioclip;
    public AudioSource audiosource;

    int ran = 0;
    int ran1 = 0;

    bool audioing = false;


    bool istutorial0 = false;
    //재료가 앞에 놓여있습니다.
    bool istutorial1 = true;
    //4. 컨트롤러를 활용하여 햄버거를 주문에 맞춰 만들어보세요.
    bool istutorial2 = false;
    //5. 첫번째로 패티를 구워볼까요?
    bool istutorial3 = false;
    //6. 너무 장시간 구우면 패티가 탈 수 있으니 주의해주세요
    bool istutorial4 = false;
    //7. 잘하셨습니다. 나머지 재료를 접시에 담아 햄버거를 완성해주세요. 
    bool istutorial5 = false;
    //정답입니다. 성공시에는 손님이 자리에 앉으며 점수를 획득합니다.
    bool istutorial6 = false;
    //오답시에는 기회를 빼앗기고 손님이 실망하며 나가니 주의해주세요.
    bool istutorial7 = false;
    //게임이 종료시 랭킹이 보여지며 사용자의 닉네임과 순위가 보여지게 됩니다.
    bool istutorial8 = false;


    private void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        table[0] = GameObject.FindGameObjectWithTag("1TABLE");
        table[1] = GameObject.FindGameObjectWithTag("2TABLE");
        table[2] = GameObject.FindGameObjectWithTag("3TABLE");
        table[3] = GameObject.FindGameObjectWithTag("4TABLE");
        door = GameObject.FindGameObjectWithTag("DOOR");
        clerkcollider = GameObject.FindGameObjectWithTag("CLERKCOLLIDER");
        agent.destination = clerkcollider.transform.position;

        //1. 손님 등판
        StartCoroutine(ClerkStateCheck());
        //2. 숫자 랜덤하게 섞기
        //RandomNumberSelect();
    }


    //1. 점원이 매장까지 걸어오다가 멈춤
    IEnumerator ClerkStateCheck()
    {
        while (tutorialgamemanager.isClerk == false)
        {
            yield return new WaitForSeconds(0.3f);
            if (tutorialgamemanager.isClerk == true)
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
        yield return new WaitForSeconds(2f);
        //Text와 버튼 Canvas 틀기
        guidetext.transform.parent.gameObject.SetActive(true);
        guidetext.text = "주문이 들어왔습니다. \n\n20초간 재료, 순서를 기억하세요";
        //주문이 들어왔습니다. 20초간 재료와 순서를 기억하세요. 오디오 틀기
        audiosource.PlayOneShot(audioclip[0]);

        //손님이 주문하는 상태 켜기
        tutorialgamemanager.isThinking = true;
        //손님이 주문하는 애니메이션 켜기
        animator.SetBool(hashIdle, false);
        animator.SetBool(hashTalk, true);

        //StartCoroutine(VoiceControl());

        //난이도에 따라 햄버거 카드에 맞는 햄버거와 소스를 카드를 보여주기
        LevelBurgurSetting();
        //제한시간 캔버스 켜기
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //튜토리얼 안내캔버스 끝나야 제한시간 흐르게 하기
        yield return new WaitUntil(() => audioing == true);
        //주문하고 있으면 계속 반복 켜기
        while (tutorialgamemanager.isThinking == true)
        {
            yield return null;
            //주문하는 시간 15초 생성
            tutorialgamemanager.orderlimitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "제한 시간 : " + Mathf.Round(tutorialgamemanager.orderlimitTime).ToString() + "초";

            //15초 지나면 다음단계로 넘어감
            if (tutorialgamemanager.orderlimitTime < 0)
            {
                //초기화
                tutorialgamemanager.orderlimitTime = 15;
                tutorialgamemanager.isThinking = false;
            }
        }

        //카드 Setactive(false); 시키기
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        TutorialLevelBurgerCard.SetActive(false);
        sourceCard[0].SetActive(false);
        animator.SetBool(hashTalk, false);
        StartCoroutine(Order());
    }

    //3. 주문 받음
    IEnumerator Order()
    {
        //주문 상태 On
        tutorialgamemanager.isOrder = true;

        while (tutorialgamemanager.isOrder == true)
        {
            yield return null;
            //제한 시간 켜주기
            tutorialgamemanager.limitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "제한 시간 : " + Mathf.Round(tutorialgamemanager.limitTime).ToString() + "초";

            //시간 끝나면
            if (tutorialgamemanager.limitTime < 0)
            {
                //agent 꺼줬던거 켜죽
                agent.isStopped = false;
                //제한 시간 끄기
                animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                //이때 TrayControl의 적층한 것과 정답과의 비교 함수를 시작할 것임
                OnLimitTimeComplete();
                yield return new WaitForSeconds(1f);
                //초기화 시켜주기
                tutorialgamemanager.limitTime = 30f;
                tutorialgamemanager.isOrder = false;
                //검사 후 성공이면
                if (tutorialgamemanager.iscompletesuccess == true || tutorialgamemanager.islittlesuccess == true)
                {
                    //테이블 10개 중 순차적으로 앉고, 만약 이전 손님이 테이블에 앉았으면
                    //다음 테이블로 넘어가기
                    for (int i = 0; i < 10; i++)
                    {
                        if (istable[i] == false)
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
                            transform.localPosition = tableposition[i];
                            transform.localRotation = tablerotation[i];
                            //먹는 애니메이션 실행
                            animator.SetBool(hasheat, true);
                            //들어간 테이블은 닫게 하기
                            istable[i] = true;
                            //반복 끝내기
                            break;
                        }
                    }
                }
                //검사 후 실패면
                else if (tutorialgamemanager.isfail)
                {
                    //실패 애니메이션 설정
                    animator.SetBool(hashfail, true);
                    //5초 뒤에
                    yield return new WaitForSeconds(5f);
                    //문으로 간다.
                    agent.destination = door.transform.position;
                    //문 일정 범위 안으로 들어오면
                    yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);

                    agent.isStopped = true;
                    agent.enabled = false;
                    transform.position = new Vector3(100, 100, 100);
                    animator.SetBool(hashfail, false);
                }
            }
        }
    }

    //다음 버튼 누르면
    public void ButtonTutorialing()
    {
        StartCoroutine(VoiceControl());
    }

    //음성 컨트롤 메서드
    IEnumerator VoiceControl()
    {
        if (istutorial1 == true)
        {
            audiosource.Stop();
            guidetext.text = "재료가 앞에 놓여있습니다.";
            audiosource.PlayOneShot(audioclip[1]);
            istutorial2 = true;
            istutorial1 = false;
            yield return new WaitForSeconds(1f);
        }

        else if (istutorial2 == true && istutorial1 == false)
        {
            audiosource.Stop();
            guidetext.text = "컨트롤러를 활용하여 햄버거를 주문에 맞춰 만들어보세요.";
            audiosource.PlayOneShot(audioclip[2]);
            istutorial3 = true;
            istutorial2 = false;

        }
        else if(istutorial3 == true && istutorial2 == false)
        {
            audiosource.Stop();
            guidetext.text = "첫번째로 패티를 구워볼까요?";
            audiosource.PlayOneShot(audioclip[3]);
            OnMeatHighlight();
            istutorial4 = true;
            istutorial3 = false;
            yield return new WaitForSeconds(5f);
            guidetext.text = " 너무 장시간 구우면 패티가 탈 수 있으니 주의해주세요";
            audiosource.PlayOneShot(audioclip[4]);
        }

        ////고기패티 강조하는 쉐이더
        //audioing = true;
    }

    //이건 소스 하나로 통일할 거임, 튜토리얼에선 없어도 됨
    //public void RandomNumberSelect()
    //{
    //    for (int i = 0; i < 4; i++)
    //    {
    //        sourcenumber.Add(i);
    //    }

    //    for (int i = 0; i < 4; i++)
    //    {
    //        int temp1 = sourcenumber[i];
    //        ran1 = UnityEngine.Random.Range(0, 3);
    //        sourcenumber[i] = sourcenumber[ran1];
    //        sourcenumber[ran1] = temp1;
    //    }
    //}

    //난이도에 맞는 카드를 Setactiove 하고, selecthambugurcard에 넣는 함수
    public void LevelBurgurSetting()
    {
        TutorialLevelBurgerCard.SetActive(true);
        selecthambugurcard = TutorialLevelBurgerCard;
        sourceCard[0].SetActive(true);
    }

}
