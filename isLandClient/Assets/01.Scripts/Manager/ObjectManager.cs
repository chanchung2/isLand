using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    private Dictionary<Type, GameObject> m_DicCreateObject = new Dictionary<Type, GameObject>();

    public void CreateObject<T>(string prefabPath)
    {
        if (m_DicCreateObject.ContainsKey(typeof(T)))
        {
            Debug.Log("Already Create Prefab : " + typeof(T).ToString());
            return ;
        }

        var prefab = Resources.Load(prefabPath);
        var obj = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;

        m_DicCreateObject.Add(typeof(T), obj);
    }

    public void DestroyObject<T>()
    {
        GameObject obj;
        if (!m_DicCreateObject.TryGetValue(typeof(T), out obj))
        {
            Debug.Log("Not Find Object : " + typeof(T).ToString());
            return ;
        }

        m_DicCreateObject.Remove(typeof(T));
    }

    public T GetObject<T>()
    {
        GameObject obj;
        
        if (!m_DicCreateObject.TryGetValue(typeof(T), out obj))
        {
            Debug.Log("Not Find Object : " + typeof(T).ToString());
            return default(T);
        }

        return obj.GetComponent<T>();
    }
}
