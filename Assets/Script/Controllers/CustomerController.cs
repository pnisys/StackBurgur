using System;
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

   public bool IsOrdering = false;

    public void SetCustomerStateType(Define.CustomerState type)
    {
        customerStateType = type;
    }

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
            case Define.CustomerState.Judgeing:
                UpdateJudge();
                break;
            case Define.CustomerState.Finish:
                UpdateFinish();
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
        yield return new WaitUntil(() => IsOrdering == true);
        customerStateType = Define.CustomerState.Judgeing;
        co = null;
    }

    private void UpdateJudge()
    {
        int stage = Managers.Game.CurrentStage;
        int score = Managers.Game.Score;

        bool isPass = false;
        switch (stage)
        {
            case 0:
                if (score >= 30)
                    isPass = true;
                break;
            case 1:
                if (score >= 40)
                    isPass = true;
                break;
            case 2:
                if (score >= 50)
                    isPass = true;
                break;
            case 3:
                if (score >= 60)
                    isPass = true;
                break;
            case 4:
                if (score >= 70)
                    isPass = true;
                break;
        }

        if (isPass == true)
        {
            //�հ��ϸ� �ڸ��� ����
            GameObject seatPosition = GameObject.Find("SeatPosition");

            Vector3 direction = seatPosition.transform.position - transform.position;
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
                customerStateType = Define.CustomerState.Finish;
            }
        }
        else
        {
            GameObject customerSpawnPosition = GameObject.Find("CustomerSpawnPosition");

            Vector3 direction = customerSpawnPosition.transform.position - transform.position;
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
                customerStateType = Define.CustomerState.Finish;
            }
        }
    }

    private void UpdateFinish()
    {
        Managers.UI.CloseAllPopupUI();
        Managers.Object.Despawn(gameObject);
    }
}

