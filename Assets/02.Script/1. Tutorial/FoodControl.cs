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


    bool ishaticbool = false;
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

    IEnumerator HaticControl(float delay)
    {
        OVRInput.SetControllerVibration(1f, 0.5f, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(1f, 0.5f, OVRInput.Controller.RTouch);
        yield return new WaitForSeconds(delay);
        OVRInput.SetControllerVibration(0f, 0f, OVRInput.Controller.LTouch);
        OVRInput.SetControllerVibration(0, 0f, OVRInput.Controller.RTouch);
    }

    private void Update()
    {
        if (grabstatus[0].IsGrabbing == true && ishaticbool == false)
        {
            ishaticbool = true;
            StartCoroutine(HaticControl(0.2f));
        }
        if (grabstatus[1].IsGrabbing == true && ishaticbool == false)
        {
            ishaticbool = true;
            StartCoroutine(HaticControl(0.2f));
        }

        if (grabstatus[0].IsGrabbing == true && islgrabstatus == false)
        {
            islgrabstatus = true;
        }
        else if (grabstatus[1].IsGrabbing == true && isrgrabstatus == false)
        {
            isrgrabstatus = true;
        }
        //접시에서 뺐고, 손을 놓아야하며, 쓰레기통에 닿으면 음식이 사라진다.
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
