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
        //���ӸŴ�����, ������ ��� ���
        gamemanager.uppeople.Add(gameObject);
        gamemanager.peoplenumbur++;
        gamemanager.phase1selectedsource = null;
        gamemanager.phase2selectedsource = null;

        //1. �մ� ����
        StartCoroutine(ClerkStateCheck());
        //2.���� �����ϰ� ����
        RandomNumberSelect();

        StageLevelRandom();
        agent.destination = clerkcollider.transform.position;
    }

    //��������,���̵� �ǵ� �Լ�
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


    //1. ������ ������� �ɾ���ٰ� ����
    IEnumerator ClerkStateCheck()
    {
        while (gamemanager.isClerk == false)
        {
            yield return new WaitForSeconds(0.3f);
            if (gamemanager.isClerk == true)
            {
                print("�����");
                audiosource.Stop();
                //�մ� Navagent ���ֱ�
                agent.isStopped = true;
                //�մ� �� �ִ� �ִϸ��̼� Ʋ��
                animator.SetBool(hashIdle, true);
                StartCoroutine(ThinkBallon());
            }
        }
    }

    //2. 2�� ����ϴٰ� �ֹ���
    IEnumerator ThinkBallon()
    {
        yield return new WaitForSeconds(1f);
        audiosource.PlayOneShot(audioclip[5]);
        //�մ��� �ֹ��ϴ� ���� �ѱ�
        gamemanager.isThinking = true;
        //�մ��� �ֹ��ϴ� �ִϸ��̼� �ѱ�
        animator.SetBool(hashIdle, false);
        animator.SetBool(hashTalk, true);

        //���̵��� ���� �ܹ��� ī�忡 �´� �ܹ��ſ� �ҽ��� ī�带 �����ֱ�
        LevelBurgurSetting();
        //���ѽð� ĵ���� �ѱ�
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        audiosource.clip = audioclip[0];
        audiosource.Play();
        //�ֹ��ϰ� ������ ��� �ݺ� �ѱ�
        while (gamemanager.isThinking == true)
        {
            yield return null;
            //�ֹ��ϴ� �ð� 15�� ����
            gamemanager.orderlimitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "���� �ð� : " + Mathf.Round(gamemanager.orderlimitTime).ToString() + "��";

            //15�� ������ �����ܰ�� �Ѿ
            if (gamemanager.orderlimitTime < 0)
            {
                //�ʱ�ȭ
                //gamemanager.orderlimitTime = 20;
                audiosource.Stop();
                gamemanager.isThinking = false;
            }
        }
        //���������� 4�� ��쿡��
        if (gamemanager.stage >= 4)
        {
            selecthambugurcard2.SetActive(false);
            selectsourcecard2.SetActive(false);
        }
        //ī�� Setactive(false); ��Ű��
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        selecthambugurcard.SetActive(false);
        selectsourcecard.SetActive(false);
        animator.SetBool(hashTalk, false);
        StartCoroutine(Order());
    }

    //3. �ֹ� ����
    IEnumerator Order()
    {
        audiosource.PlayOneShot(audioclip[6]);
        //�ֹ� ���� On
        gamemanager.isOrder = true;
        animator.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        audiosource.Play();
        while (gamemanager.isOrder == true)
        {
            yield return null;
            //���� �ð� ���ֱ�

            gamemanager.limitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "���� �ð� : " + Mathf.Round(gamemanager.limitTime).ToString() + "��";

            //�ð� ������
            if (gamemanager.limitTime < 0)
            {
                //���������� 4�̻��̸�
                if (gamemanager.stage >= 4)
                {
                    audiosource.PlayOneShot(audioclip[6]);
                    phase1source = false;
                    phase2source = true;
                    StartCoroutine(Stage45());
                    yield break;
                }
                //���������� 4�����̸�
                else
                {
                    audiosource.Stop();
                    //agent ������� ����
                    agent.isStopped = false;
                    //���� �ð� ����
                    animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    animator.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                    //�̶� TrayControl�� ������ �Ͱ� ������� �� �Լ��� ������ ����
                    OnLimitTimeComplete();
                    yield return new WaitForSeconds(1f);
                    //�ʱ�ȭ �����ֱ�
                    gamemanager.isOrder = false;
                    //�˻� �� �����̸�
                    if (gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true)
                    {
                        StartCoroutine(SucessTable());
                    }
                    //�˻� �� ���и�
                    else if (gamemanager.isfail)
                    {
                        StartCoroutine(FailTable());
                    }
                }
            }
        }
    }

    //�������� 4,5 ���� ó��
    IEnumerator Stage45()
    {
        audiosource.Play();
        print("�̰� �� ��Ž?");
        foreach (var item in traycontrol.stackcreateburgur.ToArray())
        {
            item.transform.parent = traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(15).transform;
            item.transform.localPosition = new Vector3(0, item.transform.localPosition.y + 0.27f, 0);
            traycontrol.stackcreateburgur.Pop();
        }
        //�ҽ��� ���� �ִٸ�?
        if (traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(13).childCount == 1)
        {
            GameObject aa = traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(13).GetChild(0).gameObject;
            aa.transform.parent = traycontrol.gameObject.transform.GetChild(0).GetChild(0).GetChild(16).transform;
            aa.transform.localPosition = new Vector3(0, 0, 0);
        }

        //4�ܰ質 5�ܰ�� �ѹ� �� ���ƾ� ��
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
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "���� �ð� : " + Mathf.Round(gamemanager.limitTime).ToString() + "��";
            if (gamemanager.limitTime < 0)
            {
                audiosource.Stop();
                phase2source = false;

                //agent ������� ����
                agent.isStopped = false;
                //���� �ð� ����
                animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);

                //�̶� TrayControl�� ������ �Ͱ� ������� �� �Լ��� ������ ����
                OnLimitTimeComplete();
                yield return new WaitForSeconds(1f);
                //�ʱ�ȭ �����ֱ�
                gamemanager.isOrder = false;
                //�˻� �� �����̸�
                if (((gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true) && (gamemanager.iscompletesuccess2 == true || gamemanager.islittlesuccess2 == true))/* || testsuccess == true*/)
                {
                    StartCoroutine(SucessTable());
                }
                //�� �� �ϳ��� �����ϸ�
                else if (gamemanager.isfail == true || gamemanager.isfail2 == true)
                {
                    StartCoroutine(FailTable());
                }
                yield break;
            }
        }
    }

    //���н�, ���̺�, ĳ���� �ִϸ��̼� ó��
    IEnumerator FailTable()
    {
        audiosource.PlayOneShot(audioclip[1]);
        gamemanager.LifeScore--;
        //���� �ִϸ��̼� ����
        animator.SetBool(hashfail, true);
        mood.SetActive(true);
        mood.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        //���� �� ���� ��
        if (gamemanager.lifescore == 0)
        {
            OnLifeDie();
            StartCoroutine(Died());
        }
        yield return new WaitForSeconds(5f);
        mood.SetActive(false);
        mood.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        //������ ����.
        agent.destination = door.transform.position;
        audiosource.clip = audioclip[4];
        audiosource.Play();

        //�� ���� ���� ������ ������
        yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);
        audiosource.Stop();
        agent.isStopped = true;
        agent.enabled = false;
        transform.position = new Vector3(100, 100, 100);
        animator.SetBool(hashfail, false);
        yield return new WaitForSeconds(2f);
        //���� �ٸ� ��� �Ѹ��� �Ѿߵ�
        GameManagerInit();
        gamemanager.people[gamemanager.peoplenumbur].SetActive(true);

    }
    //������, ���̺�, ĳ���� �ִϸ��̼� ó��
    IEnumerator SucessTable()
    {
        mood.SetActive(true);
        mood.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        audiosource.PlayOneShot(audioclip[2]);
        //���̺� ���� �Ѹ� �÷��ְ�
        gamemanager.tablepeoplenumbur++;
        mantray.gameObject.SetActive(true);
        //���̺� 10�� �� ���������� �ɰ�, ���� ���� �մ��� ���̺� �ɾ�����
        //���� ���̺�� �Ѿ��
        for (int i = 0; i < 10; i++)
        {
            if (gamemanager.istable[i] == false)
            {
                animator.SetBool(hashSuccess, true);
                //yield return new WaitForSeconds(3f);
                //������ �ִϸ��̼�
                //���̺�� ���� �ϱ�
                agent.destination = table[i].transform.position;
                audiosource.clip = audioclip[4];
                audiosource.Play();
                //��ó ���� ���������� ����
                yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);
                mood.SetActive(false);
                mood.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
                audiosource.Stop();
                //agent�� ���߱�
                agent.isStopped = true;
                agent.enabled = false;
                //����� ���̺� �ڽ����� ����.
                transform.parent = table[i].transform;
                //�� ���̺� �������� �̸� �����س��Ҵ� ������ ����.
                transform.localPosition = gamemanager.tableposition[i];
                transform.localRotation = gamemanager.tablerotation[i];
                //�Դ� �ִϸ��̼� ����
                animator.SetBool(hasheat, true);
                gameObject.transform.GetChild(3).localPosition = new Vector3(0, 0.879f, 0.579f);
                //�� ���̺��� �ݰ� �ϱ�
                gamemanager.istable[i] = true;
                GameManagerInit();
                //���̺���ڰ� 5���� �Ǿ�����
                if (gamemanager.tablepeoplenumbur == 5)
                {
                    if (gamemanager.stage <= 4)
                    {
                        //�������� �� �Լ� ȣ��
                        StageUp();
                        gamemanager.isClerk = false;
                    }
                    //���� ��� Ŭ����
                    else
                    {
                        OnClear();
                        StartCoroutine(StageComplete5());
                        gamemanager.isClerk = false;
                    }
                    yield break;
                }
                gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
                //�ݺ� ������
                break;
            }
        }
        yield return new WaitForSeconds(2f);
        gamemanager.iscompletesuccess = false;
        gamemanager.iscompletesuccess2 = false;
        gamemanager.islittlesuccess = false;
        gamemanager.islittlesuccess2 = false;

    }

    //������
    IEnumerator Died()
    {
        //ranking.ScoreSet(gamemanager.score, SoundManager.instance.username,gamemanager.stage);
        audiosource.PlayOneShot(audioclip[8]);
        gamemanager.GetComponent<AudioSource>().Stop();
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(0);
    }

    //��� 5�������� �Ϸ�
    IEnumerator StageComplete5()
    {
        GameManagerInit();
        gamemanager.GetComponent<AudioSource>().Stop();
        audiosource.PlayOneShot(audioclip[9]);
        //gamemanager.GuideUiText.text = "���� ��� Ŭ����";
        //gamemanager.GuideUiText.fontSize = 0.05f;
        //gamemanager.GuideUiText2.text = null;
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene(0);
    }

    //�������� ������ �� �Լ�
    void StageUp()
    {
        //�������� ��!
        gamemanager.Stage++;
        OnStageChange();
        gamemanager.tablepeoplenumbur = 0;
        //gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
        //���� ���̺� �ִ� �մԵ� �ٽ� People�� �ְ� �ʱ�ȭ��Ű��
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
    //���̵��� �´� ī�带 Setactiove �ϰ�, selecthambugurcard�� �ִ� �Լ�
    public void LevelBurgurSetting()
    {
        if (gamemanager.level == 1)
        {
            //���������� 4���
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
            //���������� 4���
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
            //���������� 4���
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
            //���������� 4���
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

    //8���߿� ������ ���� ��󳻴� �Լ�
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
