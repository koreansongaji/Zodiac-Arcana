using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton pattern.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T _instance;
    public static bool HasInstance => _instance != null;
    public static T TryGetInstance() => HasInstance ? _instance : null;
    public static T Current => _instance;

    /// <summary>
    /// Singleton design pattern
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                Create(true);
            }
            return _instance;
        }
    }
    public static void Create()
    {
        if (_instance == null)
        {
            GameObject obj = new GameObject(typeof(T).Name);
            obj.name = typeof(T).Name + "_AutoCreated";
            _instance = obj.AddComponent<T>();
        }
    }
    public static void Create(bool dontDestroy)
    {
        if (_instance == null)
        {
            GameObject obj = new GameObject(typeof(T).Name);
            obj.name = typeof(T).Name + "_AutoCreated";
            _instance = obj.AddComponent<T>();
            if (dontDestroy) DontDestroyOnLoad(obj);
        }
    }

    /// <summary>
    /// On awake, we initialize our instance. Make sure to call base.Awake() in override if you need awake.
    /// </summary>
    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    /// <summary>
    /// Initializes the singleton.
    /// </summary>
    protected virtual void InitializeSingleton()
    {
        if (!Application.isPlaying)
        {
            return;
        }

        _instance = this as T;
    }
}