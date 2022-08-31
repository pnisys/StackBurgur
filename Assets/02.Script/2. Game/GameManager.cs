using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    private void Awake()
    {
        if (instance == null) //instance�� null. ��, �ý��ۻ� �����ϰ� ���� ������
        {
            instance = this; //���ڽ��� instance�� �־��ݴϴ�.
            DontDestroyOnLoad(gameObject); //OnLoad(���� �ε� �Ǿ�����) �ڽ��� �ı����� �ʰ� ����
        }
        else
        {
            if (instance != this) //instance�� ���� �ƴ϶�� �̹� instance�� �ϳ� �����ϰ� �ִٴ� �ǹ�
                Destroy(this.gameObject); //�� �̻� �����ϸ� �ȵǴ� ��ü�̴� ��� AWake�� �ڽ��� ����
        }
    }

    #region ����
    //1. ������ �Ŵ� �տ� ����
    public bool isClerk = false;
    //2. ������ �����ϴ� ��
    public bool isThinking = false;
    //3. �ֹ� ����
    public bool isOrder = false;
    //4. ������ ���� ����
    public bool isSelect = false;

    public GameObject[] people;
    public Animator animator;

    public int score = 0;
    public int lifescore = 5;

    int ran = 0;
    int ran1 = 0;

    //���� ǳ�� �����ִ� �ð�
    public float orderlimitTime = 15;
    //�ֹ� �ð� 30��
    public float limitTime = 30;

    public GameObject TutorialLevelBurgerCard;
    public GameObject[] OneLevelBurgerCard;
    public GameObject[] TwoLevelBurgerCard;
    public GameObject[] ThreeLevelBurgerCard;
    public GameObject[] FourLevelBurgerCard;
    public GameObject[] sourceCard;

    public GameObject selecthambugurcard;
    public GameObject selectsourcecard;

    public GameObject[] breadmaterial;
    public GameObject[] vegetablematerial;
    public GameObject[] meatmaterial;
    public GameObject[] addmaterial;
    public GameObject[] sourcematerial;


    public List<int> hambugernumber = new List<int>();
    public List<int> sourcenumber = new List<int>();

    //level�� �ܹ��� ���̵�, Ʃ�丮�� ���̵����� 4���̵����� ����
    public int level = 0;
    //stage�� �ܰ�, 1�ܰ�~5�ܰ���� ���� -> Ʃ�丮�� �ܰ�� ����
    public int stage = 1;

    #endregion

    //���̵��� �´� ī�带 Setactiove �ϰ�, selecthambugurcard�� �ִ� �Լ�
    public void LevelBurgurSetting()
    {
        //������ �ܹ��� ���̵�, 0���� ���õ� ����
        //Ʃ�丮�� ����
        if (level == 0)
        {
            TutorialLevelBurgerCard.SetActive(true);
            selecthambugurcard = TutorialLevelBurgerCard;
        }
        else if (level == 1)
        {
            OneLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = OneLevelBurgerCard[hambugernumber[0]];
        }
        else if (level == 2)
        {
            TwoLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = TwoLevelBurgerCard[hambugernumber[0]];

        }
        else if (level == 3)
        {
            ThreeLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = ThreeLevelBurgerCard[hambugernumber[0]];

        }
        else if (level == 4)
        {
            FourLevelBurgerCard[hambugernumber[0]].SetActive(true);
            selecthambugurcard = FourLevelBurgerCard[hambugernumber[0]];
        }

        sourceCard[sourcenumber[0]].SetActive(true);
        selectsourcecard = sourceCard[sourcenumber[0]];
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
