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
        TutorialPeopleAnimator.OnLimitTimeComplete += this.MenuSelection;
        //FoodControl.OnTraying += this.UpdateStatus;
    }
    private void OnDisable()
    {
        TutorialPeopleAnimator.OnLimitTimeComplete -= this.MenuSelection;
        //FoodControl.OnTraying -= this.UpdateStatus;
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


    //Food가 Tray를 벗어날때마다 traystatus 상태 체크해줘야 함
    //void UpdateStatus()
    //{
    //    if (combination[traystatus - 1].childCount == 0)
    //    {
    //        stackcreateburgur.Pop();
    //        traystatus = stackcreateburgur.ToArray().Length;
    //        if (traystatus != 0)
    //        {
    //            //8. 남아 있는 가장 최상단에 있는 것들을 다시 잡을 수 있게 만들어 놓음
    //            stackcreateburgur.ToArray()[0].gameObject.GetComponent<Grabbable>().enabled = true;
    //            stackcreateburgur.ToArray()[0].gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
    //            stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().useGravity = true;
    //            stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
    //            stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
    //            stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabPose>().enabled = true;
    //        }
    //    }
    //}

    //내가 적층한 햄버거와 정답 적층 순서를 비교한다.
    void MenuSelection()
    {
        LocalMenuSeletionOneLevel(hamburgurbread, bulgogi, hamburgurbread);
        void LocalMenuSeletionOneLevel(params string[] a)
        {
            //햄버거에 아예 올리지도 못했을 때
            if (burgurs.GetChild(0).childCount == 0 || burgurs.GetChild(1).childCount == 0 || burgurs.GetChild(2).childCount == 0)
            {
                tutorialgamemanager.isfail = true;
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
            foreach (var item in stackcreateburgur.ToArray())
            {
                if (item.gameObject.CompareTag(a[0]) || item.gameObject.CompareTag(a[1]) || item.gameObject.CompareTag(a[2]))
                {
                    littlesuccessscore++;
                }
            }
            //이거 성공인거임
            if (successscore == 3 && sourcecorrect == true)
            {
                tutorialgamemanager.iscompletesuccess = true;
                print("완벽한성공");
                //손님이 자리에 앉으러 돌아가는 애니메이션 구현
                //몸 방향을 돌려줘서 z방향으로 가게 해야 함
            }
            //이건 부분성공한거임
            else if (successscore != 3 && littlesuccessscore == 3 && sourcecorrect == true)
            {
                tutorialgamemanager.islittlesuccess = true;
                print("부분성공");

                //손님이 자리에 앉으러 돌아가는 애니메이션 구현
                //몸 방향을 돌려줘서 z방향으로 가게 해야 함
            }
            //이건 다 쌓았으나 실패
            else/* if (successscore != 3 && littlesuccessscore != 3)*/
            {
                tutorialgamemanager.isfail = true;
                print("실패");
                //손님이 실망하면서 나가는 애니메이션 구현
            }
            sourcecorrect = false;
        }
    }


    //적층
    IEnumerator OnTriggerEnter(Collider other)
    {
        //들어와서 놓았을때는 함수 타면 안됨, 그러나 놓아진 걸 다시 잡을 때는 한번 탈 필요도 있음
        if (other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isInGrab == false)
        {
            print("들어와서 놓을 때");
            yield break;
        }
        //안에 있을 때 잡을 때
        if (other.gameObject.GetComponent<FoodControl>().isInGrab == true)
        {
            print("안에 있는데 잡을 때");
            yield return new WaitForSeconds(0.2f);
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            other.gameObject.GetComponent<FoodControl>().isInGrab = false;
            other.gameObject.GetComponent<FoodControl>().isOutGrab = true;
        }
        

        //1. 밖에서 안으로 음식 넣을 때
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD") && other.gameObject.GetComponent<FoodControl>().isEntry == false)
        {
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            other.gameObject.GetComponent<FoodControl>().isEntry = true;
            //잡고 있으면 계속 검사하다가
            while (grabstatus.IsGrabbing == true)
            {
                ////0.3초마다 검사를 할 것
                yield return null;
                //여기서 잡기를 놓는다면
                if (grabstatus.IsGrabbing == false)
                {
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
                        dicmaterialtest[other.gameObject.tag] = false;
                        dictriggertest[other.gameObject.tag] = false;
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
        //소스 집어 넣을 때
        if (other.gameObject.layer == LayerMask.NameToLayer("SOURCE") && grabstatus.IsGrabbing == false)
        {
            //1. 소스오브젝트를 트레이 위에 위치시킨다.
            other.gameObject.transform.position = sourceposition.position;
            //2. 180도 회전에서 아래를 보게 한다.
            other.gameObject.GetComponent<Animator>().SetBool("try", true);
            //3. 애니메이션 실행시간 1초
            yield return new WaitForSeconds(1f);
            //other.gameObject.transform.localRotation = Quaternion.Euler(-270, 0, 0);
            //4. 소스병을 꽉 짜는 애니메이션
            other.gameObject.GetComponent<Animator>().SetBool("wait", true);
            //5. 짜는 소리
            other.gameObject.GetComponent<AudioSource>().enabled = true;
            //6. 선택한 소스의 태그와 카드에서 제시된 소스의 태그를 비교한다.
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
        //음식이 벗어날때
        if (other.gameObject.layer == LayerMask.NameToLayer("FOOD"))
        {
            //안에서 놓을때나, 안에서 집을 때나 TriggerExit 방지
            if (other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isOutGrab == false)
            {
                print("안에서 놓았을 때 방지");
                yield break;
            }
            else if (other.gameObject.GetComponent<FoodControl>().isEntry == true && other.gameObject.GetComponent<FoodControl>().isOutGrab == true)
            {
                other.gameObject.GetComponent<FoodControl>().isEntry = false;
                other.gameObject.GetComponent<FoodControl>().isOutGrab = false;
            }

            print("나감");
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            other.gameObject.GetComponent<Rigidbody>().useGravity = true;

            //other.gameObject.GetComponent<FoodControl>().isEntry = false;
            //other.gameObject.GetComponent<FoodControl>().isOutGrab = false;
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
            ////잡고 있다가
            //while (grabstatus.IsGrabbing == true)
            //{
            //    yield return null;
            //    //이게 문제는 아직 안나갔는데 TriggerExit가 발동된다는거다.
            //    print("나가고 나서 계속 검사중이야");
              
            //    //놓게 되면
            //    if (grabstatus.IsGrabbing == false)
            //    {
            //        print("놓았다");
            //        //3. stack에서 빼버린다.
            //        //stackcreateburgur.Pop();

            //        ////6.  stack의 배열길이를 트레이상태로 업데이트
            //        //traystatus = stackcreateburgur.ToArray().Length;
            //        //other.gameObject.transform.parent = empty;
            //        //4. 빼는 순간 Rigidbody의 모든 제약을 풀어줌
            //        //5. 중력값을 켜줌
            //        //7. 빼도 아직 남아 있을 때
                  
            //    }
            //}

            #region 일단 보류해놓은 코드
            ////나가면 false
            ////dicmaterialtest[other.gameObject.tag] = false;
            ////1. 손에 쥐고 있고, 여러번 방지용 딕셔너리가 false라면
            //if (grabstatus.IsGrabbing == true && dicmaterialtest[other.gameObject.tag] == false && dictriggertest[other.gameObject.tag] == false)
            //{
            //    //Debug.Log("여기 타냐");
            //    //dictriggertest[other.gameObject.tag] = true;

            //    yield return new WaitForSeconds(2f);
            //    //3. stack에서 빼버린다.
            //    stackcreateburgur.Pop();
            //    //4. 빼는 순간 Rigidbody의 모든 제약을 풀어줌
            //    other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //    //5. 중력값을 켜줌
            //    other.gameObject.GetComponent<Rigidbody>().useGravity = true;
            //    //6.  stack의 배열길이를 트레이상태로 업데이트
            //    traystatus = stackcreateburgur.ToArray().Length;
            //    other.gameObject.transform.parent = empty;

            //    //7. 빼도 아직 남아 있을 때
            //    if (traystatus != 0)
            //    {
            //        //8. 남아 있는 가장 최상단에 있는 것들을 다시 잡을 수 있게 만들어 놓음
            //        stackcreateburgur.ToArray()[0].gameObject.GetComponent<Grabbable>().enabled = true;
            //        stackcreateburgur.ToArray()[0].gameObject.GetComponent<PhysicsGrabbable>().enabled = true;
            //        stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().useGravity = true;
            //        stackcreateburgur.ToArray()[0].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            //        stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabInteractable>().enabled = true;
            //        stackcreateburgur.ToArray()[0].gameObject.transform.GetChild(0).gameObject.GetComponent<HandGrabPose>().enabled = true;
            //    }
            //    //9. 만약 마지막 것을 빼는 거라면
            //    else
            //    {
            //        //10. 그냥 집어들었던 게임오브젝트의 rigidbody를 다 풀어줌
            //        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            //    }
            //    yield return new WaitForSeconds(1f);
            //    dictriggertest[other.gameObject.tag] = false;
            //}
            #endregion
        }
    }












}