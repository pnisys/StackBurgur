using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : BaseController
{
    CustomerState customerStateType;
    [SerializeField]
    float speed = 1.0f; // �ӵ��� ���ϴ� ������ ���� �����մϴ�.

    Coroutine co;

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
        customerStateType = CustomerState.WalkingToCounter;
    }
    private void UpdateWalkingToCounter()
    {
        // ����� �Ÿ��� ���մϴ�.
        Vector3 direction = Managers.Tutorial.CounterPosition.position - transform.position;
        direction.y = 0; // y ���� ������� �ʽ��ϴ�.
        Vector3 dirNormalized = direction.normalized;

        // ���� �������� �ɾ�ϴ�.
        float initialY = transform.position.y; // �ʱ� Y ���� �����մϴ�.
        transform.position += dirNormalized * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, initialY, transform.position.z); // Y ���� �ʱⰪ���� �����մϴ�.

        // ���� �Ÿ�(��: 1.0f)���� ������ �����ߴٰ� �����ϰ� ���¸� �����մϴ�.
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

