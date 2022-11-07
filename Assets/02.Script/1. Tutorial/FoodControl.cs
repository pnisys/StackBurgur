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
    GameObject tray;
    GameObject grill;
    HandGrabInteractor[] grabstatus = new HandGrabInteractor[2];
    Transform empty;
    Rigidbody rb;
    public delegate void Dishing();
    public static event Dishing OnDishing;

    public delegate void Traying();
    public static event Traying OnTraying;

    private void Start()
    {
        tray = GameObject.FindGameObjectWithTag("TRAYCOLOR");
        grill = GameObject.FindGameObjectWithTag("GRILLCOLOR");
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
            tray.GetComponent<MeshRenderer>().material.color = new Color32(255, 186, 186, 255);
            if (gameObject.CompareTag("BULGOGI"))
            {
                grill.GetComponent<MeshRenderer>().material.color = new Color32(255, 122, 122, 255);
            }
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
        if (grabstatus[0].IsGrabbing == true && ishaticbool == false)
        {
            ishaticbool = true;
        }
        if (grabstatus[1].IsGrabbing == true && ishaticbool == false)
        {
            ishaticbool = true;
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
        tray.GetComponent<MeshRenderer>().material.color = new Color32(255, 255, 255, 255);
        grill.GetComponent<MeshRenderer>().material.color = new Color32(77, 77, 77, 255);
        //OnTraying();
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }


}
