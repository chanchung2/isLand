using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private DataTable m_DataTable;
    private StatData m_StatData;

    public void Initialization()
    {
        // 세이브, 로드 코드 추가.
        m_StatData = new StatData();
        m_DataTable = Resources.Load<ScriptableObject>("Data/DataTable") as DataTable;
    }

    public StatData GetStatData()
    {
        return m_StatData;
    }
}
