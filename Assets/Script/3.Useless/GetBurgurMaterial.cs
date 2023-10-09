using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;

public class GetBurgurMaterial : MonoBehaviour
{
    //grab ���� Ȯ�ο�
    public HandGrabInteractor grabstatus;

    //�տ� ������ ��ġ
    public Transform grabtransform;

    //�ܹ��� ��� �����յ�
    public GameObject[] materials;
    public GameObject nullmaterial;

    Dictionary<string, bool> dicmaterialtest = new Dictionary<string, bool>();
    Dictionary<GameObject, bool> dicthingtest = new Dictionary<GameObject, bool>();


    //���� �����ִ� ����
    bool isHand = false;
    //�տ� ������ �ֳ� ������
    bool isThing = false;

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
    private void Start()
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
    }

    //�տ� �ݶ��̴��� �ٿ�����.
    //�־��� ��, 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            //���⼭ �׼��� ���� �� �ְ� bool�� ����
            isHand = true;
            Checking(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            //���⼭ �׼��� ���� �� ���� bool�� ����
            isHand = false;
            nullmaterial = null;
        }
    }

    //���� �ִ´�.
    //���� ���¿��� Ʈ���Ÿ� ���� �������� ���´�.
    private void Update()
    {
        //�տ� �����ְ�, ������ ���� ���� ���¿��� ��ư�� ������ ��
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch) && isHand == true && isThing == false)
        {
            print("���� �ѹ��� Ÿ��?");
            GameObject mm = Instantiate(nullmaterial, grabtransform);
            //mm.GetComponent<Rigidbody>().useGravity = false;
            //mm.transform.localPosition = new Vector3(0, 0, 0);
            //mm.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //������ ��� �ִ� ����
            isThing = true;
        }
    }

    //�˻��Լ�
    //�˻��ؼ� nullmaterial�� �ִ´�.
    void Checking(Collider other)
    {
        for (int i = 0; i < 12; i++)
        {
            //4. ��ȯ�Ϸ��� �����հ� �ش� ���ӿ�����Ʈ �±װ� �����ϴٸ�?
            if (materials[i].CompareTag(other.gameObject.tag))
            {
                nullmaterial = materials[i];
                break;
            }
        }
    }
}
