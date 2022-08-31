using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialClerkManager : MonoBehaviour
{
    public TutorialGamemanager tutorialgamemanager; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CLERK"))
        {
            //�մ��� ���� ��, �մ� ���� �Դٴ� ��ȣ on
            if (tutorialgamemanager.isClerk == false)
            {
                tutorialgamemanager.isClerk = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CLERK"))
        {
            //�մ� ������
            if (tutorialgamemanager.isClerk == true)
            {
                tutorialgamemanager.isClerk = false;
            }
        }
    }
}
