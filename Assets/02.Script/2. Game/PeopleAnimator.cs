using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class PeopleAnimator : MonoBehaviour
{
    Animator animator;
    readonly int hashIdle = Animator.StringToHash("Idle");
    readonly int hashTalk = Animator.StringToHash("Talk");

    public delegate void LimitTimeComplete();
    public static event LimitTimeComplete OnLimitTimeComplete;

    private void Start()
    {
        animator = GetComponent<Animator>();
        //1. �մ� ����
        StartCoroutine(ClerkStateCheck());
        //2. ���� �����ϰ� ����
        GameManager.instance.RandomNumberSelect();
    }

    //1. ������ ������� �ɾ���ٰ� ����
    IEnumerator ClerkStateCheck()
    {
        while (GameManager.instance.isClerk == false)
        {
            yield return new WaitForSeconds(0.3f);
            if (GameManager.instance.isClerk == true)
            {
                animator.SetBool(hashIdle, true);
                StartCoroutine(ThinkBallon());
            }
        }
    }

    //2. 2�� ����ϴٰ� �ֹ���
    IEnumerator ThinkBallon()
    {
        yield return new WaitForSeconds(2f);
        GameManager.instance.isThinking = true;
        animator.SetBool(hashIdle, false);
        animator.SetBool(hashTalk, true);
        animator.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        while (GameManager.instance.isThinking == true)
        {
            yield return null;
            //�ֹ��ϴ� �ð� 15�� ����
            GameManager.instance.orderlimitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "���� �ð� : " + Mathf.Round(GameManager.instance.orderlimitTime).ToString() + "��";

            //���̵��� ���� �ܹ��� ī�忡 �´� �ܹ��ſ� �ҽ��� ī�带 �����ֱ�
            GameManager.instance.LevelBurgurSetting();

            //15�� ������ �����ܰ�� �Ѿ
            if (GameManager.instance.orderlimitTime < 0)
            {
                GameManager.instance.orderlimitTime = 15;
                GameManager.instance.isThinking = false;
            }
        }

        animator.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        GameManager.instance.OneLevelBurgerCard[GameManager.instance.hambugernumber[0]].SetActive(false);
        GameManager.instance.sourceCard[GameManager.instance.sourcenumber[0]].SetActive(false);
        animator.SetBool(hashTalk, false);
        animator.SetBool(hashIdle, true);

        StartCoroutine(Order());
    }

    //3. �ֹ� ����
    IEnumerator Order()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.isOrder = true;
        while (GameManager.instance.isOrder == true)
        {
            yield return null;
            GameManager.instance.limitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "���� �ð� : " + Mathf.Round(GameManager.instance.limitTime).ToString() + "��";
            if (GameManager.instance.limitTime < 0)
            {
                //�̶� TrayControl�� ������ �Ͱ� ������� �� �Լ��� ������ ����
                OnLimitTimeComplete();
                GameManager.instance.limitTime = 30f;
                GameManager.instance.isOrder = false;
            }
        }
    }
}
