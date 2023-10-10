using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IPointerClickHandler
{
    public Action<PointerEventData> OnClickHandler = null;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("���� ������");
        if (OnClickHandler != null)
        {
            OnClickHandler.Invoke(eventData);
            Debug.Log("Ŭ��");
        }
    }
}
