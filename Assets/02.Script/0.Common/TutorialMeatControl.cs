using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using HighlightPlus;
using UnityEngine.SceneManagement;

public class TutorialMeatControl : MonoBehaviour
{
    public float currentTime = 0f;

    public TutorialGamemanager tutorialgamemanager;
    public GameObject meateffect;
    public GameObject saveffect;
    bool ismeateffect = false;
    bool full;
    GameObject[] hands;
    HandGrabInteractor[] grabstatus = new HandGrabInteractor[2];
    Rigidbody rb;
    GameObject grill;
    public Transform[] grilltransform;
    Renderer meatrenderer;
    public AudioSource audiosource;

    public delegate void Traying();
    public static event Traying OnTraying;

    public delegate void GoodMeat();
    public static event GoodMeat OnGoodMeeting;

    public delegate void BadMeat();
    public static event BadMeat OnBadMeeting;

    bool islivemeat = true;
    bool isgoodmeat = false;
    bool isbadmeat = false;
    bool isposition = false;
    public bool ismeattrash = false;
    public bool islgrabstatus = false;
    public bool isrgrabstatus = false;
    private void Start()
    {
        meatrenderer = GetComponent<Renderer>();
        hands = GameObject.FindGameObjectsWithTag("HANDGRAB");
        for (int i = 0; i < 2; i++)
        {
            grabstatus[i] = hands[i].GetComponent<HandGrabInteractor>();
            print(grabstatus[i]);
        }

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
        //화살표
        grill.transform.GetChild(7).gameObject.SetActive(true);
        gameObject.GetComponent<HighlightEffect>().outline = 0.3f;
        gameObject.GetComponent<HighlightEffect>().innerGlow = 0.3f;
    }

    private void OnTriggerStay(Collider collision)
    {
        var a = 0;
        if (collision.gameObject.CompareTag("GRILL"))
        {
            if (gameObject.GetComponent<BoxCollider>().isTrigger == true)
            {
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }

            if ((islgrabstatus == true && grabstatus[0].IsGrabbing == false) || (grabstatus[1].IsGrabbing == false && isrgrabstatus == true))
            {
                if (ismeateffect == false)
                {
                    saveffect = Instantiate(meateffect, gameObject.transform);
                    StartCoroutine(StartAudioControl());
                    ismeateffect = true;
                }
                currentTime += Time.deltaTime;
                if (isposition == false)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (grilltransform[i].childCount == 0)
                        {
                            full = true;
                            gameObject.transform.parent = grilltransform[i];
                            gameObject.GetComponent<Rigidbody>().useGravity = false;
                            gameObject.transform.localPosition = new Vector3(0, 0, 0);
                            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                            gameObject.GetComponent<BoxCollider>().isTrigger = true;
                            isposition = true;
                            break;
                        }
                    }
                    if (full == false)
                    {
                        Destroy(gameObject);
                    }
                }
            }
            //5초 지나면 고기가 구워짐
            if (currentTime > 5f && isgoodmeat == false && islivemeat == true)
            {
                islivemeat = false;
                isgoodmeat = true;
                meatrenderer.material.color = new Color(94 / 255f, 64 / 255f, 52 / 255f);
                gameObject.tag = "GOODMEAT";
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    OnGoodMeeting();
                }

            }
            //10초 지나면 고기가 탐
            else if (currentTime > 10f && isbadmeat == false)
            {
                isgoodmeat = false;
                isbadmeat = true;
                meatrenderer.material.color = new Color(0, 0, 0);
                gameObject.tag = "BULGOGI";
                if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    OnBadMeeting();
                }
            }
        }
    }

    void FullChecking()
    {
        print("풀체킹");
        int fullcheck = 0;
        for (int i = 0; i < 4; i++)
        {
            if (grilltransform[i].childCount == 1)
            {
                fullcheck++;
            }
        }
        if (fullcheck == 4)
        {
            Destroy(gameObject);
        }
    }
    IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GRILL"))
        {
            while (grabstatus[0].IsGrabbing == true || grabstatus[1].IsGrabbing == true)
            {
                if (grabstatus[0].IsGrabbing == true && islgrabstatus == false)
                {
                    islgrabstatus = true;
                }
                else if (grabstatus[1].IsGrabbing == true && isrgrabstatus == false)
                {
                    isrgrabstatus = true;
                }
                yield return null;
                if (gameObject.GetComponent<FoodControl>().isGrill == false)
                {
                    yield break;
                }
                if ((grabstatus[1].IsGrabbing == false && isrgrabstatus == true) || (grabstatus[0].IsGrabbing == false && islgrabstatus == true))
                {
                    yield return new WaitForSeconds(0.5f);
                    ismeattrash = true;
                    break;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        //yield return new WaitForSeconds(1);
        if (other.gameObject.CompareTag("GRILL"))
        {
            if (ismeateffect == true)
            {
                StartCoroutine(EndAudioControl());
                Destroy(saveffect);
                print("그릴에서 나갔다고 판단하는건가?");
                gameObject.GetComponent<FoodControl>().isGrill = false;
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }

    IEnumerator StartAudioControl()
    {
        audiosource.enabled = true;
        float audiocurrenttime = 0f;
        while (audiocurrenttime < 2f)
        {
            audiocurrenttime += Time.deltaTime;
            audiosource.volume = Mathf.Lerp(0, 1, currentTime / 2);
            yield return null;
        }
    }
    IEnumerator EndAudioControl()
    {
        print("이게 타버리니 문제야");
        float audiocurrentTime = 2f;
        while (audiocurrentTime > 0)
        {
            audiocurrentTime -= Time.deltaTime;
            audiosource.volume = Mathf.Lerp(1, 0, currentTime / 2);
            yield return null;
        }
        audiosource.enabled = false;
        yield break;
    }

}
