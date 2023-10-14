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

    }
    private void UpdateWaitingAtCounter() { }
    private void UpdateWalkingToCounter() { }
    private void UpdateOrdering() { }

    private void OnTriggerEnter(Collider other)
    {
        if (customerStateType == CustomerState.Spawned && other == Managers.Tutorial.CountCollider)
        {
            Debug.Log("����");
        }
    }
}
