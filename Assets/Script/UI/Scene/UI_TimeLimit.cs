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
                countdownText.text = string.Format("���ѽð� : {0:00}", seconds);
                yield return new WaitForSeconds(1f); // 0.01�ʸ��� ������Ʈ
                countdownTime -= 1f; // �ð� ����
            }
            else
            {
                // �ؽ�Ʈ ������Ʈ: ���� �ڸ��� �Ҽ��� ��° �ڸ����� ǥ��
                countdownText.text = string.Format("���ѽð� : {0:00}:{1:00.0}", minutes, seconds);
                yield return new WaitForSeconds(0.1f); // 0.01�ʸ��� ������Ʈ
                countdownTime -= 0.1f; // �ð� ����
            }
        }

        countdownText.text = string.Empty; // ī��Ʈ�ٿ��� ������ ǥ���� �޽���
        // ���⿡ �ð��� �� �� �Ŀ� ������ �ڵ带 �߰�
    }

    public void OnDestroy()
    {
        StopCoroutine(countDownCo);
    }
}
