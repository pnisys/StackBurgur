using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClerkManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CLERK"))
        {
            //�մ��� ���� ��, �մ� ���� �Դٴ� ��ȣ on
            if (GameManager.instance.isClerk == false)
            {
                GameManager.instance.isClerk = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CLERK"))
        {
            //�մ� ������
            if (GameManager.instance.isClerk == true)
            {
                GameManager.instance.isClerk = false;
            }
        }
    }
}
