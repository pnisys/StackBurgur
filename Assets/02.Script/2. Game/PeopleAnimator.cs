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
        //1. 손님 등판
        StartCoroutine(ClerkStateCheck());
        //2. 숫자 랜덤하게 섞기
        GameManager.instance.RandomNumberSelect();
    }

    //1. 점원이 매장까지 걸어오다가 멈춤
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

    //2. 2초 대기하다가 주문함
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
            //주문하는 시간 15초 생성
            GameManager.instance.orderlimitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "제한 시간 : " + Mathf.Round(GameManager.instance.orderlimitTime).ToString() + "초";

            //난이도에 따라 햄버거 카드에 맞는 햄버거와 소스를 카드를 보여주기
            GameManager.instance.LevelBurgurSetting();

            //15초 지나면 다음단계로 넘어감
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

    //3. 주문 받음
    IEnumerator Order()
    {
        yield return new WaitForSeconds(1f);
        GameManager.instance.isOrder = true;
        while (GameManager.instance.isOrder == true)
        {
            yield return null;
            GameManager.instance.limitTime -= Time.deltaTime;
            animator.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            animator.gameObject.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "제한 시간 : " + Mathf.Round(GameManager.instance.limitTime).ToString() + "초";
            if (GameManager.instance.limitTime < 0)
            {
                //이때 TrayControl의 적층한 것과 정답과의 비교 함수를 시작할 것임
                OnLimitTimeComplete();
                GameManager.instance.limitTime = 30f;
                GameManager.instance.isOrder = false;
            }
        }
    }
}
