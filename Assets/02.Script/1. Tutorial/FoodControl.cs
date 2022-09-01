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

    public bool isEntry = false;
    public bool isInGrab = false;
    public bool isOutGrab = false;
    public bool isInOutGrab = false;


    HandGrabInteractor grabstatus;
    Transform empty;
    Rigidbody rb;
    public delegate void Dishing();
    public static event Dishing OnDishing;

    public delegate void Traying();
    public static event Traying OnTraying;

    private void Start()
    {
        grabstatus = GameObject.FindGameObjectWithTag("HANDGRAB").GetComponent<HandGrabInteractor>();
        empty = GameObject.FindGameObjectWithTag("EMPTY").transform;
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TRASH"))
        {
            isTrash = true;
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
        //���ÿ��� ����, ���� ���ƾ��ϸ�, �������뿡 ������ ������ �������.
        if (isOk == true && grabstatus.IsGrabbing == false && isTrash == true)
        {
            StartCoroutine(FoodDestory());
        }
    }

    IEnumerator FoodDestory()
    {
        //OnTraying();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("FOOD"))
    //    {
    //        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    //    }
    //}
}
