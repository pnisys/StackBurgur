using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;

public class GameManager : MonoBehaviour
{
    private void Update()
    {
        if (lgrabstatus.IsGrabbing == true && islgrab == false)
        {
            StartCoroutine(LHaticControl(0.1f));
            islgrab = true;
        }
        if (rgrabstatus.IsGrabbing == true && isrgrab == false)
        {
            StartCoroutine(RHaticControl(0.1f));
            isrgrab = true;
        }
        if (lgrabstatus.IsGrabbing == false)
        {
            islgrab = false;
        }
        if (rgrabstatus.IsGrabbing == false)
        {
            isrgrab = false;
        }
    }
    //1. ������ �Ŵ� �տ� ����
    public bool isClerk = false;
    //2. ������ �����ϴ� ��
    public bool isThinking = false;
    //3. �ֹ� ����
    public bool isOrder = false;
    //4. 4����, 5���� �ֹ�
    public bool isOrder2 = false;

    //4. ������ ���� ����
    public bool isSelect = false;

    public bool iscompletesuccess = false;
    public bool islittlesuccess = false;
    public bool isfail = false;

    public bool iscompletesuccess2 = false;
    public bool islittlesuccess2 = false;
    public bool isfail2 = false;

    public bool isTray = false;

    public bool isbutton = false;
    bool islgrab = false;
    bool isrgrab = false;
    public GameObject[] people;

    //���� �������� ���
    public GameObject currentpeople;
    public TextMeshProUGUI GuideUiText;
    public TextMeshProUGUI GuideUiText2;
    public TextMeshProUGUI GuideUiText3;
    public GameObject[] lifescoreimage;
    public HandGrabInteractor lgrabstatus;
    public HandGrabInteractor rgrabstatus;

    public string phase1selectedsource;
    public string phase2selectedsource;

    public List<GameObject> uppeople = new List<GameObject>();
    public int tablepeoplenumbur = 0;
    public int peoplenumbur = 0;

    public delegate void ButtonLifeDie();
    public static event ButtonLifeDie OnButtonLifeDie;

    public int score = 0;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            GuideUiText3.text = score.ToString();
        }
    }
    public int lifescore = 5;
    public int LifeScore
    {
        get
        {
            return lifescore;
        }
        set
        {
            lifescore = value;
            lifescoreimage[lifescore].SetActive(false);
        }
    }

    //���� ǳ�� �����ִ� �ð�
    [HideInInspector]
    public float orderlimitTime = 1;
    //�ֹ� �ð� 30��
    [HideInInspector]
    public float limitTime = 1;

    //level�� �ܹ��� ���̵�, 1���̵�~ 4���̵����� ����
    public int level = 1;

    //stage�� �ܰ�, 1�ܰ�~5�ܰ���� ����
    public int stage = 1;
    public int Stage
    {
        get
        {
            return stage;
        }
        set
        {
            stage = value;
            GuideUiText.text = "��������  " + stage.ToString();
        }
    }
    private void Start()
    {
        //gameObject.GetComponent<AudioSource>().volume = SoundManager.instance.bgmSound;
        GuideUiText = gameObject.transform.parent.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>();
        GuideUiText2 = gameObject.transform.parent.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>();
        GuideUiText3 = gameObject.transform.parent.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>();

        GuideUiText.text = "�������� : " + stage.ToString();
        GuideUiText3.text = score.ToString();
    }

    public GameObject selecthambugurcard;
    public GameObject selecthambugurcard2;
    public GameObject selectsourcecard;
    public GameObject selectsourcecard2;

    public void Lobby()
    {
        if (isbutton == false)
        {
            isbutton = true;
            OnButtonLifeDie();
        }
        else
        {
            return;
        }
    }
    public void ButtonHaticControl()
    {
        StartCoroutine(LHaticControl(0.1f));
        StartCoroutine(RHaticControl(0.1f));
    }
    IEnumerator LHaticControl(float delay)
    {
        OVRInput.SetControllerVibration(1f, 0.5f, OVRInput.Controller.LTouch);
        yield return new WaitForSeconds(delay);
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
    }
    IEnumerator RHaticControl(float delay)
    {
        OVRInput.SetControllerVibration(1f, 0.5f, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(delay);
        OVRInput.SetControllerVibration(0, 0f, OVRInput.Controller.RTouch);
    }

    public bool[] istable = new bool[10] { false, false, false, false, false, false, false, false, false, false };
    public Vector3[] tableposition = new Vector3[10] { new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0.051f, 0.32f, -0.075f), new Vector3(0, 0.278f, 0) };
    public Quaternion[] tablerotation = new Quaternion[10] { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 29.878f, 0), Quaternion.Euler(0, 58.383f, 0) };
}
