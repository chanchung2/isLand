using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject<T>: MonoBehaviour where T : Component
{
    private string m_PrefabPath;
    private Queue<GameObject> m_QueueObject = new Queue<GameObject>();

    private Transform m_ParentPos;

    public PoolObject(string prefabPath, Transform parentPos)
    {
        m_PrefabPath = prefabPath;
        m_ParentPos = parentPos;

        CreateObject();
    }

    public void Push(GameObject obj)
    {
        Debug.Log(m_ParentPos.name);
        obj.transform.parent = m_ParentPos;
        obj.SetActive(false);
        
        m_QueueObject.Enqueue(obj);
    }

    public GameObject Pop()
    {
        if (m_QueueObject.Count == 0)
        {
            CreateObject();
        }

        return m_QueueObject.Dequeue();
    }

    private void CreateObject()
    {
        var prefab = Resources.Load(m_PrefabPath);
        
        var obj = Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
        obj.SetActive(false);

        m_QueueObject.Enqueue(obj);
    }
}
