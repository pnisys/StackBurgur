using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    private Define.SceneType _curSceneType = Define.SceneType.Unknown;
    public Define.SceneType CurrentSceneType
    {
        get
        {
            if (_curSceneType != Define.SceneType.Unknown)
                return _curSceneType;
            return CurrentScene.SceneType;
        }
        set { _curSceneType = value; }
    }

    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.SceneType type)
    {
        CurrentScene.Clear();
        SceneManager.LoadScene(GetSceneName(type));
    }

    string GetSceneName(Define.SceneType type)
    {
        string name = System.Enum.GetName(typeof(Define.SceneType), type);
        return name;
    }
}
