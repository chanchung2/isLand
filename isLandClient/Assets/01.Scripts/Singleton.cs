﻿using System.Collections;
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
                m_Instance = GameObject.FindObjectOfType<T>() ;
                
                // 씬에 존재하지 않는 오브젝트인 경우.
                if (m_Instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).ToString());
                    obj.AddComponent<T>();
                    DontDestroyOnLoad(obj);

                    m_Instance = obj.GetComponent<T>();
                }   
            }

            return m_Instance;
        }
    }
}
