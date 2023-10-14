using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : BaseController
{
    CustomerState customerStateType;
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
        WorldObjectType = Define.WorldObject.Monster;
        customerStateType = CustomerState.Spawned;
    }
}
