using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    //팝업 소트오더를 활용해야 함
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    List<UI_Scene> _sceneList = new List<UI_Scene>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponet<Canvas>(go);
        canvas.renderMode = RenderMode.WorldSpace;
        canvas.overrideSorting = true;
        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }
    public T ShowSceneUI<T>(string name = null, Vector3 position = default) where T : UI_Scene
    {
        Vector3 addPosition = position;

        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantite($"UI/Scene/{name}", addPosition);

        Managers.Resource.ResourcesDict.TryAdd(go.name, go);
        T sceneUI = Util.GetOrAddComponet<T>(go);
        _sceneUI = sceneUI;

        go.transform.SetParent(Root.transform);
        _sceneList.Add(sceneUI);
        return sceneUI;
    }


    public T ShowPopupUI<T>(string name = null, Vector3 position = default) where T : UI_Popup
    {
        Vector3 addPosition = position;

        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantite($"UI/Popup/{name}", addPosition);

        T popup = Util.GetOrAddComponet<T>(go);
        _popupStack.Push(popup);

        GameObject root = GameObject.Find("@UI_Root");
        if (root == null)
            root = new GameObject { name = "@UI_Root" };

        go.transform.SetParent(root.transform);
        return popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.LogError("Close Popup Failed");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destory(popup.gameObject);
        popup = null;
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}
