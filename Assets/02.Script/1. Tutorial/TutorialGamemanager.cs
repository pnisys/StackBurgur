using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class TutorialGamemanager : MonoBehaviour
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
    public Animator animator;

    public int score = 0;
    public int lifescore = 5;

    //���� ǳ�� �����ִ� �ð�
    public float orderlimitTime = 15;
    //�ֹ� �ð� 30��
    public float limitTime = 30;

    private void Start()
    {
        gameObject.GetComponent<AudioSource>().volume = SoundManager.instance.bgmSound;
    }
}
