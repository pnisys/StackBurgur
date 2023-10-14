using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : BaseController
{
    CustomerState customerStateType;
    public enum CustomerState
    {
        Spawned, // �մ��� ���� �ȿ��� ó�� ��ȯ�� ����, �Ŵ� �ձ��� �ɾ�� �� ��� ����
        WalkingToCounter, // �մ��� �Ŵ� �ձ��� �ɾ���� ����
        WaitingAtCounter, // �մ��� �Ŵ� �տ��� ���߰� �ֹ��� ��ٸ��� ����
        Ordering // �մ��� �ֹ��� �ϴ� ����
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
}
