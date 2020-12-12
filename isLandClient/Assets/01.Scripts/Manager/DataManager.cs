using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private DataBase m_DataBase;
    private StatData m_StatData;

    public void Initialization()
    {
        // 세이브, 로드 코드 추가.
        m_StatData = new StatData();
        m_DataBase = Resources.Load<ScriptableObject>("Data/DataBase") as DataBase;
    }

    public StatData GetStatData()
    {
        return m_StatData;
    }

    public string GetCropObjectName(int itemCode)
    {   
        return m_DataBase.CROP_OBJECT_DATA.Find(findData => findData.ITEM_CODE == itemCode).CROP_OBJECT_NAME;
    }

    public ItemData GetItemData(int itemCode)
    {
        return m_DataBase.ITEM_DATA.Find(findData => findData.ITEM_CODE == itemCode);
    }

    public void SaveData<T>(T data)
    {
        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(typeof(T).Name, json);
    }

    public T LoadData<T>() where T : class, new()
    {
        string json = PlayerPrefs.GetString(typeof(T).Name);
        if (json == null)
        {
            T defaultValue = new T();
            json = JsonUtility.ToJson(defaultValue);
            defaultValue = null;
        }

        return JsonUtility.FromJson<T>(json);
    }
}
