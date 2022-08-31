using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;

public class GetBurgurMaterial : MonoBehaviour
{
    //grab 상태 확인용
    public HandGrabInteractor grabstatus;

    //손에 잡히는 위치
    public Transform grabtransform;

    //햄버거 재료 프리팹들
    public GameObject[] materials;
    public GameObject nullmaterial;

    Dictionary<string, bool> dicmaterialtest = new Dictionary<string, bool>();
    Dictionary<GameObject, bool> dicthingtest = new Dictionary<GameObject, bool>();


    //손이 들어와있는 상태
    bool isHand = false;
    //손에 물건이 있냐 없느냐
    bool isThing = false;

    #region 스트링 선언
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

    //손에 콜라이더를 붙여놨다.
    //넣었을 때, 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            //여기서 액션을 취할 수 있게 bool값 조정
            isHand = true;
            Checking(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            //여기서 액션을 취할 수 없게 bool값 조정
            isHand = false;
            nullmaterial = null;
        }
    }

    //손을 넣는다.
    //넣은 상태에서 트리거를 당기면 아이템이 나온다.
    private void Update()
    {
        //손에 들어와있고, 물건을 집지 않은 상태에서 버튼을 눌렀을 때
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch) && isHand == true && isThing == false)
        {
            print("여기 한번만 타지?");
            GameObject mm = Instantiate(nullmaterial, grabtransform);
            //mm.GetComponent<Rigidbody>().useGravity = false;
            //mm.transform.localPosition = new Vector3(0, 0, 0);
            //mm.transform.localRotation = Quaternion.Euler(0, 0, 0);
            //물건이 들려 있는 상태
            isThing = true;
        }
    }

    //검사함수
    //검사해서 nullmaterial에 넣는다.
    void Checking(Collider other)
    {
        for (int i = 0; i < 12; i++)
        {
            //4. 소환하려는 프리팹과 해당 게임오브젝트 태그가 동일하다면?
            if (materials[i].CompareTag(other.gameObject.tag))
            {
                nullmaterial = materials[i];
                break;
            }
        }
    }
}
