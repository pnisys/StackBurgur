using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClerkManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CLERK"))
        {
            //손님이 없을 때, 손님 오면 왔다는 신호 on
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
            //손님 나가면
            if (GameManager.instance.isClerk == true)
            {
                GameManager.instance.isClerk = false;
            }
        }
    }
}
