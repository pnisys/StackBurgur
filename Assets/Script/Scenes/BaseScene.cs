using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene SceneType { get; protected set; } = Define.Scene.Unknown;
    public Define.Mode ModeType { get; protected set; } = Define.Mode.Test;

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
        {
            GameObject eventSystem = Managers.Resource.Instantite("UI/EventSystem");
            eventSystem.name = "@EventSystem";

            switch (ModeType)
            {
                case Define.Mode.Test:
                    eventSystem.GetComponent<OVRInputModule>().enabled = false;
                    eventSystem.GetComponent<StandaloneInputModule>().enabled = true;
                    break;
                case Define.Mode.Game:
                    eventSystem.GetComponent<StandaloneInputModule>().enabled = false;
                    eventSystem.GetComponent<OVRInputModule>().enabled = true;
                    break;
            }
        }
    }
    public abstract void Clear();
}
