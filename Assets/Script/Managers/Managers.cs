using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    private static Managers s_Instance;//유일성 보장
    public static Managers Instance { get { Init(); return s_Instance; } }

    #region Core
    private ResourceManager _resource = new ResourceManager();
    private UIManager _ui = new UIManager();
    private DataManager _data = new DataManager();
    private SoundManager _sound = new SoundManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private TutorialGameManagerEx _tutorial = new TutorialGameManagerEx();
    private PoolManager _pool = new PoolManager();
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static DataManager Data { get { return Instance._data; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static TutorialGameManagerEx Tutorial { get { return Instance._tutorial; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    #endregion

    private static void Init()
    {
        if (s_Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<Managers>();

            s_Instance._sound.Init();
            s_Instance._pool.Init();
            s_Instance._tutorial.Init();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    static void Clear()
    {
        Sound.Clear();
        UI.Clear();
        Pool.Clear();
    }
}
