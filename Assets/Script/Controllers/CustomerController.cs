using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : BaseController
{
    CustomerState customerStateType;
    [SerializeField]
    float speed = 1.0f; // 속도는 원하는 값으로 조정 가능합니다.

    Coroutine co;

    public enum CustomerState
    {
        Spawned, // 손님이 매장 안에서 처음 소환된 상태, 매대 앞까지 걸어가기 전 대기 상태
        WalkingToCounter, // 손님이 매대 앞까지 걸어오는 상태
        WaitingAtCounter, // 손님이 매대 앞에서 멈추고 주문을 기다리는 상태
        Ordering // 손님이 주문을 하는 상태
    }

    void Start()
    {
        Init();
    }

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Customer;
        customerStateType = CustomerState.Spawned;
    }

    void Update()
    {
        switch (customerStateType)
        {
            case CustomerState.Spawned:
                UpdateSpawned();
                break;
            case CustomerState.WalkingToCounter:
                UpdateWalkingToCounter();
                break;
            case CustomerState.WaitingAtCounter:
                UpdateWaitingAtCounter();
                break;
            case CustomerState.Ordering:
                UpdateOrdering();
                break;
        }
    }

    private void UpdateSpawned()
    {
        customerStateType = CustomerState.WalkingToCounter;
    }
    private void UpdateWalkingToCounter()
    {
        // 방향과 거리를 구합니다.
        Vector3 direction = Managers.Tutorial.CounterPosition.position - transform.position;
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
            customerStateType = CustomerState.WaitingAtCounter;
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
        customerStateType = CustomerState.Ordering;
        co = null;
    }

    private void UpdateOrdering()
    {
    }


}

