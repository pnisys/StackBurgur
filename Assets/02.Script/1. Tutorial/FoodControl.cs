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
    //이게 트레이를 벗어나면
    private void OnTriggerExit(Collider other)
    {
        //접시에서 빼면
        if (other.gameObject.layer == LayerMask.NameToLayer("DISH"))
        {
            isOk = true;
            //중력이 작용하게 풀어줌
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            //다른 곳으로 가게끔 만듬 그래야 접시에서 프리팹이 생김
            gameObject.transform.parent = empty;
            //접시에 프리팹 생기게 하는 순간
            OnDishing();
        }

    }

    private void Update()
    {
        //접시에서 뺐고, 손을 놓아야하며, 쓰레기통에 닿으면 음식이 사라진다.
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
