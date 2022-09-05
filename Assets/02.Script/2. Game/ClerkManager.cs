using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClerkManager : MonoBehaviour
{
    public GameManager gamemanager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CLERK"))
        {
            //�մ��� ���� ��, �մ� ���� �Դٴ� ��ȣ on
            if (gamemanager.isClerk == false)
            {
                gamemanager.isClerk = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CLERK"))
        {
            //�մ� ������
            if (gamemanager.isClerk == true)
            {
                gamemanager.isClerk = false;
            }
        }
    }
}
