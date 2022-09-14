using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using System.Linq;
using System;
public class TrayControl : MonoBehaviour
{
    public AudioClip[] audioclips;
    public AudioSource audiosource;
    public Transform[] combination;
    public GameManager gamemanager;
    public HandGrabInteractor grabstatus;
    public PeopleAnimator peopleanimator;
    //Ʈ���̿��� �ҽ� ���ϰ�
    public Transform burgursource;
    public Transform empty;
    public string selectedsource;
    public GameObject selectedsourceobject;

    //�ҽ� 4�� ��Ƴ��� ��ġ
    public Transform source;
    //�ҽ� 4�� ������
    public GameObject[] sourceprefab;
    //�ҽ� ¥���� 4�� ������
    public GameObject[] burgursourceprefab;

    public int traystatus = 0;
    //public int successscore = 0;
    //public int littlesuccessscore = 0;
    public float between = 0.02f;

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
        PeopleAnimator.OnLimitTimeComplete += this.MenuSelection;
    }
    private void OnDisable()
    {
        PeopleAnimator.OnLimitTimeComplete -= this.MenuSelection;
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
        GameObject selectburgur = gamemanager.selecthambugurcard;
        GameObject selectburgur2 = gamemanager.selecthambugurcard2;
        void Checking1(GameObject aa, GameObject bb)
        {
            //���������� 4�̻��� ��
            if (gamemanager.stage >= 4)
            {
                if (bb.CompareTag("ShrimpBurger"))
                {
                    LocalMenuSeletionOneLevel2(blackbread, lettuce, shrimp, blackbread);
                }
                else if (bb.CompareTag("TotoBurger"))
                {
                    LocalMenuSeletionOneLevel2(sandwichbreadbread, tomato, tomato, sandwichbreadbread);
                }
                else if (bb.CompareTag("TomatoPattyBurger"))
                {
                    LocalMenuSeletionOneLevel2(sandwichbreadbread, bulgogi, tomato, bulgogi);
                }
                else if (bb.CompareTag("DoubleCheezeBurger"))
                {
                    LocalMenuSeletionOneLevel2(hamburgurbread, cheeze, cheeze, hamburgurbread);
                }
                else if (bb.CompareTag("BaconBurger"))
                {
                    LocalMenuSeletionOneLevel2(blackbread, bulgogi, bacon, blackbread);
                }
                else if (bb.CompareTag("MushroomBurger"))
                {
                    LocalMenuSeletionOneLevel2(hamburgurbread, bulgogi, mushroom, blackbread);
                }
                else if (bb.CompareTag("ChokchokBurger"))
                {
                    LocalMenuSeletionOneLevel2(hamburgurbread, bulgogi, onion, hamburgurbread);
                }
                else if (bb.CompareTag("BulgogiBurger"))
                {
                    LocalMenuSeletionOneLevel2(sandwichbreadbread, lettuce, bulgogi, hamburgurbread);
                }
            }
            if (aa.CompareTag("ShrimpBurger"))
            {
                LocalMenuSeletionOneLevel(blackbread, lettuce, shrimp, blackbread);
            }
            else if (aa.CompareTag("TotoBurger"))
            {
                LocalMenuSeletionOneLevel(sandwichbreadbread, tomato, tomato, sandwichbreadbread);
            }
            else if (aa.CompareTag("TomatoPattyBurger"))
            {
                LocalMenuSeletionOneLevel(sandwichbreadbread, bulgogi, tomato, bulgogi);
            }
            else if (aa.CompareTag("DoubleCheezeBurger"))
            {
                LocalMenuSeletionOneLevel(hamburgurbread, cheeze, cheeze, hamburgurbread);
            }
            else if (aa.CompareTag("BaconBurger"))
            {
                LocalMenuSeletionOneLevel(blackbread, bulgogi, bacon, blackbread);
            }
            else if (aa.CompareTag("MushroomBurger"))
            {
                LocalMenuSeletionOneLevel(hamburgurbread, bulgogi, mushroom, blackbread);
            }
            else if (aa.CompareTag("ChokchokBurger"))
            {
                LocalMenuSeletionOneLevel(hamburgurbread, bulgogi, onion, hamburgurbread);
            }
            else if (aa.CompareTag("BulgogiBurger"))
            {
                LocalMenuSeletionOneLevel(sandwichbreadbread, lettuce, bulgogi, hamburgurbread);
            }
        }
        void Checking2(GameObject aa, GameObject bb)
        {
            //���������� 4�̻��� ��
            if (gamemanager.stage >= 4)
            {
                if (bb.CompareTag("CheezeBulgogiBurger"))
                {
                    LocalMenuSeletionTwoLevel2(hamburgurbread, bulgogi, cheeze, lettuce, hamburgurbread);
                }
                else if (bb.CompareTag("SesameBurger"))
                {
                    LocalMenuSeletionTwoLevel2(sandwichbreadbread, bulgogi, chicken, cheeze, sandwichbreadbread);
                }
                else if (bb.CompareTag("ChickenBurger"))
                {
                    LocalMenuSeletionTwoLevel2(sandwichbreadbread, cheeze, chicken, tomato, sandwichbreadbread);
                }
                else if (bb.CompareTag("TripleMeatBurger"))
                {
                    LocalMenuSeletionTwoLevel2(blackbread, chicken, bulgogi, bacon, sandwichbreadbread);
                }
                else if (bb.CompareTag("InkBurger"))
                {
                    LocalMenuSeletionTwoLevel2(blackbread, shrimp, bulgogi, lettuce, blackbread);
                }
                else if (bb.CompareTag("OnionBurger"))
                {
                    LocalMenuSeletionTwoLevel2(lettuce, onion, shrimp, onion, blackbread);
                }
                else if (bb.CompareTag("YugjeubBurger"))
                {
                    LocalMenuSeletionTwoLevel2(bulgogi, onion, lettuce, tomato, bulgogi);
                }
                else if (bb.CompareTag("ManymushroomBurger"))
                {
                    LocalMenuSeletionTwoLevel2(sandwichbreadbread, mushroom, bulgogi, mushroom, hamburgurbread);
                }
            }
            if (aa.CompareTag("CheezeBulgogiBurger"))
            {
                LocalMenuSeletionTwoLevel(hamburgurbread, bulgogi, cheeze, lettuce, hamburgurbread);
            }
            else if (aa.CompareTag("SesameBurger"))
            {
                LocalMenuSeletionTwoLevel(sandwichbreadbread, bulgogi, chicken, cheeze, sandwichbreadbread);
            }
            else if (aa.CompareTag("ChickenBurger"))
            {
                LocalMenuSeletionTwoLevel(sandwichbreadbread, cheeze, chicken, tomato, sandwichbreadbread);
            }
            else if (aa.CompareTag("TripleMeatBurger"))
            {
                LocalMenuSeletionTwoLevel(blackbread, chicken, bulgogi, bacon, sandwichbreadbread);
            }
            else if (aa.CompareTag("InkBurger"))
            {
                LocalMenuSeletionTwoLevel(blackbread, shrimp, bulgogi, lettuce, blackbread);
            }
            else if (aa.CompareTag("OnionBurger"))
            {
                LocalMenuSeletionTwoLevel(lettuce, onion, shrimp, onion, blackbread);
            }
            else if (aa.CompareTag("YugjeubBurger"))
            {
                LocalMenuSeletionTwoLevel(bulgogi, onion, lettuce, tomato, bulgogi);
            }
            else if (aa.CompareTag("ManymushroomBurger"))
            {
                LocalMenuSeletionTwoLevel(sandwichbreadbread, mushroom, bulgogi, mushroom, hamburgurbread);
            }
        }
        void Checking3(GameObject aa, GameObject bb)
        {
            //���������� 4�̻��� ��
            if (gamemanager.stage >= 4)
            {
                if (bb.CompareTag("FatBurger"))
                {
                    LocalMenuSeletionThreeLevel2(sandwichbreadbread, cheeze, chicken, lettuce, bulgogi, hamburgurbread);
                }
                else if (bb.CompareTag("CheeseTomatoBurger"))
                {
                    LocalMenuSeletionThreeLevel2(hamburgurbread, lettuce, tomato, cheeze, bulgogi, hamburgurbread);
                }
                else if (bb.CompareTag("SesameCheeseBurger"))
                {
                    LocalMenuSeletionThreeLevel2(hamburgurbread, bulgogi, cheeze, tomato, lettuce, sandwichbreadbread);
                }
                else if (bb.CompareTag("DietBurger"))
                {
                    LocalMenuSeletionThreeLevel2(lettuce, sandwichbreadbread, chicken, tomato, cheeze, lettuce);
                }
                else if (bb.CompareTag("HighcalorieBurger"))
                {
                    LocalMenuSeletionThreeLevel2(blackbread, cheeze, bacon, shrimp, bulgogi, blackbread);
                }
                else if (bb.CompareTag("DoubleShrimpBurger"))
                {
                    LocalMenuSeletionThreeLevel2(sandwichbreadbread, shrimp, bacon, onion, shrimp, hamburgurbread);
                }
                else if (bb.CompareTag("GrilledMushroomBulgogiHamburger"))
                {
                    LocalMenuSeletionThreeLevel2(blackbread, mushroom, bulgogi, cheeze, onion, hamburgurbread);
                }
                else if (bb.CompareTag("CheeseShrimpBurger"))
                {
                    LocalMenuSeletionThreeLevel2(sandwichbreadbread, cheeze, onion, bacon, shrimp, hamburgurbread);
                }
            }
            if (aa.CompareTag("FatBurger"))
            {
                LocalMenuSeletionThreeLevel(sandwichbreadbread, cheeze, chicken, lettuce, bulgogi, hamburgurbread);
            }
            else if (aa.CompareTag("CheeseTomatoBurger"))
            {
                LocalMenuSeletionThreeLevel(hamburgurbread, lettuce, tomato, cheeze, bulgogi, hamburgurbread);
            }
            else if (aa.CompareTag("SesameCheeseBurger"))
            {
                LocalMenuSeletionThreeLevel(hamburgurbread, bulgogi, cheeze, tomato, lettuce, sandwichbreadbread);
            }
            else if (aa.CompareTag("DietBurger"))
            {
                LocalMenuSeletionThreeLevel(lettuce, sandwichbreadbread, chicken, tomato, cheeze, lettuce);
            }
            else if (aa.CompareTag("HighcalorieBurger"))
            {
                LocalMenuSeletionThreeLevel(blackbread, cheeze, bacon, shrimp, bulgogi, blackbread);
            }
            else if (aa.CompareTag("DoubleShrimpBurger"))
            {
                LocalMenuSeletionThreeLevel(sandwichbreadbread, shrimp, bacon, onion, shrimp, hamburgurbread);
            }
            else if (aa.CompareTag("GrilledMushroomBulgogiHamburger"))
            {
                LocalMenuSeletionThreeLevel(blackbread, mushroom, bulgogi, cheeze, onion, hamburgurbread);
            }
            else if (aa.CompareTag("CheeseShrimpBurger"))
            {
                LocalMenuSeletionThreeLevel(sandwichbreadbread, cheeze, onion, bacon, shrimp, hamburgurbread);
            }
        }
        void Checking4(GameObject aa, GameObject bb)
        {
            //���������� 4�̻��� ��
            if (gamemanager.stage >= 4)
            {
                if (bb.CompareTag("GuinnessBurger"))
                {
                    LocalMenuSeletionFourLevel(blackbread, bulgogi, cheeze, cheeze, bulgogi, onion, blackbread);
                }
                else if (bb.CompareTag("MoistureBurger"))
                {
                    LocalMenuSeletionFourLevel(hamburgurbread, bacon, onion, lettuce, shrimp, mushroom, hamburgurbread);
                }
                else if (bb.CompareTag("BigBurger"))
                {
                    LocalMenuSeletionFourLevel(bulgogi, lettuce, onion, bacon, shrimp, mushroom, bulgogi);
                }
                else if (bb.CompareTag("VisualBurger"))
                {
                    LocalMenuSeletionFourLevel(hamburgurbread, lettuce, cheeze, bacon, bulgogi, onion, sandwichbreadbread);
                }
                else if (bb.CompareTag("BombcalorieBurger"))
                {
                    LocalMenuSeletionFourLevel(blackbread, lettuce, cheeze, bulgogi, cheeze, bulgogi, blackbread);
                }
                else if (bb.CompareTag("SingleHeartBurger"))
                {
                    LocalMenuSeletionFourLevel(lettuce, bulgogi, onion, shrimp, onion, bulgogi, lettuce);
                }
                else if (bb.CompareTag("EnergyBoosterBurger"))
                {
                    LocalMenuSeletionFourLevel(sandwichbreadbread, mushroom, bacon, onion, shrimp, mushroom, sandwichbreadbread);
                }
                else if (bb.CompareTag("ManyBreadBurger"))
                {
                    LocalMenuSeletionFourLevel(blackbread, cheeze, bacon, onion, sandwichbreadbread, bulgogi, hamburgurbread);
                }
            }
            if (aa.CompareTag("GuinnessBurger"))
            {
                LocalMenuSeletionFourLevel2(blackbread, bulgogi, cheeze, cheeze, bulgogi, onion, blackbread);
            }
            else if (aa.CompareTag("MoistureBurger"))
            {
                LocalMenuSeletionFourLevel2(hamburgurbread, bacon, onion, lettuce, shrimp, mushroom, hamburgurbread);
            }
            else if (aa.CompareTag("BigBurger"))
            {
                LocalMenuSeletionFourLevel2(bulgogi, lettuce, onion, bacon, shrimp, mushroom, bulgogi);
            }
            else if (aa.CompareTag("VisualBurger"))
            {
                LocalMenuSeletionFourLevel2(hamburgurbread, lettuce, cheeze, bacon, bulgogi, onion, sandwichbreadbread);
            }
            else if (aa.CompareTag("BombcalorieBurger"))
            {
                LocalMenuSeletionFourLevel2(blackbread, lettuce, cheeze, bulgogi, cheeze, bulgogi, blackbread);
            }
            else if (aa.CompareTag("SingleHeartBurger"))
            {
                LocalMenuSeletionFourLevel2(lettuce, bulgogi, onion, shrimp, onion, bulgogi, lettuce);
            }
            else if (aa.CompareTag("EnergyBoosterBurger"))
            {
                LocalMenuSeletionFourLevel2(sandwichbreadbread, mushroom, bacon, onion, shrimp, mushroom, sandwichbreadbread);
            }
            else if (aa.CompareTag("ManyBreadBurger"))
            {
                LocalMenuSeletionFourLevel2(blackbread, cheeze, bacon, onion, sandwichbreadbread, bulgogi, hamburgurbread);
            }
        }

        //�޴��� ��� �����ϰ� �մԿ��� ���о��� ��
        if (gamemanager.level == 1)
        {
            Checking1(selectburgur, selectburgur2);
        }
        else if (gamemanager.level == 2)
        {
            Checking2(selectburgur, selectburgur2);
        }
        else if (gamemanager.level == 3)
        {
            Checking3(selectburgur, selectburgur2);
        }
        else if (gamemanager.level == 4)
        {
            Checking4(selectburgur, selectburgur2);
        }

        void LocalMenuSeletionOneLevel(params string[] a)
        {
            if (gamemanager.stage >= 4)
            {
                int localsuccessscore = 0;
                int locallittlesuccessscore = 0;

                if (burgurs.GetChild(15).childCount != 4 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false || gamemanager.selectsourcecard == null || gamemanager.phase1selectedsource == null)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        print("�������" + i + " " + burgurs.GetChild(i).gameObject);
                    }
                    print(gamemanager.selectsourcecard);
                    print(gamemanager.phase1selectedsource);
                    print("��� ������ �ȸ¾Ƽ� ���� : childcount : " + burgurs.GetChild(15).childCount);
                    print("gamemanager.selectsourcecard2�� �±� : " + gamemanager.selectsourcecard2.tag);
                    print("gamemanager.phase2selectedsource �̸� : " + gamemanager.phase2selectedsource);

                    gamemanager.isfail2 = true;
                    return;
                }

                for (int i = 3; i >= 0; i--)
                {
                    if (burgurs.GetChild(15).GetChild(i).gameObject.CompareTag(a[(int)MathF.Abs(i - 3)]))
                    {
                        localsuccessscore++;
                    }
                }

                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]))
                    {
                        locallittlesuccessscore++;
                    }
                }
                //�̰� �����ΰ���
                if (localsuccessscore == 4)
                {
                    print("��������ok");

                    gamemanager.iscompletesuccess2 = true;
                    gamemanager.Score += 500;
                }
                //�̰� �κм����Ѱ���
                else if (localsuccessscore != 4 && locallittlesuccessscore == 4)
                {
                    print("�Ϻμ���ok");

                    gamemanager.islittlesuccess2 = true;
                    gamemanager.Score += 250;
                }
                else if (localsuccessscore != 4 || locallittlesuccessscore != 4)
                {
                    print(localsuccessscore);
                    print(locallittlesuccessscore);
                    print("��ᰡ �޶� ����");
                    gamemanager.isfail2 = true;
                }
            }
            else
            {
                int localsuccessscore = 0;
                int locallittlesuccessscore = 0;

                if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false || gamemanager.selectsourcecard == null || gamemanager.phase1selectedsource == null)
                {
                    print("��� ������ �ȸ¾Ƽ� ����");
                    for (int i = 0; i < 4; i++)
                    {
                        print("�������" + i + " " + burgurs.GetChild(i).gameObject);
                    }
                    print(gamemanager.selectsourcecard);
                    print(gamemanager.phase1selectedsource);

                    gamemanager.isfail = true;
                    if (gamemanager.stage < 4)
                    {
                        Invoke("FailTray", 3f);
                    }
                    return;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (burgurs.GetChild(i).GetChild(0).gameObject.CompareTag(a[i]))
                    {
                        localsuccessscore++;
                    }
                }
                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]))
                    {
                        locallittlesuccessscore++;
                    }
                }
                //�̰� �����ΰ���
                if (localsuccessscore == 4)
                {
                    print("��������ok");
                    gamemanager.iscompletesuccess = true;
                    gamemanager.Score += 500;
                    if (gamemanager.stage < 4)
                    {
                        Invoke(nameof(SuccessTray), 1.1f);
                    }
                }
                //�̰� �κм����Ѱ���
                else if (localsuccessscore != 4 && locallittlesuccessscore == 4)
                {
                    print("�κм���ok");
                    gamemanager.islittlesuccess = true;
                    gamemanager.Score += 250;
                    if (gamemanager.stage < 4)
                    {
                        Invoke(nameof(SuccessTray), 1.1f);
                    }
                }
                else if (localsuccessscore != 4 || locallittlesuccessscore != 4)
                {
                    print("��ᰡ �޶� ����");
                    if (gamemanager.stage < 4)
                    {
                        Invoke("FailTray", 3f);
                    }
                    gamemanager.isfail = true;
                }
                //}
            }

        }
        void LocalMenuSeletionTwoLevel(params string[] a)
        {
            if (gamemanager.stage >= 4)
            {
                int localsuccessscore = 0;
                int locallittlesuccessscore = 0;

                if (burgurs.GetChild(15).childCount != 5 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false || gamemanager.selectsourcecard == null || gamemanager.phase1selectedsource == null)
                {
                    print("��ᰳ���� �ȸ¾Ƽ� ����");
                    for (int i = 0; i < 5; i++)
                    {
                        print("�������" + i + " " + burgurs.GetChild(i).gameObject.name);
                    }
                    print(gamemanager.selectsourcecard);
                    print(gamemanager.phase1selectedsource);
                    print("��� ������ �ȸ¾Ƽ� ���� : childcount : " + burgurs.GetChild(15).childCount);
                    print("gamemanager.selectsourcecard2�� �±� : " + gamemanager.selectsourcecard2.tag);
                    print("gamemanager.phase2selectedsource �̸� : " + gamemanager.phase2selectedsource);

                    gamemanager.isfail2 = true;
                    return;
                }

                for (int i = 4; i >= 0; i--)
                {
                    if (burgurs.GetChild(15).GetChild(i).gameObject.CompareTag(a[(int)MathF.Abs(i - 4)]))
                    {
                        localsuccessscore++;
                    }
                }
                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]))
                    {
                        locallittlesuccessscore++;
                    }
                }
                //�̰� �����ΰ���
                if (localsuccessscore == 5)
                {
                    print("��������ok");

                    gamemanager.iscompletesuccess2 = true;
                    gamemanager.Score += 500;
                }
                //�̰� �κм����Ѱ���
                else if (localsuccessscore != 5 && locallittlesuccessscore == 5)
                {
                    print("�Ϻμ���ok");

                    gamemanager.islittlesuccess2 = true;
                    gamemanager.Score += 250;
                }
                else if (localsuccessscore != 5 && locallittlesuccessscore != 5)
                {
                    print("��ᰡ �ȸ¾Ƽ� ����");

                    gamemanager.isfail2 = true;
                }
            }
            else
            {
                int localsuccessscore = 0;
                int locallittlesuccessscore = 0;

                if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false || gamemanager.selectsourcecard == null || gamemanager.phase1selectedsource == null)
                {
                    print("��� ������ �ȸ¾Ƽ� ����");
                    for (int i = 0; i < 5; i++)
                    {
                        print("�������" + i + " " + burgurs.GetChild(i).gameObject);
                    }
                    print(gamemanager.selectsourcecard);
                    print(gamemanager.phase1selectedsource);
                    gamemanager.isfail = true;
                    if (gamemanager.stage < 4)
                    {
                        Invoke("FailTray", 3f);
                    }
                    return;
                }
                for (int i = 0; i < 5; i++)
                {
                    if (burgurs.GetChild(i).GetChild(0).gameObject.CompareTag(a[i]))
                    {
                        localsuccessscore++;
                    }
                }

                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]))
                    {
                        locallittlesuccessscore++;
                    }
                }
                //�̰� �����ΰ���
                if (localsuccessscore == 5)
                {
                    print("��������ok");
                    gamemanager.iscompletesuccess = true;
                    gamemanager.Score += 500;
                    if (gamemanager.stage < 4)
                    {
                        Invoke(nameof(SuccessTray), 1.1f);
                    }
                }
                //�̰� �κм����Ѱ���
                else if (localsuccessscore != 5 && locallittlesuccessscore == 5)
                {
                    print("�κм���ok");
                    gamemanager.islittlesuccess = true;
                    gamemanager.Score += 250;
                    if (gamemanager.stage < 4)
                    {
                        Invoke(nameof(SuccessTray), 1.1f);
                    }
                }

                else if (localsuccessscore != 5 && locallittlesuccessscore != 5)
                {
                    print("��ᰡ �޶� ����");
                    if (gamemanager.stage < 4)
                    {
                        Invoke("FailTray", 3f);
                    }
                    gamemanager.isfail = true;
                }
            }

        }
        void LocalMenuSeletionThreeLevel(params string[] a)
        {
            if (gamemanager.stage >= 4)
            {
                int localsuccessscore = 0;
                int locallittlesuccessscore = 0;

                if (burgurs.GetChild(15).childCount != 6 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false || gamemanager.selectsourcecard == null || gamemanager.phase1selectedsource == null)
                {
                    print("��� ������ �ȸ¾Ƽ� ����");
                    for (int i = 0; i < 6; i++)
                    {
                        print("�������" + i + " " + burgurs.GetChild(i).gameObject.name);
                    }
                    print(gamemanager.selectsourcecard);
                    print(gamemanager.phase1selectedsource);
                    print("��� ������ �ȸ¾Ƽ� ���� : childcount : " + burgurs.GetChild(15).childCount);
                    print("gamemanager.selectsourcecard2�� �±� : " + gamemanager.selectsourcecard2.tag);
                    print("gamemanager.phase2selectedsource �̸� : " + gamemanager.phase2selectedsource);
                    gamemanager.isfail2 = true;
                    return;
                }
                for (int i = 5; i >= 0; i--)
                {
                    if (burgurs.GetChild(15).GetChild(i).gameObject.CompareTag(a[(int)MathF.Abs(i - 5)]))
                    {
                        localsuccessscore++;
                    }
                }

                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]))
                    {
                        locallittlesuccessscore++;
                    }
                }
                //�̰� �����ΰ���
                if (localsuccessscore == 6)
                {
                    print("��������ok");

                    gamemanager.iscompletesuccess2 = true;
                    gamemanager.Score += 500;
                }
                //�̰� �κм����Ѱ���
                else if (localsuccessscore != 6 && locallittlesuccessscore == 6)
                {
                    print("�Ϻμ���ok");

                    gamemanager.islittlesuccess2 = true;
                    gamemanager.Score += 250;
                }
                else if (localsuccessscore != 6 && locallittlesuccessscore != 6)
                {
                    print("localsuccessscore" + localsuccessscore);
                    print("locallittlesuccessscore" + locallittlesuccessscore);

                    print("��ᰡ �ȸ¾Ƽ� ����");
                    gamemanager.isfail2 = true;
                }
            }
            else
            {
                int localsuccessscore = 0;
                int locallittlesuccessscore = 0;

                if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0 || burgurs.GetChild(5).childCount == 0 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false || gamemanager.selectsourcecard == null || gamemanager.phase1selectedsource == null)
                {
                    print("��� ������ �ȸ¾Ƽ� ����");
                    for (int i = 0; i < 6; i++)
                    {
                        print("�������" + i + " " + burgurs.GetChild(i).gameObject);
                    }
                    print(gamemanager.selectsourcecard);
                    print(gamemanager.phase1selectedsource);
                    gamemanager.isfail = true;
                    if (gamemanager.stage < 4)
                    {
                        Invoke("FailTray", 3f);
                    }
                    return;
                }

                for (int i = 0; i < 6; i++)
                {
                    if (burgurs.GetChild(i).GetChild(0).gameObject.CompareTag(a[i]))
                    {
                        localsuccessscore++;
                    }
                }

                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]))
                    {
                        locallittlesuccessscore++;
                    }
                }
                //�̰� �����ΰ���
                if (localsuccessscore == 6)
                {
                    print("��������ok");

                    gamemanager.iscompletesuccess = true;
                    gamemanager.Score += 500;
                    if (gamemanager.stage < 4)
                    {
                        Invoke(nameof(SuccessTray), 1.1f);
                    }
                }
                //�̰� �κм����Ѱ���
                else if (localsuccessscore != 6 && locallittlesuccessscore == 6)
                {
                    print("�Ϻμ���ok");

                    gamemanager.islittlesuccess = true;
                    gamemanager.Score += 250;
                    if (gamemanager.stage < 4)
                    {
                        Invoke(nameof(SuccessTray), 1.1f);
                    }
                }
                else if (localsuccessscore != 6 && locallittlesuccessscore != 6)
                {
                    print("��ᰡ �޶� ����");

                    if (gamemanager.stage < 4)
                    {
                        Invoke("FailTray", 3f);
                    }
                    gamemanager.isfail = true;
                }
            }

        }
        void LocalMenuSeletionFourLevel(params string[] a)
        {
            if (gamemanager.stage >= 4)
            {
                int localsuccessscore = 0;
                int locallittlesuccessscore = 0;
                if (burgurs.GetChild(15).childCount != 7 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false || gamemanager.selectsourcecard == null || gamemanager.phase1selectedsource == null)
                {
                    print("��� ������ �ȸ¾Ƽ� ����");
                    for (int i = 0; i < 7; i++)
                    {
                        print("�������" + i + " " + burgurs.GetChild(i).gameObject.name);
                    }
                    print(gamemanager.selectsourcecard);
                    print(gamemanager.phase1selectedsource);
                    print("��� ������ �ȸ¾Ƽ� ���� : childcount : " + burgurs.GetChild(15).childCount);
                    print("gamemanager.selectsourcecard2�� �±� : " + gamemanager.selectsourcecard2.tag);
                    print("gamemanager.phase2selectedsource �̸� : " + gamemanager.phase2selectedsource);
                    gamemanager.isfail2 = true;
                    return;
                }
                for (int i = 6; i >= 0; i--)
                {
                    if (burgurs.GetChild(15).GetChild(i).gameObject.CompareTag(a[(int)MathF.Abs(i - 6)]))
                    {
                        localsuccessscore++;
                    }
                }

                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]) || item.gameObject.CompareTag(a[6]))
                    {
                        locallittlesuccessscore++;
                    }
                }
                //�̰� �����ΰ���
                if (localsuccessscore == 7)
                {
                    print("��������ok");

                    gamemanager.iscompletesuccess2 = true;
                    gamemanager.Score += 500;
                }
                //�̰� �κм����Ѱ���
                else if (localsuccessscore != 7 && locallittlesuccessscore == 7)
                {
                    print("�Ϻμ���ok");
                    print(locallittlesuccessscore);
                    print(localsuccessscore);

                    for (int i = 0; i < 7; i++)
                    {
                        print("�������" + i + " " + burgurs.GetChild(i).gameObject);
                    }
                    print(gamemanager.selectsourcecard);
                    print(gamemanager.phase1selectedsource);
                    print("��� ������ �ȸ¾Ƽ� ���� : childcount : " + burgurs.GetChild(15).childCount);
                    print("gamemanager.selectsourcecard2�� �±� : " + gamemanager.selectsourcecard2.tag);
                    print("gamemanager.phase2selectedsource �̸� : " + gamemanager.phase2selectedsource);
                    gamemanager.islittlesuccess2 = true;
                    gamemanager.Score += 250;
                }
                else if (localsuccessscore != 7 && locallittlesuccessscore != 7)
                {
                    print("��ᰡ �ȸ¾Ƽ� ����");
                    gamemanager.isfail2 = true;
                }
            }
            else
            {
                int localsuccessscore = 0;
                int locallittlesuccessscore = 0;
                if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0 || burgurs.GetChild(5).childCount == 0 || burgurs.GetChild(6).childCount == 0 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false || gamemanager.selectsourcecard == null || gamemanager.phase1selectedsource == null)
                {
                    print("��� ������ �ȸ¾Ƽ� ����");
                    for (int i = 0; i < 7; i++)
                    {
                        print("�������" + i + " " + burgurs.GetChild(i).gameObject);
                    }
                    print(gamemanager.selectsourcecard);
                    print(gamemanager.phase1selectedsource);
                    gamemanager.isfail = true;
                    if (gamemanager.stage <= 4)
                    {
                        Invoke("FailTray", 3f);
                    }
                    return;
                }
                for (int i = 0; i < 7; i++)
                {
                    if (burgurs.GetChild(i).GetChild(0).gameObject.CompareTag(a[i]))
                    {
                        localsuccessscore++;
                    }
                }

                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]) || item.gameObject.CompareTag(a[6]))
                    {
                        locallittlesuccessscore++;
                    }
                }
                //�̰� �����ΰ���
                if (localsuccessscore == 7)
                {
                    print("��������ok");
                    gamemanager.iscompletesuccess = true;
                    gamemanager.Score += 500;
                    if (gamemanager.stage < 4)
                    {
                        Invoke(nameof(SuccessTray), 1.1f);
                    }
                }
                //�̰� �κм����Ѱ���
                else if (localsuccessscore != 7 && locallittlesuccessscore == 7)
                {
                    print("�κм���ok");
                    gamemanager.islittlesuccess = true;
                    gamemanager.Score += 250;
                    if (gamemanager.stage < 4)
                    {
                        Invoke(nameof(SuccessTray), 1.1f);
                    }
                }
                else if (localsuccessscore != 7 && locallittlesuccessscore != 7)
                {
                    print("��ᰡ �ȸ¾Ƽ� ����");
                    for (int i = 0; i < 7; i++)
                    {
                        print("�������" + i + " " + burgurs.GetChild(i).gameObject.name);
                    }
                    print(gamemanager.selectsourcecard);
                    print(gamemanager.phase1selectedsource);
                    if (gamemanager.stage < 4)
                    {
                        Invoke("FailTray", 3f);
                    }
                    gamemanager.isfail = true;
                }
            }
        }

        void LocalMenuSeletionOneLevel2(params string[] a)
        {
            if (gamemanager.stage >= 4)
            {
                int localsuccessscore = 0;
                int locallittlesuccessscore = 0;

                if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || gamemanager.selectsourcecard2.CompareTag(gamemanager.phase2selectedsource) == false || gamemanager.selectsourcecard2 == null || gamemanager.phase2selectedsource == null)
                {
                    print("��� ������ �ȸ¾Ƽ� ����");
                    for (int i = 0; i < 4; i++)
                    {
                        print("�������" + i + " " + burgurs.GetChild(i).gameObject);
                    }
                    print(gamemanager.selectsourcecard);
                    print(gamemanager.phase1selectedsource);

                    gamemanager.isfail = true;
                    if (gamemanager.stage < 4)
                    {
                        Invoke("FailTray", 3f);
                    }
                    return;
                }
                for (int i = 0; i < 4; i++)
                {
                    if (burgurs.GetChild(i).GetChild(0).gameObject.CompareTag(a[i]))
                    {
                        localsuccessscore++;
                    }
                }
                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]))
                    {
                        locallittlesuccessscore++;
                    }
                }
                //�̰� �����ΰ���
                if (localsuccessscore == 4)
                {
                    print("��������ok");
                    gamemanager.iscompletesuccess = true;
                    gamemanager.Score += 500;
                    if (gamemanager.stage < 4)
                    {
                        Invoke(nameof(SuccessTray), 1.1f);
                    }
                }
                //�̰� �κм����Ѱ���
                else if (localsuccessscore != 4 && locallittlesuccessscore == 4)
                {
                    print("�κм���ok");
                    gamemanager.islittlesuccess = true;
                    gamemanager.Score += 250;
                    if (gamemanager.stage < 4)
                    {
                        Invoke(nameof(SuccessTray), 1.1f);
                    }
                }
                else if (localsuccessscore != 4 || locallittlesuccessscore != 4)
                {
                    print("��ᰡ �޶� ����");
                    if (gamemanager.stage < 4)
                    {
                        Invoke("FailTray", 3f);
                    }
                    gamemanager.isfail = true;
                }
            }

        }
        void LocalMenuSeletionTwoLevel2(params string[] a)
        {

            int localsuccessscore = 0;
            int locallittlesuccessscore = 0;

            if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0 || gamemanager.selectsourcecard2.CompareTag(gamemanager.phase2selectedsource) == false || gamemanager.selectsourcecard2 == null || gamemanager.phase2selectedsource == null)
            {
                print("��� ������ �ȸ¾Ƽ� ����");
                for (int i = 0; i < 5; i++)
                {
                    print("�������" + i + " " + burgurs.GetChild(i).gameObject);
                }
                print(gamemanager.selectsourcecard);
                print(gamemanager.phase1selectedsource);
                gamemanager.isfail = true;
                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                return;
            }
            for (int i = 0; i < 5; i++)
            {
                if (burgurs.GetChild(i).GetChild(0).gameObject.CompareTag(a[i]))
                {
                    localsuccessscore++;
                }
            }

            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]))
                {
                    locallittlesuccessscore++;
                }
            }
            //�̰� �����ΰ���
            if (localsuccessscore == 5)
            {
                print("��������ok");
                gamemanager.iscompletesuccess = true;
                gamemanager.Score += 500;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            //�̰� �κм����Ѱ���
            else if (localsuccessscore != 5 && locallittlesuccessscore == 5)
            {
                print("�κм���ok");
                gamemanager.islittlesuccess = true;
                gamemanager.Score += 250;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }

            else if (localsuccessscore != 5 && locallittlesuccessscore != 5)
            {
                print("��ᰡ �޶� ����");
                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                gamemanager.isfail = true;
            }
        }
        void LocalMenuSeletionThreeLevel2(params string[] a)
        {
            int localsuccessscore = 0;
            int locallittlesuccessscore = 0;

            if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0 || burgurs.GetChild(5).childCount == 0 || gamemanager.selectsourcecard2.CompareTag(gamemanager.phase2selectedsource) == false || gamemanager.selectsourcecard2 == null || gamemanager.phase2selectedsource == null)
            {
                print("��� ������ �ȸ¾Ƽ� ����");
                for (int i = 0; i < 6; i++)
                {
                    print("�������" + i + " " + burgurs.GetChild(i).gameObject);
                }
                print(gamemanager.selectsourcecard);
                print(gamemanager.phase1selectedsource);
                gamemanager.isfail = true;
                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                return;
            }

            for (int i = 0; i < 6; i++)
            {
                if (burgurs.GetChild(i).GetChild(0).gameObject.CompareTag(a[i]))
                {
                    localsuccessscore++;
                }
            }

            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]))
                {
                    locallittlesuccessscore++;
                }
            }
            //�̰� �����ΰ���
            if (localsuccessscore == 6)
            {
                print("��������ok");

                gamemanager.iscompletesuccess = true;
                gamemanager.Score += 500;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            //�̰� �κм����Ѱ���
            else if (localsuccessscore != 6 && locallittlesuccessscore == 6)
            {
                print("�Ϻμ���ok");

                gamemanager.islittlesuccess = true;
                gamemanager.Score += 250;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            else if (localsuccessscore != 6 && locallittlesuccessscore != 6)
            {
                print("��ᰡ �޶� ����");

                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                gamemanager.isfail = true;
            }

        }
        void LocalMenuSeletionFourLevel2(params string[] a)
        {
            int localsuccessscore = 0;
            int locallittlesuccessscore = 0;
            if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0 || burgurs.GetChild(5).childCount == 0 || burgurs.GetChild(6).childCount == 0 || gamemanager.selectsourcecard2.CompareTag(gamemanager.phase2selectedsource) == false || gamemanager.selectsourcecard2 == null || gamemanager.phase2selectedsource == null)
            {
                print("��� ������ �ȸ¾Ƽ� ����");
                for (int i = 0; i < 7; i++)
                {
                    print("�������" + i + " " + burgurs.GetChild(i).gameObject);
                }
                print(gamemanager.selectsourcecard);
                print(gamemanager.phase1selectedsource);
                gamemanager.isfail = true;
                if (gamemanager.stage <= 4)
                {
                    Invoke("FailTray", 3f);
                }
                return;
            }
            for (int i = 0; i < 7; i++)
            {
                if (burgurs.GetChild(i).GetChild(0).gameObject.CompareTag(a[i]))
                {
                    localsuccessscore++;
                }
            }

            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]) || item.gameObject.CompareTag(a[6]))
                {
                    locallittlesuccessscore++;
                }
            }
            //�̰� �����ΰ���
            if (localsuccessscore == 7)
            {
                print("��������ok");
                gamemanager.iscompletesuccess = true;
                gamemanager.Score += 500;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            //�̰� �κм����Ѱ���
            else if (localsuccessscore != 7 && locallittlesuccessscore == 7)
            {
                print("�κм���ok");
                gamemanager.islittlesuccess = true;
                gamemanager.Score += 250;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            else if (localsuccessscore != 7 && locallittlesuccessscore != 7)
            {
                print("��ᰡ �ȸ¾Ƽ� ����");
                for (int i = 0; i < 7; i++)
                {
                    print("�������" + i + " " + burgurs.GetChild(i).gameObject.name);
                }
                print(gamemanager.selectsourcecard);
                print(gamemanager.phase1selectedsource);
                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                gamemanager.isfail = true;
            }
        }

        //���������� 4�̻��̸�
        if (gamemanager.stage >= 4)
        {
            //�����ÿ�
            if ((gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true) && (gamemanager.iscompletesuccess2 == true || gamemanager.islittlesuccess2 == true))
            {
                Invoke(nameof(SuccessTray), 0.5f);
            }
            //���нÿ�
            else if (gamemanager.isfail == true || gamemanager.isfail2 == true)
            {
                print("�� �� �ϳ��� Ʋ���� Ʋ����");
                Invoke("FailTray", 3f);
            }
        }
    }

    //������ Ʈ���̿� �ܹ��� ���̴� �Լ�
    void SuccessTray()
    {
        if (burgursource.GetChild(0) == null)
        {
            return;
        }
        else
        {
            Destroy(burgursource.GetChild(0).gameObject);
        }
        if (gamemanager.stage >= 4)
        {
            int childcount = burgurs.GetChild(15).childCount;
            List<GameObject> firstburgur = new List<GameObject>();
            //�갡 ù��° �׾ƿø� �� Ÿ��
            for (int i = 0; i < childcount; i++)
            {
                firstburgur.Add(burgurs.GetChild(15).GetChild(i).gameObject);
            }
            foreach (var item in firstburgur)
            {
                item.transform.parent = gamemanager.people[gamemanager.peoplenumbur - 1].transform.GetChild(3).GetChild(0);
                item.transform.localPosition = new Vector3(0, item.transform.localPosition.y, 0);
            }
            //�갡 �ι�°�� �׾ƿø� �� Ÿ��
            foreach (var item in stackcreateburgur.ToArray())
            {
                item.transform.parent = gamemanager.people[gamemanager.peoplenumbur - 1].transform.GetChild(3).GetChild(1);
                item.transform.localPosition = new Vector3(0, item.transform.localPosition.y + 0.4219f, 0);
                stackcreateburgur.Pop();
            }
        }
        else
        {
            foreach (var item in stackcreateburgur.ToArray())
            {
                item.transform.parent = gamemanager.people[gamemanager.peoplenumbur - 1].transform.GetChild(3).GetChild(0);
                item.transform.localPosition = new Vector3(0, item.transform.localPosition.y + 0.4219f, 0);
                stackcreateburgur.Pop();
            }
        }
    }
    //���н� Ʈ���� �ܹ��� �����ִ� �Լ�
    void FailTray()
    {
        foreach (var item in stackcreateburgur.ToArray())
        {
            stackcreateburgur.Pop();
            Destroy(item);
        }
        if (burgursource.childCount == 0)
        {
            return;
        }
        else
        {
            Destroy(burgursource.GetChild(0).gameObject);
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

    #region �ܹ��� ���, �ҽ� ���� �����ۿ� �ڵ�
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
                while (grabstatus.IsGrabbing == true)
                {
                    ////0.3�ʸ��� �˻縦 �� ��
                    yield return null;
                    //���⼭ ��⸦ ���´ٸ�
                    if (grabstatus.IsGrabbing == false)
                    {
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

                        //���� ���������� 4�̻��̶��
                        //gamemanager��
                        if (gamemanager.stage >= 4)
                        {
                            if (gamemanager.currentpeople.GetComponent<PeopleAnimator>().phase1source == true)
                            {
                                gamemanager.phase1selectedsource = selectedsource;
                            }
                            else if (gamemanager.currentpeople.GetComponent<PeopleAnimator>().phase2source == true)
                            {
                                gamemanager.phase2selectedsource = selectedsource;
                            }
                        }
                        else
                        {
                            if (gamemanager.currentpeople.GetComponent<PeopleAnimator>().phase1source == true)
                            {
                                gamemanager.phase1selectedsource = selectedsource;
                            }
                        }
                        Destroy(other.gameObject);
                        yield break;
                    }
                }
            }
        }


        //Ʈ���̸� ����� ���� ���� �� �Լ��� ����
        //��Ⱑ ������ ���ٰ�, �ٽ� ������ ��
        //�ٽ� ���� ���� �ְ�, ���� ���� �ִ�.
        if (((other.gameObject.CompareTag(badbulgogi) || other.gameObject.CompareTag(bulgogi)) && other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == true))
        {
            other.gameObject.GetComponent<FoodControl>().isEntry = true;
            //���Ծ�!
            while (grabstatus.IsGrabbing == true)
            {
                yield return null;
                //���Դٰ� �ٽó�����?
                if (other.gameObject.GetComponent<FoodControl>().isEntry == false)
                {
                    yield break;
                }
                if (grabstatus.IsGrabbing == false)
                {
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

                    //if (!(other.gameObject.CompareTag(hamburgurbread) || other.gameObject.CompareTag(blackbread)))
                    //{
                    //������ ���ӿ�����Ʈ�� position�� rotation ���� �ʱ�ȭ��Ų��.
                    other.gameObject.transform.localRotation = Quaternion.Euler(180, 0, 0);
                    other.gameObject.transform.localPosition = new Vector3(0, 0.08f, 0);
                    //}
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
                while (grabstatus.IsGrabbing == true)
                {
                    //0.3�ʸ��� �˻縦 �� ��
                    yield return null;
                    //���Դٰ� �ٽó�����?
                    if (other.gameObject.GetComponent<FoodControl>().isEntry == false)
                    {
                        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        yield break;
                    }
                    //���⼭ ��⸦ ���´ٸ�
                    if (grabstatus.IsGrabbing == false)
                    {
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
            #region �Ϲ����� ��Ȳ�ƴ�
            //��� ���� �������� ��� �ִ� ���°� �ȳ��� �ٷ� �׸������� ������
            if (grabstatus.IsGrabbing == true && other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false && other.gameObject.GetComponent<FoodControl>().isOutGrab == false)
            {
                print("������ �ȳ��� �׳� �����");
                //other.gameObject.GetComponent<FoodControl>().isOnlyMeat = true;
                other.gameObject.GetComponent<FoodControl>().isEntry = false;
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                yield break;
            }

            //��⸦ ��� �ִ� ���°� �ȳ��� �ٷ� �׸������� ������
            if (grabstatus.IsGrabbing == true && other.gameObject.CompareTag(badbulgogi) && other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false && other.gameObject.GetComponent<FoodControl>().isOutGrab == false && other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == false)
            {
                print("��� �ִ� ���°� �ȳ��� �ٷ� �׸������� ������");
                //other.gameObject.GetComponent<FoodControl>().isOnlyMeat = true;
                other.gameObject.GetComponent<FoodControl>().isEntry = false;
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                yield break;
            }
            //�������ִ� ��⸦ �׸����� ������ Ʈ���̿� �ȳ��� �״�� �������ö�
            else if (grabstatus.IsGrabbing == true && other.gameObject.CompareTag(badbulgogi) && other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false && other.gameObject.GetComponent<FoodControl>().isOutGrab == false && other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == true)
            {
                print("�̰� ����?");
                other.gameObject.GetComponent<FoodControl>().isEntry = false;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                yield break;
            }

            //if (grabstatus.IsGrabbing == true && other.gameObject.CompareTag(badbulgogi) && other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false && other.gameObject.GetComponent<FoodControl>().isOutGrab == false && /*other.gameObject.GetComponent<FoodControl>().isOnlyMeat == false && */other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == true)
            //{
            //    print("���� Ÿ�°Ŵ�?");
            //    other.gameObject.GetComponent<FoodControl>().isEntry = false;
            //    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            //    yield break;
            //}
            #region �ȿ��� ���ų�, ������ (�Ϲ����� ��Ȳ�ƴ�)
            //�ȿ��� ������
            if (other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isOutGrab == false)
            {
                print("�ȿ��� ������ �� ����");
                yield break;
            }
            //�ȿ��� ������
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
            #endregion
            #endregion


            audiosource.PlayOneShot(audioclips[0]);
            print("����");
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;

            //3. stack���� ��������.
            stackcreateburgur.Pop();
            //6.  stack�� �迭���̸� Ʈ���̻��·� ������Ʈ
            traystatus = stackcreateburgur.ToArray().Length;

            //ó���� �ƴϸ�
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
    #endregion


}