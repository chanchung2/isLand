using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    private StatData m_StatData;

    public void Initialization()
    {
        // 세이브, 로드 코드 추가.
        m_StatData = new StatData();
    }

    public StatData GetStatData()
    {
        return m_StatData;
    }
}
