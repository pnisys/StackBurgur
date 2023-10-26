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
    float speed = 1.0f; // �ӵ��� ���ϴ� ������ ���� �����մϴ�.

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
        //���̵��� ���� �ܹ��� ī�忡 �´� �ܹ��ſ� �ҽ��� ī�带 �����ֱ�
        Managers.EventBus.Trigger("ShowCard");
        customerStateType = Define.CustomerState.Ordering;
        co = null;
    }

    private void UpdateOrdering()
    {
        //���ѽð� ĵ���� �ѱ�
        //Ʃ�丮�� �ȳ�ĵ���� ������ ���ѽð� �帣�� �ϱ�
    }
}

