using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventBus
{
    private Dictionary<string, Action> events = new Dictionary<string, Action>();

    public void Subscribe(string eventID, Action handler)
    {
        if (!events.ContainsKey(eventID))
            events[eventID] = handler;
        else
            events[eventID] += handler;
    }

    // 이벤트 구독을 해지
    public void Unsubscribe(string eventID, Action handler)
    {
        if (events.ContainsKey(eventID))
            events[eventID] -= handler;
        else
            Debug.LogError("해당 eventID에 해당하는 메서드가 없습니다");
    }

    // 이벤트를 발생
    public void Trigger(string eventID)
    {
        if (events.ContainsKey(eventID))
            events[eventID]?.Invoke();
        else
            Debug.LogError("해당 eventID에 해당하는 메서드가 없습니다");
    }

    public void ShowCard()
    {
        Managers.Game.SetBurgurAndSource();
        UI_Card_Burgur burgurCard = Managers.UI.ShowSceneUI<UI_Card_Burgur>();

        burgurCard.transform.position = new Vector3(0.9900001f, -0.572f, 0.013f);
        burgurCard.transform.Rotate(new Vector3(0f, 180f, 0f));

        UI_Card_Source sourceCard = Managers.UI.ShowSceneUI<UI_Card_Source>();

        sourceCard.transform.position = new Vector3(0.397f, -0.572f, 0.013f);
        sourceCard.transform.Rotate(new Vector3(0f, 180f, 0f));

        UI_TimeLimit timeLimit = Managers.UI.ShowSceneUI<UI_TimeLimit>();
        timeLimit.transform.Rotate(new Vector3(0f, 180f, 0f));
        timeLimit.transform.position = new Vector3(-0.285f, -0.54f, 0);

        Managers.UI.ShowSceneUI<UI_Select>();
    }


    public void GameSceneLoad()
    {
        GameObject select = Managers.Resource.ResourcesDict["UI_Select"];
        if (select != null)
            Managers.Resource.Destory(select.gameObject);

        Managers.Clear();
        Managers.Scene.LoadScene(Define.SceneType.Game);
    }

    public void ChangeCustomerStateToJudge()
    {
        Managers.Game.CurrentCustomer.GetComponent<CustomerController>().IsOrdering = true;
    }

    public void Clear()
    {
        events.Clear();
    }
}
