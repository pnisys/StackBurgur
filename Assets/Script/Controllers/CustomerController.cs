using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerController : BaseController
{
    Define.CustomerState customerStateType;
    [SerializeField]
    float speed = 1.0f; // �ӵ��� ���ϴ� ������ ���� �����մϴ�.

    Coroutine co;



    void Start()
    {
        Init();
    }

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Customer;
        customerStateType = Define.CustomerState.Spawned;
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
            customerStateType = Define.CustomerState.WaitingAtCounter;
            //Managers.Tutorial.StateControl(customerStateType);
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
        customerStateType = Define.CustomerState.Ordering;
        co = null;
    }

    private void UpdateOrdering()
    {
        //���̵��� ���� �ܹ��� ī�忡 �´� �ܹ��ſ� �ҽ��� ī�带 �����ֱ�
        //���ѽð� ĵ���� �ѱ�
        //Ʃ�丮�� �ȳ�ĵ���� ������ ���ѽð� �帣�� �ϱ�
    }


}

