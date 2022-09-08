using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.Video;
using HighlightPlus;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;

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
    public TutorialTrayControl tutorialtraycontrol;
    public GameObject[] sourceCard;


    public GameObject selecthambugurcard;
    //public GameObject selectsourcecard;
    public TextMeshProUGUI guidetext;
    public VideoClip[] viedoclips;
    public VideoPlayer viedoplayer;
    public GameObject patty;
    public HandGrabInteractor grabstatus;

    private void OnEnable()
    {
        TutorialMeatControl.OnGoodMeeting += GoodMeeting;
        TutorialMeatControl.OnBadMeeting += BadMeeting;

    }
    private void OnDisable()
    {
        TutorialMeatControl.OnGoodMeeting -= GoodMeeting;
        TutorialMeatControl.OnBadMeeting -= BadMeeting;
    }

    void GoodMeeting()
    {
        isgoodmeat = true;
    }

    void BadMeeting()
    {
        isbadmeat = true;
    }

    public AudioClip[] audioclip;
    public AudioClip[] audioclip2;

    public AudioSource audiosource;

    int ran = 0;
    int ran1 = 0;

    bool audioing = false;
    bool isgoodmeat = false;
    bool isbadmeat = false;
    bool testkey = false;

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        table[0] = GameObject.FindGameObjectWithTag("1TABLE");
        table[1] = GameObject.FindGameObjectWithTag("2TABLE");
        table[2] = GameObject.FindGameObjectWithTag("3TABLE");
        table[3] = GameObject.FindGameObjectWithTag("4TABLE");
        door = GameObject.FindGameObjectWithTag("DOOR");
        clerkcollider = GameObject.FindGameObjectWithTag("CLERKCOLLIDER");
        agent.destination = clerkcollider.transform.position;
        audiosource.PlayOneShot(audioclip2[3]);
        audiosource.clip = audioclip2[4];
        audiosource.Play();
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
        audiosource.PlayOneShot(audioclip2[5]);


        //손님이 주문하는 상태 켜기
        tutorialgamemanager.isThinking = true;
        //손님이 주문하는 애니메이션 켜기
        animator.SetBool(hashIdle, false);
        animator.SetBool(hashTalk, true);

        StartCoroutine(VoiceControl());

        //난이도에 따라 햄버거 카드에 맞는 햄버거와 소스를 카드를 보여주기
        LevelBurgurSetting();
        //제한시간 캔버스 켜기
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        //튜토리얼 안내캔버스 끝나야 제한시간 흐르게 하기
        yield return new WaitUntil(() => audioing == true);
        //주문하고 있으면 계속 반복 켜기
        //while (tutorialgamemanager.isThinking == true)
        //{
        //    yield return null;
        //    //주문하는 시간 15초 생성
        //    tutorialgamemanager.orderlimitTime -= Time.deltaTime;
        //    animator.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "제한 시간 : " + Mathf.Round(tutorialgamemanager.orderlimitTime).ToString() + "초";

        //    //15초 지나면 다음단계로 넘어감
        //    if (tutorialgamemanager.orderlimitTime < 0)
        //    {
        //        //초기화
        //        tutorialgamemanager.orderlimitTime = 15;
        //        tutorialgamemanager.isThinking = false;
        //    }
        //}

        //카드 Setactive(false); 시키기
        //animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        //TutorialLevelBurgerCard.SetActive(false);
        //sourceCard[0].SetActive(false);
        animator.SetBool(hashTalk, false);
        StartCoroutine(Order());
    }

    //3. 주문 받음
    IEnumerator Order()
    {
        audiosource.PlayOneShot(audioclip2[6]);
        //주문 상태 On
        tutorialgamemanager.isOrder = true;
        audiosource.clip = audioclip2[0];
        audiosource.Play();

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
                tutorialgamemanager.limitTime = 30f;
                tutorialgamemanager.isOrder = false;
                //검사 후 성공이면
                if (tutorialgamemanager.iscompletesuccess == true || tutorialgamemanager.islittlesuccess == true)
                {
                    audiosource.PlayOneShot(audioclip2[2]);

                    //테이블 10개 중 순차적으로 앉고, 만약 이전 손님이 테이블에 앉았으면
                    //다음 테이블로 넘어가기
                    for (int i = 0; i < 10; i++)
                    {
                        if (istable[i] == false)
                        {
                            audiosource.clip = audioclip2[4];
                            audiosource.Play();
                            //성공의 애니메이션
                            animator.SetBool(hashSuccess, true);
                            //테이블로 가게 하기
                            agent.destination = table[i].transform.position;
                            //근처 일정 범위안으로 들어가면
                            yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);
                            audiosource.Stop();

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
                            gameObject.transform.GetChild(16).localPosition = new Vector3(0, 0.831f, 0.579f);
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
                    yield return new WaitForSeconds(2f);
                    audiosource.PlayOneShot(audioclip2[1]);
                    yield return new WaitForSeconds(3f);

                    //문으로 간다.
                    agent.destination = door.transform.position;
                    audiosource.clip = audioclip2[4];
                    audiosource.Play();
                    //문 일정 범위 안으로 들어오면
                    yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);
                    audiosource.Stop();

                    agent.isStopped = true;
                    agent.enabled = false;
                    transform.position = new Vector3(100, 100, 100);
                    animator.SetBool(hashfail, false);
                    Destroy(gameObject);
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
        audiosource.PlayOneShot(audioclip[0]);
        audiosource.PlayOneShot(audioclip2[8]);
        guidetext.transform.parent.gameObject.SetActive(true);
        guidetext.text = "카드를 보고\n\n햄버거 재료 순서를\n\n알 수 있습니다.";

        yield return new WaitForSeconds(10f);
        audiosource.PlayOneShot(audioclip2[8]);
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        viedoplayer.transform.parent.GetChild(1).gameObject.SetActive(true);
        audiosource.PlayOneShot(audioclip[1]);
        guidetext.text = "버튼을 누르면\n\n재료를 집을 수 있습니다.\n\n버튼을 떼면 재료를 떨어뜨립니다.\n\n고기를 잡고 떼보세요";
        viedoplayer.clip = viedoclips[0];
        viedoplayer.Play();
        patty.GetComponent<HighlightEffect>().highlighted = true;
        patty.GetComponent<HighlightEffect>().outline = 0.07f;
        patty.GetComponent<HighlightEffect>().innerGlow = 0.07f;
        while (grabstatus.IsGrabbing == false)
        {
            yield return null;
            if (grabstatus.IsGrabbing == true)
            {
                break;
            }
        }
        while (grabstatus.IsGrabbing == true)
        {
            yield return null;
            if (grabstatus.IsGrabbing == false)
            {
                break;
            }
        }
        yield return new WaitForSeconds(2f);
        audiosource.PlayOneShot(audioclip2[8]);
        audiosource.Stop();
        patty.GetComponent<HighlightEffect>().highlighted = false;
        guidetext.text = "고기를 구워보세요.\n\n5초가 지나면 구워지지만\n\n10초가 지나면 타게 됩니다.";
        audiosource.PlayOneShot(audioclip[2]);
        viedoplayer.clip = viedoclips[2];
        viedoplayer.transform.parent.GetChild(1).localPosition = new Vector3(-0.025f, 0.557f, 0);
        yield return new WaitForSeconds(10f);
        audiosource.PlayOneShot(audioclip2[8]);

        audioing = true;
        audiosource.PlayOneShot(audioclip[5]);
        viedoplayer.clip = viedoclips[3];
        guidetext.text = "제한 시간 이내에 \n\n햄버거 카드의 순서대로\n\n 햄버거를 쌓아올려보세요.";

        ////고기패티 강조하는 쉐이더
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
