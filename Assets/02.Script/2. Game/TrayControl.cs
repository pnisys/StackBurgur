using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction.HandGrab;
using Oculus.Interaction;
using System.Linq;
using System;
public class TrayControl : MonoBehaviour
{
    public Transform[] combination;
    public GameManager gamemanager;
    public HandGrabInteractor grabstatus;
    public PeopleAnimator peopleanimator;
    //트레이에서 소스 놓일곳
    public Transform burgursource;
    public Transform empty;
    public string selectedsource;
    public GameObject selectedsourceobject;

    //소스 4개 모아놓는 위치
    public Transform source;
    //소스 4개 프리팹
    public GameObject[] sourceprefab;
    //소스 짜여진 4개 프리팹
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

    private void OnEnable()
    {
        PeopleAnimator.OnLimitTimeComplete += this.MenuSelection;
    }
    private void OnDisable()
    {
        PeopleAnimator.OnLimitTimeComplete -= this.MenuSelection;
    }

    //각종 Start함수 선언들
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
        //적층 높이 계산
        combination[0].localPosition = new Vector3(0, 0, 0f);
        for (int i = 1; i < 13; i++)
        {
            combination[i].localPosition = new Vector3(combination[i - 1].localPosition.x, combination[i - 1].localPosition.y + between, combination[i - 1].localPosition.z);
        }
    }

    private void Start()
    {
        //각종 Start함수 선언들
        StartVariable();
        //위치값 간격 설정
        CombinationControl();
        //Tray
        burgurs = gameObject.transform.GetChild(0).GetChild(0);
    }

    //내가 적층한 햄버거와 정답 적층 순서를 비교한다.
    void MenuSelection()
    {
        //메뉴를 모두 적층하고 손님에게 내밀었을 때
        if (gamemanager.level == 1)
        {
            //스테이지가 4이상일 때
            if (gamemanager.stage >= 4)
            {
                if (gamemanager.selecthambugurcard2.CompareTag("ShrimpBurger"))
                {
                    LocalMenuSeletionOneLevel2(blackbread, lettuce, shrimp, blackbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("TotoBurger"))
                {
                    LocalMenuSeletionOneLevel2(sandwichbreadbread, tomato, tomato, sandwichbreadbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("TomatoPattyBurger"))
                {
                    LocalMenuSeletionOneLevel2(sandwichbreadbread, bulgogi, tomato, bulgogi);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("DoubleCheezeBurger"))
                {
                    LocalMenuSeletionOneLevel2(hamburgurbread, cheeze, cheeze, hamburgurbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("BaconBurger"))
                {
                    LocalMenuSeletionOneLevel2(blackbread, bulgogi, bacon, blackbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("MushroomBurger"))
                {
                    LocalMenuSeletionOneLevel2(hamburgurbread, bulgogi, mushroom, blackbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("ChokchokBurger"))
                {
                    LocalMenuSeletionOneLevel2(hamburgurbread, bulgogi, onion, hamburgurbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("BulgogiBurger"))
                {
                    LocalMenuSeletionOneLevel2(sandwichbreadbread, lettuce, bulgogi, hamburgurbread);
                }
            }
            if (gamemanager.selecthambugurcard.CompareTag("ShrimpBurger"))
            {
                LocalMenuSeletionOneLevel(blackbread, lettuce, shrimp, blackbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("TotoBurger"))
            {
                LocalMenuSeletionOneLevel(sandwichbreadbread, tomato, tomato, sandwichbreadbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("TomatoPattyBurger"))
            {
                LocalMenuSeletionOneLevel(sandwichbreadbread, bulgogi, tomato, bulgogi);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("DoubleCheezeBurger"))
            {
                LocalMenuSeletionOneLevel(hamburgurbread, cheeze, cheeze, hamburgurbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("BaconBurger"))
            {
                LocalMenuSeletionOneLevel(blackbread, bulgogi, bacon, blackbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("MushroomBurger"))
            {
                LocalMenuSeletionOneLevel(hamburgurbread, bulgogi, mushroom, blackbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("ChokchokBurger"))
            {
                LocalMenuSeletionOneLevel(hamburgurbread, bulgogi, onion, hamburgurbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("BulgogiBurger"))
            {
                LocalMenuSeletionOneLevel(sandwichbreadbread, lettuce, bulgogi, hamburgurbread);
            }
        }
        else if (gamemanager.level == 2)
        {
            //스테이지가 4이상일 때
            if (gamemanager.stage >= 4)
            {
                if (gamemanager.selecthambugurcard2.CompareTag("CheezeBulgogiBurger"))
                {
                    LocalMenuSeletionTwoLevel2(hamburgurbread, bulgogi, cheeze, lettuce, hamburgurbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("SesameBurger"))
                {
                    LocalMenuSeletionTwoLevel2(sandwichbreadbread, bulgogi, chicken, cheeze, sandwichbreadbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("ChickenBurger"))
                {
                    LocalMenuSeletionTwoLevel2(sandwichbreadbread, cheeze, chicken, tomato, sandwichbreadbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("TripleMeatBurger"))
                {
                    LocalMenuSeletionTwoLevel2(blackbread, chicken, bulgogi, bacon, sandwichbreadbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("InkBurger"))
                {
                    LocalMenuSeletionTwoLevel2(blackbread, shrimp, bulgogi, lettuce, blackbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("OnionBurger"))
                {
                    LocalMenuSeletionTwoLevel2(lettuce, onion, shrimp, onion, blackbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("YugjeubBurger"))
                {
                    LocalMenuSeletionTwoLevel2(bulgogi, onion, lettuce, tomato, bulgogi);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("ManymushroomBurger"))
                {
                    LocalMenuSeletionTwoLevel2(sandwichbreadbread, mushroom, bulgogi, mushroom, hamburgurbread);
                }
            }
            if (gamemanager.selecthambugurcard.CompareTag("CheezeBulgogiBurger"))
            {
                LocalMenuSeletionTwoLevel(hamburgurbread, bulgogi, cheeze, lettuce, hamburgurbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("SesameBurger"))
            {
                LocalMenuSeletionTwoLevel(sandwichbreadbread, bulgogi, chicken, cheeze, sandwichbreadbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("ChickenBurger"))
            {
                LocalMenuSeletionTwoLevel(sandwichbreadbread, cheeze, chicken, tomato, sandwichbreadbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("TripleMeatBurger"))
            {
                LocalMenuSeletionTwoLevel(blackbread, chicken, bulgogi, bacon, sandwichbreadbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("InkBurger"))
            {
                LocalMenuSeletionTwoLevel(blackbread, shrimp, bulgogi, lettuce, blackbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("OnionBurger"))
            {
                LocalMenuSeletionTwoLevel(lettuce, onion, shrimp, onion, blackbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("YugjeubBurger"))
            {
                LocalMenuSeletionTwoLevel(bulgogi, onion, lettuce, tomato, bulgogi);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("ManymushroomBurger"))
            {
                LocalMenuSeletionTwoLevel(sandwichbreadbread, mushroom, bulgogi, mushroom, hamburgurbread);
            }
        }
        else if (gamemanager.level == 3)
        {
            //스테이지가 4이상일 때
            if (gamemanager.stage >= 4)
            {
                if (gamemanager.selecthambugurcard2.CompareTag("FatBurger"))
                {
                    LocalMenuSeletionThreeLevel2(sandwichbreadbread, cheeze, chicken, lettuce, bulgogi, hamburgurbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("CheeseTomatoBurger"))
                {
                    LocalMenuSeletionThreeLevel2(hamburgurbread, lettuce, tomato, cheeze, bulgogi, hamburgurbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("SesameCheeseBurger"))
                {
                    LocalMenuSeletionThreeLevel2(hamburgurbread, bulgogi, cheeze, tomato, lettuce, sandwichbreadbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("DietBurger"))
                {
                    LocalMenuSeletionThreeLevel2(lettuce, sandwichbreadbread, chicken, tomato, cheeze, lettuce);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("HighcalorieBurger"))
                {
                    LocalMenuSeletionThreeLevel2(blackbread, cheeze, bacon, shrimp, bulgogi, hamburgurbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("DoubleShrimpBurger"))
                {
                    LocalMenuSeletionThreeLevel2(sandwichbreadbread, shrimp, bacon, onion, shrimp, hamburgurbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("GrilledMushroomBulgogiHamburger"))
                {
                    LocalMenuSeletionThreeLevel2(blackbread, mushroom, bulgogi, cheeze, onion, hamburgurbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("CheeseShrimpBurger"))
                {
                    LocalMenuSeletionThreeLevel2(sandwichbreadbread, cheeze, onion, bacon, shrimp, hamburgurbread);
                }
            }
            if (gamemanager.selecthambugurcard.CompareTag("FatBurger"))
            {
                LocalMenuSeletionThreeLevel(sandwichbreadbread, cheeze, chicken, lettuce, bulgogi, hamburgurbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("CheeseTomatoBurger"))
            {
                LocalMenuSeletionThreeLevel(hamburgurbread, lettuce, tomato, cheeze, bulgogi, hamburgurbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("SesameCheeseBurger"))
            {
                LocalMenuSeletionThreeLevel(hamburgurbread, bulgogi, cheeze, tomato, lettuce, sandwichbreadbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("DietBurger"))
            {
                LocalMenuSeletionThreeLevel(lettuce, sandwichbreadbread, chicken, tomato, cheeze, lettuce);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("HighcalorieBurger"))
            {
                LocalMenuSeletionThreeLevel(blackbread, cheeze, bacon, shrimp, bulgogi, hamburgurbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("DoubleShrimpBurger"))
            {
                LocalMenuSeletionThreeLevel(sandwichbreadbread, shrimp, bacon, onion, shrimp, hamburgurbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("GrilledMushroomBulgogiHamburger"))
            {
                LocalMenuSeletionThreeLevel(blackbread, mushroom, bulgogi, cheeze, onion, hamburgurbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("CheeseShrimpBurger"))
            {
                LocalMenuSeletionThreeLevel(sandwichbreadbread, cheeze, onion, bacon, shrimp, hamburgurbread);
            }
        }
        else if (gamemanager.level == 4)
        {
            //스테이지가 4이상일 때
            if (gamemanager.stage >= 4)
            {
                if (gamemanager.selecthambugurcard2.CompareTag("GuinnessBurger"))
                {
                    LocalMenuSeletionFourLevel(blackbread, bulgogi, cheeze, cheeze, bulgogi, onion, blackbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("MoistureBurger"))
                {
                    LocalMenuSeletionFourLevel(hamburgurbread, bacon, onion, lettuce, shrimp, mushroom, hamburgurbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("BigBurger"))
                {
                    LocalMenuSeletionFourLevel(bulgogi, lettuce, onion, bacon, shrimp, mushroom, bulgogi);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("VisualBurger"))
                {
                    LocalMenuSeletionFourLevel(hamburgurbread, lettuce, cheeze, bacon, bulgogi, onion, sandwichbreadbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("BombcalorieBurger"))
                {
                    LocalMenuSeletionFourLevel(blackbread, lettuce, cheeze, bulgogi, cheeze, bulgogi, blackbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("SingleHeartBurger"))
                {
                    LocalMenuSeletionFourLevel(lettuce, bulgogi, onion, shrimp, onion, bulgogi, lettuce);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("EnergyBoosterBurger"))
                {
                    LocalMenuSeletionFourLevel(sandwichbreadbread, mushroom, bacon, onion, shrimp, mushroom, sandwichbreadbread);
                }
                else if (gamemanager.selecthambugurcard2.CompareTag("ManyBreadBurger"))
                {
                    LocalMenuSeletionFourLevel(blackbread, cheeze, bacon, onion, sandwichbreadbread, bulgogi, hamburgurbread);
                }
            }
            if (gamemanager.selecthambugurcard.CompareTag("GuinnessBurger"))
            {
                LocalMenuSeletionFourLevel2(blackbread, bulgogi, cheeze, cheeze, bulgogi, onion, blackbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("MoistureBurger"))
            {
                LocalMenuSeletionFourLevel2(hamburgurbread, bacon, onion, lettuce, shrimp, mushroom, hamburgurbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("BigBurger"))
            {
                LocalMenuSeletionFourLevel2(bulgogi, lettuce, onion, bacon, shrimp, mushroom, bulgogi);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("VisualBurger"))
            {
                LocalMenuSeletionFourLevel2(hamburgurbread, lettuce, cheeze, bacon, bulgogi, onion, sandwichbreadbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("BombcalorieBurger"))
            {
                LocalMenuSeletionFourLevel2(blackbread, lettuce, cheeze, bulgogi, cheeze, bulgogi, blackbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("SingleHeartBurger"))
            {
                LocalMenuSeletionFourLevel2(lettuce, bulgogi, onion, shrimp, onion, bulgogi, lettuce);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("EnergyBoosterBurger"))
            {
                LocalMenuSeletionFourLevel2(sandwichbreadbread, mushroom, bacon, onion, shrimp, mushroom, sandwichbreadbread);
            }
            else if (gamemanager.selecthambugurcard.CompareTag("ManyBreadBurger"))
            {
                LocalMenuSeletionFourLevel2(blackbread, cheeze, bacon, onion, sandwichbreadbread, bulgogi, hamburgurbread);
            }
        }

        void LocalMenuSeletionOneLevel(params string[] a)
        {
            int localsuccessscore = 0;
            int locallittlesuccessscore = 0;


            if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false)
            {
                print("재료 개수가 안맞아서 실패");
                gamemanager.isfail = true;
                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                return;
            }

            //가장 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(0).GetChild(0).gameObject.CompareTag(a[0]))
            {
                print("ok");
                localsuccessscore++;
            }

            //그다음 아래에는 양상추가 깔렸으면
            if (burgurs.GetChild(1).GetChild(0).gameObject.CompareTag(a[1]))
            {
                print("ok");
                localsuccessscore++;
            }
            //그다음 아래에는 새우가 깔렸으면
            if (burgurs.GetChild(2).GetChild(0).gameObject.CompareTag(a[2]))
            {
                print("ok");
                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(3).GetChild(0).gameObject.CompareTag(a[3]))
            {
                print("ok");
                localsuccessscore++;
            }
            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]))
                {
                    locallittlesuccessscore++;
                }
            }
            //이거 성공인거임
            if (localsuccessscore == 4)
            {
                print("완전성공ok");
                gamemanager.iscompletesuccess = true;
                gamemanager.score += 500;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            //이건 부분성공한거임
            else if (localsuccessscore != 4 && locallittlesuccessscore == 4)
            {
                print("부분성공ok");
                gamemanager.islittlesuccess = true;
                gamemanager.score += 250;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            else if (localsuccessscore != 4 || locallittlesuccessscore != 4)
            {
                print("재료가 달라서 실패");
                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                gamemanager.isfail = true;
            }
        }
        void LocalMenuSeletionTwoLevel(params string[] a)
        {
            int localsuccessscore = 0;
            int locallittlesuccessscore = 0;

            if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false)
            {
                print("재료 개수가 안맞아서 실패");
                gamemanager.isfail = true;
                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                return;
            }
            //가장 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(0).GetChild(0).gameObject.CompareTag(a[0]))
            {
                print("ok");
                localsuccessscore++;
            }
            //그다음 아래에는 양상추가 깔렸으면
            if (burgurs.GetChild(1).GetChild(0).gameObject.CompareTag(a[1]))
            {
                print("ok");
                localsuccessscore++;
            }
            //그다음 아래에는 새우가 깔렸으면
            if (burgurs.GetChild(2).GetChild(0).gameObject.CompareTag(a[2]))
            {
                print("ok");
                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(3).GetChild(0).gameObject.CompareTag(a[3]))
            {
                print("ok");
                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(4).GetChild(0).gameObject.CompareTag(a[4]))
            {
                print("ok");
                localsuccessscore++;
            }
            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]))
                {
                    locallittlesuccessscore++;
                }
            }
            //이거 성공인거임
            if (localsuccessscore == 5)
            {
                print("완전성공ok");
                gamemanager.iscompletesuccess = true;
                gamemanager.score += 500;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            //이건 부분성공한거임
            else if (localsuccessscore != 5 && locallittlesuccessscore == 5)
            {
                print("부분성공ok");
                gamemanager.islittlesuccess = true;
                gamemanager.score += 250;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }

            else if (localsuccessscore != 5 && locallittlesuccessscore != 5)
            {
                print("재료가 달라서 실패");
                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                gamemanager.isfail = true;
            }
        }
        void LocalMenuSeletionThreeLevel(params string[] a)
        {
            int localsuccessscore = 0;
            int locallittlesuccessscore = 0;

            if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0 || burgurs.GetChild(5).childCount == 0 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false)
            {
                print("재료 개수가 안맞아서 실패");
                gamemanager.isfail = true;
                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                return;
            }
            //가장 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(0).GetChild(0).gameObject.CompareTag(a[0]))
            {
                print("ok");
                localsuccessscore++;
            }
            //그다음 아래에는 양상추가 깔렸으면
            if (burgurs.GetChild(1).GetChild(0).gameObject.CompareTag(a[1]))
            {
                print("ok");
                localsuccessscore++;
            }
            //그다음 아래에는 새우가 깔렸으면
            if (burgurs.GetChild(2).GetChild(0).gameObject.CompareTag(a[2]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(3).GetChild(0).gameObject.CompareTag(a[3]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(4).GetChild(0).gameObject.CompareTag(a[4]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(5).GetChild(0).gameObject.CompareTag(a[5]))
            {
                print("ok");

                localsuccessscore++;
            }
            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]))
                {
                    locallittlesuccessscore++;
                }
            }
            //이거 성공인거임
            if (localsuccessscore == 6)
            {
                print("완전성공ok");

                gamemanager.iscompletesuccess = true;
                gamemanager.score += 500;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            //이건 부분성공한거임
            else if (localsuccessscore != 6 && locallittlesuccessscore == 6)
            {
                print("일부성공ok");

                gamemanager.islittlesuccess = true;
                gamemanager.score += 250;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            else if (localsuccessscore != 6 && locallittlesuccessscore != 6)
            {
                print("재료가 달라서 실패");

                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                gamemanager.isfail = true;
            }
        }
        void LocalMenuSeletionFourLevel(params string[] a)
        {
            int localsuccessscore = 0;
            int locallittlesuccessscore = 0;
            if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0 || burgurs.GetChild(5).childCount == 0 || burgurs.GetChild(6).childCount == 0 || gamemanager.selectsourcecard.CompareTag(gamemanager.phase1selectedsource) == false)
            {
                print("재료 개수가 안맞아서 실패");
                gamemanager.isfail = true;
                if (gamemanager.stage <= 4)
                {
                    Invoke("FailTray", 3f);
                }
                return;
            }
            //가장 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(0).GetChild(0).gameObject.CompareTag(a[0]))
            {
                print("ok");
                localsuccessscore++;
            }
            //그다음 아래에는 양상추가 깔렸으면
            if (burgurs.GetChild(1).GetChild(0).gameObject.CompareTag(a[1]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 새우가 깔렸으면
            if (burgurs.GetChild(2).GetChild(0).gameObject.CompareTag(a[2]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(3).GetChild(0).gameObject.CompareTag(a[3]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(4).GetChild(0).gameObject.CompareTag(a[4]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(5).GetChild(0).gameObject.CompareTag(a[5]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(6).GetChild(0).gameObject.CompareTag(a[6]))
            {
                print("ok");

                localsuccessscore++;
            }
            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]) || item.gameObject.CompareTag(a[6]))
                {
                    locallittlesuccessscore++;
                }
            }
            //이거 성공인거임
            if (localsuccessscore == 7)
            {
                print("완전성공ok");
                gamemanager.iscompletesuccess = true;
                gamemanager.score += 500;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            //이건 부분성공한거임
            else if (localsuccessscore != 7 && locallittlesuccessscore == 7)
            {
                print("부분성공ok");
                gamemanager.islittlesuccess = true;
                gamemanager.score += 250;
                if (gamemanager.stage < 4)
                {
                    Invoke(nameof(SuccessTray), 1.1f);
                }
            }
            else if (localsuccessscore != 7 && locallittlesuccessscore != 7)
            {
                print("재료가 안맞아서 실패");
                if (gamemanager.stage < 4)
                {
                    Invoke("FailTray", 3f);
                }
                gamemanager.isfail = true;
            }
        }

        void LocalMenuSeletionOneLevel2(params string[] a)
        {
            int localsuccessscore = 0;
            int locallittlesuccessscore = 0;

            if (burgurs.GetChild(15).childCount != 4 || gamemanager.selectsourcecard2.CompareTag(gamemanager.phase2selectedsource) == false)
            {
                print("재료 개수가 안맞아서 실패 : childcount : " + burgurs.GetChild(15).childCount);
                print("gamemanager.selectsourcecard2의 태그 : " + gamemanager.selectsourcecard2.tag);
                print("gamemanager.phase2selectedsource 이름 : " + gamemanager.phase2selectedsource);


                gamemanager.isfail2 = true;
                return;
            }
            //가장 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(3).gameObject.CompareTag(a[0]))
            {
                print("ok");
                localsuccessscore++;
            }

            //그다음 아래에는 양상추가 깔렸으면
            if (burgurs.GetChild(15).GetChild(2).gameObject.CompareTag(a[1]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 새우가 깔렸으면
            if (burgurs.GetChild(15).GetChild(1).gameObject.CompareTag(a[2]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(0).gameObject.CompareTag(a[3]))
            {
                print("ok");

                localsuccessscore++;
            }
            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]))
                {
                    locallittlesuccessscore++;
                }
            }
            //이거 성공인거임
            if (localsuccessscore == 4)
            {
                print("완전성공ok");

                gamemanager.iscompletesuccess2 = true;
                gamemanager.score += 500;
            }
            //이건 부분성공한거임
            else if (localsuccessscore != 4 && locallittlesuccessscore == 4)
            {
                print("일부성공ok");

                gamemanager.islittlesuccess2 = true;
                gamemanager.score += 250;
            }
            else if (localsuccessscore != 4 || locallittlesuccessscore != 4)
            {
                print("재료가 달라서 실패");
                gamemanager.isfail2 = true;
            }
        }
        void LocalMenuSeletionTwoLevel2(params string[] a)
        {
            int localsuccessscore = 0;
            int locallittlesuccessscore = 0;

            if (burgurs.GetChild(15).childCount != 5 || gamemanager.selectsourcecard2.CompareTag(gamemanager.phase2selectedsource) == false)
            {
                print("재료개수가 안맞아서 실패");
                gamemanager.isfail2 = true;
                return;
            }
            //가장 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(4).gameObject.CompareTag(a[0]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 양상추가 깔렸으면
            if (burgurs.GetChild(15).GetChild(3).gameObject.CompareTag(a[1]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 새우가 깔렸으면
            if (burgurs.GetChild(15).GetChild(2).gameObject.CompareTag(a[2]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(1).gameObject.CompareTag(a[3]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(0).gameObject.CompareTag(a[4]))
            {
                print("ok");

                localsuccessscore++;
            }
            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]))
                {
                    locallittlesuccessscore++;
                }
            }
            //이거 성공인거임
            if (localsuccessscore == 5)
            {
                print("완전성공ok");

                gamemanager.iscompletesuccess2 = true;
                gamemanager.score += 500;
            }
            //이건 부분성공한거임
            else if (localsuccessscore != 5 && locallittlesuccessscore == 5)
            {
                print("일부성공ok");

                gamemanager.islittlesuccess2 = true;
                gamemanager.score += 250;
            }

            else if (localsuccessscore != 5 && locallittlesuccessscore != 5)
            {
                print("재료가 안맞아서 실패");

                gamemanager.isfail2 = true;
            }
        }
        void LocalMenuSeletionThreeLevel2(params string[] a)
        {
            int localsuccessscore = 0;
            int locallittlesuccessscore = 0;

            if (burgurs.GetChild(15).childCount != 6 || gamemanager.selectsourcecard2.CompareTag(gamemanager.phase2selectedsource) == false)
            {
                print("재료 개수가 안맞아서 실패");
                gamemanager.isfail2 = true;
                return;
            }
            //가장 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(5).gameObject.CompareTag(a[0]))
            {
                print("ok");
                localsuccessscore++;
            }
            //그다음 아래에는 양상추가 깔렸으면
            if (burgurs.GetChild(15).GetChild(4).gameObject.CompareTag(a[1]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 새우가 깔렸으면
            if (burgurs.GetChild(15).GetChild(3).gameObject.CompareTag(a[2]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(2).gameObject.CompareTag(a[3]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(1).gameObject.CompareTag(a[4]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(0).gameObject.CompareTag(a[5]))
            {
                print("ok");

                localsuccessscore++;
            }
            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]))
                {
                    locallittlesuccessscore++;
                }
            }
            //이거 성공인거임
            if (localsuccessscore == 6)
            {
                print("완전성공ok");

                gamemanager.iscompletesuccess2 = true;
                gamemanager.score += 500;
            }
            //이건 부분성공한거임
            else if (localsuccessscore != 6 && locallittlesuccessscore == 6)
            {
                print("일부성공ok");

                gamemanager.islittlesuccess2 = true;
                gamemanager.score += 250;
            }
            else if (localsuccessscore != 6 && locallittlesuccessscore != 6)
            {
                print("재료가 안맞아서 실패");
                gamemanager.isfail2 = true;
            }
        }
        void LocalMenuSeletionFourLevel2(params string[] a)
        {
            int localsuccessscore = 0;
            int locallittlesuccessscore = 0;
            if (burgurs.GetChild(15).childCount != 7 || gamemanager.selectsourcecard2.CompareTag(gamemanager.phase2selectedsource) == false)
            {
                print("재료 개수가 안맞아서 실패");
                gamemanager.isfail2 = true;
                return;
            }
            //가장 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(6).gameObject.CompareTag(a[0]))
            {
                print("ok");
                localsuccessscore++;
            }
            //그다음 아래에는 양상추가 깔렸으면
            if (burgurs.GetChild(15).GetChild(5).gameObject.CompareTag(a[1]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 새우가 깔렸으면
            if (burgurs.GetChild(15).GetChild(4).gameObject.CompareTag(a[2]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(3).gameObject.CompareTag(a[3]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(2).gameObject.CompareTag(a[4]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(1).gameObject.CompareTag(a[5]))
            {
                print("ok");

                localsuccessscore++;
            }
            //그다음 아래에는 먹물빵이 깔렸으면
            if (burgurs.GetChild(15).GetChild(0).gameObject.CompareTag(a[6]))
            {
                print("ok");

                localsuccessscore++;
            }
            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]) || item.gameObject.CompareTag(a[6]))
                {
                    locallittlesuccessscore++;
                }
            }
            //이거 성공인거임
            if (localsuccessscore == 7)
            {
                print("완전성공ok");

                gamemanager.iscompletesuccess2 = true;
                gamemanager.score += 500;
            }
            //이건 부분성공한거임
            else if (localsuccessscore != 7 && locallittlesuccessscore == 7)
            {
                print("일부성공ok");

                gamemanager.islittlesuccess2 = true;
                gamemanager.score += 250;
            }
            else if (localsuccessscore != 7 && locallittlesuccessscore != 7)
            {
                print("재료가 안맞아서 실패");
                gamemanager.isfail2 = true;
            }
        }

        //스테이지가 4이상이면
        if (gamemanager.stage >= 4)
        {
            //성공시에
            if ((gamemanager.iscompletesuccess == true || gamemanager.islittlesuccess == true) && (gamemanager.iscompletesuccess2 == true || gamemanager.islittlesuccess2 == true))
            {
                Invoke(nameof(SuccessTray), 1.1f);
            }
            //실패시에
            else if (gamemanager.isfail == true || gamemanager.isfail2 == true)
            {
                print("둘 중 하나라도 틀리면 틀린거");
                Invoke("FailTray", 3f);
            }
        }
    }

    //성공시 트레이에 햄버거 놓이는 함수
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
        int childcount = burgurs.GetChild(15).childCount;
        List<GameObject> firstburgur = new List<GameObject>();
        //얘가 첫번째 쌓아올린 걸 타네
        for (int i = 0; i < childcount; i++)
        {
            firstburgur.Add(burgurs.GetChild(15).GetChild(i).gameObject);
        }
        foreach (var item in firstburgur)
        {
            item.transform.parent = gamemanager.people[gamemanager.peoplenumbur - 1].transform.GetChild(3).GetChild(0);
            item.transform.localPosition = new Vector3(0, item.transform.localPosition.y, 0);
        }

        //얘가 두번째에 쌓아올린 걸 타네
        foreach (var item in stackcreateburgur.ToArray())
        {
            item.transform.parent = gamemanager.people[gamemanager.peoplenumbur - 1].transform.GetChild(3).GetChild(1);
            item.transform.localPosition = new Vector3(0, item.transform.localPosition.y + 0.4219f, 0);
            stackcreateburgur.Pop();
        }
    }
    //실패시 트레이 햄버거 없애주는 함수
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



    #region 햄버거 재료, 소스 적층 물리작용 코드
    //적층
    IEnumerator OnTriggerEnter(Collider other)
    {
        //소스 집어 넣을 때
        if (other.gameObject.layer == LayerMask.NameToLayer("SOURCE"))
        {
            //안들어와있는 상태
            if (other.gameObject.GetComponent<SourceControl>().isEntry == false)
            {
                print("여길탔니?");
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<SourceControl>().isEntry = true;
                //잡고 있으면 계속 검사하다가
                while (grabstatus.IsGrabbing == true)
                {
                    ////0.3초마다 검사를 할 것
                    yield return null;
                    //여기서 잡기를 놓는다면
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

                        //소스 부어져있는게 없으면
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
                        //마요네즈 소스라면
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
                        //칠리소스라면
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
                        //머스타드소스
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

                        //만약 스테이지가 4이상이라면
                        //gamemanager에
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

        //트레이를 벗어나서 버릴 때는 이 함수가 맞음
        //고기가 불판을 갔다가, 다시 입장할 때
        //다시 쌓을 때가 있고, 버릴 때가 있다.
        if (((other.gameObject.CompareTag(badbulgogi) || other.gameObject.CompareTag(bulgogi)) && other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == true))
        {
            other.gameObject.GetComponent<FoodControl>().isEntry = true;
            //들어왔어!
            while (grabstatus.IsGrabbing == true)
            {
                //들어왔다가 다시나가면?
                if (other.gameObject.GetComponent<FoodControl>().isEntry == false)
                {
                    yield break;
                }
                yield return null;
                if (grabstatus.IsGrabbing == false)
                {

                    other.gameObject.GetComponent<FoodControl>().isOutGrab = false;
                    other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash = false;

                    #region 1. 만약 13개보다 더 쌓는다면? 그냥 Destory 해버리기
                    try
                    {
                        //컬렉션 stack을 써서 적재한 것을 Push 밀어넣는다.
                        stackcreateburgur.Push(other.gameObject);
                        //tray에 얼마나 쌓여있는지는 stack의 배열 길이만큼 셀 것이다.
                        traystatus = stackcreateburgur.ToArray().Length;

                        //13개의 combination position의 자식으로 게임오브젝트가 들어가게 한다. 
                        other.gameObject.transform.parent = combination[traystatus - 1];
                    }
                    catch (System.IndexOutOfRangeException)
                    {
                        //13.   모든 과정이 완료되면 여러번 Trigger 방지용 딕셔너리 다시 false 시켜줌
                        Destroy(other.gameObject);
                        break;
                    }
                    #endregion
                    //처음 적층하는거라면, 햄버거 거꾸로 뒤집기
                    if (traystatus == 1 && ((other.gameObject.CompareTag(hamburgurbread) || other.gameObject.CompareTag(blackbread))))
                    {
                        other.gameObject.transform.localRotation = Quaternion.Euler(0, 90, 0);
                        other.gameObject.transform.localPosition = new Vector3(0, 0.1f, 0);
                    }
                    //처음 적층하는게 아니라면
                    else if (traystatus != 1 && ((other.gameObject.CompareTag(hamburgurbread) || other.gameObject.CompareTag(blackbread))))
                    {
                        other.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
                        other.gameObject.transform.localPosition = new Vector3(0, 0.1f, 0);
                    }
                    else if (!(other.gameObject.CompareTag(hamburgurbread) || other.gameObject.CompareTag(blackbread)))
                    {
                        //적층한 게임오브젝트의 position과 rotation 값을 초기화시킨다.
                        other.gameObject.transform.localRotation = Quaternion.Euler(180, 0, 0);
                        other.gameObject.transform.localPosition = new Vector3(0, 0.08f, 0);
                    }
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                    yield return new WaitForSeconds(0.3f);
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    other.gameObject.GetComponent<FoodControl>().isInGrab = true;

                    //10.  처음 쌓는 거 말고, 두번 째부터 쌓을 때
                    if (traystatus != 1)
                    {
                        //11.  놓은 것 직전의 것은 못집게 잡는 컴포넌트 모두 Off시킴
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
                else if (other.gameObject.GetComponent<FoodControl>().isEntry == false)
                {
                    yield break;
                }
            }
        }
        //


        ////안에 있을 때 잡을 때
        //if (other.gameObject.GetComponent<FoodControl>().isInGrab == true)
        //{
        //    print("안에 있는데 잡을 때");
        //    yield return new WaitForSeconds(0.2f);
        //    other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        //    other.gameObject.GetComponent<FoodControl>().isInGrab = false;
        //    other.gameObject.GetComponent<FoodControl>().isOutGrab = true;
        //}

        //1. 밖에서 안으로 음식 넣을 때
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            //안에 있을 때 잡을 때
            if (other.gameObject.GetComponent<FoodControl>().isInGrab == true)
            {
                print("안에 있는데 잡을 때");
                yield return new WaitForSeconds(0.2f);
                other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
                other.gameObject.GetComponent<FoodControl>().isInGrab = false;
                other.gameObject.GetComponent<FoodControl>().isOutGrab = true;
            }

            //밖에서 안으로 들어온것일 때
            if (other.gameObject.GetComponent<FoodControl>().isEntry == false)
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<FoodControl>().isEntry = true;
                //잡고 있으면 계속 검사하다가
                while (grabstatus.IsGrabbing == true)
                {
                    //0.3초마다 검사를 할 것
                    yield return null;
                    //들어왔다가 다시나가면?
                    if (other.gameObject.GetComponent<FoodControl>().isEntry == false)
                    {
                        //if(other.gameObject.CompareTag(badbulgogi))
                        //{
                        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        //}
                        yield break;
                    }
                    //여기서 잡기를 놓는다면
                    if (grabstatus.IsGrabbing == false)
                    {
                        //놓는데 트레이밖으로 나가서 그릴에 있으면
                        if (other.gameObject.GetComponent<FoodControl>().isGrill == true)
                        {
                            print("이건가?");
                            yield break;
                        }
                        //놓는데 트레이밖, 그릴도 아니면
                        else if (other.gameObject.GetComponent<FoodControl>().isEntry == false)
                        {
                            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        }

                        #region 1. 만약 13개보다 더 쌓는다면? 그냥 Destory 해버리기
                        try
                        {
                            //컬렉션 stack을 써서 적재한 것을 Push 밀어넣는다.
                            stackcreateburgur.Push(other.gameObject);
                            //tray에 얼마나 쌓여있는지는 stack의 배열 길이만큼 셀 것이다.
                            traystatus = stackcreateburgur.ToArray().Length;

                            //13개의 combination position의 자식으로 게임오브젝트가 들어가게 한다. 
                            other.gameObject.transform.parent = combination[traystatus - 1];
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            //13.   모든 과정이 완료되면 여러번 Trigger 방지용 딕셔너리 다시 false 시켜줌
                            Destroy(other.gameObject);
                            break;
                        }
                        #endregion
                        //처음 적층하는거라면, 햄버거 거꾸로 뒤집기
                        if (traystatus == 1 && ((other.gameObject.CompareTag(hamburgurbread) || other.gameObject.CompareTag(blackbread))))
                        {
                            other.gameObject.transform.localRotation = Quaternion.Euler(0, 90, 0);
                            other.gameObject.transform.localPosition = new Vector3(0, 0.1f, 0);
                        }
                        //처음 적층하는게 아니라면
                        else if (traystatus != 1 && ((other.gameObject.CompareTag(hamburgurbread) || other.gameObject.CompareTag(blackbread))))
                        {
                            other.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 180);
                            other.gameObject.transform.localPosition = new Vector3(0, 0.1f, 0);
                        }
                        else if (!(other.gameObject.CompareTag(hamburgurbread) || other.gameObject.CompareTag(blackbread)))
                        {
                            //적층한 게임오브젝트의 position과 rotation 값을 초기화시킨다.
                            other.gameObject.transform.localRotation = Quaternion.Euler(180, 0, 0);
                            other.gameObject.transform.localPosition = new Vector3(0, 0.08f, 0);
                        }
                        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        yield return new WaitForSeconds(0.3f);
                        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                        other.gameObject.GetComponent<FoodControl>().isInGrab = true;

                        //10.  처음 쌓는 거 말고, 두번 째부터 쌓을 때
                        if (traystatus != 1)
                        {
                            //11.  놓은 것 직전의 것은 못집게 잡는 컴포넌트 모두 Off시킴
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
        //음식이 벗어날때
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            //잡고 있는 상태고 안놓고 바로 그릴쪽으로 가려면
            if (grabstatus.IsGrabbing == true && other.gameObject.CompareTag(badbulgogi) && other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false && other.gameObject.GetComponent<FoodControl>().isOutGrab == false && other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == false)
            {
                //other.gameObject.GetComponent<FoodControl>().isOnlyMeat = true;
                other.gameObject.GetComponent<FoodControl>().isEntry = false;
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                yield break;
            }
            else if (grabstatus.IsGrabbing == true && other.gameObject.CompareTag(badbulgogi) && other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false && other.gameObject.GetComponent<FoodControl>().isOutGrab == false && other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == true)
            {
                other.gameObject.GetComponent<FoodControl>().isEntry = false;
                yield break;
            }

            if (grabstatus.IsGrabbing == true && other.gameObject.CompareTag(badbulgogi) && other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false && other.gameObject.GetComponent<FoodControl>().isOutGrab == false && /*other.gameObject.GetComponent<FoodControl>().isOnlyMeat == false && */other.gameObject.GetComponent<TutorialMeatControl>().ismeattrash == true)
            {
                print("여길 타는거니?");
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                yield break;
            }

            //안에서 놓을때나, 안에서 집을 때나 TriggerExit 방지
            if (other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isOutGrab == false)
            {
                print("안에서 놓았을 때 방지");
                yield break;
            }
            else if (other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isOutGrab == true)
            {
                Vector3 offset = burgurs.position - other.gameObject.transform.position;
                float sqrLen = offset.magnitude;
                if (sqrLen > 1f)
                {
                    print("나간거?" + sqrLen);
                    other.gameObject.GetComponent<FoodControl>().isEntry = false;
                    other.gameObject.GetComponent<FoodControl>().isOutGrab = false;
                }
                else
                {
                    Destroy(other.gameObject);
                    //3. stack에서 빼버린다.
                    stackcreateburgur.Pop();
                    //6.  stack의 배열길이를 트레이상태로 업데이트
                    traystatus = stackcreateburgur.ToArray().Length;
                    if (traystatus != 0)
                    {
                        //8. 남아 있는 가장 최상단에 있는 것들을 다시 잡을 수 있게 만들어 놓음
                        stackcreateburgur.ToArray()[0].gameObject.GetComponent<Grabbable>().enabled = true;
                        stackcreateburgur.ToArray()[0].gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
                        stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().useGravity = true;
                        stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                        stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                        stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                        stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabPose>().enabled = true;
                        stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabPose>().enabled = true;
                    }
                    yield break;
                }
            }

            print("나감");
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;

            //3. stack에서 빼버린다.
            stackcreateburgur.Pop();
            //6.  stack의 배열길이를 트레이상태로 업데이트
            traystatus = stackcreateburgur.ToArray().Length;
            if (traystatus != 0)
            {
                //8. 남아 있는 가장 최상단에 있는 것들을 다시 잡을 수 있게 만들어 놓음
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<Grabbable>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().useGravity = true;
                stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabPose>().enabled = true;
                stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(1).gameObject.GetComponent<HandGrabPose>().enabled = true;

            }
            //9. 만약 마지막 것을 빼는 거라면
            else
            {
                //10. 그냥 집어들었던 게임오브젝트의 rigidbody를 다 풀어줌
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            }
            yield break;
        }
    }
    #endregion


}