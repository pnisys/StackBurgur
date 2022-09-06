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

        //���ӸŴ�����, ������ ��� ���
        gamemanager.uppeople.Add(gameObject);
        gamemanager.peoplenumbur++;
        //1. �մ� ����
        StartCoroutine(ClerkStateCheck());
        //2.���� �����ϰ� ����
        RandomNumberSelect();

        //��������,���̵� �ǵ� �Լ�
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


    //1. ������ ������� �ɾ���ٰ� ����
    IEnumerator ClerkStateCheck()
    {
        while (gamemanager.isClerk == false)
        {
            yield return new WaitForSeconds(0.3f);
            if (gamemanager.isClerk == true)
            {
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
        //�մ��� �ֹ��ϴ� ���� �ѱ�
        gamemanager.isThinking = true;
        //�մ��� �ֹ��ϴ� �ִϸ��̼� �ѱ�
        animator.SetBool(hashIdle, false);
        animator.SetBool(hashTalk, true);

        //���̵��� ���� �ܹ��� ī�忡 �´� �ܹ��ſ� �ҽ��� ī�带 �����ֱ�
        LevelBurgurSetting();
        //���ѽð� ĵ���� �ѱ�
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(true);
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
                gamemanager.orderlimitTime = 1;
                gamemanager.isThinking = false;
            }
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
        //�ֹ� ���� On
        gamemanager.isOrder = true;

        while (gamemanager.isOrder == true)
        {
            yield return null;
            //���� �ð� ���ֱ�
            gamemanager.limitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "���� �ð� : " + Mathf.Round(gamemanager.limitTime).ToString() + "��";

            //�ð� ������
            if (gamemanager.limitTime < 0)
            {
                //agent ������� ����
                agent.isStopped = false;
                //���� �ð� ����
                animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                animator.gameObject.transform.GetChild(2).gameObject.SetActive(false);
                //�̶� TrayControl�� ������ �Ͱ� ������� �� �Լ��� ������ ����
                print("���� ����� Ÿ�°ž�?");
                OnLimitTimeComplete();
                yield return new WaitForSeconds(1f);
                //�ʱ�ȭ �����ֱ�
                gamemanager.limitTime = 1f;
                gamemanager.isOrder = false;
                //�˻� �� �����̸�
                if (gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true)
                {

                    mantray.gameObject.SetActive(true);
                    //���̺� 10�� �� ���������� �ɰ�, ���� ���� �մ��� ���̺� �ɾ�����
                    //���� ���̺�� �Ѿ��
                    for (int i = 0; i < 10; i++)
                    {
                        if (gamemanager.istable[i] == false)
                        {
                            //������ �ִϸ��̼�
                            animator.SetBool(hashSuccess, true);
                            //���̺�� ���� �ϱ�
                            agent.destination = table[i].transform.position;
                            //��ó ���� ���������� ����
                            yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);
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
                            //�ݺ� ������
                            break;
                        }
                    }
                    yield return new WaitForSeconds(2f);
                    //���� �ٸ� ��� �Ѹ��� �Ѿߵ�
                    gamemanager.iscompletesuccess = false;
                    gamemanager.islittlesuccess = false;
                    //���̺� ���� �Ѹ� �÷��ְ�
                    gamemanager.tablepeoplenumbur++;
                    //���̺���ڰ� 5���� �Ǿ�����
                    if (gamemanager.tablepeoplenumbur == 5)
                    {
                        //�������� �� �Լ� ȣ��
                        StageUp();
                        yield break;
                    }
                    else
                    {
                        gamemanager.people[gamemanager.tablepeoplenumbur].SetActive(true);
                    }

                }
                //�˻� �� ���и�
                else if (gamemanager.isfail)
                {
                    //���� �ִϸ��̼� ����
                    animator.SetBool(hashfail, true);
                    yield return new WaitForSeconds(5f);
                    //������ ����.
                    agent.destination = door.transform.position;

                    //�� ���� ���� ������ ������
                    yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);

                    agent.isStopped = true;
                    agent.enabled = false;
                    transform.position = new Vector3(100, 100, 100);
                    animator.SetBool(hashfail, false);
                    yield return new WaitForSeconds(2f);
                    //���� �ٸ� ��� �Ѹ��� �Ѿߵ�
                    gamemanager.people[gamemanager.peoplenumbur].SetActive(true);
                    gamemanager.isfail = false;
                    Destroy(gameObject);
                }
            }
        }
    }

    void StageUp()
    {
        //�������� ��!
        gamemanager.stage++;
        gamemanager.people[gamemanager.tablepeoplenumbur].SetActive(true);
        //���� ���̺� �ִ� �մԵ� �ٽ� People�� �ְ� �ʱ�ȭ��Ű��
        foreach (var item in gamemanager.uppeople)
        {
            item.SetActive(false);
            item.transform.parent = people.transform;
            item.transform.localPosition = new Vector3(0, 0, 0);
            item.transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
        gamemanager.tablepeoplenumbur = 0;
        //��������
    }



    //���̵��� �´� ī�带 Setactiove �ϰ�, selecthambugurcard�� �ִ� �Լ�
    public void LevelBurgurSetting()
    {
        //������ �ܹ��� ���̵�, 0���� ���õ� ����
        //Ʃ�丮�� ����
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
