using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using UnityEngine.Video;
using HighlightPlus;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using UnityEngine.SceneManagement;
public class TutorialPeopleAnimator : MonoBehaviour
{
    Animator animator;
    NavMeshAgent agent;
    public GameObject[] table;
    public GameObject[] foods;
    public GameObject[] sources;

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

    GameObject mood;
    public GameObject selecthambugurcard;
    //public GameObject selectsourcecard;
    public TextMeshProUGUI guidetext;
    public VideoClip[] viedoclips;
    public VideoPlayer viedoplayer;
    public GameObject patty;
    public HandGrabInteractor lgrabstatus;
    public HandGrabInteractor rgrabstatus;
    public GameObject arrow;
    public bool islgrabstatus = false;
    public bool isrgrabstatus = false;
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
        mood = transform.GetChild(4).gameObject;
        audiosource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        audiosource.volume = SoundManager.instance.anothersound;
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
        //1. ???? ????
        StartCoroutine(ClerkStateCheck());
        //2. ???? ???????? ????
        //RandomNumberSelect();
    }


    //1. ?????? ???????? ?????????? ????
    IEnumerator ClerkStateCheck()
    {
        while (tutorialgamemanager.isClerk == false)
        {
            yield return new WaitForSeconds(0.3f);
            if (tutorialgamemanager.isClerk == true)
            {
                audiosource.Stop();
                //???? Navagent ??????
                agent.isStopped = true;
                //???? ?? ???? ?????????? ????
                animator.SetBool(hashIdle, true);
                StartCoroutine(ThinkBallon());
            }
        }
    }

    //2. 2?? ?????????? ??????
    IEnumerator ThinkBallon()
    {
        yield return new WaitForSeconds(1f);
        audiosource.PlayOneShot(audioclip2[5]);


        //?????? ???????? ???? ????
        tutorialgamemanager.isThinking = true;
        //?????? ???????? ?????????? ????
        animator.SetBool(hashIdle, false);
        animator.SetBool(hashTalk, true);

        StartCoroutine(VoiceControl());

        //???????? ???? ?????? ?????? ???? ???????? ?????? ?????? ????????
        LevelBurgurSetting();
        //???????? ?????? ????
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(true);

        //???????? ?????????? ?????? ???????? ?????? ????
        yield return new WaitUntil(() => audioing == true);

        animator.SetBool(hashTalk, false);
        StartCoroutine(Order());
    }

    //3. ???? ????
    IEnumerator Order()
    {
        audiosource.PlayOneShot(audioclip2[6]);
        //???? ???? On
        tutorialgamemanager.isOrder = true;
        audiosource.clip = audioclip2[0];
        audiosource.Play();

        while (tutorialgamemanager.isOrder == true)
        {
            yield return null;
            //???? ???? ??????
            tutorialgamemanager.limitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "???? ???? : " + Mathf.Round(tutorialgamemanager.limitTime).ToString() + "??";

            //???? ??????
            if (tutorialgamemanager.limitTime < 0)
            {
                audiosource.Stop();

                //agent ???????? ????
                agent.isStopped = false;
                //???? ???? ????
                animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                animator.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                //???? TrayControl?? ?????? ???? ???????? ???? ?????? ?????? ????
                OnLimitTimeComplete();
                yield return new WaitForSeconds(1f);
                //?????? ????????
                tutorialgamemanager.limitTime = 30f;
                tutorialgamemanager.isOrder = false;
                //???? ?? ????????
                if (tutorialgamemanager.iscompletesuccess == true || tutorialgamemanager.islittlesuccess == true)
                {
                    mood.SetActive(true);
                    mood.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    audiosource.PlayOneShot(audioclip2[2]);
                    audiosource.PlayOneShot(audioclip[6]);
                    //?????? 10?? ?? ?????????? ????, ???? ???? ?????? ???????? ????????
                    //???? ???????? ????????
                    for (int i = 0; i < 10; i++)
                    {
                        if (istable[i] == false)
                        {
                            audiosource.clip = audioclip2[4];
                            audiosource.Play();
                            //?????? ??????????
                            animator.SetBool(hashSuccess, true);
                            //???????? ???? ????
                            agent.destination = table[i].transform.position;
                            //???? ???? ?????????? ????????
                            yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);
                            mood.SetActive(false);
                            mood.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                            audiosource.Stop();

                            //agent?? ??????
                            agent.isStopped = true;
                            agent.enabled = false;
                            //?????? ?????? ???????? ????????.
                            transform.parent = table[i].transform;
                            //?? ?????? ???????? ???? ???????????? ?????? ????.
                            transform.localPosition = tableposition[i];
                            transform.localRotation = tablerotation[i];
                            //???? ?????????? ????
                            animator.SetBool(hasheat, true);
                            gameObject.transform.GetChild(17).localPosition = new Vector3(0, 0.831f, 0.579f);
                            //?????? ???????? ???? ????
                            istable[i] = true;
                            //???? ??????
                            break;
                        }
                    }
                    yield return new WaitForSeconds(1f);
                    audiosource.PlayOneShot(audioclip[8]);
                    yield return new WaitForSeconds(6f);

                    //?????????????? Ui???? ????
                    SceneManager.LoadScene(2);
                }
                //???? ?? ??????
                else if (tutorialgamemanager.isfail)
                {
                    mood.SetActive(true);
                    mood.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    //???? ?????????? ????
                    animator.SetBool(hashfail, true);
                    yield return new WaitForSeconds(2f);
                    audiosource.PlayOneShot(audioclip[7]);
                    audiosource.PlayOneShot(audioclip2[1]);
                    yield return new WaitForSeconds(3f);
                    mood.SetActive(false);
                    mood.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                    //?????? ????.
                    agent.destination = door.transform.position;
                    audiosource.clip = audioclip2[4];
                    audiosource.Play();
                    //?? ???? ???? ?????? ????????
                    yield return new WaitForSeconds(6f);
                    audiosource.PlayOneShot(audioclip[8]);
                    yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);
                    audiosource.Stop();

                    agent.isStopped = true;
                    agent.enabled = false;
                    transform.position = new Vector3(100, 100, 100);
                    animator.SetBool(hashfail, false);
                    SceneManager.LoadScene(2);

                }
                //yield return new WaitForSeconds(3f);
                //SceneManager.LoadScene(2);
            }
        }
    }

    //???? ???? ??????
    public void ButtonTutorialing()
    {
        StartCoroutine(VoiceControl());
    }

    //???? ?????? ??????
    IEnumerator VoiceControl()
    {
        audiosource.PlayOneShot(audioclip[0]);
        audiosource.PlayOneShot(audioclip2[8]);
        guidetext.transform.parent.gameObject.SetActive(true);
        guidetext.text = "?????? ????\n?????? ???? ??????\n?? ?? ????????.";

        yield return new WaitForSeconds(10f);
        patty.GetComponent<Grabbable>().enabled = true;
        patty.GetComponent<PhysicsGrabbable>().enabled = true;

        audiosource.PlayOneShot(audioclip2[8]);
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        viedoplayer.transform.parent.GetChild(1).gameObject.SetActive(true);
        audiosource.PlayOneShot(audioclip[1]);
        guidetext.text = "?????? ???? ??????????????";
        viedoplayer.clip = viedoclips[0];
        viedoplayer.Play();
        //patty.GetComponent<HighlightEffect>().highlighted = true;
        //patty.GetComponent<HighlightEffect>().outline = 0.05f;
        //patty.GetComponent<HighlightEffect>().innerGlow = 0.1f;
        while (lgrabstatus.IsGrabbing == false || rgrabstatus.IsGrabbing == false)
        {
            yield return null;
            if (lgrabstatus.IsGrabbing == true || rgrabstatus.IsGrabbing == true)
            {
                break;
            }
        }
        while (lgrabstatus.IsGrabbing == true || rgrabstatus.IsGrabbing == true)
        {
            if (lgrabstatus.IsGrabbing == true && islgrabstatus == false)
            {
                islgrabstatus = true;
            }
            else if (rgrabstatus.IsGrabbing == true && isrgrabstatus == false)
            {
                isrgrabstatus = true;
            }
            yield return null;
            if ((lgrabstatus.IsGrabbing == false && lgrabstatus == true) || (rgrabstatus.IsGrabbing == false && rgrabstatus == true))
            {
                break;
            }
        }
        yield return new WaitForSeconds(2f);
        audiosource.PlayOneShot(audioclip2[8]);
        audiosource.Stop();
        patty.GetComponent<HighlightEffect>().highlighted = false;
        arrow.SetActive(true);
        guidetext.text = "?????? ??????????.\n5???? ?????? ??????????\n10???? ?????? ???? ??????.";
        audiosource.PlayOneShot(audioclip[2]);
        viedoplayer.clip = viedoclips[2];
        viedoplayer.transform.parent.GetChild(1).localPosition = new Vector3(-0.025f, 0.557f, 0);
        yield return new WaitForSeconds(10f);
        foreach (var item in foods)
        {
            item.GetComponent<Grabbable>().enabled = true;
            item.GetComponent<PhysicsGrabbable>().enabled = true;
        }
        foreach (var item in sources)
        {
            item.GetComponent<Grabbable>().enabled = true;
        }
        audiosource.PlayOneShot(audioclip2[8]);

        audioing = true;
        audiosource.PlayOneShot(audioclip[5]);
        viedoplayer.clip = viedoclips[3];
        guidetext.text = "???? ???? ?????? \n?????? ?????? ????????\n ???????? ??????????????.";

        ////???????? ???????? ??????
    }

    //???????? ???? ?????? Setactiove ????, selecthambugurcard?? ???? ????
    public void LevelBurgurSetting()
    {
        TutorialLevelBurgerCard.SetActive(true);
        selecthambugurcard = TutorialLevelBurgerCard;
        sourceCard[0].SetActive(true);
    }

}
