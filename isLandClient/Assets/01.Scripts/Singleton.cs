using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    private static T m_Instance;
    public static T Instance {
        get
        {
            if (m_Instance == null)
            {
                GameObject obj = new GameObject(typeof(T).ToString());
                obj.AddComponent<T>();

                DontDestroyOnLoad(obj);

                m_Instance = obj.GetComponent<T>();   
            }

            return m_Instance;
        }
    }
}
