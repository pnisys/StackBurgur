using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using System.Linq;
using System;
public class TutorialTrayControl : MonoBehaviour
{
    public Transform[] combination;
    public TutorialGamemanager tutorialgamemanager;
    public TutorialPeopleAnimator tutorialpeopleanimator;
    public AudioClip[] audioclips;
    public AudioSource audiosource;
    public GameObject selectedsourceobject;
    //Ʈ���̿��� �ҽ� ���ϰ�
    public Transform burgursource;
    public Transform empty;
    public string selectedsource;
    public GameObject arrow;
    //�ҽ� 4�� ��Ƴ��� ��ġ
    public Transform source;
    //�ҽ� 4�� ������
    public GameObject[] sourceprefab;
    //�ҽ� ¥���� 4�� ������
    public GameObject[] burgursourceprefab;

    public int traystatus = 0;
    public int successscore = 0;
    public int littlesuccessscore = 0;
    public float between = 0.02f;
    bool sourcecorrect = false;
    public bool islgrabstatus = false;
    public bool isrgrabstatus = false;
    public HandGrabInteractor lgrabstatus;
    public HandGrabInteractor rgrabstatus;

    public Stack<GameObject> stackcreateburgur = new Stack<GameObject>();

    public delegate void Traying();
    public static event Traying OnTraying;

    Transform burgurs;
    public Transform mantray;
    #region ��Ʈ�� ����
    string sandwichbreadbread;
    string hamburgurbread;
    string blackbread;
    string lettuce;
    string tomato;
    string onion;
    string chicken;
    string bulgogi;
    string badbulgogi;

    string bacon;
    string mushroom;
    string cheeze;
    string shrimp;

    string chillsource;
    string bbqsource;
    string mustadsource;
    string mayonnaise;
    #endregion

    private void OnEnable()
    {
        TutorialPeopleAnimator.OnLimitTimeComplete += this.MenuSelection;
    }
    private void OnDisable()
    {
        TutorialPeopleAnimator.OnLimitTimeComplete -= this.MenuSelection;
    }

    //���� Start�Լ� �����
    void StartVariable()
    {
        sandwichbreadbread = "SANDWICHBREAD";
        hamburgurbread = "HAMBURGURBREAD";
        blackbread = "BLACKBREAD";
        lettuce = "LETTUCE";
        tomato = "TOMATO";
        onion = "ONION";
        chicken = "CHICKEN";
        bulgogi = "GOODMEAT";
        badbulgogi = "BULGOGI";
        bacon = "BACON";
        mushroom = "MUSHROOM";
        cheeze = "CHEEZE";
        shrimp = "SHIRMP";
        chillsource = "CHILLSOURCE";
        bbqsource = "BBQSOURCE";
        mustadsource = "MUSTADSOURCE";
        mayonnaise = "MAYONAISE";
    }

    void CombinationControl()
    {
        //���� ���� ���
        combination[0].localPosition = new Vector3(0, 0, 0f);
        for (int i = 1; i < 13; i++)
        {
            combination[i].localPosition = new Vector3(combination[i - 1].localPosition.x, combination[i - 1].localPosition.y + between, combination[i - 1].localPosition.z);
        }
    }

    private void Start()
    {
        //���� Start�Լ� �����
        StartVariable();
        //��ġ�� ���� ����
        CombinationControl();
        //Tray
        burgurs = gameObject.transform.GetChild(0).GetChild(0);
    }

    //���� ������ �ܹ��ſ� ���� ���� ������ ���Ѵ�.
    void MenuSelection()
    {
        LocalMenuSeletionOneLevel(hamburgurbread, bulgogi, hamburgurbread);
        void LocalMenuSeletionOneLevel(params string[] a)
        {
            //�ܹ��ſ� �ƿ� �ø����� ������ ��
            if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0)
            {
                tutorialgamemanager.isfail = true;
                return;
            }
            //���� �Ʒ����� �Թ����� �������
            if (burgurs.GetChild(0).GetChild(0).gameObject.CompareTag(a[0]))
            {
                successscore++;
            }
            //�״��� �Ʒ����� ����߰� �������
            if (burgurs.GetChild(1).GetChild(0).gameObject.CompareTag(a[1]))
            {
                successscore++;
            }
            //�״��� �Ʒ����� ���찡 �������
            if (burgurs.GetChild(2).GetChild(0).gameObject.CompareTag(a[2]))
            {
                successscore++;
            }
            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]))
                {
                    littlesuccessscore++;
                }
            }
            //�̰� �����ΰ���
            if (successscore == 3 && sourcecorrect == true)
            {
                tutorialgamemanager.iscompletesuccess = true;
                print("�Ϻ��Ѽ���");
                Invoke(nameof(SuccessTray), 1.1f);
            }
            //�̰� �κм����Ѱ���
            else if (successscore != 3 && littlesuccessscore == 3 && sourcecorrect == true)
            {
                tutorialgamemanager.islittlesuccess = true;
                print("�κм���");
                Invoke(nameof(SuccessTray), 1.1f);

            }
            else
            {
                tutorialgamemanager.isfail = true;
                print("����");
            }
            sourcecorrect = false;
        }
    }

    void SuccessTray()
    {
        mantray.gameObject.SetActive(true);
        foreach (var item in stackcreateburgur.ToArray())
        {
            if (burgurs.childCount != 0)
            {
                Destroy(burgursource.GetChild(0).gameObject);
            }
            else
            {
                return;
            }
            
            item.transform.parent = mantray;
            item.transform.localPosition = new Vector3(0, item.transform.localPosition.y + 0.4219f, 0);
        }
    }

    IEnumerator TooFast(Collider other)
    {
        while (other.gameObject != null)
        {
            yield return new WaitForSeconds(1f);
            Vector3 offset = burgurs.position - other.gameObject.transform.position;
            float sqrLen = offset.magnitude;
            print(offset);
            print(sqrLen);
            if (sqrLen > 0.3f)
            {
                Destroy(other.gameObject);
                stackcreateburgur.Pop();
                traystatus = stackcreateburgur.ToArray().Length;
                if (traystatus != 0)
                {
                    //�ҽ��� ���ٸ�
                    if (burgursource.childCount == 0)
                    {
                        print("�ҽ�����");
                    }
                    //�ҽ��� �ִٸ�
                    else
                    {
                        burgursource.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = false;
                        burgursource.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        yield return new WaitForSeconds(0.3f);
                        burgursource.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    }
                    //���� �ִ� ���� �ֻ�ܿ� �ִ� �͵��� �ٽ� ���� �� �ְ� ����� ����
                    stackcreateburgur.ToArray()[0].gameObject.GetComponent<Grabbable>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().useGravity = true;
                    stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabPose>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabPose>().enabled = true;
                    yield break;
                }
                else
                {
                    print("���� �۵���ų�� ����");
                    yield break;
                }
            }
        }
    }



    //����
    IEnumerator OnTriggerEnter(Collider other)
    {
        //�ҽ� ���� ���� ��
        if (other.gameObject.layer == LayerMask.NameToLayer("SOURCE"))
        {
            //�ȵ����ִ� ����
            if (other.gameObject.GetComponent<SourceControl>().isEntry == false)
            {
                print("��������?");
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<SourceControl>().isEntry = true;
                //��� ������ ��� �˻��ϴٰ�
                while (lgrabstatus.IsGrabbing == true || rgrabstatus.IsGrabbing == true)
                {
                    if (lgrabstatus.IsGrabbing == true && islgrabstatus == false)
                    {
                        islgrabstatus = true;
                    }
                    else if (rgrabstatus.IsGrabbing == true && isrgrabstatus == false)
                    {
                        isrgrabstatus = true;
                    }
                    ////0.3�ʸ��� �˻縦 �� ��
                    yield return null;
                    //���⼭ ��⸦ ���´ٸ�
                    if ((rgrabstatus.IsGrabbing == false && isrgrabstatus == true) || (lgrabstatus.IsGrabbing == false && islgrabstatus == true))
                    {
                        islgrabstatus = false;
                        isrgrabstatus = false;
                        if (selectedsourceobject != null)
                        {
                            if (other.gameObject.CompareTag(bbqsource))
                            {
                                Instantiate(sourceprefab[0], source.GetChild(0));
                            }
                            else if (other.gameObject.CompareTag(mayonnaise))
                            {
                                Instantiate(sourceprefab[1], source.GetChild(1));
                            }
                            else if (other.gameObject.CompareTag(chillsource))
                            {
                                Instantiate(sourceprefab[2], source.GetChild(2));
                            }
                            else if (other.gameObject.CompareTag(mustadsource))
                            {
                                Instantiate(sourceprefab[3], source.GetChild(3));
                            }
                            Destroy(other.gameObject);
                            yield break;
                        }
                        selectedsourceobject = other.gameObject;
                        Destroy(other.gameObject.GetComponent<Rigidbody>());
                        other.gameObject.transform.position = new Vector3(burgurs.transform.position.x, burgurs.transform.position.y + 0.4f, burgurs.transform.position.z);
                        other.gameObject.transform.rotation = Quaternion.Euler(-90, 0, -90);
                        yield return new WaitForSeconds(0.3f);
                        other.gameObject.GetComponent<Animator>().enabled = true;
                        yield return new WaitForSeconds(0.7f);
                        other.gameObject.GetComponent<AudioSource>().enabled = true;

                        if (burgursource.childCount != 0)
                        {
                            Destroy(burgursource.GetChild(0).gameObject);

                        }
                        //�ҽ� �ξ����ִ°� ������
                        if (other.gameObject.CompareTag(bbqsource))
                        {

                            selectedsource = "BBQSOURCE";
                            burgursource.position = new Vector3(combination[traystatus].position.x, combination[traystatus].position.y + 0.01f, combination[traystatus].position.z);
                            GameObject aa = Instantiate(burgursourceprefab[0], burgursource);
                            yield return new WaitForSeconds(1.5f);
                            aa.GetComponent<BoxCollider>().isTrigger = true;
                            aa.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                            yield return new WaitForSeconds(1.5f);
                            Instantiate(sourceprefab[0], source.GetChild(0));
                        }
                        //������� �ҽ����
                        else if (other.gameObject.CompareTag(mayonnaise))
                        {
                            selectedsource = "MAYONAISE";
                            burgursource.position = new Vector3(combination[traystatus].position.x, combination[traystatus].position.y + 0.01f, combination[traystatus].position.z);
                            GameObject aa = Instantiate(burgursourceprefab[1], burgursource);
                            yield return new WaitForSeconds(1.5f);
                            aa.GetComponent<BoxCollider>().isTrigger = true;
                            aa.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                            yield return new WaitForSeconds(1.5f);
                            Instantiate(sourceprefab[1], source.GetChild(1));

                        }
                        //ĥ���ҽ����
                        else if (other.gameObject.CompareTag(chillsource))
                        {
                            selectedsource = "CHILLSOURCE";
                            burgursource.position = new Vector3(combination[traystatus].position.x, combination[traystatus].position.y + 0.01f, combination[traystatus].position.z);
                            GameObject aa = Instantiate(burgursourceprefab[2], burgursource);
                            yield return new WaitForSeconds(1.5f);
                            aa.GetComponent<BoxCollider>().isTrigger = true;
                            aa.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                            yield return new WaitForSeconds(1.5f);
                            Instantiate(sourceprefab[2], source.GetChild(2));
                        }
                        //�ӽ�Ÿ��ҽ�
                        else if (other.gameObject.CompareTag(mustadsource))
                        {
                            selectedsource = "MUSTADSOURCE";
                            burgursource.position = new Vector3(combination[traystatus].position.x, combination[traystatus].position.y + 0.01f, combination[traystatus].position.z);
                            GameObject aa = Instantiate(burgursourceprefab[3], burgursource);
                            yield return new WaitForSeconds(1.5f);
                            aa.GetComponent<BoxCollider>().isTrigger = true;
                            aa.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                            yield return new WaitForSeconds(1.5f);
                            Instantiate(sourceprefab[3], source.GetChild(3));
                        }
                        Destroy(other.gameObject);

                        if (tutorialpeopleanimator.sourceCard[0].tag == selectedsource)
                        {
                            sourcecorrect = true;
                        }
                        else
                        {
                            sourcecorrect = false;
                        }
                        yield break;
                    }
                }
            }
        }

        //Ʈ���̸� ����� ���� ���� �� �Լ��� ����
        //��Ⱑ ������ ���ٰ�, �ٽ� ������ ��
        //�ٽ� ���� ���� �ְ�, ���� ���� �ִ�.
        if ((other.gameObject.CompareTag(badbulgogi) || other.gameObject.CompareTag(bulgogi)) && other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == true)
        {
            other.gameObject.GetComponent<FoodControl>().isEntry = true;
            //���Ծ�!
            while (lgrabstatus.IsGrabbing == true || rgrabstatus.IsGrabbing == true)
            {
                if (lgrabstatus.IsGrabbing == true && islgrabstatus == false)
                {
                    islgrabstatus = true;
                }
                else if (rgrabstatus.IsGrabbing == true && isrgrabstatus == false)
                {
                    isrgrabstatus = true;
                }
                //���Դٰ� �ٽó�����?
                if (other.gameObject.GetComponent<FoodControl>().isEntry == false)
                {
                    other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                    yield break;
                }
                yield return null;
                if ((rgrabstatus.IsGrabbing == false && isrgrabstatus == true) || (lgrabstatus.IsGrabbing == false && islgrabstatus == true))
                {
                    isrgrabstatus = false;
                    islgrabstatus = false;
                    audiosource.PlayOneShot(audioclips[0]);
                    other.gameObject.GetComponent<FoodControl>().isOutGrab = false;
                    other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash = false;

                    #region 1. ���� 13������ �� �״´ٸ�? �׳� Destory �ع�����
                    if (traystatus != 12)
                    {
                        //�÷��� stack�� �Ἥ ������ ���� Push �о�ִ´�.
                        stackcreateburgur.Push(other.gameObject);
                        //tray�� �󸶳� �׿��ִ����� stack�� �迭 ���̸�ŭ �� ���̴�.
                        traystatus = stackcreateburgur.ToArray().Length;

                        //13���� combination position�� �ڽ����� ���ӿ�����Ʈ�� ���� �Ѵ�. 
                        other.gameObject.transform.parent = combination[traystatus - 1];
                    }
                    else
                    {
                        Destroy(other.gameObject);
                        yield break;
                    }
                    #endregion

                    //������ ���ӿ�����Ʈ�� position�� rotation ���� �ʱ�ȭ��Ų��.
                    other.gameObject.transform.localRotation = Quaternion.Euler(180, 0, 0);
                    other.gameObject.transform.localPosition = new Vector3(0, 0.08f, 0);
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    yield return new WaitForSeconds(0.3f);
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    other.gameObject.GetComponent<FoodControl>().isInGrab = true;

                    //10.  ó�� �״� �� ����, �ι� °���� ���� ��
                    if (traystatus != 1)
                    {
                        //11.  ���� �� ������ ���� ������ ��� ������Ʈ ��� Off��Ŵ
                        stackcreateburgur.ToArray()[1].gameObject.GetComponent<Grabbable>().enabled = false;
                        stackcreateburgur.ToArray()[1].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        stackcreateburgur.ToArray()[1].gameObject.GetComponent<PhysicsGrabbable>().enabled = false;
                        stackcreateburgur.ToArray()[1].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabInteractable>().enabled = false;
                        stackcreateburgur.ToArray()[1].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabInteractable>().enabled = false;
                        stackcreateburgur.ToArray()[1].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabPose>().enabled = false;
                        stackcreateburgur.ToArray()[1].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabPose>().enabled = false;
                    }
                    yield break;
                }
            }
        }



        //1. �ۿ��� ������ ���� ���� ��
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            //�ȿ� ���� �� ���� ��
            if (other.gameObject.GetComponent<FoodControl>().isInGrab == true)
            {
                audiosource.PlayOneShot(audioclips[0]);
                print("�ȿ� �ִµ� ���� ��");
                yield return new WaitForSeconds(0.2f);
                other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                other.gameObject.GetComponent<FoodControl>().isInGrab = false;
                other.gameObject.GetComponent<FoodControl>().isOutGrab = true;
                StartCoroutine(TooFast(other));
                yield break;
            }
            //�ۿ��� ������ ���°��� ��
            if (other.gameObject.GetComponent<FoodControl>().isEntry == false)
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<FoodControl>().isEntry = true;
                //��� ������ ��� �˻��ϴٰ�
                while (lgrabstatus.IsGrabbing == true || rgrabstatus.IsGrabbing == true)
                {
                    if (lgrabstatus.IsGrabbing == true && islgrabstatus == false)
                    {
                        islgrabstatus = true;
                    }
                    else if (rgrabstatus.IsGrabbing == true && isrgrabstatus == false)
                    {
                        isrgrabstatus = true;
                    }
                    //0.3�ʸ��� �˻縦 �� ��
                    yield return null;
                    //���Դٰ� �ٽó�����?
                    if (other.gameObject.GetComponent<FoodControl>().isEntry == false)
                    {
                        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        yield break;
                    }
                    //���⼭ ��⸦ ���´ٸ�
                    if ((rgrabstatus.IsGrabbing == false && isrgrabstatus == true) || (lgrabstatus.IsGrabbing == false && islgrabstatus == true))
                    {
                        isrgrabstatus = false;
                        islgrabstatus = false;
                        //���µ� Ʈ���̹����� ������ �׸��� ������
                        if (other.gameObject.GetComponent<FoodControl>().isGrill == true)
                        {
                            yield break;
                        }
                        //���µ� Ʈ���̹�, �׸��� �ƴϸ�
                        else if (other.gameObject.GetComponent<FoodControl>().isEntry == false)
                        {
                            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                            yield break;
                        }
                        audiosource.PlayOneShot(audioclips[0]);
                        #region 1. ���� 13������ �� �״´ٸ�? �׳� Destory �ع�����
                        if (traystatus != 12)
                        {
                            //�÷��� stack�� �Ἥ ������ ���� Push �о�ִ´�.
                            stackcreateburgur.Push(other.gameObject);
                            //tray�� �󸶳� �׿��ִ����� stack�� �迭 ���̸�ŭ �� ���̴�.
                            traystatus = stackcreateburgur.ToArray().Length;

                            //13���� combination position�� �ڽ����� ���ӿ�����Ʈ�� ���� �Ѵ�. 
                            other.gameObject.transform.parent = combination[traystatus - 1];
                        }
                        else
                        {
                            Destroy(other.gameObject);
                            yield break;
                        }
                        #endregion
                        //ó�� �����ϴ°Ŷ��, �ܹ��� �Ųٷ� ������
                        if (traystatus == 1 && ((other.gameObject.CompareTag(hamburgurbread) || other.gameObject.CompareTag(blackbread))))
                        {
                            other.gameObject.transform.localRotation = Quaternion.Euler(0, 90, 0);
                            other.gameObject.transform.localPosition = new Vector3(0, 0.1f, 0);
                        }
                        //ó�� �����ϴ°� �ƴ϶��
                        else if (traystatus != 1 && ((other.gameObject.CompareTag(hamburgurbread) || other.gameObject.CompareTag(blackbread))))
                        {
                            other.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
                            other.gameObject.transform.localPosition = new Vector3(0, 0.1f, 0);
                        }
                        else if (!(other.gameObject.CompareTag(hamburgurbread) || other.gameObject.CompareTag(blackbread)))
                        {
                            //������ ���ӿ�����Ʈ�� position�� rotation ���� �ʱ�ȭ��Ų��.
                            other.gameObject.transform.localRotation = Quaternion.Euler(180, 0, 0);
                            other.gameObject.transform.localPosition = new Vector3(0, 0.08f, 0);
                        }
                        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        yield return new WaitForSeconds(0.3f);
                        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        other.gameObject.GetComponent<FoodControl>().isInGrab = true;

                        //10.  ó�� �״� �� ����, �ι� °���� ���� ��
                        if (traystatus != 1)
                        {
                            //11.  ���� �� ������ ���� ������ ��� ������Ʈ ��� Off��Ŵ
                            stackcreateburgur.ToArray()[1].gameObject.GetComponent<Grabbable>().enabled = false;
                            stackcreateburgur.ToArray()[1].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                            stackcreateburgur.ToArray()[1].gameObject.GetComponent<PhysicsGrabbable>().enabled = false;
                            stackcreateburgur.ToArray()[1].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabInteractable>().enabled = false;
                            stackcreateburgur.ToArray()[1].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabInteractable>().enabled = false;
                            stackcreateburgur.ToArray()[1].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabPose>().enabled = false;
                            stackcreateburgur.ToArray()[1].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabPose>().enabled = false;
                        }
                    }
                }
            }
        }
    }

    IEnumerator OnTriggerExit(Collider other)
    {
        //������ �����
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            if ((lgrabstatus.IsGrabbing == true || rgrabstatus.IsGrabbing == true) && other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false && other.gameObject.GetComponent<FoodControl>().isOutGrab == false)
            {
                //other.gameObject.GetComponent<FoodControl>().isOnlyMeat = true;
                other.gameObject.GetComponent<FoodControl>().isEntry = false;
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                yield break;
            }

            //��� �ִ� ���°� �ȳ��� �ٷ� �׸������� ������
            if ((lgrabstatus.IsGrabbing == true || rgrabstatus.IsGrabbing == true) && other.gameObject.CompareTag(badbulgogi) && other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false && other.gameObject.GetComponent<FoodControl>().isOutGrab == false && other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == false)
            {
                //other.gameObject.GetComponent<FoodControl>().isOnlyMeat = true;
                other.gameObject.GetComponent<FoodControl>().isEntry = false;
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                yield break;
            }
            else if ((lgrabstatus.IsGrabbing == true || rgrabstatus.IsGrabbing == true) && other.gameObject.CompareTag(badbulgogi) && other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false && other.gameObject.GetComponent<FoodControl>().isOutGrab == false && other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == true)
            {
                print("�̰� ����?");
                other.gameObject.GetComponent<FoodControl>().isEntry = false;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            }

            //if (grabstatus.IsGrabbing == true && other.gameObject.CompareTag(badbulgogi) && other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false && other.gameObject.GetComponent<FoodControl>().isOutGrab == false && /*other.gameObject.GetComponent<FoodControl>().isOnlyMeat == false && */other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == true)
            //{
            //    print("���� Ÿ�°Ŵ�?");
            //    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            //    yield break;
            //}

            //�ȿ��� ��������, �ȿ��� ���� ���� TriggerExit ����
            if (other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isOutGrab == false)
            {
                print("�ȿ��� ������ �� ����");
                yield break;
            }
            else if (other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isOutGrab == true)
            {
                Destroy(other.gameObject);
                stackcreateburgur.Pop();
                traystatus = stackcreateburgur.ToArray().Length;
                if (traystatus != 0)
                {
                    //�ҽ��� ���ٸ�
                    if (burgursource.childCount == 0)
                    {
                        print("�ҽ�����");
                    }
                    //�ҽ��� �ִٸ�
                    else
                    {
                        burgursource.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = false;
                        burgursource.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        yield return new WaitForSeconds(0.3f);
                        burgursource.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    }
                    //���� �ִ� ���� �ֻ�ܿ� �ִ� �͵��� �ٽ� ���� �� �ְ� ����� ����
                    stackcreateburgur.ToArray()[0].gameObject.GetComponent<Grabbable>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().useGravity = true;
                    stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabPose>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabPose>().enabled = true;
                }
                else
                {
                    print("���� �۵���ų�� ����");
                }
                yield break;
            }
            audiosource.PlayOneShot(audioclips[0]);
            print("����");
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;

            //3. stack���� ��������.
            stackcreateburgur.Pop();
            //6.  stack�� �迭���̸� Ʈ���̻��·� ������Ʈ
            traystatus = stackcreateburgur.ToArray().Length;
            if (traystatus != 0)
            {
                if (burgursource.childCount == 0)
                {
                    print("�ƹ��͵� ���ص��ǰ�");
                }
                else
                {
                    burgursource.GetChild(0).gameObject.GetComponent<BoxCollider>().isTrigger = false;
                    burgursource.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    yield return new WaitForSeconds(0.3f);
                    burgursource.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                //8. ���� �ִ� ���� �ֻ�ܿ� �ִ� �͵��� �ٽ� ���� �� �ְ� ����� ����
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<Grabbable>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().useGravity = true;
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabPose>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabPose>().enabled = true;

            }
            //9. ���� ������ ���� ���� �Ŷ��
            else
            {
                //10. �׳� �������� ���ӿ�����Ʈ�� rigidbody�� �� Ǯ����
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            yield break;
        }
    }
}
