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
    public HandGrabInteractor grabstatus;
    public TutorialPeopleAnimator tutorialpeopleanimator;
    public Transform sourceposition;
    public Transform empty;

    public int traystatus = 0;
    public int successscore = 0;
    public int littlesuccessscore = 0;
    public float between = 0.02f;
    bool sourcecorrect = false;

    public Stack<GameObject> stackcreateburgur = new Stack<GameObject>();
    Dictionary<string, bool> dicmaterialtest = new Dictionary<string, bool>();
    Dictionary<string, bool> dictriggertest = new Dictionary<string, bool>();
    Dictionary<string, bool> dicsourcetest = new Dictionary<string, bool>();

    public delegate void Traying();
    public static event Traying OnTraying;

    Transform burgurs;
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
        //FoodControl.OnTraying += this.UpdateStatus;
    }
    private void OnDisable()
    {
        TutorialPeopleAnimator.OnLimitTimeComplete -= this.MenuSelection;
        //FoodControl.OnTraying -= this.UpdateStatus;
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

        dicmaterialtest[sandwichbreadbread] = false;
        dicmaterialtest[hamburgurbread] = false;
        dicmaterialtest[blackbread] = false;
        dicmaterialtest[lettuce] = false;
        dicmaterialtest[tomato] = false;
        dicmaterialtest[onion] = false;
        dicmaterialtest[chicken] = false;
        dicmaterialtest[bulgogi] = false;
        dicmaterialtest[bacon] = false;
        dicmaterialtest[mushroom] = false;
        dicmaterialtest[cheeze] = false;
        dicmaterialtest[shrimp] = false;
        dicmaterialtest[badbulgogi] = false;

        dictriggertest[sandwichbreadbread] = false;
        dictriggertest[hamburgurbread] = false;
        dictriggertest[blackbread] = false;
        dictriggertest[lettuce] = false;
        dictriggertest[tomato] = false;
        dictriggertest[onion] = false;
        dictriggertest[chicken] = false;
        dictriggertest[bulgogi] = false;
        dictriggertest[bacon] = false;
        dictriggertest[mushroom] = false;
        dictriggertest[cheeze] = false;
        dictriggertest[shrimp] = false;
        dictriggertest[badbulgogi] = false;

        dicsourcetest[chillsource] = false;
        dicsourcetest[bbqsource] = false;
        dicsourcetest[mustadsource] = false;
        dicsourcetest[mayonnaise] = false;
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


    //Food�� Tray�� ��������� traystatus ���� üũ����� ��
    //void UpdateStatus()
    //{
    //    if (combination[traystatus - 1].childCount == 0)
    //    {
    //        stackcreateburgur.Pop();
    //        traystatus = stackcreateburgur.ToArray().Length;
    //        if (traystatus != 0)
    //        {
    //            //8. ���� �ִ� ���� �ֻ�ܿ� �ִ� �͵��� �ٽ� ���� �� �ְ� ����� ����
    //            stackcreateburgur.ToArray()[0].gameObject.GetComponent<Grabbable>().enabled = true;
    //            stackcreateburgur.ToArray()[0].gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
    //            stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().useGravity = true;
    //            stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    //            stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
    //            stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabPose>().enabled = true;
    //        }
    //    }
    //}

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
                //�մ��� �ڸ��� ������ ���ư��� �ִϸ��̼� ����
                //�� ������ �����༭ z�������� ���� �ؾ� ��
            }
            //�̰� �κм����Ѱ���
            else if (successscore != 3 && littlesuccessscore == 3 && sourcecorrect == true)
            {
                tutorialgamemanager.islittlesuccess = true;
                print("�κм���");

                //�մ��� �ڸ��� ������ ���ư��� �ִϸ��̼� ����
                //�� ������ �����༭ z�������� ���� �ؾ� ��
            }
            //�̰� �� �׾����� ����
            else/* if (successscore != 3 && littlesuccessscore != 3)*/
            {
                tutorialgamemanager.isfail = true;
                print("����");
                //�մ��� �Ǹ��ϸ鼭 ������ �ִϸ��̼� ����
            }
            sourcecorrect = false;
        }
    }


    //����
    IEnumerator OnTriggerEnter(Collider other)
    {
        //���ͼ� ���������� �Լ� Ÿ�� �ȵ�, �׷��� ������ �� �ٽ� ���� ���� �ѹ� Ż �ʿ䵵 ����
        if (other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false)
        {
            print("���ͼ� ���� ��");
            yield break;
        }
        //�ȿ� ���� �� ���� ��
        if (other.gameObject.GetComponent<FoodControl>().isInGrab == true)
        {
            print("�ȿ� �ִµ� ���� ��");
            yield return new WaitForSeconds(0.2f);
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            other.gameObject.GetComponent<FoodControl>().isInGrab = false;
            other.gameObject.GetComponent<FoodControl>().isOutGrab = true;
        }
        

        //1. �ۿ��� ������ ���� ���� ��
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD") && other.gameObject.GetComponent<FoodControl>().isEntry == false)
        {
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            other.gameObject.GetComponent<FoodControl>().isEntry = true;
            //��� ������ ��� �˻��ϴٰ�
            while (grabstatus.IsGrabbing == true)
            {
                ////0.3�ʸ��� �˻縦 �� ��
                yield return null;
                //���⼭ ��⸦ ���´ٸ�
                if (grabstatus.IsGrabbing == false)
                {
                    #region 1. ���� 13������ �� �״´ٸ�? �׳� Destory �ع�����
                    try
                    {
                        //�÷��� stack�� �Ἥ ������ ���� Push �о�ִ´�.
                        stackcreateburgur.Push(other.gameObject);
                        //tray�� �󸶳� �׿��ִ����� stack�� �迭 ���̸�ŭ �� ���̴�.
                        traystatus = stackcreateburgur.ToArray().Length;

                        //13���� combination position�� �ڽ����� ���ӿ�����Ʈ�� ���� �Ѵ�. 
                        other.gameObject.transform.parent = combination[traystatus - 1];
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        //13.   ��� ������ �Ϸ�Ǹ� ������ Trigger ������ ��ųʸ� �ٽ� false ������
                        dicmaterialtest[other.gameObject.tag] = false;
                        dictriggertest[other.gameObject.tag] = false;
                        Destroy(other.gameObject);
                        break;
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
        //�ҽ� ���� ���� ��
        if (other.gameObject.layer == LayerMask.NameToLayer("SOURCE") && grabstatus.IsGrabbing == false)
        {
            //1. �ҽ�������Ʈ�� Ʈ���� ���� ��ġ��Ų��.
            other.gameObject.transform.position = sourceposition.position;
            //2. 180�� ȸ������ �Ʒ��� ���� �Ѵ�.
            other.gameObject.GetComponent<Animator>().SetBool("try", true);
            //3. �ִϸ��̼� ����ð� 1��
            yield return new WaitForSeconds(1f);
            //other.gameObject.transform.localRotation = Quaternion.Euler(-270, 0, 0);
            //4. �ҽ����� �� ¥�� �ִϸ��̼�
            other.gameObject.GetComponent<Animator>().SetBool("wait", true);
            //5. ¥�� �Ҹ�
            other.gameObject.GetComponent<AudioSource>().enabled = true;
            //6. ������ �ҽ��� �±׿� ī�忡�� ���õ� �ҽ��� �±׸� ���Ѵ�.
            if (tutorialpeopleanimator.selectsourcecard.tag == other.gameObject.tag)
            {
                sourcecorrect = true;
            }
            else
            {
                sourcecorrect = false;
            }
        }
    }


    IEnumerator OnTriggerExit(Collider other)
    {
        //������ �����
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            //�ȿ��� ��������, �ȿ��� ���� ���� TriggerExit ����
            if (other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isOutGrab == false)
            {
                print("�ȿ��� ������ �� ����");
                yield break;
            }
            else if (other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isOutGrab == true)
            {
                other.gameObject.GetComponent<FoodControl>().isEntry = false;
                other.gameObject.GetComponent<FoodControl>().isOutGrab = false;
            }

            print("����");
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;

            //other.gameObject.GetComponent<FoodControl>().isEntry = false;
            //other.gameObject.GetComponent<FoodControl>().isOutGrab = false;
            //3. stack���� ��������.
            stackcreateburgur.Pop();
            //6.  stack�� �迭���̸� Ʈ���̻��·� ������Ʈ
            traystatus = stackcreateburgur.ToArray().Length;
            if (traystatus != 0)
            {
                //8. ���� �ִ� ���� �ֻ�ܿ� �ִ� �͵��� �ٽ� ���� �� �ְ� ����� ����
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<Grabbable>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().useGravity = true;
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
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
            ////��� �ִٰ�
            //while (grabstatus.IsGrabbing == true)
            //{
            //    yield return null;
            //    //�̰� ������ ���� �ȳ����µ� TriggerExit�� �ߵ��ȴٴ°Ŵ�.
            //    print("������ ���� ��� �˻����̾�");
              
            //    //���� �Ǹ�
            //    if (grabstatus.IsGrabbing == false)
            //    {
            //        print("���Ҵ�");
            //        //3. stack���� ��������.
            //        //stackcreateburgur.Pop();

            //        ////6.  stack�� �迭���̸� Ʈ���̻��·� ������Ʈ
            //        //traystatus = stackcreateburgur.ToArray().Length;
            //        //other.gameObject.transform.parent = empty;
            //        //4. ���� ���� Rigidbody�� ��� ������ Ǯ����
            //        //5. �߷°��� ����
            //        //7. ���� ���� ���� ���� ��
                  
            //    }
            //}

            #region �ϴ� �����س��� �ڵ�
            ////������ false
            ////dicmaterialtest[other.gameObject.tag] = false;
            ////1. �տ� ��� �ְ�, ������ ������ ��ųʸ��� false���
            //if (grabstatus.IsGrabbing == true && dicmaterialtest[other.gameObject.tag] == false && dictriggertest[other.gameObject.tag] == false)
            //{
            //    //Debug.Log("���� Ÿ��");
            //    //dictriggertest[other.gameObject.tag] = true;

            //    yield return new WaitForSeconds(2f);
            //    //3. stack���� ��������.
            //    stackcreateburgur.Pop();
            //    //4. ���� ���� Rigidbody�� ��� ������ Ǯ����
            //    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //    //5. �߷°��� ����
            //    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            //    //6.  stack�� �迭���̸� Ʈ���̻��·� ������Ʈ
            //    traystatus = stackcreateburgur.ToArray().Length;
            //    other.gameObject.transform.parent = empty;

            //    //7. ���� ���� ���� ���� ��
            //    if (traystatus != 0)
            //    {
            //        //8. ���� �ִ� ���� �ֻ�ܿ� �ִ� �͵��� �ٽ� ���� �� �ְ� ����� ����
            //        stackcreateburgur.ToArray()[0].gameObject.GetComponent<Grabbable>().enabled = true;
            //        stackcreateburgur.ToArray()[0].gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
            //        stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().useGravity = true;
            //        stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            //        stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
            //        stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabPose>().enabled = true;
            //    }
            //    //9. ���� ������ ���� ���� �Ŷ��
            //    else
            //    {
            //        //10. �׳� �������� ���ӿ�����Ʈ�� rigidbody�� �� Ǯ����
            //        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //    }
            //    yield return new WaitForSeconds(1f);
            //    dictriggertest[other.gameObject.tag] = false;
            //}
            #endregion
        }
    }












}