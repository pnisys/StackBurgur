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
    public Transform[] intialcombination;

    public HandGrabInteractor grabstatus;
    public int traystatus = 0;
    public int successscore = 0;
    public int littlesuccessscore = 0;

    public List<GameObject> createburgur = new List<GameObject>();
    public Stack<GameObject> stackcreateburgur = new Stack<GameObject>();
    Dictionary<GameObject, bool> dicmaterial = new Dictionary<GameObject, bool>();

    Transform burgurs;
    public GameObject Sandwichbreadbread;
    public GameObject Sandwichbreadbread1;

    public GameObject Hamburgurbread;
    public GameObject Hamburgurbread1;

    public GameObject Blackbread;
    public GameObject Blackbread1;

    public GameObject Lettuce;
    public GameObject Lettuce1;

    public GameObject Tomato;
    public GameObject Tomato1;

    public GameObject Onion;
    public GameObject Onion1;

    public GameObject Chicken;
    public GameObject Chicken1;

    public GameObject Bulgogi;
    public GameObject Bulgogi1;

    public GameObject Bacon;
    public GameObject Bacon1;

    public GameObject Mushroom;
    public GameObject Mushroom1;

    public GameObject Cheeze;
    public GameObject Cheeze1;

    public GameObject Shirmp;
    public GameObject Shirmp1;


    string sandwichbreadbread;
    string hamburgurbread;
    string blackbread;
    string lettuce;
    string tomato;
    string onion;
    string chicken;
    string bulgogi;
    string bacon;
    string mushroom;
    string cheeze;
    string shrimp;

    private void Start()
    {
        dicmaterial[Sandwichbreadbread] = false;
        dicmaterial[Sandwichbreadbread1] = false;
        dicmaterial[Hamburgurbread] = false;
        dicmaterial[Hamburgurbread1] = false;
        dicmaterial[Blackbread] = false;
        dicmaterial[Blackbread1] = false;
        dicmaterial[Lettuce] = false;
        dicmaterial[Lettuce1] = false;
        dicmaterial[Tomato] = false;
        dicmaterial[Tomato1] = false;
        dicmaterial[Onion] = false;
        dicmaterial[Onion1] = false;
        dicmaterial[Chicken] = false;
        dicmaterial[Chicken1] = false;
        dicmaterial[Bulgogi] = false;
        dicmaterial[Bulgogi1] = false;
        dicmaterial[Bacon] = false;
        dicmaterial[Bacon1] = false;
        dicmaterial[Mushroom] = false;
        dicmaterial[Mushroom1] = false;
        dicmaterial[Cheeze] = false;
        dicmaterial[Cheeze1] = false;
        dicmaterial[Shirmp] = false;
        dicmaterial[Shirmp1] = false;

        burgurs = gameObject.transform.parent;
        sandwichbreadbread = "SANDWICHBREAD";
        hamburgurbread = "HAMBURGURBREAD";
        blackbread = "BLACKBREAD";
        lettuce = "LETTUCE";
        tomato = "TOMATO";
        onion = "ONION";
        chicken = "CHICKEN";
        bulgogi = "BULGOGI";
        bacon = "BACON";
        mushroom = "MUSHROOM";
        cheeze = "CHEEZE";
        shrimp = "SHIRMP";

    }

    private void OnEnable()
    {
       PeopleAnimator.OnLimitTimeComplete += this.MenuSelection;
    }
    private void OnDisable()
    {
        PeopleAnimator.OnLimitTimeComplete -= this.MenuSelection;
    }

    //내가 적층한 햄버거와 정답 적층 순서를 비교한다.
    void MenuSelection()
    {
        //메뉴를 모두 적층하고 손님에게 내밀었을 때
        if (GameManager.instance.isSelect == true)
        {
            if (GameManager.instance.level == 1)
            {
                if (GameManager.instance.selecthambugurcard.CompareTag("ShrimpBurger"))
                {
                    LocalMenuSeletionOneLevel(blackbread, lettuce, shrimp, blackbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("TotoBurger"))
                {
                    LocalMenuSeletionOneLevel(sandwichbreadbread, tomato, tomato, sandwichbreadbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("TomatoPattyBurger"))
                {
                    LocalMenuSeletionOneLevel(sandwichbreadbread, bulgogi, tomato, bulgogi);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("DoubleCheezeBurger"))
                {
                    LocalMenuSeletionOneLevel(hamburgurbread, cheeze, cheeze, hamburgurbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("BaconBurger"))
                {
                    LocalMenuSeletionOneLevel(blackbread, bulgogi, bacon, blackbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("MushroomBurger"))
                {
                    LocalMenuSeletionOneLevel(hamburgurbread, bulgogi, mushroom, blackbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("ChokchokBurger"))
                {
                    LocalMenuSeletionOneLevel(hamburgurbread, bulgogi, onion, hamburgurbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("BulgogiBurger"))
                {
                    LocalMenuSeletionOneLevel(sandwichbreadbread, lettuce, bulgogi, hamburgurbread);
                }
            }
            else if (GameManager.instance.level == 2)
            {
                if (GameManager.instance.selecthambugurcard.CompareTag("CheezeBulgogiBurger"))
                {
                    LocalMenuSeletionTwoLevel(hamburgurbread, bulgogi, cheeze, lettuce, hamburgurbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("SesameBurger"))
                {
                    LocalMenuSeletionTwoLevel(sandwichbreadbread, bulgogi, chicken, cheeze, sandwichbreadbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("ChickenBurger"))
                {
                    LocalMenuSeletionTwoLevel(sandwichbreadbread, cheeze, chicken, tomato, sandwichbreadbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("TripleMeatBurger"))
                {
                    LocalMenuSeletionTwoLevel(blackbread, chicken, bulgogi, bacon, sandwichbreadbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("InkBurger"))
                {
                    LocalMenuSeletionTwoLevel(blackbread, shrimp, bulgogi, lettuce, blackbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("OnionBurger"))
                {
                    LocalMenuSeletionTwoLevel(lettuce, onion, shrimp, onion, blackbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("YugjeubBurger"))
                {
                    LocalMenuSeletionTwoLevel(bulgogi, onion, lettuce, tomato, bulgogi);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("ManymushroomBurger"))
                {
                    LocalMenuSeletionTwoLevel(sandwichbreadbread, mushroom, bulgogi, mushroom, hamburgurbread);
                }
            }
            else if (GameManager.instance.level == 3)
            {
                if (GameManager.instance.selecthambugurcard.CompareTag("FatBurger"))
                {
                    LocalMenuSeletionThreeLevel(sandwichbreadbread, cheeze, chicken, lettuce, bulgogi, hamburgurbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("CheeseTomatoBurger"))
                {
                    LocalMenuSeletionThreeLevel(hamburgurbread, lettuce, tomato, cheeze, bulgogi, hamburgurbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("SesameCheeseBurger"))
                {
                    LocalMenuSeletionThreeLevel(hamburgurbread, bulgogi, cheeze, tomato, lettuce, sandwichbreadbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("DietBurger"))
                {
                    LocalMenuSeletionThreeLevel(lettuce, sandwichbreadbread, chicken, tomato, cheeze, lettuce);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("HighcalorieBurger"))
                {
                    LocalMenuSeletionThreeLevel(blackbread, cheeze, bacon, shrimp, bulgogi, hamburgurbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("DoubleShrimpBurger"))
                {
                    LocalMenuSeletionThreeLevel(sandwichbreadbread, shrimp, bacon, onion, shrimp, hamburgurbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("GrilledMushroomBulgogiHamburger"))
                {
                    LocalMenuSeletionThreeLevel(blackbread, mushroom, bulgogi, cheeze, onion, hamburgurbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("CheeseShrimpBurger"))
                {
                    LocalMenuSeletionThreeLevel(sandwichbreadbread, cheeze, onion, bacon, shrimp, hamburgurbread);
                }
            }
            else if (GameManager.instance.level == 4)
            {
                if (GameManager.instance.selecthambugurcard.CompareTag("GuinnessBurger"))
                {
                    LocalMenuSeletionFourLevel(blackbread, bulgogi, cheeze, cheeze, bulgogi, onion, blackbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("MoistureBurger"))
                {
                    LocalMenuSeletionFourLevel(hamburgurbread, bacon, onion, lettuce, shrimp, mushroom, hamburgurbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("BigBurger"))
                {
                    LocalMenuSeletionFourLevel(bulgogi, lettuce, onion, bacon, shrimp, mushroom, bulgogi);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("VisualBurger"))
                {
                    LocalMenuSeletionFourLevel(hamburgurbread, lettuce, cheeze, bacon, bulgogi, onion, sandwichbreadbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("BombcalorieBurger"))
                {
                    LocalMenuSeletionFourLevel(blackbread, lettuce, cheeze, bulgogi, cheeze, bulgogi, blackbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("SingleHeartBurger"))
                {
                    LocalMenuSeletionFourLevel(lettuce, bulgogi, onion, shrimp, onion, bulgogi, lettuce);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("EnergyBoosterBurger"))
                {
                    LocalMenuSeletionFourLevel(sandwichbreadbread, mushroom, bacon, onion, shrimp, mushroom, sandwichbreadbread);
                }
                else if (GameManager.instance.selecthambugurcard.CompareTag("ManyBreadBurger"))
                {
                    LocalMenuSeletionFourLevel(blackbread, cheeze, bacon, onion, sandwichbreadbread, bulgogi, hamburgurbread);
                }
            }

            void LocalMenuSeletionOneLevel(params string[] a)
            {
                if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0)
                {
                    GameManager.instance.lifescore--;
                    return;
                }
                //가장 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(0).GetChild(0).gameObject.CompareTag(a[0]))
                {
                    successscore++;
                }

                //그다음 아래에는 양상추가 깔렸으면
                if (burgurs.GetChild(1).GetChild(0).gameObject.CompareTag(a[1]))
                {
                    successscore++;
                }
                //그다음 아래에는 새우가 깔렸으면
                if (burgurs.GetChild(2).GetChild(0).gameObject.CompareTag(a[2]))
                {
                    successscore++;
                }
                //그다음 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(3).GetChild(0).gameObject.CompareTag(a[3]))
                {
                    successscore++;
                }
                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]))
                    {
                        littlesuccessscore++;
                    }
                }
                //이거 성공인거임
                if (successscore == 4)
                {
                    GameManager.instance.score += 500;
                }
                //이건 부분성공한거임
                else if (successscore != 4 && littlesuccessscore == 4)
                {
                    GameManager.instance.score += 250;
                }
                //이건 실패한거임
                else if (GameManager.instance.limitTime < 0)
                {
                    GameManager.instance.lifescore--;
                }
                else if (successscore != 4 && littlesuccessscore != 4)
                {
                    GameManager.instance.lifescore--;
                }
            }
            void LocalMenuSeletionTwoLevel(params string[] a)
            {
                if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0)
                {
                    GameManager.instance.lifescore--;
                    return;
                }
                //가장 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(0).GetChild(0).gameObject.CompareTag(a[0]))
                {
                    successscore++;
                }
                //그다음 아래에는 양상추가 깔렸으면
                if (burgurs.GetChild(1).GetChild(0).gameObject.CompareTag(a[1]))
                {
                    successscore++;
                }
                //그다음 아래에는 새우가 깔렸으면
                if (burgurs.GetChild(2).GetChild(0).gameObject.CompareTag(a[2]))
                {
                    successscore++;
                }
                //그다음 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(3).GetChild(0).gameObject.CompareTag(a[3]))
                {
                    successscore++;
                }
                //그다음 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(4).GetChild(0).gameObject.CompareTag(a[4]))
                {
                    successscore++;
                }
                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]))
                    {
                        littlesuccessscore++;
                    }
                }
                //이거 성공인거임
                if (successscore == 5)
                {
                    GameManager.instance.score += 500;
                }
                //이건 부분성공한거임
                else if (successscore != 5 && littlesuccessscore == 5)
                {
                    GameManager.instance.score += 250;
                }
                //이건 실패한거임
                else if (GameManager.instance.limitTime < 0)
                {
                    GameManager.instance.lifescore--;
                }
                else if (successscore != 5 && littlesuccessscore != 5)
                {
                    GameManager.instance.lifescore--;
                }
            }
            void LocalMenuSeletionThreeLevel(params string[] a)
            {
                if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0 || burgurs.GetChild(5).childCount == 0)
                {
                    GameManager.instance.lifescore--;
                    return;
                }
                //가장 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(0).GetChild(0).gameObject.CompareTag(a[0]))
                {
                    successscore++;
                }
                //그다음 아래에는 양상추가 깔렸으면
                if (burgurs.GetChild(1).GetChild(0).gameObject.CompareTag(a[1]))
                {
                    successscore++;
                }
                //그다음 아래에는 새우가 깔렸으면
                if (burgurs.GetChild(2).GetChild(0).gameObject.CompareTag(a[2]))
                {
                    successscore++;
                }
                //그다음 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(3).GetChild(0).gameObject.CompareTag(a[3]))
                {
                    successscore++;
                }
                //그다음 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(4).GetChild(0).gameObject.CompareTag(a[4]))
                {
                    successscore++;
                }
                //그다음 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(5).GetChild(0).gameObject.CompareTag(a[5]))
                {
                    successscore++;
                }
                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]))
                    {
                        littlesuccessscore++;
                    }
                }
                //이거 성공인거임
                if (successscore == 6)
                {
                    GameManager.instance.score += 500;
                }
                //이건 부분성공한거임
                else if (successscore != 6 && littlesuccessscore == 6)
                {
                    GameManager.instance.score += 250;
                }
                //이건 실패한거임
                else if (GameManager.instance.limitTime < 0)
                {
                    GameManager.instance.lifescore--;
                }
                else if (successscore != 6 && littlesuccessscore != 6)
                {
                    GameManager.instance.lifescore--;
                }
            }
            void LocalMenuSeletionFourLevel(params string[] a)
            {
                if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0 || burgurs.GetChild(3).childCount == 0 || burgurs.GetChild(4).childCount == 0 || burgurs.GetChild(5).childCount == 0 || burgurs.GetChild(6).childCount == 0)
                {
                    GameManager.instance.lifescore--;
                    return;
                }
                //가장 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(0).GetChild(0).gameObject.CompareTag(a[0]))
                {
                    successscore++;
                }
                //그다음 아래에는 양상추가 깔렸으면
                if (burgurs.GetChild(1).GetChild(0).gameObject.CompareTag(a[1]))
                {
                    successscore++;
                }
                //그다음 아래에는 새우가 깔렸으면
                if (burgurs.GetChild(2).GetChild(0).gameObject.CompareTag(a[2]))
                {
                    successscore++;
                }
                //그다음 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(3).GetChild(0).gameObject.CompareTag(a[3]))
                {
                    successscore++;
                }
                //그다음 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(4).GetChild(0).gameObject.CompareTag(a[4]))
                {
                    successscore++;
                }
                //그다음 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(5).GetChild(0).gameObject.CompareTag(a[5]))
                {
                    successscore++;
                }
                //그다음 아래에는 먹물빵이 깔렸으면
                if (burgurs.GetChild(6).GetChild(0).gameObject.CompareTag(a[6]))
                {
                    successscore++;
                }
                foreach (var item in stackcreateburgur.ToArray())
                {
                    if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]) || item.gameObject.CompareTag(a[3]) || item.gameObject.CompareTag(a[4]) || item.gameObject.CompareTag(a[5]) || item.gameObject.CompareTag(a[6]))
                    {
                        littlesuccessscore++;
                    }
                }
                //이거 성공인거임
                if (successscore == 7)
                {
                    GameManager.instance.score += 500;
                }
                //이건 부분성공한거임
                else if (successscore != 7 && littlesuccessscore == 7)
                {
                    GameManager.instance.score += 250;
                }
                //이건 실패한거임
                else if (GameManager.instance.limitTime < 0)
                {
                    GameManager.instance.lifescore--;
                }
                else if (successscore != 7 && littlesuccessscore != 7)
                {
                    GameManager.instance.lifescore--;
                }
            }
        }
    }

    //적층
    IEnumerator OnTriggerEnter(Collider other)
    {
        //FOOD라는 레이어를 감지하고
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            yield return new WaitForSeconds(0.1f);
            //물체를 놓았을 때만
            if (grabstatus.IsGrabbing == false && dicmaterial[other.gameObject] == false)
            {
                dicmaterial[other.gameObject] = true;
                stackcreateburgur.Push(other.gameObject);
                traystatus = stackcreateburgur.ToArray().Length;
                other.gameObject.transform.parent = combination[traystatus - 1];
                other.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                other.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                other.gameObject.GetComponent<Rigidbody>().useGravity = false;

                if (traystatus != 1)
                {
                    stackcreateburgur.ToArray()[1].gameObject.GetComponent<Grabbable>().enabled = false;
                    stackcreateburgur.ToArray()[1].gameObject.GetComponent<PhysicsGrabbable>().enabled = false;
                    stackcreateburgur.ToArray()[1].gameObject.transform.GetChild(2).gameObject.GetComponent<HandGrabInteractable>().enabled = false;
                    stackcreateburgur.ToArray()[1].gameObject.transform.GetChild(2).gameObject.GetComponent<HandGrabPose>().enabled = false;
                }
            }
        }
    }

    IEnumerator OnTriggerExit(Collider other)
    {
        //음식이 벗어날때
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            yield return new WaitForSeconds(0.1f);
            if (grabstatus.IsGrabbing == true && dicmaterial[other.gameObject] == true)
            {
                dicmaterial[other.gameObject] = false;
                stackcreateburgur.Pop();
                if (other.gameObject.name.StartsWith("1"))
                {
                    other.gameObject.transform.parent = intialcombination[0];
                }
                else if (other.gameObject.name.StartsWith("2"))
                {
                    other.gameObject.transform.parent = intialcombination[1];
                }
                else if (other.gameObject.name.StartsWith("3"))
                {
                    other.gameObject.transform.parent = intialcombination[2];
                }
                else if (other.gameObject.name.StartsWith("4"))
                {
                    other.gameObject.transform.parent = intialcombination[3];
                }
                other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                other.gameObject.GetComponent<Rigidbody>().useGravity = true;
                traystatus = stackcreateburgur.ToArray().Length;
                if (traystatus != 0)
                {
                    stackcreateburgur.ToArray()[0].gameObject.GetComponent<Grabbable>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().useGravity = true;
                    stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(2).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
                    stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(2).gameObject.GetComponent<HandGrabPose>().enabled = true;
                }
                else
                {
                    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
        }
    }
}
