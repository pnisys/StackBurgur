using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
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
    public GameObject[] oculusbutton;

    int ran = 0;
    int ran1 = 0;
    bool testsuccess = true;
    public bool phase1source = false;
    public bool phase2source = false;
    bool buttonclick = false;
    InteractableUnityEventWrapper buttonevent;

    private void OnEnable()
    {
        GameManager.OnButtonLifeDie += this.Died;
    }
    private void OnDisable()
    {
        GameManager.OnButtonLifeDie -= this.Died;
    }

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
        buttonevent = GameObject.FindGameObjectWithTag("BIGBUTTON").GetComponent<InteractableUnityEventWrapper>();
        buttonevent.WhenSelect.AddListener(ButtonControl);
        audiosource.PlayOneShot(audioclip[3]);
        audiosource.clip = audioclip[4];
        audiosource.Play();
        mantray = gameObject.transform.GetChild(3);
        gamemanager.currentpeople = gameObject;
        phase1source = true;
        //????????????, ?????? ???? ????
        gamemanager.uppeople.Add(gameObject);
        gamemanager.peoplenumbur++;
        gamemanager.phase1selectedsource = null;
        gamemanager.phase2selectedsource = null;
        oculusbutton[0].SetActive(false);
        oculusbutton[1].SetActive(false);
        //1. ???? ????
        StartCoroutine(ClerkStateCheck());
        //2.???? ???????? ????
        RandomNumberSelect();

        StageLevelRandom();
        agent.destination = clerkcollider.transform.position;
    }

    //????????,?????? ???? ????
    void StageLevelRandom()
    {
        int randomstage = 0;
        if (gamemanager.stage == 1)
        {
            gamemanager.level = 1;
            gamemanager.limitTime = 90f;
            gamemanager.orderlimitTime = 20f;
        }
        else if (gamemanager.stage == 2)
        {
            randomstage = UnityEngine.Random.Range(1, 3);
            gamemanager.level = randomstage;
            gamemanager.limitTime = 90f;
            gamemanager.orderlimitTime = 20f;

        }
        else if (gamemanager.stage == 3)
        {
            randomstage = UnityEngine.Random.Range(2, 4);
            gamemanager.level = randomstage;
            gamemanager.limitTime = 90f;
            gamemanager.orderlimitTime = 20f;

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


    //1. ?????? ???????? ?????????? ????
    IEnumerator ClerkStateCheck()
    {
        while (gamemanager.isClerk == false)
        {
            yield return new WaitForSeconds(0.3f);
            if (gamemanager.isClerk == true)
            {
                print("??????");
                audiosource.Stop();
                //???? Navagent ??????
                agent.isStopped = true;
                //???? ?? ???? ?????????? ????
                animator.SetBool(hashIdle, true);
                if (gamemanager.isbutton == false)
                {
                    StartCoroutine(ThinkBallon());
                }
            }
        }
    }

    //2. 2?? ?????????? ??????
    IEnumerator ThinkBallon()
    {
        if (gamemanager.isbutton == true)
        {
            yield break;
        }
        yield return new WaitForSeconds(1f);


        audiosource.PlayOneShot(audioclip[5]);
        //?????? ???????? ???? ????
        gamemanager.isThinking = true;
        //?????? ???????? ?????????? ????
        animator.SetBool(hashIdle, false);
        animator.SetBool(hashTalk, true);

        //???????? ???? ?????? ?????? ???? ???????? ?????? ?????? ????????
        LevelBurgurSetting();
        //???????? ?????? ????
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        if (gamemanager.isbutton == false)
        {
            audiosource.clip = audioclip[0];
            audiosource.Play();
        }
        //???????? ?????? ???? ???? ????
        while (gamemanager.isThinking == true)
        {
            yield return null;
            if (gamemanager.isbutton == true)
            {
                yield break;
            }
            //???????? ???? 15?? ????
            gamemanager.orderlimitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "???? ???? : " + Mathf.Round(gamemanager.orderlimitTime).ToString() + "??";

            //15?? ?????? ?????????? ??????
            if (gamemanager.orderlimitTime < 0)
            {
                //??????
                //gamemanager.orderlimitTime = 20;
                audiosource.Stop();
                gamemanager.isThinking = false;
            }
        }
        //?????????? 4?? ????????
        if (gamemanager.stage >= 4)
        {
            selecthambugurcard2.SetActive(false);
            selectsourcecard2.SetActive(false);
        }
        //???? Setactive(false); ??????
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        selecthambugurcard.SetActive(false);
        selectsourcecard.SetActive(false);
        animator.SetBool(hashTalk, false);
        StartCoroutine(Order());
    }

    //3. ???? ????
    IEnumerator Order()
    {
        if (gamemanager.isbutton == true)
        {
            yield break;
        }
        oculusbutton[0].SetActive(true);
        oculusbutton[1].SetActive(true);
        audiosource.PlayOneShot(audioclip[6]);
        //???? ???? On
        gamemanager.isOrder = true;
        animator.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        audiosource.Play();
        while (gamemanager.isOrder == true)
        {
            yield return null;
            //???? ???? ??????

            gamemanager.limitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "???? ???? : " + Mathf.Round(gamemanager.limitTime).ToString() + "??";

            //???? ??????
            if (gamemanager.limitTime < 0)
            {
                ////?????????? 4????????
                //if (gamemanager.stage >= 4)
                //{
                //    audiosource.PlayOneShot(audioclip[6]);
                //    phase1source = false;
                //    phase2source = true;
                //    StartCoroutine(Stage45());
                //    yield break;
                //}
                ////?????????? 4????????
                //else
                //{
                //    audiosource.Stop();
                //    //agent ???????? ????
                //    agent.isStopped = false;
                //    //???? ???? ????
                //    animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                //    animator.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                //    //???? TrayControl?? ?????? ???? ???????? ???? ?????? ?????? ????
                //    OnLimitTimeComplete();
                //    yield return new WaitForSeconds(1f);
                //    //?????? ????????
                //    gamemanager.isOrder = false;
                //    //???? ?? ????????
                //    if (gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true)
                //    {
                //        StartCoroutine(SucessTable());
                //    }
                //    //???? ?? ??????
                //    else if (gamemanager.isfail)
                //    {
                //        StartCoroutine(FailTable());
                //    }
                //}
                StartCoroutine(RealButtonControl());
            }
        }
    }
    public void ButtonControl()
    {
        StartCoroutine(RealButtonControl());
    }
    IEnumerator RealButtonControl()
    {
        print("?p?? ?????");
        if (gamemanager.stage < 4 && gamemanager.isOrder == false)
        {
            yield break;
        }
        else if (gamemanager.stage >= 4 && gamemanager.isOrder == false && gamemanager.isOrder2 == false)
        {
            yield break;
        }
        //?????????? 4????????
        if (gamemanager.stage >= 4 && gamemanager.isOrder2 == false && gamemanager.isOrder == true)
        {
            gamemanager.isOrder = false;
            gamemanager.isOrder2 = true;
            audiosource.PlayOneShot(audioclip[6]);
            phase1source = false;
            phase2source = true;
            StartCoroutine(Stage45());
            yield break;
        }
        //?????????? 4????????
        else if (gamemanager.stage < 4 && gamemanager.isOrder == true)
        {
            gamemanager.isOrder = false;
            audiosource.Stop();
            //agent ???????? ????
            agent.isStopped = false;
            //???? ???? ????
            animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            animator.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            //???? TrayControl?? ?????? ???? ???????? ???? ?????? ?????? ????
            OnLimitTimeComplete();
            yield return new WaitForSeconds(1f);
            ////?????? ????????
            //gamemanager.isOrder = false;
            //???? ?? ????????
            if (gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true)
            {
                StartCoroutine(SucessTable());
            }
            //???? ?? ??????
            else if (gamemanager.isfail)
            {
                StartCoroutine(FailTable());
            }
            buttonevent.WhenSelect.RemoveListener(ButtonControl);
        }
        else if (gamemanager.isOrder2 == true && gamemanager.isOrder == false && gamemanager.stage >= 4)
        {
            buttonclick = true;
            gamemanager.isOrder2 = false;
            audiosource.Stop();
            phase2source = false;

            //agent ???????? ????
            agent.isStopped = false;
            //???? ???? ????
            animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);

            //???? TrayControl?? ?????? ???? ???????? ???? ?????? ?????? ????
            OnLimitTimeComplete();
            yield return new WaitForSeconds(1f);
            //?????? ????????
            gamemanager.isOrder = false;
            //???? ?? ????????
            if (((gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true) && (gamemanager.iscompletesuccess2 == true || gamemanager.islittlesuccess2 == true)))
            {
                StartCoroutine(SucessTable());
            }
            //?? ?? ???????? ????????
            else if (gamemanager.isfail == true || gamemanager.isfail2 == true)
            {
                StartCoroutine(FailTable());
            }
            buttonevent.WhenSelect.RemoveListener(ButtonControl);
        }
    }
    //???????? 4,5 ???? ????
    IEnumerator Stage45()
    {
        if (gamemanager.isbutton == true)
        {
            yield break;
        }
        audiosource.Play();
        print("???? ?? ?????");
        foreach (var item in traycontrol.stackcreateburgur.ToArray())
        {
            item.transform.parent = traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(15).transform;
            item.transform.localPosition = new Vector3(0, item.transform.localPosition.y + 0.27f, 0);
            traycontrol.stackcreateburgur.Pop();
        }
        //?????? ???? ???????
        if (traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(13).childCount == 1)
        {
            GameObject aa = traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(13).GetChild(0).gameObject;
            aa.transform.parent = traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(16).transform;
            aa.transform.localPosition = new Vector3(0, 0, 0);
        }

        //4?????? 5?????? ???? ?? ?????? ??
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
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "???? ???? : " + Mathf.Round(gamemanager.limitTime).ToString() + "??";
            if (gamemanager.limitTime < 0)
            {
                audiosource.Stop();
                phase2source = false;

                //agent ???????? ????
                agent.isStopped = false;
                //???? ???? ????
                animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);

                //???? TrayControl?? ?????? ???? ???????? ???? ?????? ?????? ????
                OnLimitTimeComplete();
                yield return new WaitForSeconds(1f);
                //?????? ????????
                gamemanager.isOrder = false;
                //???? ?? ????????
                if (((gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true) && (gamemanager.iscompletesuccess2 == true || gamemanager.islittlesuccess2 == true))/* || testsuccess == true*/)
                {
                    StartCoroutine(SucessTable());
                }
                //?? ?? ???????? ????????
                else if (gamemanager.isfail == true || gamemanager.isfail2 == true)
                {
                    StartCoroutine(FailTable());
                }
                yield break;
            }
            else if (buttonclick == true)
            {
                yield break;
            }
        }
    }

    //??????, ??????, ?????? ?????????? ????
    IEnumerator FailTable()
    {
        audiosource.PlayOneShot(audioclip[1]);
        gamemanager.LifeScore--;
        //???? ?????????? ????
        animator.SetBool(hashfail, true);
        mood.SetActive(true);
        mood.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        //???? ?? ?????? ??
        if (gamemanager.lifescore == 0)
        {
            OnLifeDie();
            StartCoroutine(RealDied());
        }
        yield return new WaitForSeconds(5f);
        mood.SetActive(false);
        mood.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        //?????? ????.
        agent.destination = door.transform.position;
        audiosource.clip = audioclip[4];
        audiosource.Play();

        //?? ???? ???? ?????? ????????
        yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);
        audiosource.Stop();
        agent.isStopped = true;
        agent.enabled = false;
        transform.position = new Vector3(100, 100, 100);
        animator.SetBool(hashfail, false);
        yield return new WaitForSeconds(2f);
        //???? ???? ???? ?????? ??????
        GameManagerInit();
        gamemanager.people[gamemanager.peoplenumbur].SetActive(true);

    }
    //??????, ??????, ?????? ?????????? ????
    IEnumerator SucessTable()
    {
        mood.SetActive(true);
        mood.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        audiosource.PlayOneShot(audioclip[2]);
        //?????? ???? ???? ????????
        gamemanager.tablepeoplenumbur++;
        mantray.gameObject.SetActive(true);
        //?????? 10?? ?? ?????????? ????, ???? ???? ?????? ???????? ????????
        //???? ???????? ????????
        for (int i = 0; i < 10; i++)
        {
            if (gamemanager.istable[i] == false)
            {
                animator.SetBool(hashSuccess, true);
                //yield return new WaitForSeconds(3f);
                //?????? ??????????
                //???????? ???? ????
                agent.destination = table[i].transform.position;
                audiosource.clip = audioclip[4];
                audiosource.Play();
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
                transform.localPosition = gamemanager.tableposition[i];
                transform.localRotation = gamemanager.tablerotation[i];
                //???? ?????????? ????
                animator.SetBool(hasheat, true);
                gameObject.transform.GetChild(3).localPosition = new Vector3(0, 0.879f, 0.579f);
                //?????? ???????? ???? ????
                gamemanager.istable[i] = true;
                GameManagerInit();
                //???????????? 5???? ????????
                if (gamemanager.tablepeoplenumbur == 5)
                {
                    if (gamemanager.stage <= 4)
                    {
                        //???????? ?? ???? ????
                        StageUp();
                        gamemanager.isClerk = false;
                    }
                    //???? ???? ??????
                    else
                    {
                        OnClear();
                        StartCoroutine(StageComplete5());
                        gamemanager.isClerk = false;
                    }
                    yield break;
                }
                gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
                //???? ??????
                break;
            }
        }
        yield return new WaitForSeconds(2f);
        gamemanager.iscompletesuccess = false;
        gamemanager.iscompletesuccess2 = false;
        gamemanager.islittlesuccess = false;
        gamemanager.islittlesuccess2 = false;

    }

    //????????
    void Died()
    {
        StartCoroutine(RealDied());
    }
    //??????
    IEnumerator RealDied()
    {
        foreach (var item in gamemanager.people)
        {
            if (gamemanager.currentpeople != item)
            {
                item.SetActive(false);
            }
        }
        audiosource.Stop();
        ranking.gameObject.transform.position = new Vector3(-67.44f, 0.62f, 34.16f);
        gamemanager.GetComponent<AudioSource>().Stop();

        animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        animator.gameObject.transform.GetChild(2).gameObject.SetActive(false);

        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(0);
    }

    //???? 5???????? ????
    IEnumerator StageComplete5()
    {
        GameManagerInit();
        gamemanager.GetComponent<AudioSource>().Stop();
        ranking.gameObject.transform.position = new Vector3(-67.44f, 0.62f, 34.16f);

        //gamemanager.GuideUiText.text = "???? ???? ??????";
        //gamemanager.GuideUiText.fontSize = 0.05f;
        //gamemanager.GuideUiText2.text = null;
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }

    //???????? ?????? ?? ????
    void StageUp()
    {
        //???????? ??!
        gamemanager.Stage++;
        //?????? ??????
        OnStageChange();
        gamemanager.tablepeoplenumbur = 0;
        //gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
        //???? ???????? ???? ?????? ???? People?? ???? ????????????
        foreach (var item in gamemanager.uppeople)
        {
            item.SetActive(false);
            item.transform.parent = people.transform;
            item.transform.localPosition = new Vector3(0, 0, 0);
            item.transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
        if (gamemanager.istable[9] == true)
        {
            for (int i = 0; i < 10; i++)
            {
                gamemanager.istable[i] = false;
            }
        }
        GameManagerInit();
    }


    //GameManager ?????? ??????
    public void GameManagerInit()
    {
        gamemanager.iscompletesuccess = false;
        gamemanager.iscompletesuccess2 = false;
        gamemanager.islittlesuccess = false;
        gamemanager.islittlesuccess2 = false;
        gamemanager.isfail = false;
        gamemanager.isfail2 = false;
    }
    //???????? ???? ?????? Setactiove ????, selecthambugurcard?? ???? ????
    public void LevelBurgurSetting()
    {
        if (gamemanager.level == 1)
        {
            //?????????? 4????
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
            //?????????? 4????
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
            //?????????? 4????
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
            //?????????? 4????
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

    //8?????? ?????? ???? ???????? ????
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
