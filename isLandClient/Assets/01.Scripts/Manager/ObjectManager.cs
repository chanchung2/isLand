using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    private Dictionary<Type, GameObject> m_DicCreateObject = new Dictionary<Type, GameObject>();
    private Dictionary<Type, PoolObject<Component>> m_DicPoolObject = new Dictionary<Type, PoolObject<Component>>();

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

    public void CreatePoolObject<T>(string prefabPath) where T : Component
    {
        if (m_DicPoolObject.ContainsKey(typeof(T)))
        {
            Debug.Log("Already Create Pool Prefab : " + typeof(T).ToString());
            return ;
        }

        PoolObject<Component> poolObject = new PoolObject<Component>(prefabPath, this.transform);
        m_DicPoolObject.Add(typeof(T), poolObject);
    }

    public void PushPoolObject<T>(GameObject gameObject)
    {
        if (!m_DicPoolObject.ContainsKey(typeof(T)))
        {
            Debug.Log("Not Find Object : " + typeof(T).ToString());
            return ;
        }

        var poolObject = m_DicPoolObject[typeof(T)];
        poolObject.Push(gameObject);
    }

    public T PopPoolObject<T>(Transform parent)
    {
        if (!m_DicPoolObject.ContainsKey(typeof(T)))
        {
            Debug.Log("Not Find Object : " + typeof(T).ToString());
            return default(T);
        }  

        var poolObject = m_DicPoolObject[typeof(T)]; 

        var obj = poolObject.Pop();
        obj.transform.SetParent(parent);
        obj.gameObject.SetActive(true);

        return obj.GetComponent<T>();
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
