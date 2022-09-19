using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
public class FoodControl : MonoBehaviour
{
    public bool isOk = false;
    public bool isTrash = false;
    public bool isChecking = false;
    public bool isGrill = false;


    public bool isEntry = false;
    public bool isInGrab = false;
    public bool isOutGrab = false;
    public bool isInOutGrab = false;
    public bool isOnlyMeat = false;
    public bool islgrabstatus = false;
    public bool isrgrabstatus = false;

    GameObject[] hands;
    HandGrabInteractor[] grabstatus = new HandGrabInteractor[2];
    Transform empty;
    Rigidbody rb;
    public delegate void Dishing();
    public static event Dishing OnDishing;

    public delegate void Traying();
    public static event Traying OnTraying;

    private void Start()
    {
        hands = GameObject.FindGameObjectsWithTag("HANDGRAB");
        for (int i = 0; i < 2; i++)
        {
            grabstatus[i] = hands[i].GetComponent<HandGrabInteractor>();
            print(grabstatus[i]);
        }
        empty = GameObject.FindGameObjectWithTag("EMPTY").transform;
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TRASH"))
        {
            isTrash = true;
        }

        if (other.gameObject.CompareTag("GRILL") && gameObject.CompareTag("BULGOGI"))
        {
            isGrill = true;
        }
    }
    //�̰� Ʈ���̸� �����
    private void OnTriggerExit(Collider other)
    {
        //���ÿ��� ����
        if (other.gameObject.layer == LayerMask.NameToLayer("DISH"))
        {
            isOk = true;
            //�߷��� �ۿ��ϰ� Ǯ����
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            //�ٸ� ������ ���Բ� ���� �׷��� ���ÿ��� �������� ����
            gameObject.transform.parent = empty;
            //���ÿ� ������ ����� �ϴ� ����
            OnDishing();
        }
    }

    private void Update()
    {
        if (grabstatus[0].IsGrabbing == true && islgrabstatus == false)
        {
            islgrabstatus = true;
        }
        else if (grabstatus[1].IsGrabbing == true && isrgrabstatus == false)
        {
            isrgrabstatus = true;
        }
        //���ÿ��� ����, ���� ���ƾ��ϸ�, �������뿡 ������ ������ �������.
        if (isOk == true && ((grabstatus[0].IsGrabbing == false && islgrabstatus == true) || (grabstatus[1].IsGrabbing == false && isrgrabstatus == true)) && isTrash == true)
        {
            StartCoroutine(FoodDestory());
        }
        //if (isOk == true && grabstatus.IsGrabbing == false && isEntry == false)
        //{
        //    Destroy(gameObject);
        //}
    }

    IEnumerator FoodDestory()
    {
        //OnTraying();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


}
