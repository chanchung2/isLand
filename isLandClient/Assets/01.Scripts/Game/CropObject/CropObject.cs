using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CropObject : MonoBehaviour
{
    [SerializeField]
    protected BoxCollider2D m_BoxColldier;
    [SerializeField]
    protected SpriteRenderer m_SpriteCrop;

    private int m_MaxLevel = 3;                         // 최대 성장 레벨.
    private float m_LevelUpDelay = 5;                   // 성장 딜레이.
    protected int m_CurrentLevel = 1;                     // 성장 레벨.

    private float m_Time = 0;                           // 육성 시간.
    
    void Update()
    {
        if (m_CurrentLevel >= m_MaxLevel)
            return ;

        if (!CanLevelUp())
            return ;

        m_Time += Time.deltaTime;
        if (m_Time >= m_LevelUpDelay)
        {
            m_Time -= m_LevelUpDelay;
            m_CurrentLevel++;

            UpdateSprite();
        }
    }

    protected abstract void UpdateSprite();
    protected abstract bool CanLevelUp();         // 성장 할 수 있는지.
}
