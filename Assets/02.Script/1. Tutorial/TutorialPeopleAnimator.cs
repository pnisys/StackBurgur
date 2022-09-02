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
        MeatControl.OnGoodMeeting += GoodMeeting;
        MeatControl.OnBadMeeting += BadMeeting;

    }
    private void OnDisable()
    {
        MeatControl.OnGoodMeeting -= GoodMeeting;
        MeatControl.OnBadMeeting -= BadMeeting;

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
    public AudioSource audiosource;

    int ran = 0;
    int ran1 = 0;

    bool audioing = false;
    bool isgoodmeat = false;
    bool isbadmeat = false;

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

        //1. �մ� ����
        StartCoroutine(ClerkStateCheck());
        //2. ���� �����ϰ� ����
        //RandomNumberSelect();
    }


    //1. ������ ������� �ɾ���ٰ� ����
    IEnumerator ClerkStateCheck()
    {
        while (tutorialgamemanager.isClerk == false)
        {
            yield return new WaitForSeconds(0.3f);
            if (tutorialgamemanager.isClerk == true)
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
        yield return new WaitForSeconds(2f);


        //�մ��� �ֹ��ϴ� ���� �ѱ�
        tutorialgamemanager.isThinking = true;
        //�մ��� �ֹ��ϴ� �ִϸ��̼� �ѱ�
        animator.SetBool(hashIdle, false);
        animator.SetBool(hashTalk, true);

        StartCoroutine(VoiceControl());

        //���̵��� ���� �ܹ��� ī�忡 �´� �ܹ��ſ� �ҽ��� ī�带 �����ֱ�
        LevelBurgurSetting();
        //���ѽð� ĵ���� �ѱ�
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        //Ʃ�丮�� �ȳ�ĵ���� ������ ���ѽð� �帣�� �ϱ�
        yield return new WaitUntil(() => audioing == true);
        //�ֹ��ϰ� ������ ��� �ݺ� �ѱ�
        while (tutorialgamemanager.isThinking == true)
        {
            yield return null;
            //�ֹ��ϴ� �ð� 15�� ����
            tutorialgamemanager.orderlimitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "���� �ð� : " + Mathf.Round(tutorialgamemanager.orderlimitTime).ToString() + "��";

            //15�� ������ �����ܰ�� �Ѿ
            if (tutorialgamemanager.orderlimitTime < 0)
            {
                //�ʱ�ȭ
                tutorialgamemanager.orderlimitTime = 15;
                tutorialgamemanager.isThinking = false;
            }
        }

        //ī�� Setactive(false); ��Ű��
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        TutorialLevelBurgerCard.SetActive(false);
        sourceCard[0].SetActive(false);
        animator.SetBool(hashTalk, false);
        StartCoroutine(Order());
    }

    //3. �ֹ� ����
    IEnumerator Order()
    {
        //�ֹ� ���� On
        tutorialgamemanager.isOrder = true;

        while (tutorialgamemanager.isOrder == true)
        {
            yield return null;
            //���� �ð� ���ֱ�
            tutorialgamemanager.limitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "���� �ð� : " + Mathf.Round(tutorialgamemanager.limitTime).ToString() + "��";

            //�ð� ������
            if (tutorialgamemanager.limitTime < 0)
            {
                //agent ������� ����
                agent.isStopped = false;
                //���� �ð� ����
                animator.gameObject.transform.GetChild(1).gameObject.SetActive(false);
                //�̶� TrayControl�� ������ �Ͱ� ������� �� �Լ��� ������ ����
                OnLimitTimeComplete();
                yield return new WaitForSeconds(1f);
                //�ʱ�ȭ �����ֱ�
                tutorialgamemanager.limitTime = 30f;
                tutorialgamemanager.isOrder = false;
                //�˻� �� �����̸�
                if (tutorialgamemanager.iscompletesuccess == true || tutorialgamemanager.islittlesuccess == true)
                {
                    //���̺� 10�� �� ���������� �ɰ�, ���� ���� �մ��� ���̺� �ɾ�����
                    //���� ���̺�� �Ѿ��
                    for (int i = 0; i < 10; i++)
                    {
                        if (istable[i] == false)
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
                            transform.localPosition = tableposition[i];
                            transform.localRotation = tablerotation[i];
                            //�Դ� �ִϸ��̼� ����
                            animator.SetBool(hasheat, true);
                            //�� ���̺��� �ݰ� �ϱ�
                            istable[i] = true;
                            //�ݺ� ������
                            break;
                        }
                    }
                }
                //�˻� �� ���и�
                else if (tutorialgamemanager.isfail)
                {
                    //���� �ִϸ��̼� ����
                    animator.SetBool(hashfail, true);
                    //5�� �ڿ�
                    yield return new WaitForSeconds(5f);
                    //������ ����.
                    agent.destination = door.transform.position;
                    //�� ���� ���� ������ ������
                    yield return new WaitUntil(() => agent.velocity.sqrMagnitude >= 0.2f && agent.remainingDistance <= 1);

                    agent.isStopped = true;
                    agent.enabled = false;
                    transform.position = new Vector3(100, 100, 100);
                    animator.SetBool(hashfail, false);
                }
            }
        }
    }

    //���� ��ư ������
    public void ButtonTutorialing()
    {
        StartCoroutine(VoiceControl());
    }

    //���� ��Ʈ�� �޼���
    IEnumerator VoiceControl()
    {
        guidetext.transform.parent.gameObject.SetActive(true);
        guidetext.text = "ī�带 ����\n\n�ܹ��� ��� ������\n\n�� �� �ֽ��ϴ�.";
        audiosource.PlayOneShot(audioclip[0]);

        yield return new WaitForSeconds(10f);
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        viedoplayer.transform.parent.GetChild(1).gameObject.SetActive(true);
        audiosource.PlayOneShot(audioclip[1]);
        guidetext.text = "������ ��Ʈ�ѷ� ��ư�� ������\n\n��Ḧ ���� �� �ֽ��ϴ�.\n\n��ư�� ���� ��Ḧ ����߸��ϴ�.\n\n��⸦ ��� ��������";
        viedoplayer.clip = viedoclips[0];
        viedoplayer.Play();
        patty.GetComponent<HighlightEffect>().highlighted = true;
        patty.GetComponent<HighlightEffect>().outline = 0.2f;
        patty.GetComponent<HighlightEffect>().innerGlow = 0.2f;
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
        audiosource.Stop();
        patty.GetComponent<HighlightEffect>().highlighted = false;
        guidetext.text = "��⸦ ���\n\n��⸦ ����������.\n\n5�ʰ� ������ ����������\n\n10�ʰ� ������ Ÿ�� �˴ϴ�.";
        audiosource.PlayOneShot(audioclip[2]);
        //viedoplayer.transform.parent.GetChild(1).localScale = new Vector3(0.005124412f, 0.0087115f, 0.005124412f);
        viedoplayer.transform.parent.GetChild(1).localPosition = new Vector3(-0.025f, 0.557f, 0);
        while (isgoodmeat == false)
        {
            yield return null;
            if (isgoodmeat == true)
            {
                audiosource.PlayOneShot(audioclip[3]);
                guidetext.text = "��Ⱑ �� ���������ϴ�.";
                break;
            }
        }
        while (isbadmeat == false)
        {
            yield return null;
            if (isbadmeat == true)
            {
                audiosource.PlayOneShot(audioclip[4]);
                guidetext.text = "��Ⱑ Ÿ���Ƚ��ϴ�. \n\n Ÿ���� ���� ������ \n\n �ٽ� ��⸦ �� ������ \n\n ���ÿ� �÷��ּ���";
                viedoplayer.clip = viedoclips[3];
                viedoplayer.Play();
                break;
            }
        }

        //���� �ܹ��� ī��





        ////�����Ƽ �����ϴ� ���̴�
        //audioing = true;
    }

    //�̰� �ҽ� �ϳ��� ������ ����, Ʃ�丮�󿡼� ��� ��
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

    //���̵��� �´� ī�带 Setactiove �ϰ�, selecthambugurcard�� �ִ� �Լ�
    public void LevelBurgurSetting()
    {
        TutorialLevelBurgerCard.SetActive(true);
        selecthambugurcard = TutorialLevelBurgerCard;
        sourceCard[0].SetActive(true);
    }

}
