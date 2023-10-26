using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICustomerStrategy
{
    void Init(CustomerController controller);
}

public class TutorialCustomerStrategy : ICustomerStrategy
{
    public void Init(CustomerController controller)
    {
        Managers.EventBus.Subscribe("ShowCard", Managers.EventBus.ShowCard);
    }
}

public class GameCustomerStrategy : ICustomerStrategy
{
    public void Init(CustomerController controller)
    {
        Managers.EventBus.Subscribe("ShowCard", Managers.EventBus.ShowCard);
    }
}

public class CustomerController : BaseController
{
    Define.CustomerState customerStateType;
    private ICustomerStrategy _strategy;

    [SerializeField]
    float speed = 1.0f; // 속도는 원하는 값으로 조정 가능합니다.

    Coroutine co;

    void Start()
    {
        if (Managers.Scene.CurrentSceneType == Define.SceneType.Tutorial)
            _strategy = new TutorialCustomerStrategy();
        else if (Managers.Scene.CurrentSceneType == Define.SceneType.Game)
            _strategy = new GameCustomerStrategy();

        Init();
        _strategy.Init(this);
    }

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Customer;
        customerStateType = Define.CustomerState.Spawned;

        transform.position = GameObject.Find("CustomerSpawnPosition").transform.position;
    }

    void Update()
    {
        switch (customerStateType)
        {
            case Define.CustomerState.Spawned:
                UpdateSpawned();
                break;
            case Define.CustomerState.WalkingToCounter:
                UpdateWalkingToCounter();
                break;
            case Define.CustomerState.WaitingAtCounter:
                UpdateWaitingAtCounter();
                break;
            case Define.CustomerState.Ordering:
                UpdateOrdering();
                break;
        }
    }

    private void UpdateSpawned()
    {
        customerStateType = Define.CustomerState.WalkingToCounter;
    }
    private void UpdateWalkingToCounter()
    {
        GameObject counterPosition = GameObject.Find("CounterPosition");

        Vector3 direction = counterPosition.transform.position - transform.position;
        direction.y = 0; // y 값을 고려하지 않습니다.
        Vector3 dirNormalized = direction.normalized;

        // 고객이 목적지로 걸어갑니다.
        float initialY = transform.position.y; // 초기 Y 값을 저장합니다.
        transform.position += dirNormalized * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, initialY, transform.position.z); // Y 값을 초기값으로 유지합니다.

        // 일정 거리(예: 1.0f)보다 작으면 도착했다고 간주하고 상태를 변경합니다.
        float distanceThreshold = 1f;
        if (direction.magnitude < distanceThreshold)
        {
            customerStateType = Define.CustomerState.WaitingAtCounter;
        }
    }
    private void UpdateWaitingAtCounter()
    {
        if (co == null)
            co = StartCoroutine(WaitAtCounter(2f));
    }
    private IEnumerator WaitAtCounter(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //난이도에 따라 햄버거 카드에 맞는 햄버거와 소스를 카드를 보여주기
        Managers.EventBus.Trigger("ShowCard");
        customerStateType = Define.CustomerState.Ordering;
        co = null;
    }

    private void UpdateOrdering()
    {
        //제한시간 캔버스 켜기
        //튜토리얼 안내캔버스 끝나야 제한시간 흐르게 하기
    }
}

