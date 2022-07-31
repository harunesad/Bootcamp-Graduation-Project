using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : GenericSingleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                    instance = new GameObject(typeof(T).Name).AddComponent<T>();
            }
            return instance;
        }
    }

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
}
