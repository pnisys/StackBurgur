using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class GameManager : MonoBehaviour
{
    //1. ������ �Ŵ� �տ� ����
    public bool isClerk = false;
    //2. ������ �����ϴ� ��
    public bool isThinking = false;
    //3. �ֹ� ����
    public bool isOrder = false;
    //4. ������ ���� ����
    public bool isSelect = false;

    public bool iscompletesuccess = false;
    public bool islittlesuccess = false;
    public bool isfail = false;

    public bool isTray = false;

    public GameObject[] people;
    public int peoplenumbur = 0;

    public Animator animator;

    public int score = 0;
    public int lifescore = 5;

    //���� ǳ�� �����ִ� �ð�
    public float orderlimitTime = 20;
    //�ֹ� �ð� 30��
    public float limitTime = 5;

    //level�� �ܹ��� ���̵�, Ʃ�丮�� ���̵����� 4���̵����� ����
    public int level = 1;
    //stage�� �ܰ�, 1�ܰ�~5�ܰ���� ���� -> Ʃ�丮�� �ܰ�� ����
    public int stage = 1;

    public GameObject selecthambugurcard;
    public GameObject selectsourcecard;

    public bool[] istable = new bool[10] { true, true, true, true, false, false, false, false, false, false };
    public Vector3[] tableposition = new Vector3[10] { new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0, 0.17f, 0), new Vector3(0.051f, 0.32f, -0.075f), new Vector3(0, 0.278f, 0) };
    public Quaternion[] tablerotation = new Quaternion[10] { Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 0, 0), Quaternion.Euler(0, 29.878f, 0), Quaternion.Euler(0, 58.383f, 0) };
}
