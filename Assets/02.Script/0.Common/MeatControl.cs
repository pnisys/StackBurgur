using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using HighlightPlus;

public class MeatControl : MonoBehaviour
{
    public float currentTime = 0f;

    public TutorialGamemanager tutorialgamemanager;
    HandGrabInteractor grabstatus;
    Rigidbody rb;
    GameObject grill;
    public Transform[] grilltransform;
    Renderer meatrenderer;

    public delegate void Traying();
    public static event Traying OnTraying;

    bool islivemeat = true;
    bool isgoodmeat = false;
    bool isbadmeat = false;
    bool isposition = false;

    private void Start()
    {
        meatrenderer = GetComponent<Renderer>();
        grabstatus = GameObject.FindGameObjectWithTag("HANDGRAB").GetComponent<HandGrabInteractor>();
        rb = GetComponent<Rigidbody>();
        grill = GameObject.FindGameObjectWithTag("GRILL");
        rb.sleepThreshold = 0;
        grilltransform = new Transform[4];
        for (int i = 0; i < 4; i++)
        {
            grilltransform[i] = grill.transform.GetChild(i + 3);
        }
    }

    private void OnEnable()
    {
        TutorialPeopleAnimator.OnMeatHighlight += MeatHighlight;
    }
    private void OnDisable()
    {
        TutorialPeopleAnimator.OnMeatHighlight -= MeatHighlight;
    }

    void MeatHighlight()
    {
        //ȭ��ǥ
        grill.transform.GetChild(7).gameObject.SetActive(true);
        gameObject.GetComponent<HighlightEffect>().outline = 0.3f;
        gameObject.GetComponent<HighlightEffect>().innerGlow = 0.3f;
    }

    private void OnTriggerStay(Collider collision)
    {
        var a = 0;
        if (collision.gameObject.CompareTag("GRILL"))
        {
            if (grabstatus.IsGrabbing == false)
            {
                currentTime += Time.deltaTime;
                if (isposition == false)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (grilltransform[i].childCount == 0)
                        {
                            gameObject.transform.parent = grilltransform[i];
                            gameObject.GetComponent<Rigidbody>().useGravity = false;
                            gameObject.transform.localPosition = new Vector3(0, 0, 0);
                            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                            gameObject.GetComponent<BoxCollider>().isTrigger = true;
                            isposition = true;
                            break;
                        }
                    }
                }
            }
            //5�� ������ ��Ⱑ ������
            if (currentTime > 5f && isgoodmeat == false && islivemeat == true)
            {
                islivemeat = false;
                isgoodmeat = true;
                meatrenderer.material.color = new Color(94 / 255f, 64 / 255f, 52 / 255f);
                gameObject.tag = "GOODMEAT";
                print("��Ⱑ �� ������");
            }
            //10�� ������ ��Ⱑ Ž
            else if (currentTime > 10f && isbadmeat == false)
            {
                isgoodmeat = false;
                isbadmeat = true;
                meatrenderer.material.color = new Color(0, 0, 0);
                gameObject.tag = "BULGOGI";
                print("��Ⱑ Ž");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("GRILL"))
        {
            gameObject.GetComponent<FoodControl>().isGrill = false;
            gameObject.GetComponent<FoodControl>().isOnlyMeat = false;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }




    //�߰��ؾ� �� ����
    //1. ���� �� ����
    //2. ���� �� �Ҹ�
    //3. �ܹ��� �ϼ� �Ҹ�
}
