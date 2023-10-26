using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI_TimeLimit : UI_Scene
{
    private TextMeshProUGUI countdownText;
    private float countdownTime = 10f;

    private Coroutine countDownCo;
    private void Start()
    {
        base.Init();
        countdownText = GetComponent<TextMeshProUGUI>();
        countDownCo = StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while (countdownTime > 0)
        {
            int minutes = (int)(countdownTime / 60);
            float seconds = countdownTime % 60f;

            if (minutes == 0)
            {
                countdownText.text = string.Format("제한시간 : {0:00}", seconds);
                yield return new WaitForSeconds(1f); // 0.01초마다 업데이트
                countdownTime -= 1f; // 시간 감소
            }
            else
            {
                // 텍스트 업데이트: 십의 자리와 소수점 둘째 자리까지 표시
                countdownText.text = string.Format("제한시간 : {0:00}:{1:00.0}", minutes, seconds);
                yield return new WaitForSeconds(0.1f); // 0.01초마다 업데이트
                countdownTime -= 0.1f; // 시간 감소
            }
        }

        countdownText.text = string.Empty; // 카운트다운이 끝나면 표시할 메시지
        // 여기에 시간이 다 된 후에 실행할 코드를 추가
    }

    public void OnDestroy()
    {
        StopCoroutine(countDownCo);
    }
}
