﻿using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    [SerializeField] bool isDestroyOnLoad = true;

    protected virtual void Awake()
    {
        if (Instance)
        {
            Destroy(Instance.gameObject);
        }
        else
        {
            Instance = GetComponent<T>();
            if (!isDestroyOnLoad)
                DontDestroyOnLoad(Instance);
        }
    }
}
